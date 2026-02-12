using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    sealed class GammaSkiaRenderTarget : IRenderTarget, ISkiaGpuRenderTarget
    {
        private readonly CallerInfo _callerInfo;
        private readonly SKSurface? _gpuSurface;

        public GammaSkiaRenderTarget(CallerInfo callerInfo, SKSurface? gpuSurface)
        {
            _callerInfo = callerInfo;
            _gpuSurface = gpuSurface;
        }

        public bool IsCorrupted
        {
            get
            {
                return IsDiposed(_callerInfo.Canvas);

                [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_IsDisposed")]
                static extern bool IsDiposed(SKNativeObject obj);
            }
        }

        public ISkiaGpuRenderSession BeginRenderingSession()
        {
            return new GammaSkiaGpuRenderSession(
                _callerInfo.GRContext,
                _gpuSurface!,
                _callerInfo.Scaling
            );
        }

        public IDrawingContextImpl CreateDrawingContext(bool useScaledDrawing)
        {
            // CPU fallback when GRContext is null
            return DrawingContextHelper.WrapSkiaCanvas(_callerInfo.Canvas, new Vector(96, 96));
        }

        public void Dispose() { }
    }
}
