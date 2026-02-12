using Avalonia.Skia;
using SkiaSharp;

namespace VL.Avalonia.Skia
{
    sealed class GammaSkiaGpuRenderSession : ISkiaGpuRenderSession
    {
        public GammaSkiaGpuRenderSession(
            GRContext grContext,
            SKSurface skSurface,
            double scaleFactor
        )
        {
            GrContext = grContext;
            SkSurface = skSurface;
            ScaleFactor = scaleFactor;
        }

        public GRContext GrContext { get; }
        public SKSurface SkSurface { get; }
        public double ScaleFactor { get; }
        public GRSurfaceOrigin SurfaceOrigin => GRSurfaceOrigin.TopLeft;

        public void Dispose()
        {
            // Surface and context lifecycle managed by GammaTopLevelImpl
        }
    }
}
