using Avalonia.Controls;
using Avalonia.Input;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for generic menu item, TODO generic command parameter
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class MenuItemWrapperBase<T> : HeaderedSelectingItemsControlWrapperBase<MenuItem, T>
{
    #region Command & Gesture Properties

    protected ChannelCommandBindingUnit _commandBinding;
    protected ChannelTwoWayBinding<bool> _isSelectedBinding;
    protected ChannelTwoWayBinding<bool> _isSubMenuOpenBinding;
    protected ChannelTwoWayBinding<bool> _isCheckedBinding;
    public MenuItemWrapperBase()
    {
        _commandBinding = new ChannelCommandBindingUnit();

        _isSelectedBinding = new ChannelTwoWayBinding<bool>(_output, MenuItem.IsSelectedProperty);
        _isSubMenuOpenBinding = new ChannelTwoWayBinding<bool>(_output, MenuItem.IsSubMenuOpenProperty);
        _isCheckedBinding = new ChannelTwoWayBinding<bool>(_output, MenuItem.IsCheckedProperty);

        _output.SetValue(MenuItem.CommandProperty, _commandBinding);
    }

    /// <param name="commandChannel">
    /// Unit command, fired on click
    /// </param>
    [Fragment(Order = PinOrder.Action)]
    public void SetCommandChannel(IChannel<Unit>? commandChannel) =>
        _commandBinding.SetCommandChannel(commandChannel);

    /* TODO:
    /// <param name="commandParameter">
    /// Parameter passed to the command when executed.
    /// </param>
    [ImplementProperty("MenuItem.CommandParameterProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _commandParameter;
    */

    /// <param name="hotKey">
    /// The hotkey gesture associated with this menu item (triggers via keyboard).
    /// </param>
    [ImplementProperty("MenuItem.HotKeyProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<KeyGesture> _hotKey;

    /// <param name="inputGesture">
    /// The input gesture displayed in the menu (not actual hotkey binding).
    /// </param>
    [ImplementProperty("MenuItem.InputGestureProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<KeyGesture> _inputGesture;

    #endregion

    #region Visual & Icon Properties

    /// <param name="icon">
    /// The icon to show in the menu item (object or UI).
    /// </param>
    [ImplementProperty("MenuItem.IconProperty", Order = PinOrder.Secondary, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _icon;

    #endregion

    #region Hierarchy & State Properties

    /// <param name="isSelectedChannel">
    /// Whether this menu item is currently selected.
    /// </param>
    public void SetIsSelectedChannel([Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool>? isSelectedChannel) =>
        _isSelectedBinding.SetChannel(isSelectedChannel);

    /// <param name="isSubMenuOpenChannel">
    /// Whether this menu item's submenu is currently open.
    /// </param>
    public void SetIsSubmenuOpenChannel([Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool>? isSubMenuOpenChannel) =>
        _isSubMenuOpenBinding.SetChannel(isSubMenuOpenChannel);

    /// <param name="staysOpenOnClick">
    /// Whether clicking this item keeps the parent menu open.
    /// </param>
    [ImplementProperty("MenuItem.StaysOpenOnClickProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _staysOpenOnClick;

    #endregion

    #region Toggle/Check Properties

    /// <param name="toggleType">
    /// The toggle type: None, CheckBox, or Radio (MenuItemToggleType).
    /// </param>
    [ImplementProperty("MenuItem.ToggleTypeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<MenuItemToggleType> _toggleType;

    /// <param name="isChecked">
    /// Whether the menu item is checked (for check/radio types).
    /// </param>
    public void SetIsCheckedChannel([Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool>? isCheckedChannel) =>
        _isCheckedBinding.SetChannel(isCheckedChannel);

    /// <param name="groupName">
    /// The group name for radio button grouping.
    /// </param>
    [ImplementProperty("MenuItem.GroupNameProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _groupName;

    #endregion
}
