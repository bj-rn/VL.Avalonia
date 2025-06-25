using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>RepeatButton</c> is a control that has the added feature of regularly generating click events while the button is being pressed down.
/// </summary>
[ProcessNode(Name = "RepeatButton")]
public partial class RepeatButtonWrapper : ControlWrapperBase<RepeatButton>
{
    [ImplementProperty("RepeatButton.ContentProperty", Order = -10)]
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
    /// <summary>
    /// The time (milliseconds) to wait before repeated click generation begins. Default is 300.
    /// </summary>
    [ImplementProperty("RepeatButton.DelayProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _delay;

    /// <summary>
    /// The time (milliseconds) between clicks being generated. Default is 100.
    /// </summary>
    [ImplementProperty("RepeatButton.IntervalProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _interval;
}
