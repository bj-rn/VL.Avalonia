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

        public GammaSkiaRenderTarget(CallerInfo callerInfo)
        {
            _callerInfo = callerInfo;
        }

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
            return new GammaSkiaGpuRenderSession(_callerInfo);
        }

        public IDrawingContextImpl CreateDrawingContext(bool useScaledDrawing)
        {
            return DrawingContextHelper.WrapSkiaCanvas(_callerInfo.Canvas, new Vector(96, 96));
        }

        public void Dispose() { }
    }
}
