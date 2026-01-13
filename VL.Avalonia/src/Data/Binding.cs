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
            if (_input != null && _property != null)
            {
                var channelToBind = (_channel ?? _internalChannel) as IObservable<object?>;

                if (channelToBind != null)
                {
                    _subscriptions?.Clear();

                    _subscriptions?.Add(_input.Bind(_property, channelToBind));

                    switch (_mode)
                    {
                        case SupportedBindingMode.TwoWay:
                            var observable = _input.GetObservable(_property).Skip(1);

                            observable.Subscribe(x =>
                                (_channel ?? _internalChannel).OnNext((TValue)x)
                            );

                            return;
                        case SupportedBindingMode.OneWay:
                        default:
                            return;
                    }
                }
            }

            _subscriptions?.Clear();
        }

        public void Dispose()
        {
            _subscriptions?.Clear();
            _internalChannel?.Dispose();
        }
    }
}
