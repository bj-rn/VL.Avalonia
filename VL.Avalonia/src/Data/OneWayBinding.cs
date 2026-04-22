using System.Reactive.Linq;
using Avalonia;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    /// <summary>
    /// Handles case when setter is protected
    /// </summary>
    public class OneWayBinding<TValue, TProperty> : IDisposable
    {
        private readonly AvaloniaObject _control;
        private readonly AvaloniaProperty<TProperty?> _property;

        private IChannel<TValue>? _channel;
        private IDisposable? _subscription;

        private readonly Func<TProperty?, TValue?> _toChannel;

        public OneWayBinding(
            AvaloniaObject control,
            AvaloniaProperty<TProperty?> property,
            Func<TProperty?, TValue?>? toChannel = null
        )
        {
            _control = control;
            _property = property;

            _toChannel =
                toChannel
                ?? (v => v is null ? default! : (TValue)Convert.ChangeType(v, typeof(TValue)));
        }

        public void Bind(IChannel<TValue>? channel)
        {
            if (ReferenceEquals(_channel, channel))
                return;

            _subscription?.Dispose();
            _channel = channel;

            if (channel != null)
            {
                _subscription = _control
                    .GetObservable(_property)
                    .Select(_toChannel)
                    .DistinctUntilChanged()
                    .Subscribe(value =>
                    {
                        channel.EnsureValue(value);
                    });
            }
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _channel = null;
        }
    }

    public class OneWayBinding<T> : OneWayBinding<T, T>
    {
        public OneWayBinding(AvaloniaObject control, AvaloniaProperty<T?> property)
            : base(control, property) { }
    }
}
