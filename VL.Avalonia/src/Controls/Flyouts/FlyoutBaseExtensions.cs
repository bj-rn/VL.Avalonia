using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="FlyoutBase"/>.
    /// </summary>
    public static class FlyoutBaseExtensions
    {
        /// <inheritdoc cref="FlyoutBase.IsOpen"/>
        public static bool IsOpen(this FlyoutBase flyoutBase) =>
            flyoutBase != null ? flyoutBase.IsOpen : false;

        /// <inheritdoc cref="FlyoutBase.Target"/>
        public static Control? Target(this FlyoutBase flyoutBase) =>
            flyoutBase != null ? flyoutBase.Target : null;

        /// <inheritdoc cref="FlyoutBase.ShowAt"/>
        public static void ShowAt(this FlyoutBase flyoutBase, Control placementTarget)
        {
            flyoutBase?.ShowAt(placementTarget);
        }

        /// <inheritdoc cref="FlyoutBase.Hide"/>
        public static void Hide(this FlyoutBase flyoutBase)
        {
            flyoutBase?.Hide();
        }

        /// <inheritdoc cref="FlyoutBase.AttachedFlyoutProperty"/>
        public static FlyoutBase? AttachedFlyout(this Control control) =>
            control != null ? FlyoutBase.GetAttachedFlyout(control) : null;

        /// <inheritdoc cref="FlyoutBase.AttachedFlyoutProperty"/>
        public static void SetAttachedFlyout(this Control control, FlyoutBase? flyoutBase)
        {
            if (control is not null)
                FlyoutBase.SetAttachedFlyout(control, flyoutBase);
        }

        /// <inheritdoc cref="FlyoutBase.ShowAttachedFlyout"/>
        public static void ShowAttachedFlyout(this Control control)
        {
            if (control is not null)
                FlyoutBase.ShowAttachedFlyout(control);
        }
    }
}
