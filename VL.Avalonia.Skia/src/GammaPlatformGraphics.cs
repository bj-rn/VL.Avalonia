using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
using System.Reactive.Disposables;
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
            private readonly Dictionary<CallerInfo, GammaSkiaRenderTarget> renderTargets = new();

            public bool IsLost => false;

            public void Dispose() { }

            public IDisposable EnsureCurrent()
            {
                return Disposable.Empty;
            }

            public ISkiaGpuRenderTarget? TryCreateRenderTarget(IEnumerable<object> surfaces)
            {
                foreach (var s in surfaces)
                {
                    if (s is CallerInfo c)
                    {
                        if (!renderTargets.TryGetValue(c, out var renderTarget))
                            renderTargets[c] = renderTarget = new GammaSkiaRenderTarget(RemoveRenderTarget, c);
                        return renderTarget;
                    }
                }

                return null;
            }

            internal void RemoveRenderTarget(GammaSkiaRenderTarget renderTarget)
            {
                renderTargets.Remove(renderTarget.CallerInfo);
            }

            public ISkiaSurface? TryCreateSurface(PixelSize size, ISkiaGpuRenderSession? session)
            {
                if (session != null)
                {
                    // See https://github.com/vvvv/VL.StandardLibs/commit/06950fc7b9653ae1083bed66d7ad8f38954fd268
                    var info = new SKImageInfo(size.Width, size.Height, SKColorType.Bgra8888, SKAlphaType.Premul, SKColorSpace.CreateSrgb());
                    var surface = SKSurface.Create(session.GrContext, false, info, 1, GRSurfaceOrigin.TopLeft);
                    return new SkiaSurfaceWrapper(surface);
                }
                return null;
            }

            public object? TryGetFeature(Type featureType)
            {
                return null;
            }

            private sealed class SkiaSurfaceWrapper : ISkiaSurface
            {
                private SKSurface? _surface;

                public SKSurface Surface => _surface ?? throw new ObjectDisposedException(nameof(SkiaSurfaceWrapper));
                public bool CanBlit => false;
                public void Blit(SKCanvas canvas) => throw new NotSupportedException();

                public SkiaSurfaceWrapper(SKSurface surface)
                {
                    _surface = surface;
                }

                public void Dispose()
                {
                    _surface?.Dispose();
                    _surface = null;
                }
            }
        }
    }
}
