using System.Reactive.Disposables;
using Avalonia.Interactivity;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Interactivity
{
    /// <summary>
    /// Abstract base class for routed event listeners.
    /// Needed to conceal SetRoutedEvent in derived classes.
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TArgs"></typeparam>
    [ProcessNode]
    public abstract class RoutedEventListener<TControl, TArgs> : IDisposable
        where TControl : Interactive
        where TArgs : RoutedEventArgs
    {
        protected TControl? Input;
        protected RoutedEvent? Event;
        protected RoutingStrategies Strategies =
            RoutingStrategies.Direct | RoutingStrategies.Bubble;
        protected bool HandledEveventsToo = false;

        protected Func<TControl, TArgs, bool>? Handler;

        private readonly SerialDisposable _subscription = new();

        /// <summary>
        /// Sets the Interactive element to listen to.
        /// </summary>
        [Fragment(Order = PinOrder.Main)]
        public void SetInput(TControl? input)
        {
            if (input != Input)
            {
                Input = input;
                Resubscribe();
            }
        }

        /// <summary>
        /// Sets the callback to execute when the event occurs.
        /// The callback receives the sender (TControl) and the event args.
        /// Return true to mark the event as Handled.
        /// </summary>
        [Fragment(Order = PinOrder.Action)]
        public void SetHandler(Func<TControl, TArgs, bool>? handler)
        {
            Handler = handler;
        }

        /// <summary>
        /// Sets the routing strategy (Bubble, Tunnel, Direct).
        /// </summary>
        [Fragment]
        public void SetRoutingStrategies(
            [Pin(Visibility = PinVisibility.Optional)]
                RoutingStrategies strategies = RoutingStrategies.Direct | RoutingStrategies.Bubble
        )
        {
            if (strategies != Strategies)
            {
                Strategies = strategies;
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
            if (handledEventsToo != HandledEveventsToo)
            {
                HandledEveventsToo = handledEventsToo;
                Resubscribe();
            }
        }

        protected virtual void Resubscribe()
        {
            // Clear existing subscription
            _subscription.Disposable = Disposable.Empty;

            if (Input == null || Event == null)
            {
                return;
            }

            var target = Input;

            void HandlerAdapter(object? sender, RoutedEventArgs args)
            {
                var currentHandler = Handler;

                // Check if we have a handler and the args are of the expected type
                if (currentHandler != null && args is TArgs typedArgs)
                {
                    // Check if sender matches TControl
                    if (sender is TControl typedSender)
                    {
                        // Invoke the user callback
                        var handled = currentHandler(typedSender, typedArgs);

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
                Event,
                (EventHandler<RoutedEventArgs>)HandlerAdapter,
                Strategies,
                HandledEveventsToo
            );

            // Cleanup
            _subscription.Disposable = Disposable.Create(() =>
            {
                target.RemoveHandler(Event, (EventHandler<RoutedEventArgs>)HandlerAdapter);
            });
        }

        public virtual void Dispose()
        {
            _subscription.Dispose();
        }
    }

    /// <summary>
    /// Generic routed event listener. Allows to setup for specific event.
    /// </summary>
    /// <typeparam name="T">RoutedEventArgs</typeparam>
    [ProcessNode(Name = "RoutedEventListener")]
    public class RoutedEventListener<T> : RoutedEventListener<Interactive, T>, IDisposable
        where T : RoutedEventArgs
    {
        /// <summary>
        /// Sets the RoutedEvent to listen for.
        /// </summary>
        [Fragment(Order = PinOrder.Action)]
        public virtual void SetRoutedEvent(RoutedEvent? routedEvent)
        {
            if (routedEvent != Event)
            {
                Event = routedEvent;
                Resubscribe();
            }
        }
    }

    /// <summary>
    /// Generic routed event listener. Allows to setup for specific event by event name.
    /// </summary>
    /// <typeparam name="TArgs">RoutedEventArgs</typeparam>
    [ProcessNode(Name = "RoutedEventListener (Name)")]
    public class RoutedEventListenerName<TArgs> : RoutedEventListener<Interactive, TArgs>
        where TArgs : RoutedEventArgs
    {
        private string? _eventName;

        [Fragment(Order = PinOrder.Action)]
        public void SetRoutedEvent(string? eventName)
        {
            if (eventName != _eventName)
            {
                _eventName = eventName;
                var resolvedEvent = ResolveEvent(eventName);

                // Update base class state
                if (resolvedEvent != Event)
                {
                    Event = resolvedEvent;
                    Resubscribe();
                }
            }
        }

        private static RoutedEvent? ResolveEvent(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            // Simple parsing logic: Expects "Owner.Name"
            var parts = name!.Split('.');
            if (parts.Length < 2)
                return null;

            // The last part is the event name, the rest is the owner
            var evtName = parts[parts.Length - 1];
            var ownerName = parts[parts.Length - 2];

            // Scan registry
            // This could be optimized with a static dictionary cache if performance becomes an issue
            return RoutedEventRegistry
                .Instance.GetAllRegistered()
                .FirstOrDefault(e => e.Name == evtName && e.OwnerType.Name == ownerName);
        }
    }
}
