using Avalonia.Controls.Primitives;

namespace VL.Avalonia.Controls.Buttons
{
    /// <summary>
    /// Extension methods for <see cref="ToggleButton"/>.
    /// </summary>
    public static class ToggleButtonExtensions
    {
        /// <inheritdoc cref="ToggleButton.IsChecked"/>
        public static bool? IsChecked(this ToggleButton toggleButton) =>
            toggleButton != null ? toggleButton.IsChecked : false;

        /// <inheritdoc cref="ToggleButton.IsChecked"/>
        public static void SetIsChecked(this ToggleButton toggleButton, bool? isChecked)
        {
            if (toggleButton is not null)
                toggleButton.IsChecked = isChecked;
        }

        /// <inheritdoc cref="ToggleButton.IsThreeState"/>
        public static bool IsThreeState(this ToggleButton toggleButton) =>
            toggleButton != null ? toggleButton.IsThreeState : false;

        /// <inheritdoc cref="ToggleButton.IsThreeState"/>
        public static void SetIsThreeState(this ToggleButton toggleButton, bool isThreeState)
        {
            if (toggleButton is not null)
                toggleButton.IsThreeState = isThreeState;
        }
    }
}
