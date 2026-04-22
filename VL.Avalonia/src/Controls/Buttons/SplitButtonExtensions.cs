using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="SplitButton"/>.
    /// </summary>
    public static class SplitButtonExtensions
    {
        /// <inheritdoc cref="SplitButton.Command"/>
        public static ICommand? Command(this SplitButton splitButton) =>
            splitButton != null ? splitButton.Command : null;

        /// <inheritdoc cref="SplitButton.Command"/>
        public static void SetCommand(this SplitButton splitButton, ICommand? command)
        {
            if (splitButton is not null)
                splitButton.Command = command;
        }

        /// <inheritdoc cref="SplitButton.CommandParameter"/>
        public static object? CommandParameter(this SplitButton splitButton) =>
            splitButton != null ? splitButton.CommandParameter : null;

        /// <inheritdoc cref="SplitButton.CommandParameter"/>
        public static void SetCommandParameter(
            this SplitButton splitButton,
            object? commandParameter
        )
        {
            if (splitButton is not null)
                splitButton.CommandParameter = commandParameter;
        }

        /// <inheritdoc cref="SplitButton.Flyout"/>
        public static FlyoutBase? Flyout(this SplitButton splitButton) =>
            splitButton != null ? splitButton.Flyout : null;

        /// <inheritdoc cref="SplitButton.Flyout"/>
        public static void SetFlyout(this SplitButton splitButton, FlyoutBase? flyout)
        {
            if (splitButton is not null)
                splitButton.Flyout = flyout;
        }

        /// <inheritdoc cref="SplitButton.HotKey"/>
        public static KeyGesture? HotKey(this SplitButton splitButton) =>
            splitButton != null ? splitButton.HotKey : null;

        /// <inheritdoc cref="SplitButton.HotKey"/>
        public static void SetHotKey(this SplitButton splitButton, KeyGesture? hotKey)
        {
            if (splitButton is not null)
                splitButton.HotKey = hotKey;
        }
    }
}
