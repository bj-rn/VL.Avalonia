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
/// Base wrapper for Split buttons
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class SplitButtonWrapperBase<T> : ContentControlWrapperBase<T>
    where T : SplitButton, new()
{
    #region Command Properties

    protected ChannelCommand<Unit> _command = new((s, a) => new Unit());
    protected Optional<IChannel<Unit>> _commandChannel;

    /// <param name="commandChannel">
    /// Unit command, fired on click
    /// </param>
    [Fragment(Order = -7)]
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

    ///// <param name="commandParameter">
    ///// Parameter to pass to the command when the primary button is clicked
    ///// </param>
    //[ImplementProperty("SplitButton.CommandParameterProperty", PinVisibility = Model.PinVisibility.Optional)]
    //protected Optional<object> _commandParameter;

    #endregion

    #region Flyout Properties

    /// <param name="flyout">
    /// Flyout to show when the secondary (dropdown) button part is clicked
    /// </param>
    [ImplementProperty("SplitButton.FlyoutProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<FlyoutBase> _flyout;

    #endregion

    #region Keyboard Shortcuts

    /// <param name="hotKey">
    /// Keyboard shortcut (key gesture) that triggers the primary button action
    /// </param>
    [ImplementProperty("SplitButton.HotKeyProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<KeyGesture> _hotKey;

    #endregion
}
