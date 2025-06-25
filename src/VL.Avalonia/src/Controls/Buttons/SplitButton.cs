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
/// The <c>SplitButton</c> functions as a Button with primary and secondary parts that can each be pressed separately. The primary part behaves like normal <c>Button</c> and the secondary part opens a <c>Flyout</c> with additional actions.
/// </summary>
[ProcessNode(Name = "SplitButton")]
public partial class SplitButtonWrapper : ControlWrapperBase<SplitButton>
{
    [ImplementProperty("SplitButton.ContentProperty", Order = -10)]
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

    [ImplementProperty("SplitButton.FlyoutProperty", Order = -3)]
    protected Optional<FlyoutBase> _flyout;
}
