using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Skia;
using SkiaSharp;
using Stride.Core.Mathematics;
using Stride.Graphics;
using VL.Avalonia.Extensions;
using VL.Avalonia.Skia;
using VL.Lib.Mathematics;

namespace VL.Avalonia.Stride.Controls
{
    public class StrideTextureControl : SkiaMediaControlBase
    {
        public Texture? Texture { get; set; }

        // Use our new wrapper around VL.Skia.FromSharedHandle
        private readonly TextureToSkImageShared _converter = new TextureToSkImageShared();

        public override void Render(DrawingContext context)
        {
            var scaling = (VisualRoot as IRenderRoot)?.RenderScaling ?? 1.0;

            context.Custom(
                new StrideTextureDrawingOperation(
                    Texture,
                    Bounds,
                    Mode ?? SizeMode.AutoWidth,
                    Anchor ?? RectangleAnchor.Center,
                    _converter,
                    (float)scaling
                )
            );

            // Keep the render loop alive
            // Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }

    public class StrideTextureDrawingOperation : SkiaMediaDrawingOperationBase
    {
        private readonly Texture? _texture;
        private readonly TextureToSkImageShared _converter;
        private readonly float _scaling;

        public StrideTextureDrawingOperation(
            Texture? texture,
            Rect bounds,
            SizeMode mode,
            RectangleAnchor anchor,
            TextureToSkImageShared converter,
            float scaling
        )
            : base(bounds, mode, anchor)
        {
            _texture = texture;
            _converter = converter;
            _scaling = scaling;
        }

        public override void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature != null)
            {
                using (var lease = leaseFeature.Lease())
                {
                    var canvas = lease.SkCanvas;

                    // Update and convert
                    SKImage? skImage = _converter.Update(_texture);

                    if (skImage == null)
                        return;

                    canvas.Save();

                    var boundsRectangle = Bounds.FromRect();
                    RectangleF targetBounds = boundsRectangle;
                    var imageSize = new Vector2(skImage.Width, skImage.Height);

                    AspectRatioUtils.FixAspectRatio(
                        ref boundsRectangle,
                        ref imageSize,
                        Mode,
                        Anchor,
                        out targetBounds
                    );

                    var paint = new SKPaint
                    {
                        FilterQuality = SKFilterQuality.High,
                        IsAntialias = true,
                    };

                    var destRect = SKRect.Create(
                        targetBounds.X,
                        targetBounds.Y,
                        targetBounds.Width,
                        targetBounds.Height
                    );

                    canvas.DrawImage(skImage, destRect, paint);
                    canvas.Restore();
                }
            }
        }

        public override void Dispose()
        {
            // The converter is owned by the Control, not the Operation
        }
    }
}
