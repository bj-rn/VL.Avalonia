using Avalonia;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public class ChannelSpreadToItemsSourceBinding<T> : IDisposable
{
    private IChannel<Spread<T>> _channel = ChannelHelpers.CreateChannelOfType<Spread<T>>();
    public ChannelSpreadToItemsSourceBinding(AvaloniaObject control, AvaloniaProperty property)
    {
        control.Bind(property, _channel);
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

    public void Dispose()
    {
        _channel?.Dispose();
    }
}
