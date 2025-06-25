using Avalonia;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public class ChannelTwoWayBinding<TProperty> : IDisposable
{
    private AvaloniaProperty<TProperty?> _property;
    private AvaloniaObject _control;
    public ChannelTwoWayBinding(AvaloniaObject control, AvaloniaProperty<TProperty?> property)
    {
        _property = property;
        _control = control;
    }

    private IChannel<TProperty>? _channel;
    private CompositeDisposable _subscriptions = new CompositeDisposable();
    public void SetChannel(IChannel<TProperty>? channel)
    {
        if (_channel != channel)
        {
            _subscriptions.Clear();

            if (channel != null)
            {
                var controlObservable = _control.GetObservable(_property).Skip(1);
                var controlSubscription = controlObservable.Subscribe(value =>
                {
                    channel.OnNext(value);
                });

                var channelSubscription = channel.Subscribe(value =>
                {
                    _control.SetValue(_property, value);
                });

                _subscriptions.Add(controlSubscription);
                _subscriptions.Add(channelSubscription);

                _control.SetValue(_property, channel.Value);
            }

            _channel = channel;
        }
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}
