using Avalonia.Controls;
using AvaOrientation = Avalonia.Layout.Orientation;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="WrapPanel"/>.
    /// </summary>
    public static class WrapPanelExtensions
    {
        /// <inheritdoc cref="WrapPanel.Orientation"/>
        public static AvaOrientation Orientation(this WrapPanel wrapPanel) =>
            wrapPanel != null ? wrapPanel.Orientation : AvaOrientation.Horizontal;

        /// <inheritdoc cref="WrapPanel.Orientation"/>
        public static void SetOrientation(this WrapPanel wrapPanel, AvaOrientation orientation)
        {
            if (wrapPanel is not null)
                wrapPanel.Orientation = orientation;
        }

        /// <inheritdoc cref="WrapPanel.ItemWidth"/>
        public static float ItemWidth(this WrapPanel wrapPanel) =>
            wrapPanel != null ? (float)wrapPanel.ItemWidth : float.NaN;

        /// <inheritdoc cref="WrapPanel.ItemWidth"/>
        public static void SetItemWidth(this WrapPanel wrapPanel, float itemWidth)
        {
            if (wrapPanel is not null)
                wrapPanel.ItemWidth = itemWidth;
        }

        /// <inheritdoc cref="WrapPanel.ItemHeight"/>
        public static float ItemHeight(this WrapPanel wrapPanel) =>
            wrapPanel != null ? (float)wrapPanel.ItemHeight : float.NaN;

        /// <inheritdoc cref="WrapPanel.ItemHeight"/>
        public static void SetItemHeight(this WrapPanel wrapPanel, float itemHeight)
        {
            if (wrapPanel is not null)
                wrapPanel.ItemHeight = itemHeight;
        }
    }
}
