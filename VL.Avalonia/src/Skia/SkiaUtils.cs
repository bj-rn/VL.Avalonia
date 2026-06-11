using SkiaSharp;
using System.Runtime.CompilerServices;

namespace VL.Avalonia.Skia;

internal static class SkiaUtils
{
    public static bool IsDisposed(this SKNativeObject obj)
    {
        return IsDisposed(obj);

        [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_IsDisposed")]
        static extern bool IsDisposed(SKNativeObject obj);
    }
}
