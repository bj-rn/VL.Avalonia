using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Canvas"/>.
    /// </summary>
    public static class CanvasExtensions
    {
        // AttachedProperties:

        /// <inheritdoc cref="Canvas.LeftProperty"/>
        public static float CanvasLeft(this Control control) =>
            control != null ? (float)Canvas.GetLeft(control) : float.NaN;

        /// <inheritdoc cref="Canvas.LeftProperty"/>
        public static void SetCanvasLeft(this Control control, float left)
        {
            if (control is not null)
                Canvas.SetLeft(control, left);
        }

        /// <inheritdoc cref="Canvas.TopProperty"/>
        public static float CanvasTop(this Control control) =>
            control != null ? (float)Canvas.GetTop(control) : float.NaN;

        /// <inheritdoc cref="Canvas.TopProperty"/>
        public static void SetCanvasTop(this Control control, float top)
        {
            if (control is not null)
                Canvas.SetTop(control, top);
        }

        /// <inheritdoc cref="Canvas.RightProperty"/>
        public static float CanvasRight(this Control control) =>
            control != null ? (float)Canvas.GetRight(control) : float.NaN;

        /// <inheritdoc cref="Canvas.RightProperty"/>
        public static void SetCanvasRight(this Control control, float right)
        {
            if (control is not null)
                Canvas.SetRight(control, right);
        }

        /// <inheritdoc cref="Canvas.BottomProperty"/>
        public static float CanvasBottom(this Control control) =>
            control != null ? (float)Canvas.GetBottom(control) : float.NaN;

        /// <inheritdoc cref="Canvas.BottomProperty"/>
        public static void SetCanvasBottom(this Control control, float bottom)
        {
            if (control is not null)
                Canvas.SetBottom(control, bottom);
        }
    }
}
