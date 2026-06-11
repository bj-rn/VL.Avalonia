using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Runtime.CompilerServices;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    sealed class GammaSkiaRenderTarget : IRenderTarget, ISkiaGpuRenderTarget
    {
        private readonly Action<GammaSkiaRenderTarget> _onDispose;
        private readonly CallerInfo _callerInfo;
        private readonly SKSurface _surface;
        private readonly bool _ownsSurface;

        public GammaSkiaRenderTarget(Action<GammaSkiaRenderTarget> onDispose, CallerInfo callerInfo)
        {
            _onDispose = onDispose;
            _callerInfo = callerInfo;

            // From vvvv 7.3 onwards CallerInfo includes a Surface property which we can use directly
            var surfaceProperty = typeof(CallerInfo).GetProperty("Surface");
            if (surfaceProperty != null && surfaceProperty.GetValue(callerInfo) is SKSurface surface)
            {
                _ownsSurface = false;
                _surface = surface;
            }
            else
            {
                _ownsSurface = true;

                var bounds = callerInfo.Canvas.DeviceClipBounds;
                var width = Math.Max(1, bounds.Width);
                var height = Math.Max(1, bounds.Height);
                var info = new SKImageInfo(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);

                // Try GPU surface first, fall back to raster
                // IMPORTANT: Explicitly ask for TopLeft origin to match the SurfaceOrigin property
                if (callerInfo.GRContext != null)
                    _surface = SKSurface.Create(callerInfo.GRContext, false, info, 1, GRSurfaceOrigin.TopLeft);

                _surface ??= SKSurface.Create(info);
            }
        }

        public CallerInfo CallerInfo => _callerInfo;

        public bool IsCorrupted
        {
            get
            {
                return IsDisposed(_callerInfo.Canvas);

                [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_IsDisposed")]
                static extern bool IsDisposed(SKNativeObject obj);
            }
        }

        public ISkiaGpuRenderSession BeginRenderingSession()
        {
            if (_ownsSurface)
            {
                return new GammaSkiaGpuRenderSession(_callerInfo, _surface);
            }
            else
            {
                return new NoOpSession()
                {
                    GrContext = _callerInfo.GRContext,
                    SkSurface = _surface,
                    ScaleFactor = _callerInfo.Scaling,
                    SurfaceOrigin = GRSurfaceOrigin.TopLeft
                };
            }
        }

        public IDrawingContextImpl CreateDrawingContext(bool useScaledDrawing)
        {
            return DrawingContextHelper.WrapSkiaCanvas(_callerInfo.Canvas, new Vector(96, 96));
        }

        public void Dispose() 
        {
            if (_ownsSurface)
            {
                _surface.Dispose();
            }
            _onDispose.Invoke(this);
        }

        sealed class NoOpSession : ISkiaGpuRenderSession
        {
            public required GRContext GrContext { get; init; }

            public required SKSurface SkSurface { get; init; }

            public required double ScaleFactor { get; init; }

            public required GRSurfaceOrigin SurfaceOrigin { get; init; }

            public void Dispose()
            {
            }
        }
    }
}
