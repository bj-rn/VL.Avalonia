using Avalonia.Controls;
using AvaDock = Avalonia.Controls.Dock;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="DockPanel"/>.
    /// </summary>
    public static class DockPanelExtensions
    {
        /// <inheritdoc cref="DockPanel.LastChildFill"/>
        public static bool LastChildFill(this DockPanel dockPanel) =>
            dockPanel != null ? dockPanel.LastChildFill : true;

        /// <inheritdoc cref="DockPanel.LastChildFill"/>
        public static void SetLastChildFill(this DockPanel dockPanel, bool lastChildFill)
        {
            if (dockPanel is not null)
                dockPanel.LastChildFill = lastChildFill;
        }

        // AttachedProperties:

        /// <inheritdoc cref="DockPanel.DockProperty"/>
        public static AvaDock Dock(this Control control) =>
            control != null ? DockPanel.GetDock(control) : AvaDock.Left;

        /// <inheritdoc cref="DockPanel.DockProperty"/>
        public static void SetDock(this Control control, AvaDock dock)
        {
            if (control is not null)
                DockPanel.SetDock(control, dock);
        }
    }
}
