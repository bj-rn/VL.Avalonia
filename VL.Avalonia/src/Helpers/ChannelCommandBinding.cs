using System.Reactive;
using System.Windows.Input;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

/// <summary>
/// Base command binding to channel, TODO...
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ChannelCommandBindingBase<T> : ICommand, IDisposable
    where T : new()
{
    internal IChannel<T>? _commandChannel = ChannelHelpers.CreateChannelOfType<T>();

    public event EventHandler? CanExecuteChanged;

    /// <param name="commandChannel">
    /// Sets Command channel
    /// </param>
    public void SetCommandChannel(IChannel<T>? commandChannel)
    {
        if (_commandChannel != commandChannel)
        {
            _commandChannel = commandChannel;
        }
    }

    public bool CanExecute(object? parameter) => true;

    public virtual void Execute(object? parameter)
    {
        _commandChannel?.OnNext((T?)parameter);
    }

    public void Dispose()
    {
        _commandChannel?.Dispose();
    }
}

/// <summary>
/// Channel command binding with generic payload. TODO
/// </summary>
public class ChannelCommandBinding<T> : ChannelCommandBindingBase<T>
    where T : new() { }

/// <summary>
/// Channel command binding to Unit.
/// </summary>
public class ChannelCommandBindingUnit : ChannelCommandBindingBase<Unit>
{
    public override void Execute(object? parameter)
    {
        _commandChannel?.OnNext(new Unit());
    }
}
