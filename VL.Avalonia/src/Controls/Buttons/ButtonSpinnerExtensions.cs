using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ButtonSpinner"/>.
    /// </summary>
    public static class ButtonSpinnerExtensions
    {
        /// <inheritdoc cref="ButtonSpinner.AllowSpin"/>
        public static bool AllowSpin(this ButtonSpinner buttonSpinner) =>
            buttonSpinner != null ? buttonSpinner.AllowSpin : true;

        /// <inheritdoc cref="ButtonSpinner.AllowSpin"/>
        public static void SetAllowSpin(this ButtonSpinner buttonSpinner, bool allowSpin)
        {
            if (buttonSpinner is not null)
                buttonSpinner.AllowSpin = allowSpin;
        }

        /// <inheritdoc cref="ButtonSpinner.ShowButtonSpinner"/>
        public static bool ShowButtonSpinner(this ButtonSpinner buttonSpinner) =>
            buttonSpinner != null ? buttonSpinner.ShowButtonSpinner : true;

        /// <inheritdoc cref="ButtonSpinner.ShowButtonSpinner"/>
        public static void SetShowButtonSpinner(
            this ButtonSpinner buttonSpinner,
            bool showButtonSpinner
        )
        {
            if (buttonSpinner is not null)
                buttonSpinner.ShowButtonSpinner = showButtonSpinner;
        }

        /// <inheritdoc cref="ButtonSpinner.ButtonSpinnerLocation"/>
        public static Location ButtonSpinnerLocation(this ButtonSpinner buttonSpinner) =>
            buttonSpinner != null ? buttonSpinner.ButtonSpinnerLocation : Location.Right;

        /// <inheritdoc cref="ButtonSpinner.ButtonSpinnerLocation"/>
        public static void SetButtonSpinnerLocation(
            this ButtonSpinner buttonSpinner,
            Location buttonSpinnerLocation
        )
        {
            if (buttonSpinner is not null)
                buttonSpinner.ButtonSpinnerLocation = buttonSpinnerLocation;
        }

        // AttachedProperties:
        // No attached properties were found in the provided file.
    }
}
