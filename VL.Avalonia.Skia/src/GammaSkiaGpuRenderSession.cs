using Avalonia.Skia;
using SkiaSharp;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    sealed class GammaSkiaGpuRenderSession : ISkiaGpuRenderSession
    {
        private readonly CallerInfo _callerInfo;
        private readonly SKSurface _surface;

        public GammaSkiaGpuRenderSession(CallerInfo callerInfo)
        {
            _callerInfo = callerInfo;
            GrContext = callerInfo.GRContext;
            ScaleFactor = callerInfo.Scaling;

            var bounds = callerInfo.Canvas.DeviceClipBounds;
            var width = Math.Max(1, bounds.Width);
            var height = Math.Max(1, bounds.Height);
            var info = new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            // Try GPU surface first, fall back to raster
            if (GrContext != null)
                _surface = SKSurface.Create(GrContext, false, info);

            _surface ??= SKSurface.Create(info);
        }

        public GRContext GrContext { get; }
        public SKSurface SkSurface => _surface;
        public double ScaleFactor { get; }
        public GRSurfaceOrigin SurfaceOrigin => GRSurfaceOrigin.TopLeft;

        public void Dispose()
        {
            // Avalonia finished rendering onto _surface.
            // Blit the result to the original VL.Skia canvas.
            _surface.Canvas.Flush();
            GrContext?.Flush();

            using var image = _surface.Snapshot();
            _callerInfo.Canvas.DrawImage(image, 0, 0);

            _surface.Dispose();
        }
    }
}
