using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for <c>ToggleButton</c> inheriting controls
/// </summary>
[ProcessNode]
public abstract partial class ToggleButtonWrapperBase<T> : ControlWrapperBase<T> where T : ToggleButton, new()
{
    [ImplementProperty("ToggleButton.ContentProperty", Order = -10)]
    protected Optional<object?> _content;

    protected ChannelTwoWayBinding<bool, bool?> _isCheckedBinding;
    protected ToggleButtonWrapperBase()
    {
        _isCheckedBinding = new(_output, ToggleButton.IsCheckedProperty, (x) => x, (y) => y ?? false);
    }

    [Fragment(Order = -7)]
    public void SetIsCheckedChannel(IChannel<bool> isCheckedChannel) =>
        _isCheckedBinding.SetChannel(isCheckedChannel);

    /// <param name="isThreeState">
    /// Whether the control supports three states (checked, unchecked, and indeterminate)
    /// </param>
    [ImplementProperty("ToggleButton.IsThreeStateProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isThreeState;
}

/// <summary>
/// The <c>ToggleButton</c> is a button control that can be toggled between checked and unchecked states. Unlike a regular button, it maintains its state after being clicked. It supports three states when IsThreeState is enabled: checked, unchecked, and indeterminate. This control serves as the base class for CheckBox and other toggleable controls.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/buttons/togglebutton">ToggleButton</see>
/// </summary>
[ProcessNode(Name = "ToggleButton")]
public partial class ToggleButtonWrapper : ToggleButtonWrapperBase<ToggleButton>
{

}
