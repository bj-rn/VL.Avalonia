using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using SkiaSharp;
using VL.Lib.Mathematics;

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
            context.Custom(new SkiaImageDrawingOperation(Image, Bounds, Mode ?? SizeMode.AutoWidth, Anchor ?? RectangleAnchor.Center));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }

    public class SkiaPictureControl : SkiaMediaControlBase
    {
        public SKPicture? Picture { get; set; }
        public override void Render(DrawingContext context)
        {
            context.Custom(new SkiaPictureDrawingOperation(Picture, Bounds, Mode ?? SizeMode.AutoWidth, Anchor ?? RectangleAnchor.Center));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}
