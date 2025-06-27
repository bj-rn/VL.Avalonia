using Avalonia;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public abstract class ChannelTwoWayBindingBase<TValue, TProperty> : IDisposable
{
    protected AvaloniaObject _control;
    protected AvaloniaProperty<TProperty> _property;

    protected IChannel<TValue>? _channel;
    protected CompositeDisposable _subscriptions = new CompositeDisposable();

    protected Func<TValue, TProperty>? _convertToProperty;
    protected Func<TProperty, TValue>? _convertToValue;

    protected ChannelTwoWayBindingBase(AvaloniaObject control, AvaloniaProperty<TProperty> property, Func<TValue, TProperty>? convertToProperty = null, Func<TProperty, TValue>? convertToValue = null)
    {
        _control = control;
        _property = property;
        _convertToProperty = convertToProperty;
        _convertToValue = convertToValue;
    }
    public abstract void SetChannel(IChannel<TValue> channel);
    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}

public class ChannelTwoWayBinding<TProperty> : ChannelTwoWayBindingBase<TProperty, TProperty>
{
    public ChannelTwoWayBinding(AvaloniaObject control, AvaloniaProperty<TProperty> property) : base(control, property) { }

    public override void SetChannel(IChannel<TProperty> channel)
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
}

public class ChannelTwoWayBinding<TValue, TProperty> : ChannelTwoWayBindingBase<TValue, TProperty>
{
    public ChannelTwoWayBinding(AvaloniaObject control, AvaloniaProperty<TProperty> property, Func<TValue, TProperty> convertToProperty, Func<TProperty, TValue> convertToValue) : base(control, property, convertToProperty, convertToValue) { }

    public override void SetChannel(IChannel<TValue> channel)
    {
        if (_channel != channel)
        {
            _subscriptions.Clear();

            if (channel != null)
            {
                var controlObservable = _control.GetObservable(_property).Skip(1);
                var controlSubscription = controlObservable.Subscribe(value =>
                {
                    channel.OnNext(_convertToValue(value));
                });

                var channelSubscription = channel.Subscribe(value =>
                {
                    _control.SetValue(_property, _convertToProperty(value));
                });

                _subscriptions.Add(controlSubscription);
                _subscriptions.Add(channelSubscription);

                _control.SetValue(_property, _convertToProperty(channel.Value));
            }

            _channel = channel;
        }
    }
}



//public class ChannelTwoWayBinding<TProperty> : IDisposable
//{
//    private AvaloniaProperty<TProperty?> _property;
//    private AvaloniaObject _control;
//    private IValueConverter _converter;

//    public ChannelTwoWayBinding(AvaloniaObject control, AvaloniaProperty<TProperty?> property, IValueConverter? converter = null)
//    {
//        _property = property;
//        _control = control;
//        _converter = converter;
//    }

//    private IChannel<TValue>? _channel;
//    private CompositeDisposable _subscriptions = new CompositeDisposable();
//    public void SetChannel(IChannel<TValue>? channel)
//    {
//        if (_channel != channel)
//        {
//            _subscriptions.Clear();

//            if (channel != null)
//            {
//                var controlObservable = _control.GetObservable(_property).Skip(1);
//                var controlSubscription = controlObservable.Subscribe(value =>
//                {
//                    channel.OnNext(value);
//                });

//                var channelSubscription = channel.Subscribe(value =>
//                {
//                    _control.SetValue(_property, value);
//                });

//                _subscriptions.Add(controlSubscription);
//                _subscriptions.Add(channelSubscription);

//                _control.SetValue(_property, channel.Value);
//            }

//            _channel = channel;
//        }
//    }

//    public void Dispose()
//    {
//        _subscriptions.Dispose();
//    }
//}
