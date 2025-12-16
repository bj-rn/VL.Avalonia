using Avalonia.Interactivity;
using VL.Core.Import;

namespace VL.Avalonia.Interactivity
{
    /// <summary>
    /// Listens to a RoutedEvent defined by its name (e.g., "Button.Click").
    /// </summary>
    [ProcessNode(
        Name = "RoutedEventListener (ByName)",
        FragmentSelection = FragmentSelection.Explicit
    )]
    public class RoutedEventListenerByName<TArgs> : RoutedEventListener<TArgs>
        where TArgs : RoutedEventArgs
    {
        private string? _eventName;

        [Fragment]
        public RoutedEventListenerByName()
            : base() { }

        /// <summary>
        /// Sets the RoutedEvent name to listen for.
        /// Format: "OwnerType.EventName" (e.g. "Button.Click" or "InputElement.PointerPressed")
        /// </summary>
        [Fragment(Order = PinOrder.Action)]
        public void SetRoutedEvent(string? eventName)
        {
            if (eventName != _eventName)
            {
                _eventName = eventName;
                var resolvedEvent = ResolveEvent(eventName);

                // Update base class state
                if (resolvedEvent != _routedEvent)
                {
                    _routedEvent = resolvedEvent;
                    Resubscribe();
                }
            }
        }

        /// <summary>
        /// Hides the direct RoutedEvent input from the VL node since we use string name.
        /// </summary>
        [Fragment(IsHidden = true)]
        public new void SetRoutedEvent(RoutedEvent? routedEvent)
        {
            base.SetRoutedEvent(routedEvent);
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
