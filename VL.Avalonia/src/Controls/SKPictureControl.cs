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
using SkiaExtensions = VL.Avalonia.Extensions.SkiaExtensions;

namespace VL.Avalonia.Skia
{
    public class SKPictureDrawOP : ICustomDrawOperation
    {
        public Rect Bounds { get; }
        public bool Equals(ICustomDrawOperation? other) => false;
        public bool HitTest(Point p) => false;
        public SKPicture? Picture { get; }
        public SizeMode Mode { get; }
        public RectangleAnchor Anchor { get; }
        public SKPictureDrawOP(Rect bounds, SKPicture? picture, SizeMode mode, RectangleAnchor anchor)
        {
            Bounds = bounds;
            Picture = picture;
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

                if (Picture != null)
                {
                    var boundsRectangle = Bounds.FromRect();
                    var pictureResolution = new Vector2(Picture.CullRect.Width, Picture.CullRect.Height);

                    AspectRatioUtils.FixAspectRatio(ref boundsRectangle, ref pictureResolution, Mode, Anchor, out RectangleF bounds);

                    // TODO: 
                    // Less hardcore way of doing this:
                    var matrix = SkiaExtensions.CreateMatrixFromPoints(bounds.TopLeft, bounds.TopRight, bounds.BottomRight, bounds.BottomLeft, pictureResolution.X, pictureResolution.Y);

                    canvas.DrawPicture(Picture, ref matrix);
                }

                canvas.Restore();

            }
        }

        public void Dispose()
        {
            // No-op
        }
    }

    public class SKPictureControl : Control
    {
        public SKPicture? Picture { get; set; } = null;
        public SizeMode? Mode { get; set; } = null;
        public RectangleAnchor? Anchor { get; set; } = null;

        public override void Render(DrawingContext context)
        {
            context.Custom(new SKPictureDrawOP(this.Bounds, Picture, Mode ?? SizeMode.AutoWidth, Anchor ?? RectangleAnchor.Center));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}

namespace VL.Avalonia.Controls
{
    [ProcessNode(Name = "SKPictureControl")]
    public partial class SKPictureControlWrapper : ControlWrapperBase<SKPictureControl>
    {
        protected Optional<SKPicture> _picture;
        [Fragment(Order = PinOrder.Main)]
        public void SetPicture(Optional<SKPicture> picture)
        {
            if (_picture != picture)
            {
                if (picture.HasValue)
                {
                    _output.Picture = picture.Value;
                }
                else
                {
                    _output.Picture = null;
                }

                _picture = picture;
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

