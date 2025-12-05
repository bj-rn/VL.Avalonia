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
        public SKImage? Image { get; set; }

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
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }

    public class SkiaPictureControl : SkiaMediaControlBase
    {
        public SKPicture? Picture { get; set; }

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
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }

    public class SkiaLayerControl : SkiaMediaControlBase
    {
        public ILayer? Layer { get; set; }
        public RectangleF? LayerBounds { get; set; }

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

            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}
