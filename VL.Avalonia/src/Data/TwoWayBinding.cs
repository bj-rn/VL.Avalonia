using System.Reactive.Linq;
using Avalonia;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    public class TwoWayBinding<TValue, TProperty> : IDisposable
    {
        private readonly AvaloniaObject _control;
        private readonly AvaloniaProperty<TProperty?> _property;

        private IChannel<TValue>? _channel;
        private IDisposable? _subscription;

        private readonly Func<TValue?, TProperty?> _toProperty;
        private readonly Func<TProperty?, TValue?> _toChannel;

        public TwoWayBinding(
            AvaloniaObject control,
            AvaloniaProperty<TProperty?> property,
            Func<TValue?, TProperty?>? toProperty = null,
            Func<TProperty?, TValue?>? toChannel = null
        )
        {
            _control = control;
            _property = property;

            // Nullable- and culture-safe; a value that can't be converted becomes default
            // instead of throwing inside the subscription.
            _toProperty = toProperty ?? (v => BindingHelpers.ChangeType<TProperty>(v));
            _toChannel = toChannel ?? (v => BindingHelpers.ChangeType<TValue>(v));
        }

        public void Bind(IChannel<TValue>? channel)
        {
            if (ReferenceEquals(_channel, channel))
                return;

            _subscription?.Dispose();
            _channel = channel;

            if (channel != null)
            {
                var controlObservable = _control
                    .GetObservable(_property)
                    // Skip the initial value from the control, trust the channel first
                    .Skip(1)
                    .Select(v => (ok: TryToChannel(channel, v, out var value), value))
                    .Where(x => x.ok)
                    .Select(x => x.value);

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

        /// <summary>
        /// Converts a control value for the channel. A value that can't be converted, or that the
        /// channel can't hold, is dropped and the channel keeps its value: a throw here would end
        /// the binding and surface in whatever changed the control.
        /// </summary>
        private bool TryToChannel(IChannel<TValue> channel, TProperty? propertyValue, out TValue? value)
        {
            try
            {
                value = _toChannel(propertyValue);
            }
            catch (Exception e)
                when (e is InvalidCastException or FormatException or OverflowException or ArgumentException)
            {
                value = default;
                return false;
            }

            // The channel can be typed narrower than TValue: VL connects a Channel<TreeViewItem>
            // to an IChannel<object> pin, and that channel casts every value it gets.
            var type = channel.ClrTypeOfValues;
            return value is null
                ? !type.IsValueType || Nullable.GetUnderlyingType(type) is not null
                : type.IsInstanceOfType(value);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _channel = null;
        }
    }

    public class TwoWayBinding<T> : TwoWayBinding<T, T>
    {
        public TwoWayBinding(AvaloniaObject control, AvaloniaProperty<T?> property)
            : base(control, property) { }
    }
}
