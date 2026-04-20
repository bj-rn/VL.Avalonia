using Avalonia.Controls;
using AvaOrientation = Avalonia.Layout.Orientation;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="StackPanel"/>.
    /// </summary>
    public static class StackPanelExtensions
    {
        /// <inheritdoc cref="StackPanel.Spacing"/>
        public static float Spacing(this StackPanel stackPanel) =>
            stackPanel != null ? (float)stackPanel.Spacing : 0f;

        /// <inheritdoc cref="StackPanel.Spacing"/>
        public static void SetSpacing(this StackPanel stackPanel, float spacing)
        {
            if (stackPanel is not null)
                stackPanel.Spacing = spacing;
        }

        /// <inheritdoc cref="StackPanel.Orientation"/>
        public static AvaOrientation Orientation(this StackPanel stackPanel) =>
            stackPanel != null ? stackPanel.Orientation : AvaOrientation.Vertical;

        /// <inheritdoc cref="StackPanel.Orientation"/>
        public static void SetOrientation(this StackPanel stackPanel, AvaOrientation orientation)
        {
            if (stackPanel is not null)
                stackPanel.Orientation = orientation;
        }

        /// <inheritdoc cref="StackPanel.AreHorizontalSnapPointsRegular"/>
        public static bool AreHorizontalSnapPointsRegular(this StackPanel stackPanel) =>
            stackPanel != null ? stackPanel.AreHorizontalSnapPointsRegular : false;

        /// <inheritdoc cref="StackPanel.AreHorizontalSnapPointsRegular"/>
        public static void SetAreHorizontalSnapPointsRegular(
            this StackPanel stackPanel,
            bool areHorizontalSnapPointsRegular
        )
        {
            if (stackPanel is not null)
                stackPanel.AreHorizontalSnapPointsRegular = areHorizontalSnapPointsRegular;
        }

        /// <inheritdoc cref="StackPanel.AreVerticalSnapPointsRegular"/>
        public static bool AreVerticalSnapPointsRegular(this StackPanel stackPanel) =>
            stackPanel != null ? stackPanel.AreVerticalSnapPointsRegular : false;

        /// <inheritdoc cref="StackPanel.AreVerticalSnapPointsRegular"/>
        public static void SetAreVerticalSnapPointsRegular(
            this StackPanel stackPanel,
            bool areVerticalSnapPointsRegular
        )
        {
            if (stackPanel is not null)
                stackPanel.AreVerticalSnapPointsRegular = areVerticalSnapPointsRegular;
        }
    }
}
