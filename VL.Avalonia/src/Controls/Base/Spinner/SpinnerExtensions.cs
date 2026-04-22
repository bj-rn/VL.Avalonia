using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Spinner"/>.
    /// </summary>
    public static class SpinnerExtensions
    {
        /// <inheritdoc cref="Spinner.ValidSpinDirection"/>
        public static ValidSpinDirections ValidSpinDirection(this Spinner spinner) =>
            spinner != null
                ? spinner.ValidSpinDirection
                : ValidSpinDirections.Increase | ValidSpinDirections.Decrease;

        /// <inheritdoc cref="Spinner.ValidSpinDirection"/>
        public static void SetValidSpinDirection(
            this Spinner spinner,
            ValidSpinDirections validSpinDirection
        )
        {
            if (spinner is not null)
                spinner.ValidSpinDirection = validSpinDirection;
        }
    }
}
