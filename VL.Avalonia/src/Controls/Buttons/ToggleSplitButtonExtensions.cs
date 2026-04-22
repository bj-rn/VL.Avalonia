using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ToggleSplitButton"/>.
    /// </summary>
    public static class ToggleSplitButtonExtensions
    {
        /// <inheritdoc cref="ToggleSplitButton.IsChecked"/>
        public static bool IsChecked(this ToggleSplitButton toggleSplitButton) =>
            toggleSplitButton != null ? toggleSplitButton.IsChecked : false;

        /// <inheritdoc cref="ToggleSplitButton.IsChecked"/>
        public static void SetIsChecked(this ToggleSplitButton toggleSplitButton, bool isChecked)
        {
            if (toggleSplitButton is not null)
                toggleSplitButton.IsChecked = isChecked;
        }
    }
}
