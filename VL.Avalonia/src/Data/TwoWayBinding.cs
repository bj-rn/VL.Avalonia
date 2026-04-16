using System.Reactive.Linq;
using Avalonia;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    public class TwoWayBinding<TChannel, TProperty> : IDisposable
    {
        private readonly AvaloniaObject _control;
        private readonly StyledProperty<TProperty?> _property;

        private IChannel<TChannel>? _channel;
        private IDisposable? _subscription;

        private readonly Func<TChannel?, TProperty?> _toProperty;
        private readonly Func<TProperty?, TChannel?> _toChannel;

        public TwoWayBinding(
            AvaloniaObject control,
            StyledProperty<TProperty?> property,
            Func<TChannel?, TProperty?>? toProperty = null,
            Func<TProperty?, TChannel?>? toChannel = null
        )
        {
            _control = control;
            _property = property;

            _toProperty =
                toProperty
                ?? (
                    v => v is null ? default : (TProperty?)Convert.ChangeType(v, typeof(TProperty))
                );

            _toChannel =
                toChannel
                ?? (v => v is null ? default! : (TChannel)Convert.ChangeType(v, typeof(TChannel)));
        }

        public void Bind(IChannel<TChannel>? channel)
        {
            if (ReferenceEquals(_channel, channel))
                return;

            _subscription?.Dispose();
            _channel = channel;

            if (channel != null)
            {
                var controlObservable = _control
                    .GetObservable(_property)
                    .Select(_toChannel)
                    // Skip the initial value from the control, trust the channel first
                    .Skip(1);

                _subscription = Observable
                    .Merge(controlObservable, channel)
                    .StartWith(channel.Value)
                    .DistinctUntilChanged()
                    .Subscribe(value =>
                    {
                        // 1. Update VL Channel safely
                        channel.EnsureValue(value);

                        // 2. Update Avalonia cleanly
                        var propertyValue = _toProperty(value);

                        if (!Equals(_control.GetValue(_property), propertyValue))
                        {
                            _control.SetCurrentValue(_property, propertyValue);
                        }
                    });
            }
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _channel = null;
        }
    }

    public class TwoWayBinding<T> : TwoWayBinding<T, T>
    {
        public TwoWayBinding(AvaloniaObject control, StyledProperty<T?> property)
            : base(control, property) { }
    }
}
