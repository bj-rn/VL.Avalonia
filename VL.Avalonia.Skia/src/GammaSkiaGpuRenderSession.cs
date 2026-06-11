using Avalonia.Skia;
using SkiaSharp;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    sealed class GammaSkiaGpuRenderSession : ISkiaGpuRenderSession
    {
        private readonly CallerInfo _callerInfo;
        private readonly SKSurface _surface;

        public GammaSkiaGpuRenderSession(CallerInfo callerInfo, SKSurface surface)
        {
            _callerInfo = callerInfo;
            GrContext = callerInfo.GRContext;
            ScaleFactor = callerInfo.Scaling;
            _surface = surface;
        }

        public GRContext GrContext { get; }
        public SKSurface SkSurface => _surface;
        public double ScaleFactor { get; }
        public GRSurfaceOrigin SurfaceOrigin => GRSurfaceOrigin.TopLeft;

        public void Dispose()
        {
            // Avalonia finished rendering onto _surface.
            _surface.Canvas.Flush();

            // Blit the result to the original VL.Skia canvas.
            _callerInfo.Canvas.Save();

            // CRITICAL: Reset matrix to Identity to draw in device pixels.
            // This prevents double-scaling and ensures alignment with DeviceClipBounds.
            _callerInfo.Canvas.ResetMatrix();

            // We must draw at the position of the clip bounds in device space
            var bounds = _callerInfo.Canvas.DeviceClipBounds;
            _callerInfo.Canvas.DrawSurface(_surface, bounds.Left, bounds.Top);

            _callerInfo.Canvas.Restore();
        }
    }
}
