using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="Button"/>
    [ProcessNode]
    public abstract partial class ButtonNodeBase<T> : ContentControlNodeBase<T>, IDisposable
        where T : Button, new()
    {
        private readonly UnitCommandBinding _commandBinding;

        public ButtonNodeBase()
        {
            _commandBinding = new UnitCommandBinding(_output, Button.CommandProperty);
        }

        /// <param name="commandChannel"><inheritdoc cref="Button.CommandProperty" path="/summary/node()"/></param>
        [Fragment(Order = PinOrder.Action)]
        public void SetCommandChannel(IChannel<Unit> commandChannel) =>
            _commandBinding.Bind(commandChannel);

        // TODO:
        ///// <inheritdoc cref="Button.CommandParameter"/>
        //[ImplementProperty(
        //    typeof(Button),
        //    nameof(Button.CommandParameterProperty),
        //    Order = PinOrder.Action,
        //    PinVisibility = Model.PinVisibility.Optional
        //)]
        //private Optional<object> _commandParameter;

        /// <inheritdoc cref="Button.HotKey"/>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.HotKeyProperty),
            Order = PinOrder.Action,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<KeyGesture> _hotKey;

        /// <inheritdoc cref="Button.ClickMode"/>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.ClickModeProperty),
            Order = PinOrder.Styled,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ClickMode> _clickMode;

        /// <inheritdoc cref="Button.IsDefault"/>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.IsDefaultProperty),
            Order = PinOrder.Action,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isDefault;

        /// <inheritdoc cref="Button.IsCancel"/>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.IsCancelProperty),
            Order = PinOrder.Action,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isCancel;

        /// <inheritdoc cref="Button.Flyout"/>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.FlyoutProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<FlyoutBase> _flyout;

        public override void Dispose()
        {
            _commandBinding.Dispose();
            base.Dispose();
        }
    }
}
