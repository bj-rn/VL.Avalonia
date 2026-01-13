using Avalonia;
using VL.Core;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers
{
    public class ChannelOptionalBinding<T> : IDisposable
    {
        private readonly AvaloniaObject _control;
        private readonly AvaloniaProperty<T> _property;

        private IChannel<T> _channel;
        private Optional<T> _value;

        private IDisposable _subscription;
        public bool IsBound => _subscription != null;

        public ChannelOptionalBinding(AvaloniaObject control, AvaloniaProperty<T> property)
        {
            _control = control;
            _property = property;
        }

        public void SetValue(Optional<T> value)
        {
            if (IsBound)
            {
                if (value.HasValue)
                {
                    _channel.EnsureValue(value.Value);
                }
            }
            else
            {
                if (_value != value)
                {
                    if (value.HasValue)
                    {
                        _control.SetValue(_property, value.Value);
                        _value = value;
                    }
                    else
                    {
                        _control.ClearValue(_property);
                        _value = value;
                    }
                }
            }
        }

        public void SetChannel(IChannel<T> channel)
        {
            if (_channel != channel)
            {
                _subscription?.Dispose();
                _subscription = null;

                _channel = channel;

                if (channel != null)
                {
                    _subscription = _control.Bind(_property, _channel);
                }
                else
                {
                    _control.ClearValue(_property);
                    _value = default;
                }
            }
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
