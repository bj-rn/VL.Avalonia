using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using SkiaSharp;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using VL.Avalonia.Skia;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Mathematics;
using Point = Avalonia.Point;

namespace VL.Avalonia.Skia
{
    public class SKImageDrawOP : ICustomDrawOperation
    {
        public Rect Bounds { get; }
        public bool Equals(ICustomDrawOperation? other) => false;
        public bool HitTest(Point p) => false;

        public SKImage? Image { get; }
        public SizeMode Mode { get; }
        public RectangleAnchor Anchor { get; }

        public SKImageDrawOP(Rect bounds, SKImage? image, SizeMode mode, RectangleAnchor anchor)
        {
            Bounds = bounds;
            Image = image;
            Mode = mode;
            Anchor = anchor;
        }

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature != null)
            {
                using var lease = leaseFeature.Lease();
                var canvas = lease.SkCanvas;

                canvas.Save();

                if (Image != null)
                {
                    var boundsRectangle = Bounds.FromRect();
                    var imageResolution = new Vector2(Image.Width, Image.Height);

                    AspectRatioUtils.FixAspectRatio(ref boundsRectangle, ref imageResolution, Mode, Anchor, out RectangleF bounds);

                    canvas.DrawImage(Image, bounds.ToRect().ToSKRect());
                }

                canvas.Restore();

            }
        }

        public void Dispose()
        {
            // No-op
        }


    }

    public class SKImageControl : Control
    {
        public SKImage? Image { get; set; } = null;
        public SizeMode? Mode { get; set; } = null;
        public RectangleAnchor? Anchor { get; set; } = null;

        public override void Render(DrawingContext context)
        {
            context.Custom(new SKImageDrawOP(this.Bounds, Image, Mode ?? SizeMode.AutoWidth, Anchor ?? RectangleAnchor.Center));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}

namespace VL.Avalonia.Controls
{
    [ProcessNode(Name = "SKImageControl")]
    public partial class SKImageControlWrapper : ControlWrapperBase<SKImageControl>
    {
        protected Optional<SKImage> _image;
        [Fragment(Order = PinOrder.Main)]
        public void SetImage(Optional<SKImage> image)
        {
            if (_image != image)
            {
                if (image.HasValue)
                {
                    _output.Image = image.Value;
                }
                else
                {
                    _output.Image = null;
                }

                _image = image;
            }
        }

        protected Optional<SizeMode> _mode;
        public void SetSizeMode(Optional<SizeMode> mode)
        {
            if (_mode != mode)
            {
                if (mode.HasValue)
                {
                    _output.Mode = mode.Value;
                }
                else
                {
                    _output.Mode = null;
                }

                _mode = mode;
            }
        }



        protected Optional<RectangleAnchor> _anchor;
        public void SetAnchor([Pin(Visibility = Model.PinVisibility.Optional)] Optional<RectangleAnchor> anchor)
        {
            if (_anchor != anchor)
            {
                if (anchor.HasValue)
                {
                    _output.Anchor = anchor.Value;
                }
                else
                {
                    _output.Anchor = null;
                }

                _anchor = anchor;
            }
        }
    }
}
