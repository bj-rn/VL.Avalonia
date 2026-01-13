using Avalonia;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public class ChannelSpreadToItemsSourceBinding<T> : IDisposable
{
    private IChannel<Spread<T>> _internalChannel => ChannelHelpers.CreateChannelOfType<Spread<T>>();

    private IChannel<Spread<T>> _channel;

    private readonly AvaloniaObject _control;
    private readonly AvaloniaProperty _property;

    public ChannelSpreadToItemsSourceBinding(AvaloniaObject control, AvaloniaProperty property)
    {
        _channel = _internalChannel;
        _control = control;
        _property = property;

        _control.Bind(_property, _channel);
    }

    private Spread<T> _items;

    public void SetItems(Spread<T> items)
    {
        if (_items != items)
        {
            _items = items;
            _channel.OnNext(items);
        }
    }

    public void SetChannel(IChannel<Spread<T>> channel)
    {
        if (_channel != channel)
        {
            if (channel != null)
            {
                _channel = channel;

                _control.Bind(_property, _channel);
                _control.SetValue(_property, _channel.Value);
            }
            else
            {
                _channel = _internalChannel;
                _control.Bind(_property, _channel);
            }
        }
    }

    public void Dispose()
    {
        _channel?.Dispose();
    }
}
