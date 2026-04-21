using Avalonia.Controls;
using Avalonia.Media;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="LayoutTransformControl"/>.
    /// </summary>
    public static class LayoutTransformControlExtensions
    {
        /// <inheritdoc cref="LayoutTransformControl.LayoutTransform"/>
        public static ITransform? LayoutTransform(
            this LayoutTransformControl layoutTransformControl
        ) => layoutTransformControl != null ? layoutTransformControl.LayoutTransform : null;

        /// <inheritdoc cref="LayoutTransformControl.LayoutTransform"/>
        public static void SetLayoutTransform(
            this LayoutTransformControl layoutTransformControl,
            ITransform? layoutTransform
        )
        {
            if (layoutTransformControl is not null)
                layoutTransformControl.LayoutTransform = layoutTransform;
        }

        /// <inheritdoc cref="LayoutTransformControl.UseRenderTransform"/>
        public static bool UseRenderTransform(this LayoutTransformControl layoutTransformControl) =>
            layoutTransformControl != null ? layoutTransformControl.UseRenderTransform : false;

        /// <inheritdoc cref="LayoutTransformControl.UseRenderTransform"/>
        public static void SetUseRenderTransform(
            this LayoutTransformControl layoutTransformControl,
            bool useRenderTransform
        )
        {
            if (layoutTransformControl is not null)
                layoutTransformControl.UseRenderTransform = useRenderTransform;
        }

        /// <inheritdoc cref="LayoutTransformControl.TransformRoot"/>
        public static Control? TransformRoot(this LayoutTransformControl layoutTransformControl) =>
            layoutTransformControl != null ? layoutTransformControl.TransformRoot : null;
    }
}
