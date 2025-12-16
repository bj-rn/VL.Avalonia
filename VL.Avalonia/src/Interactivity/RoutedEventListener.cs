using System.Reactive.Disposables;
using Avalonia.Interactivity;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Interactivity
{
    /// <summary>
    /// Base class for listening to RoutedEvents and executing a user-provided callback.
    /// </summary>
    /// <typeparam name="TArgs">The specific type of RoutedEvent arguments.</typeparam>
    [ProcessNode(Name = "RoutedEventListener")]
    public class RoutedEventListener<TArgs> : IDisposable
        where TArgs : RoutedEventArgs
    {
        protected Optional<Interactive> _interactive;
        protected RoutedEvent? _routedEvent;
        protected RoutingStrategies _routingStrategies =
            RoutingStrategies.Direct | RoutingStrategies.Bubble;
        protected bool _handledEventsToo = false;

        protected Func<Interactive, TArgs, bool>? _handler;

        private readonly SerialDisposable _subscription = new SerialDisposable();

        /// <summary>
        /// Sets the Interactive element to listen to.
        /// </summary>
        ///
        [Fragment(Order = PinOrder.Main)]
        public void SetInteractive(Optional<Interactive> interactive)
        {
            if (interactive != _interactive)
            {
                _interactive = interactive;
                Resubscribe();
            }
        }

        /// <summary>
        /// Sets the RoutedEvent to listen for.
        /// </summary>
        [Fragment(Order = PinOrder.Action)]
        public void SetRoutedEvent(RoutedEvent? routedEvent)
        {
            if (routedEvent != _routedEvent)
            {
                _routedEvent = routedEvent;
                Resubscribe();
            }
        }

        /// <summary>
        /// Sets the callback to execute when the event occurs.
        /// The callback receives the sender (Interactive) and the event args.
        /// Return true to mark the event as Handled.
        /// </summary>
        [Fragment]
        public void SetHandler(Func<Interactive, TArgs, bool>? handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Sets the routing strategy (Bubble, Tunnel, Direct).
        /// </summary>
        [Fragment]
        public void SetRoutingStrategies(
            [Pin(Visibility = PinVisibility.Optional)]
                RoutingStrategies routingStrategies =
                RoutingStrategies.Direct | RoutingStrategies.Bubble
        )
        {
            if (routingStrategies != _routingStrategies)
            {
                _routingStrategies = routingStrategies;
                Resubscribe();
            }
        }

        /// <summary>
        /// Sets whether to handle events that have already been handled.
        /// </summary>
        [Fragment]
        public void SetHandledEventsToo(
            [Pin(Visibility = PinVisibility.Optional)] bool handledEventsToo = false
        )
        {
            if (handledEventsToo != _handledEventsToo)
            {
                _handledEventsToo = handledEventsToo;
                Resubscribe();
            }
        }

        protected virtual void Resubscribe()
        {
            // Clear existing subscription
            _subscription.Disposable = Disposable.Empty;

            if (!_interactive.HasValue || _interactive.Value == null || _routedEvent == null)
            {
                return;
            }

            var target = _interactive.Value;

            void HandlerAdapter(object? sender, RoutedEventArgs args)
            {
                var currentHandler = _handler;

                // Check if we have a handler and the args are of the expected type
                if (currentHandler != null && args is TArgs typedArgs)
                {
                    // Try to cast sender to Interactive.
                    // In Avalonia, the sender of a RoutedEvent is almost always an Interactive (Control, etc.)
                    if (sender is Interactive interactiveSender)
                    {
                        // Invoke the user callback
                        var handled = currentHandler(interactiveSender, typedArgs);

                        // If user returned true, mark event as handled
                        if (handled)
                        {
                            typedArgs.Handled = true;
                        }
                    }
                }
            }

            // Subscribe using the untyped AddHandler which supports all routing strategies
            target.AddHandler(
                _routedEvent,
                (EventHandler<RoutedEventArgs>)HandlerAdapter,
                _routingStrategies,
                _handledEventsToo
            );

            // Cleanup
            _subscription.Disposable = Disposable.Create(() =>
            {
                target.RemoveHandler(_routedEvent, (EventHandler<RoutedEventArgs>)HandlerAdapter);
            });
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
