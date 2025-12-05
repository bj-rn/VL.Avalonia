using System.Globalization;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <see href="https://docs.avaloniaui.net/docs/reference/controls/maskedtextbox">MaskedTextBox</see> presents an area for typed (keyboard) input, but where the format and characters permitted can be constrained by a mask pattern formed from special characters.
/// </summary>
[ProcessNode(Name = "MaskedTextBox")]
public partial class MaskedTextBoxWrapper : TextBoxWrapperBase<MaskedTextBox>
{
    /// <param name="mask">
    /// The input mask pattern that defines allowed input format (e.g., "000-000-0000" for phone numbers)
    /// </param>
    [ImplementProperty("MaskedTextBox.MaskProperty", Order = -9)]
    protected Optional<string> _mask;

    /// <param name="promptChar">
    /// Character used to represent the absence of user input (default: '_')
    /// </param>
    [ImplementProperty("MaskedTextBox.PromptCharProperty", Order = -3)]
    protected Optional<char> _promptChar;

    #region Mask Behavior Properties

    /// <param name="hidePromptOnLeave">
    /// Whether prompt characters are hidden when the control loses focus
    /// </param>
    [ImplementProperty(
        "MaskedTextBox.HidePromptOnLeaveProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _hidePromptOnLeave;

    /// <param name="resetOnPrompt">
    /// Whether selected characters should be reset when the prompt character is pressed
    /// </param>
    [ImplementProperty(
        "MaskedTextBox.ResetOnPromptProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _resetOnPrompt;

    /// <param name="resetOnSpace">
    /// Whether selected characters should be reset when the space character is pressed
    /// </param>
    [ImplementProperty(
        "MaskedTextBox.ResetOnSpaceProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _resetOnSpace;

    #endregion
    #region Input Validation Properties

    /// <param name="asciiOnly">
    /// Whether the masked text box is restricted to accept only ASCII characters
    /// </param>
    [ImplementProperty(
        "MaskedTextBox.AsciiOnlyProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _asciiOnly;

    /// <param name="culture">
    /// Culture information used for character validation and formatting
    /// </param>
    [ImplementProperty(
        "MaskedTextBox.CultureProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<CultureInfo> _culture;
    #endregion
}
