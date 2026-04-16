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

    public enum SupprotedBindingInitialValueHandling
    {
        None,

        /// <summary>Sets channel value to control before bind</summary>
        ChannelToControl,

        /// <summary>Control value to channel before bind</summary>
        ControlToChannel,
    }

    [ProcessNode(Name = "Binding", FragmentSelection = FragmentSelection.Explicit)]
    public abstract class BindingNode<TControl, TProp, TValue> : IDisposable
        where TControl : AvaloniaObject
        where TProp : AvaloniaProperty
    {
        protected TControl? _input;
        protected TProp? _property;
        protected SupprotedBindingInitialValueHandling _initialValueHandling =
            SupprotedBindingInitialValueHandling.ChannelToControl;
        protected IChannel<TValue>? _channel;
        protected IChannel<TValue>? _internalChannel = ChannelHelpers.CreateChannelOfType<TValue>();

        protected SupportedBindingMode _mode = SupportedBindingMode.OneWay;

        private CompositeDisposable _subscriptions = new();

        [Fragment]
        public BindingNode() { }

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
        public void SetInitialValueHandling(
            [Pin(Visibility = Model.PinVisibility.Optional)]
                SupprotedBindingInitialValueHandling initialValueHandling =
                SupprotedBindingInitialValueHandling.ChannelToControl
        )
        {
            if (_initialValueHandling != initialValueHandling)
            {
                _initialValueHandling = initialValueHandling;
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
                    // Initial value handling

                    if (
                        _initialValueHandling
                        is SupprotedBindingInitialValueHandling.ChannelToControl
                    )
                    {
                        // Set from channel to control
                        _input.SetValue(_property, channel.Value);
                    }
                    if (
                        _initialValueHandling
                        is SupprotedBindingInitialValueHandling.ControlToChannel
                    )
                    {
                        var pv = _input.GetValue(_property);
                        //  Set from control to channel
                        channel.SetValue((TValue?)pv);
                    }

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
