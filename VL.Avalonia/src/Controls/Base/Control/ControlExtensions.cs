using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Control"/>.
    /// </summary>
    public static class ControlExtensions
    {
        /// <inheritdoc cref="Control.FocusAdorner"/>
        public static ITemplate<Control>? FocusAdorner(this Control control) =>
            control?.FocusAdorner;

        /// <inheritdoc cref="Control.FocusAdorner"/>
        public static void SetFocusAdorner(this Control control, ITemplate<Control>? focusAdorner)
        {
            if (control is not null)
                control.FocusAdorner = focusAdorner;
        }

        /// <inheritdoc cref="Control.DataTemplates"/>
        public static DataTemplates? DataTemplates(this Control control) => control?.DataTemplates;

        /// <inheritdoc cref="Control.ContextMenu"/>
        public static ContextMenu? ContextMenu(this Control control) => control?.ContextMenu;

        /// <inheritdoc cref="Control.ContextMenu"/>
        public static void SetContextMenu(this Control control, ContextMenu? contextMenu)
        {
            if (control is not null)
                control.ContextMenu = contextMenu;
        }

        /// <inheritdoc cref="Control.ContextFlyout"/>
        public static FlyoutBase? ContextFlyout(this Control control) => control?.ContextFlyout;

        /// <inheritdoc cref="Control.ContextFlyout"/>
        public static void SetContextFlyout(this Control control, FlyoutBase? contextFlyout)
        {
            if (control is not null)
                control.ContextFlyout = contextFlyout;
        }

        /// <inheritdoc cref="Control.IsLoaded"/>
        public static bool IsLoaded(this Control control) => control?.IsLoaded ?? false;

        /// <inheritdoc cref="Control.Tag"/>
        public static object? Tag(this Control control) => control?.Tag;

        /// <inheritdoc cref="Control.Tag"/>
        public static void SetTag(this Control control, object? tag)
        {
            if (control is not null)
                control.Tag = tag;
        }
    }
}
