using Avalonia.Controls;
using Avalonia.Media;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Panel"/>.
    /// </summary>
    public static class PanelExtensions
    {
        /// <inheritdoc cref="Panel.Children"/>
        public static IReadOnlyList<Control> Children(this Panel panel)
        {
            var items = panel?.Children;

            if (items == null || items.Count == 0)
                return Spread<Control>.Empty;

            return items.OfType<Control>().ToSpread();
        }

        /// <inheritdoc cref="Panel.Background"/>
        public static IBrush? Background(this Panel panel) =>
            panel != null ? panel.Background : null;

        /// <inheritdoc cref="Panel.Background"/>
        public static void SetBackground(this Panel panel, IBrush? background)
        {
            if (panel is not null)
                panel.Background = background;
        }

        /// <inheritdoc cref="Panel.IsItemsHost"/>
        public static bool IsItemsHost(this Panel panel) =>
            panel != null ? panel.IsItemsHost : false;
    }
}
