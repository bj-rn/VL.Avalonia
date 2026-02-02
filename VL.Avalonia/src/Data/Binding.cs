using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    public enum SupportedBindingMode
    {
        OneWay,
        TwoWay,
    }

    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract class Binding<TControl, TProp, TValue> : IDisposable
        where TControl : AvaloniaObject
        where TProp : AvaloniaProperty
    {
        protected TControl? _input;
        protected TProp? _property;
        protected IChannel<TValue>? _channel;
        protected IChannel<TValue>? _internalChannel = ChannelHelpers.CreateChannelOfType<TValue>();

        protected SupportedBindingMode _mode = SupportedBindingMode.OneWay;

        private CompositeDisposable _subscriptions = new();

        [Fragment]
        public Binding() { }

        [Fragment(Order = PinOrder.Main)]
        public void SetInput(TControl? input)
        {
            if (_input != input)
            {
                _input = input;

                Bind();
            }
        }

        public void SetProperty(TProp property)
        {
            if (_property != property)
            {
                _property = property;

                Bind();
            }
        }

        [Fragment(Order = PinOrder.Action)]
        public void SetMode(SupportedBindingMode mode = SupportedBindingMode.OneWay)
        {
            if (_mode != mode)
            {
                _mode = mode;

                Bind();
            }
        }

        [Fragment(Order = PinOrder.Action)]
        public void SetChannel(IChannel<TValue>? channel)
        {
            if (_channel != channel)
            {
                _channel = channel;

                Bind();
            }
        }

        protected virtual void Bind()
        {
            _subscriptions?.Clear();

            if (_input != null && _property != null)
            {
                var channel = (_channel ?? _internalChannel);

                if (channel != null)
                {
                    // Sets initial value on control from channel
                    _input.SetValue(_property, channel.Value);

                    var channelToBind = channel.Select(x => (object?)x);

                    _subscriptions?.Add(_input.Bind(_property, channelToBind));

                    switch (_mode)
                    {
                        case SupportedBindingMode.TwoWay:
                            var observable = _input.GetObservable(_property).Skip(1);

                            _subscriptions?.Add(
                                observable.Subscribe(x =>
                                {
                                    if (x is TValue v)
                                        channel.OnNext(v);
                                    else if (x is null)
                                        channel.OnNext(default!);
                                })
                            );

                            return;
                        case SupportedBindingMode.OneWay:
                        default:
                            return;
                    }
                }
            }
        }

        public void Dispose()
        {
            _subscriptions?.Clear();
            _internalChannel?.Dispose();
        }
    }
}
