using System.Collections;
using Avalonia;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers
{
    public class ItemsSourceBinding<T>
    {
        private IChannel<Spread<T>> _internalChannel;

        private IChannel<Spread<T>> _channel;
        private IObservable<IEnumerable?> AsObservable => _channel;

        public ItemsSourceBinding(AvaloniaObject control, AvaloniaProperty property)
        {
            _internalChannel = ChannelHelpers.CreateChannelOfType<Spread<T>>();
            _channel = _internalChannel;
            control.Bind(property, _channel);
        }

        public void SetItems(Spread<T> items)
        {
            _channel.OnNext(items);
        }

        public void SetChannel(IChannel<Spread<T>> channel)
        {
            if (_channel != channel)
            {
                _channel = channel;
            }
        }
    }
}
