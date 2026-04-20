using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Input;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="MenuItem"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class MenuItemNodeBase<T>
        : HeaderedSelectingItemsControlNodeBase<MenuItem, T>,
            IDisposable
    {
        private UnitCommandBinding _commandBinding;
        private TwoWayBinding<bool> _isSelectedBinding;
        private TwoWayBinding<bool> _isSubMenuOpenBinding;
        private TwoWayBinding<bool> _isCheckedBinding;

        public MenuItemNodeBase()
        {
            _commandBinding = new UnitCommandBinding(_output, MenuItem.CommandProperty);
            _isSelectedBinding = new TwoWayBinding<bool>(_output, MenuItem.IsSelectedProperty);
            _isSubMenuOpenBinding = new TwoWayBinding<bool>(
                _output,
                MenuItem.IsSubMenuOpenProperty
            );
            _isCheckedBinding = new TwoWayBinding<bool>(_output, MenuItem.IsCheckedProperty);
        }

        /// <param name="commandChannel">Binds menu item <see cref="ICommand"/> to <see cref="IChannel{T}"/> <see cref="Unit"/></param>
        [Fragment(Order = PinOrder.Action)]
        public void SetCommandChannel(IChannel<Unit> commandChannel) =>
            _commandBinding.Bind(commandChannel);

        // TODO:
        ///// <summary>Sets a parameter to be passed to the <see cref="MenuItem.Command"/>.</summary>
        //[ImplementProperty(
        //    typeof(MenuItem),
        //    nameof(MenuItem.CommandParameterProperty),
        //    Order = PinOrder.Action,
        //    PinVisibility = Model.PinVisibility.Optional
        //)]
        //private Optional<object> _commandParameter;

        /// <summary>Sets an <see cref="KeyGesture"/> associated with this control.</summary>
        [ImplementProperty(
            typeof(MenuItem),
            nameof(MenuItem.HotKeyProperty),
            Order = PinOrder.Action,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<KeyGesture> _hotKey;

        /// <summary>Sets the icon that appears in a <see cref="MenuItem"/>.</summary>
        [ImplementProperty(
            typeof(MenuItem),
            nameof(MenuItem.IconProperty),
            Order = PinOrder.Secondary,
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<object> _icon;

        /// <summary>Sets input gesture that will be displayed in the menu item.</summary>
        [ImplementProperty(
            typeof(MenuItem),
            nameof(MenuItem.InputGestureProperty),
            Order = PinOrder.Secondary,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<KeyGesture> _inputGesture;

        /// <param name="isSelectedChannel">Binds a value indicating whether the <see cref="MenuItem"/> is currently selected.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsSelectedChannel(
            [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isSelectedChannel
        ) => _isSelectedBinding.Bind(isSelectedChannel);

        /// <param name="isSubMenuOpenChannel">Binds a value that indicates whether the submenu of the <see cref="MenuItem"/> is open.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsSubmenuOpenChannel(
            [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isSubMenuOpenChannel
        ) => _isSubMenuOpenBinding.Bind(isSubMenuOpenChannel);

        /// <summary>Sets a value that indicates the submenu that this <see cref="MenuItem"/> is within should not close when this item is clicked.</summary>
        [ImplementProperty(
            typeof(MenuItem),
            nameof(MenuItem.StaysOpenOnClickProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _staysOpenOnClick;

        /// <summary>Sets a toggle type of the menu item.</summary>
        [ImplementProperty(
            typeof(MenuItem),
            nameof(MenuItem.ToggleTypeProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<MenuItemToggleType> _toggleType;

        /// <param name="isCheckedChannel">Binds menu item is checked (for check/radio types).</param>
        public void SetIsCheckedChannel(
            [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool>? isCheckedChannel
        ) => _isCheckedBinding.Bind(isCheckedChannel);
    }

    /// <summary>
    /// Ungeneric wrapper for <see cref="MenuItem"/>
    /// </summary>
    [ProcessNode(Name = "MenuItem")]
    public class MenuItemNode : MenuItemNodeBase<object>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuItemNode"/>
    [ProcessNode(Name = "MenuItem (Spectral)")]
    public class MenuItemSpectralNode : MenuItemNodeBase<object>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="MenuItem"/>
    /// </summary>
    [ProcessNode(Name = "MenuItem (Advanced)")]
    public class MenuItemNode<T> : MenuItemNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuItemNode{T}"/>
    [ProcessNode(Name = "MenuItem (Advanced Spectral)")]
    public class MenuItemSpectralNode<T> : MenuItemNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuItemNode{T}"/>
    [ProcessNode(Name = "MenuItem (Advanced Reactive)")]
    public class MenuItemReactiveNode<T> : MenuItemNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
