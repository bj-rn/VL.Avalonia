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
            // IMPORTANT: Explicitly ask for TopLeft origin to match the SurfaceOrigin property
            if (GrContext != null)
                _surface = SKSurface.Create(GrContext, false, info, 1, GRSurfaceOrigin.TopLeft);

            _surface ??= SKSurface.Create(info);
        }

        public GRContext GrContext { get; }
        public SKSurface SkSurface => _surface;
        public double ScaleFactor { get; }
        public GRSurfaceOrigin SurfaceOrigin => GRSurfaceOrigin.TopLeft;

        public void Dispose()
        {
            // Avalonia finished rendering onto _surface.
            _surface.Canvas.Flush();
            GrContext?.Flush();

            using var image = _surface.Snapshot();

            // Blit the result to the original VL.Skia canvas.
            _callerInfo.Canvas.Save();

            // CRITICAL: Reset matrix to Identity to draw in device pixels.
            // This prevents double-scaling and ensures alignment with DeviceClipBounds.
            _callerInfo.Canvas.ResetMatrix();

            // We must draw at the position of the clip bounds in device space
            var bounds = _callerInfo.Canvas.DeviceClipBounds;
            _callerInfo.Canvas.DrawImage(image, bounds.Left, bounds.Top);

            _callerInfo.Canvas.Restore();

            _surface.Dispose();
        }
    }
}
