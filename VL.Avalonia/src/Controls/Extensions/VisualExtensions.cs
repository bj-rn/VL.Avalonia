using Avalonia;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;

namespace VL.Avalonia.Controls
{
    public static partial class Extensions
    {
        /// <summary>
        /// Gets bounds of Avalonia control
        /// </summary>
        /// <param name="visual">Control</param>
        /// <returns>RectangleF in pixels</returns>
        public static RectangleF Bounds(this Visual? visual) =>
            visual?.Bounds.FromRect() ?? RectangleF.Empty;

        /// <summary>
        /// Gets bounds of Avalonia control
        /// </summary>
        /// <param name="visual">Control</param>
        /// <returns>RectangleF in dip</returns>
        public static RectangleF BoundsDIP(this Visual? visual, float dip = 0.01f) =>
            visual?.Bounds.FromRectDIP(dip) ?? RectangleF.Empty;
    }
}
