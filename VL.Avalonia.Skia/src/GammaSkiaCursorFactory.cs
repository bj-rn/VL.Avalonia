using Avalonia;
using Avalonia.Input;
using Avalonia.Platform;

namespace VL.Avalonia.Skia
{
    internal sealed class GammaSkiaCursorFactory : ICursorFactory
    {
        public ICursorImpl CreateCursor(IBitmapImpl cursor, PixelPoint hotSpot)
            => null;

        public ICursorImpl GetCursor(StandardCursorType cursorType)
            => null;

    }
}
