using Avalonia;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public class ChannelOneWayBinding<TProperty>
{
    private readonly AvaloniaProperty<TProperty?> _property;
    private readonly AvaloniaObject _control;

    public ChannelOneWayBinding(AvaloniaObject control, AvaloniaProperty<TProperty?> property)
    {
        _property = property;
        _control = control;
    }

    private IChannel<TProperty>? _channel;
    public void SetChannel(IChannel<TProperty>? channel)
    {
        if (_channel != channel)
        {
            if (channel != null)
            {
                _control.Bind(_property, channel);
            }

            _channel = channel;
        }
    }
}
