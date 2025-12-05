using Avalonia.Media;
using Color = Avalonia.Media.Color;

namespace VL.Avalonia.Custom.Extensions
{
    public static partial class ColorExtensions
    {
        public static HsvColor ToHsvColor(this Color color) => new HsvColor(color);

        public static Color FromHsvColor(this HsvColor color) => color.ToRgb();
    }
}
