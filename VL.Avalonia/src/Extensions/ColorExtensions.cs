using Avalonia.Media;
using Avalonia.Media.Immutable;
using Stride.Core.Mathematics;

using Color = Avalonia.Media.Color;

namespace VL.Avalonia.Extensions
{
    public static partial class ColorExtensions
    {
        public static Color ToColor(this Color4 color) =>
             new Color(
                (byte)(color.A * 255),
                (byte)(color.R * 255),
                (byte)(color.G * 255),
                (byte)(color.B * 255)
            );

        public static Color ToColor(Color4? color) =>
            color.HasValue ? color.Value.ToColor() : Colors.White;

        public static IBrush ToSolidColorBrush(this Color4 color) =>
            new ImmutableSolidColorBrush(color.ToColor());

        public static IBrush ToSolidColorBrush(Color4? color) =>
            color.HasValue ? color.Value.ToSolidColorBrush() : Brushes.White;

        public static Color4 FromColor(this Color color) =>
            new Color4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);

        public static Color4 FromColor(this Color? color) =>
            color.HasValue ? color.FromColor() : Color4.White;
    }

}

