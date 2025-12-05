using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ButtonSpinner</c> is a concrete implementation of Spinner that provides up/down buttons for incrementing and decrementing values. It combines a content area with spin buttons, supporting keyboard navigation, mouse wheel interaction, and customizable button positioning. ButtonSpinner serves as the foundation for controls like NumericUpDown that need visual spin buttons with content display.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/buttonspinner">ButtonSpinner</see>
/// </summary>
[ProcessNode(Name = "ButtonSpinner")]
public partial class ButtonSpinnerWrapper : SpinnerWrapperBase<ButtonSpinner>
{
    #region Spin Control Properties

    /// <param name="allowSpin">
    /// Whether spinning is enabled (affects button states and keyboard/mouse wheel interaction)
    /// </param>
    [ImplementProperty(
        "ButtonSpinner.AllowSpinProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _allowSpin;

    /// <param name="showButtonSpinner">
    /// Whether the spin buttons are visible in the control template
    /// </param>
    [ImplementProperty(
        "ButtonSpinner.ShowButtonSpinnerProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _showButtonSpinner;

    /// <param name="buttonSpinnerLocation">
    /// Position of the spin buttons relative to the content (Left or Right)
    /// </param>
    [ImplementProperty(
        "ButtonSpinner.ButtonSpinnerLocationProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<Location> _buttonSpinnerLocation;

    #endregion
}
