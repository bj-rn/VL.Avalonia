using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public abstract class ChannelTwoWayBindingBase<TControl, TValue, TProperty> : IDisposable
    where TControl : AvaloniaObject
{
    protected TControl _control;
    protected AvaloniaProperty<TProperty> _property;

    protected IChannel<TValue>? _channel;
    protected CompositeDisposable _subscriptions = new CompositeDisposable();

    protected Func<TValue, TProperty>? _convertToProperty;
    protected Func<TProperty, TValue>? _convertToValue;

    protected ChannelTwoWayBindingBase(
        TControl control,
        AvaloniaProperty<TProperty> property,
        Func<TValue, TProperty>? convertToProperty = null,
        Func<TProperty, TValue>? convertToValue = null
    )
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

public class ChannelTwoWayBinding<TProperty>
    : ChannelTwoWayBindingBase<AvaloniaObject, TProperty, TProperty>
{
    public ChannelTwoWayBinding(AvaloniaObject control, AvaloniaProperty<TProperty> property)
        : base(control, property) { }

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

public class ChannelTwoWayBinding<TValue, TProperty>
    : ChannelTwoWayBindingBase<AvaloniaObject, TValue, TProperty>
{
    public ChannelTwoWayBinding(
        AvaloniaObject control,
        AvaloniaProperty<TProperty> property,
        Func<TValue, TProperty> convertToProperty,
        Func<TProperty, TValue> convertToValue
    )
        : base(control, property, convertToProperty, convertToValue) { }

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

/// <summary>
/// Prototype binding to something that should be controlled via method. Likely Obsolete.
/// </summary>
/// <typeparam name="TControl"></typeparam>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TProperty"></typeparam>
[Obsolete]
public class ChannelTwoWayBinding<TControl, TValue, TProperty>
    : ChannelTwoWayBindingBase<TControl, TProperty, TProperty>
    where TControl : AvaloniaObject
{
    protected Action<TControl, AvaloniaProperty<TProperty>, TProperty?> _propertySetter = (
        control,
        property,
        value
    ) => control.SetValue(property, value);

    public ChannelTwoWayBinding(
        TControl control,
        AvaloniaProperty<TProperty> property,
        Action<TControl, AvaloniaProperty<TProperty>, TProperty?> propertySetter = null
    )
        : base(control, property)
    {
        _propertySetter = propertySetter ?? _propertySetter;
    }

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
                    _propertySetter(_control, _property, value);
                });

                _subscriptions.Add(controlSubscription);
                _subscriptions.Add(channelSubscription);

                _propertySetter(_control, _property, channel.Value);
            }

            _channel = channel;
        }
    }
}
