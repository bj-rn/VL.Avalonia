using Stride.Core.Mathematics;
using AvaloniaColort = Avalonia.Media.Color;

namespace VL.Avalonia;

/*
 * TODO
 */

public static class ColorExtensions
{
    public static Color4 FromColor(this AvaloniaColort color) =>
    new Color4(
        color.A,
        color.R,
        color.G,
        color.B
    );

    // TODO: FIX
    // public static AvaloniaColort ToColor(this Color4 color)
    // {
    //     var bytecolor = color.ToColor();
    //     return new AvaloniaColort(bytecolor.R, bytecolor.G, bytecolor.B, bytecolor.A);
    // }

    // public static SolidColorBrush ToBrush(this Color4 color) => new SolidColorBrush(color.ToColor(), color.A);
}
