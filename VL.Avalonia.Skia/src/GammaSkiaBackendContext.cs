using Avalonia;
using Avalonia.Platform;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    internal class GammaSkiaBackendContext : IPlatformRenderInterfaceContext
    {
        public IRenderTarget CreateRenderTarget(IEnumerable<object> surfaces)
        {
            foreach (var s in surfaces)
                if (s is CallerInfo c)
                    return new GammaSkiaRenderTarget(c);

            throw new NotSupportedException("Don't know how to create a Skia render target from any of provided surfaces");
        }

        public IDrawingContextLayerImpl CreateOffscreenRenderTarget(PixelSize pixelSize, double scaling)
        {
            throw new NotImplementedException();
        }

        public bool IsLost => false;
        public IReadOnlyDictionary<Type, object> PublicFeatures { get; } = new Dictionary<Type, object>();
        public object? TryGetFeature(Type featureType) => null;
        public void Dispose() { }
    }
}
