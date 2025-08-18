using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>Button</c> is a fundamental interactive control that responds to user clicks and executes commands. It extends ContentControl to display any type of content while providing comprehensive click handling, keyboard shortcuts, command binding, and flyout support. Button is the primary control for triggering actions in user interfaces.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/button">Button</see>
/// </summary>
[ProcessNode(Name = "Button")]
public partial class ButtonWrapper : ButtonWrapperBase<Button>
{
}

/// <summary>
/// Untested
/// </summary>
[ProcessNode(Name = "DialogButton")]
public partial class DailogButtonWrapper : ButtonWrapper
{
    #region Dialog Button Properties

    /// <param name="isDefault">
    /// Dialog Button. Whether this button is the default button (triggered by Enter key)
    /// </param>
    [ImplementProperty("Button.IsDefaultProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isDefault;

    /// <param name="isCancel">
    /// Dialog Button. Whether this button is the cancel button (triggered by Escape key)
    /// </param>
    [ImplementProperty("Button.IsCancelProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isCancel;

    #endregion
}
