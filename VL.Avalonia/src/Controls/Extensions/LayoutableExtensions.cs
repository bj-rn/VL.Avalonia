using Avalonia.Layout;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;

namespace VL.Avalonia.Controls
{
    public static partial class Extensions
    {
        public static Vector2 GetDesiredSize(Layoutable? layoutable) =>
            layoutable?.DesiredSize.ToVector() ?? Vector2.Zero;
    }
}
