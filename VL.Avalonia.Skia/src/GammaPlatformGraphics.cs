using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
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
                CallerInfo? callerInfo = null;
                SKSurface? gpuSurface = null;

                foreach (var s in surfaces)
                {
                    if (s is CallerInfo c)
                        callerInfo = c;
                    if (s is SKSurface surf)
                        gpuSurface = surf;
                }

                if (callerInfo != null && gpuSurface != null)
                    return new GammaSkiaRenderTarget(callerInfo, gpuSurface);

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
