using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using VL.Lib.Mathematics;
using VL.Skia;
using Point = Avalonia.Point;
using SkiaExtensions = VL.Avalonia.Extensions.SkiaExtensions;

namespace VL.Avalonia.Skia
{
    public abstract class SkiaMediaDrawingOperationBase : ICustomDrawOperation
    {
        public Rect Bounds { get; }

        public bool Equals(ICustomDrawOperation? other) => false;

        public bool HitTest(Point p) => Bounds.Contains(p);

        public SizeMode Mode { get; }
        public RectangleAnchor Anchor { get; }

        public SkiaMediaDrawingOperationBase(Rect bounds, SizeMode mode, RectangleAnchor anchor)
        {
            Bounds = bounds;
            Mode = mode;
            Anchor = anchor;
        }

        public abstract void Render(ImmediateDrawingContext context);
        public abstract void Dispose();
    }

    public class SkiaImageDrawingOperation : SkiaMediaDrawingOperationBase
    {
        public SKImage? Image { get; }

        public SkiaImageDrawingOperation(
            SKImage? image,
            Rect bounds,
            SizeMode mode,
            RectangleAnchor anchor
        )
            : base(bounds, mode, anchor)
        {
            Image = image;
        }

        public override void Render(ImmediateDrawingContext context)
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

                    AspectRatioUtils.FixAspectRatio(
                        ref boundsRectangle,
                        ref imageResolution,
                        Mode,
                        Anchor,
                        out RectangleF bounds
                    );

                    canvas.DrawImage(Image, bounds.ToRect().ToSKRect());
                }

                canvas.Restore();
            }
        }

        public override void Dispose()
        {
            // no op;
        }
    }

    public class SkiaPictureDrawingOperation : SkiaMediaDrawingOperationBase
    {
        public SKPicture? Picture { get; }

        public SkiaPictureDrawingOperation(
            SKPicture? picture,
            Rect bounds,
            SizeMode mode,
            RectangleAnchor anchor
        )
            : base(bounds, mode, anchor)
        {
            Picture = picture;
        }

        public override void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature != null)
            {
                using (var lease = leaseFeature.Lease())
                {
                    var canvas = lease.SkCanvas;

                    canvas.Save();

                    if (Picture != null)
                    {
                        var boundsRectangle = Bounds.FromRect();
                        var pictureResolution = new Vector2(
                            Picture.CullRect.Width,
                            Picture.CullRect.Height
                        );

                        AspectRatioUtils.FixAspectRatio(
                            ref boundsRectangle,
                            ref pictureResolution,
                            Mode,
                            Anchor,
                            out RectangleF bounds
                        );

                        // TODO:
                        // Less hardcore way of doing this:
                        var matrix = SkiaExtensions.CreateMatrixFromPoints(
                            bounds.TopLeft,
                            bounds.TopRight,
                            bounds.BottomRight,
                            bounds.BottomLeft,
                            pictureResolution.X,
                            pictureResolution.Y
                        );

                        canvas.DrawPicture(Picture, ref matrix);
                    }

                    canvas.Restore();
                }
            }
        }

        public override void Dispose()
        {
            // no op;
        }
    }

    public class SkiaLayerDrawingOperation : SkiaMediaDrawingOperationBase
    {
        public ILayer? Layer { get; }
        public float Scaling { get; }

        public SkiaLayerDrawingOperation(
            ILayer? layer,
            Rect bounds,
            SizeMode mode,
            RectangleAnchor anchor,
            float scaling
        )
            : base(bounds, mode, anchor)
        {
            Layer = layer;
            Scaling = scaling;
        }

        public override void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature != null)
            {
                using (var lease = leaseFeature.Lease())
                {
                    var canvas = lease.SkCanvas;

                    canvas.Save();

                    if (Layer != null)
                    {
                        var boundsRectangle = Bounds.FromRect();
                        RectangleF targetBounds = boundsRectangle;

                        var layerBounds = Layer.Bounds;

                        if (layerBounds.HasValue)
                        {
                            var layerSize = new Vector2(
                                layerBounds.Value.Width,
                                layerBounds.Value.Height
                            );
                            AspectRatioUtils.FixAspectRatio(
                                ref boundsRectangle,
                                ref layerSize,
                                Mode,
                                Anchor,
                                out targetBounds
                            );

                            // Setup Matrix to map LayerBounds to TargetBounds
                            // T = Translate(Target.TopLeft) * Scale(Target.Size / Layer.Size) * Translate(-Layer.TopLeft)

                            var scaleX = targetBounds.Width / layerSize.X;
                            var scaleY = targetBounds.Height / layerSize.Y;

                            canvas.Translate(targetBounds.X, targetBounds.Y);
                            canvas.Scale(scaleX, scaleY);
                            canvas.Translate(-layerBounds.Value.X, -layerBounds.Value.Y);
                        }
                        else
                        {
                            canvas.ClipRect(targetBounds.ToRect().ToSKRect());
                        }

                        var callerInfo = CallerInfo.InRenderer(
                            targetBounds.Width,
                            targetBounds.Height,
                            canvas,
                            lease.GrContext
                        );

                        if (layerBounds.HasValue)
                        {
                            callerInfo = callerInfo with
                            {
                                ViewportBounds = layerBounds.Value.ToRect().ToSKRect(),
                            };
                        }
                        else
                        {
                            callerInfo = callerInfo with
                            {
                                ViewportBounds = targetBounds.ToRect().ToSKRect(),
                            };
                        }

                        Layer.Render(callerInfo);
                    }

                    canvas.Restore();
                }
            }
        }

        public override void Dispose()
        {
            // no op;
        }
    }
}
