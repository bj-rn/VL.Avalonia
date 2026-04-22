using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using AvaClickMode = Avalonia.Controls.ClickMode;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Button"/>.
    /// </summary>
    public static class ButtonExtensions
    {
        /// <inheritdoc cref="Button.ClickMode"/>
        public static ClickMode ClickMode(this Button button) =>
            button?.ClickMode ?? AvaClickMode.Release;

        /// <inheritdoc cref="Button.ClickMode"/>
        public static void SetClickMode(this Button button, AvaClickMode clickMode)
        {
            if (button is not null)
                button.ClickMode = clickMode;
        }

        /// <inheritdoc cref="Button.Command"/>
        public static ICommand? Command(this Button button) => button?.Command;

        /// <inheritdoc cref="Button.Command"/>
        public static void SetCommand(this Button button, ICommand? command)
        {
            if (button is not null)
                button.Command = command;
        }

        /// <inheritdoc cref="Button.CommandParameter"/>
        public static object? CommandParameter(this Button button) => button?.CommandParameter;

        /// <inheritdoc cref="Button.CommandParameter"/>
        public static void SetCommandParameter(this Button button, object? commandParameter)
        {
            if (button is not null)
                button.CommandParameter = commandParameter;
        }

        /// <inheritdoc cref="Button.HotKey"/>
        public static KeyGesture? HotKey(this Button button) => button?.HotKey;

        /// <inheritdoc cref="Button.HotKey"/>
        public static void SetHotKey(this Button button, KeyGesture? hotKey)
        {
            if (button is not null)
                button.HotKey = hotKey;
        }

        /// <inheritdoc cref="Button.IsDefault"/>
        public static bool IsDefault(this Button button) => button?.IsDefault ?? false;

        /// <inheritdoc cref="Button.IsDefault"/>
        public static void SetIsDefault(this Button button, bool isDefault)
        {
            if (button is not null)
                button.IsDefault = isDefault;
        }

        /// <inheritdoc cref="Button.IsCancel"/>
        public static bool IsCancel(this Button button) => button?.IsCancel ?? false;

        /// <inheritdoc cref="Button.IsCancel"/>
        public static void SetIsCancel(this Button button, bool isCancel)
        {
            if (button is not null)
                button.IsCancel = isCancel;
        }

        /// <inheritdoc cref="Button.Flyout"/>
        public static FlyoutBase? Flyout(this Button button) => button?.Flyout;

        /// <inheritdoc cref="Button.Flyout"/>
        public static void SetFlyout(this Button button, FlyoutBase? flyout)
        {
            if (button is not null)
                button.Flyout = flyout;
        }
    }
}
