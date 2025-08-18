using Point = Avalonia.Point;

namespace VL.Avalonia.Skia
{
    public static class PointUtils
    {
        public static Point ToPoint(this Stride.Core.Mathematics.Vector2 v) => new Point(v.X, v.Y);
    }
}
