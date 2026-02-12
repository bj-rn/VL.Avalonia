using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    sealed class GammaPlatformGraphics : IPlatformGraphics
    {
        bool IPlatformGraphics.UsesSharedContext => true;

        IPlatformGraphicsContext IPlatformGraphics.CreateContext()
        {
            throw new NotImplementedException();
        }

        IPlatformGraphicsContext IPlatformGraphics.GetSharedContext()
        {
            return new PlatformGraphicsContext();
        }

        sealed class PlatformGraphicsContext : ISkiaGpu
        {
            public bool IsLost => false;

            public void Dispose() { }

            public IDisposable EnsureCurrent()
            {
                return Disposable.Empty;
            }

            public ISkiaGpuRenderTarget? TryCreateRenderTarget(IEnumerable<object> surfaces)
            {
                foreach (var s in surfaces)
                    if (s is CallerInfo c)
                        return new GammaSkiaRenderTarget(c);

                return null;
            }

            public ISkiaSurface? TryCreateSurface(PixelSize size, ISkiaGpuRenderSession? session)
            {
                return null;
            }

            public object? TryGetFeature(Type featureType)
            {
                return null;
            }
        }
    }
}
