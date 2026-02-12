using SkiaSharp;
using VL.Skia;

namespace VL.Avalonia.Skia
{
    /// <summary>
    /// Pairs a <see cref="CallerInfo"/> with a GPU-backed <see cref="SKSurface"/>
    /// managed by <see cref="GammaTopLevelImpl"/>.
    /// </summary>
    internal sealed record RenderSurfaceInfo(CallerInfo CallerInfo, SKSurface Surface);
}
