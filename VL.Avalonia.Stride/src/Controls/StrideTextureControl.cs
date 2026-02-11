using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using SkiaSharp;
using Stride.Core.Mathematics;
using Stride.Graphics;
using VL.Avalonia.Extensions;
using VL.Avalonia.Skia;
using VL.Lib.Mathematics;
using APoint = Avalonia.Point;

namespace VL.Avalonia.Stride.Controls
{
    public class StrideTextureControl : SkiaMediaControlBase, IDisposable
    {
        public Texture? Texture { get; set; }

        private readonly TextureToSkImageShared _converter = new TextureToSkImageShared();

        // Frame counter to ensure every drawing operation is unique
        private long _frameCounter;

        public override void Render(DrawingContext context)
        {
            var scaling = (VisualRoot as IRenderRoot)?.RenderScaling ?? 1.0;
            var frame = Interlocked.Increment(ref _frameCounter);

            context.Custom(
                new StrideTextureDrawingOperation(
                    Texture,
                    Bounds,
                    Mode ?? SizeMode.AutoWidth,
                    Anchor ?? RectangleAnchor.Center,
                    _converter,
                    (float)scaling,
                    frame
                )
            );

            // Keep the render loop alive
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Render);
        }

        public void Dispose()
        {
            _converter.Dispose();
        }
    }

    public class StrideTextureDrawingOperation : ICustomDrawOperation
    {
        private readonly Texture? _texture;
        private readonly TextureToSkImageShared _converter;
        private readonly float _scaling;
        private readonly long _frame;
        private readonly SizeMode _mode;
        private readonly RectangleAnchor _anchor;

        private static readonly SKPaint SharedPaint = new SKPaint
        {
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true,
        };

        private static readonly SKPaint DebugTextPaint = new SKPaint
        {
            Color = SKColors.White,
            TextSize = 12,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Consolas"),
        };

        private static readonly SKPaint DebugBgPaint = new SKPaint
        {
            Color = new SKColor(0, 0, 0, 200),
            Style = SKPaintStyle.Fill,
        };

        private static readonly SKPaint DebugErrorPaint = new SKPaint
        {
            Color = SKColors.Red,
            TextSize = 13,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Consolas"),
        };

        public StrideTextureDrawingOperation(
            Texture? texture,
            Rect bounds,
            SizeMode mode,
            RectangleAnchor anchor,
            TextureToSkImageShared converter,
            float scaling,
            long frame
        )
        {
            _texture = texture;
            Bounds = bounds;
            _mode = mode;
            _anchor = anchor;
            _converter = converter;
            _scaling = scaling;
            _frame = frame;
        }

        public Rect Bounds { get; }

        // CRITICAL: These two ensure Avalonia never caches/skips our render
        public bool HitTest(APoint p) => Bounds.Contains(p);

        public bool Equals(ICustomDrawOperation? other) => false; // Always different → always re-render

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature == null)
                return;

            using var lease = leaseFeature.Lease();
            var canvas = lease.SkCanvas;
            var grContext = lease.GrContext;

            SKImage? skImage = _converter.Update(_texture, grContext);

            if (skImage != null)
            {
                canvas.Save();

                var boundsRectangle = Bounds.FromRect();
                RectangleF targetBounds = boundsRectangle;
                var imageSize = new Vector2(skImage.Width, skImage.Height);

                AspectRatioUtils.FixAspectRatio(
                    ref boundsRectangle,
                    ref imageSize,
                    _mode,
                    _anchor,
                    out targetBounds
                );

                var destRect = SKRect.Create(
                    targetBounds.X,
                    targetBounds.Y,
                    targetBounds.Width,
                    targetBounds.Height
                );

                canvas.DrawImage(skImage, destRect, SharedPaint);
                canvas.Restore();
            }

            // Debug overlay
            DrawDebugOverlay(canvas);
        }

        private void DrawDebugOverlay(SKCanvas canvas)
        {
            var log = _converter.DebugLog;
            if (log.Count == 0)
                return;

            canvas.Save();
            float x = 4,
                y = 16,
                lh = 15;
            var bg = SKRect.Create(
                0,
                0,
                (float)Bounds.Width,
                Math.Min((float)Bounds.Height, log.Count * lh + 8)
            );
            canvas.DrawRect(bg, DebugBgPaint);

            foreach (var line in log)
            {
                var paint = line.StartsWith("ERROR") ? DebugErrorPaint : DebugTextPaint;
                canvas.DrawText(line, x, y, paint);
                y += lh;
                if (y > Bounds.Height - 4)
                    break;
            }
            canvas.Restore();
        }

        public void Dispose() { }
    }
}
