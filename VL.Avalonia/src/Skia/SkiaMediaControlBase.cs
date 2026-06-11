using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering;
using Avalonia.Threading;
using SkiaSharp;
using Stride.Core.Mathematics;
using VL.Lib.Mathematics;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    public abstract class SkiaMediaControlBase : Control
    {
        public SizeMode? Mode { get; set; }
        public RectangleAnchor? Anchor { get; set; }
        public abstract override void Render(DrawingContext context);
    }

    public class SkiaImageControl : SkiaMediaControlBase
    {
        private SKImage? image;

        public SKImage? Image
        {
            get => image;
            set
            {
                image = value;
                InvalidateVisual();
            }
        }

        public override void Render(DrawingContext context)
        {
            context.Custom(
                new SkiaImageDrawingOperation(
                    Image,
                    Bounds,
                    Mode ?? SizeMode.AutoWidth,
                    Anchor ?? RectangleAnchor.Center
                )
            );
        }
    }

    public class SkiaPictureControl : SkiaMediaControlBase
    {
        private SKPicture? picture;

        public SKPicture? Picture
        {
            get => picture;
            set
            {
                picture = value;
                InvalidateVisual();
            }
        }

        public override void Render(DrawingContext context)
        {
            context.Custom(
                new SkiaPictureDrawingOperation(
                    Picture,
                    Bounds,
                    Mode ?? SizeMode.AutoWidth,
                    Anchor ?? RectangleAnchor.Center
                )
            );
        }
    }

    public class SkiaLayerControl : SkiaMediaControlBase
    {
        private ILayer? layer;
        private RectangleF? layerBounds;

        public ILayer? Layer
        {
            get => layer;
            set
            {
                layer = value;
                InvalidateVisual();
            }
        }

        public RectangleF? LayerBounds
        {
            get => layerBounds;
            set
            {
                layerBounds = value;
                InvalidateVisual();
            }
        }

        public override void Render(DrawingContext context)
        {
            // Get scaling factor for correct text/line rendering
            var scaling = (VisualRoot as IRenderRoot)?.RenderScaling ?? 1.0;
            context.Custom(
                new SkiaLayerDrawingOperation(
                    Layer,
                    LayerBounds,
                    Bounds,
                    Mode ?? SizeMode.AutoWidth,
                    Anchor ?? RectangleAnchor.Center,
                    (float)scaling
                )
            );
        }
    }
}
