using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The button is a control that reacts to pointer actions (and has some keyboard equivalents). It presents visual feedback in the form of a depressed state when the pointer is down.
/// </summary>
[ProcessNode(Name = "Button")]
public partial class ButtonWrapper : ControlWrapperBase<Button>
{
    [ImplementProperty("Button.ContentProperty", Order = -10)]
    protected Optional<object?> _content;

    protected ChannelCommand<Unit> _command = new((s, a) => new Unit());
    protected Optional<IChannel<Unit>> _commandChannel;
    public void SetCommandChannel(Optional<IChannel<Unit>> commandChannel)
    {
        if (_commandChannel != commandChannel)
        {
            _commandChannel = commandChannel;
            _output.Command = _command;

            if (_commandChannel.HasValue)
            {
                _command.Channel = commandChannel.Value;
            }
        }
    }

    [ImplementProperty("Button.FlyoutProperty", Order = -3, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<FlyoutBase> _flyout;

    [ImplementProperty("Button.ClickModeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ClickMode> _clickMode;
}
