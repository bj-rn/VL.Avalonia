using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace VL.Avalonia.Rendering
{
    public class TextureViewerRender : ICustomDrawOperation
    {
        public TextureViewerRender(SKImage image, Rect bounds, Rect dest /*, float levelX, float levelY*/)// : base(src)
        {
            _image = image;
            Bounds = bounds;
            _dest = dest;
        }

        private Rect _dest;
        private SKImage _image;
        public Rect Bounds { get; private set; }

        public bool Equals(ICustomDrawOperation? other) => false;
        public bool HitTest(Point p) => true;

        public void Render(ImmediateDrawingContext drwContext)
        {
            var leaseFeature = drwContext.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature == null)
                return;

            using var lease = leaseFeature.Lease();
            var canvas = lease.SkCanvas;
            canvas.DrawImage(_image, _dest.ToSKRect(), Bounds.ToSKRect(), new SKPaint());
                
        }


        public void Dispose()
        {
            
        }
    }
}
