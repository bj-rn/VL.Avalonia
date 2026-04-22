using Avalonia.Interactivity;

namespace VL.Avalonia.Interactivity
{
    // --------------------/
    // WRNING NEEDS TESTING
    // TODO:
    // --------------------/

    /// <summary>
    /// Extension methods for <see cref="Interactive"/>.
    /// </summary>
    public static class InteractiveExtensions
    {
        /// <inheritdoc cref="Interactive.RaiseEvent(RoutedEventArgs)"/>
        public static void RaiseEvent(this Interactive interactive, RoutedEventArgs e)
        {
            if (interactive is not null && e is not null)
            {
                interactive.RaiseEvent(e);
            }
        }

        /// <inheritdoc cref="Interactive.AddHandler(RoutedEvent, Delegate, RoutingStrategies, bool)"/>
        public static void AddHandler(
            this Interactive interactive,
            RoutedEvent routedEvent,
            Delegate handler,
            RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
            bool handledEventsToo = false
        )
        {
            if (interactive is not null && routedEvent is not null && handler is not null)
            {
                interactive.AddHandler(routedEvent, handler, routes, handledEventsToo);
            }
        }

        /// <inheritdoc cref="Interactive.AddHandler{TEventArgs}(RoutedEvent{TEventArgs}, EventHandler{TEventArgs}?, RoutingStrategies, bool)"/>
        public static void AddHandler<TEventArgs>(
            this Interactive interactive,
            RoutedEvent<TEventArgs> routedEvent,
            EventHandler<TEventArgs>? handler,
            RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
            bool handledEventsToo = false
        )
            where TEventArgs : RoutedEventArgs
        {
            if (interactive is not null && routedEvent is not null && handler is not null)
            {
                interactive.AddHandler(routedEvent, handler, routes, handledEventsToo);
            }
        }

        /// <inheritdoc cref="Interactive.RemoveHandler(RoutedEvent, Delegate)"/>
        public static void RemoveHandler(
            this Interactive interactive,
            RoutedEvent routedEvent,
            Delegate handler
        )
        {
            if (interactive is not null && routedEvent is not null && handler is not null)
            {
                interactive.RemoveHandler(routedEvent, handler);
            }
        }

        /// <inheritdoc cref="Interactive.RemoveHandler{TEventArgs}(RoutedEvent{TEventArgs}, EventHandler{TEventArgs}?)"/>
        public static void RemoveHandler<TEventArgs>(
            this Interactive interactive,
            RoutedEvent<TEventArgs> routedEvent,
            EventHandler<TEventArgs>? handler
        )
            where TEventArgs : RoutedEventArgs
        {
            if (interactive is not null && routedEvent is not null && handler is not null)
            {
                interactive.RemoveHandler(routedEvent, handler);
            }
        }
    }
}
