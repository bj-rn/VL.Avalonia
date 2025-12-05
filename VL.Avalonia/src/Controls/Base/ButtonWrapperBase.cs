using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base wrapper for Button
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class ButtonWrapperBase<T> : ContentControlWrapperBase<T>
    where T : Button, new()
{
    #region Command Properties

    protected ChannelCommandBindingUnit _commandBinding;

    public ButtonWrapperBase()
    {
        _commandBinding = new ChannelCommandBindingUnit();

        _output.SetValue(Button.CommandProperty, _commandBinding);
    }

    /// <param name="commandChannel">
    /// Unit command, fired on click
    /// </param>
    [Fragment(Order = PinOrder.Action)]
    public void SetCommandChannel(IChannel<Unit>? commandChannel) =>
        _commandBinding.SetCommandChannel(commandChannel);

    ///// <param name="commandParameter">
    ///// Parameter to pass to the command when the primary button is clicked
    ///// </param>
    //[ImplementProperty("Button.CommandParameterProperty", PinVisibility = Model.PinVisibility.Optional)]
    //protected Optional<object> _commandParameter;

    #endregion

    #region Click Behavior Properties

    /// <param name="clickMode">
    /// Determines when the Click event is raised (Release = on pointer release, Press = on pointer press)
    /// </param>
    [ImplementProperty("Button.ClickModeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ClickMode> _clickMode;

    #endregion

    #region Flyout Properties

    /// <param name="flyout">
    /// Flyout to show when the button is clicked
    /// </param>
    [ImplementProperty("Button.FlyoutProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<FlyoutBase> _flyout;

    #endregion

    #region Keyboard Shortcuts

    /// <param name="hotKey">
    /// Keyboard shortcut (key gesture) that triggers the button when pressed
    /// </param>
    [ImplementProperty("Button.HotKeyProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<KeyGesture> _hotKey;

    #endregion
}
