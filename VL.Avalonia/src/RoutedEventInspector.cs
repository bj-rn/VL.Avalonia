using Avalonia.Interactivity;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia
{
    /// <summary>
    /// Inspects and lists all RoutedEvents (Direct, Bubble, Tunnel) available on an object.
    /// </summary>
    [ProcessNode]
    public class RoutedEventInspector<T>
    {
        protected Optional<T> _input;
        protected string _output = "";

        /// <summary>
        /// Analyzes the input object and returns a formatted list of all supported RoutedEvents.
        /// </summary>
        /// <param name="input">The control or object to inspect.</param>
        /// <param name="output">A newline-separated string list of events.</param>
        public void SetInput(Optional<T> input, out string output)
        {
            if (_input != input)
            {
                if (input.HasValue && input.Value != null)
                {
                    var allEvents = new List<RoutedEvent>();
                    var currentType = input.Value.GetType();

                    // RoutedEventRegistry only returns events for the specific type,
                    // so we must walk up the inheritance chain to find all inherited events.
                    while (currentType != null)
                    {
                        var registered = RoutedEventRegistry.Instance.GetRegistered(currentType);
                        if (registered != null)
                        {
                            allEvents.AddRange(registered);
                        }
                        currentType = currentType.BaseType;
                    }

                    // Format the output nicely
                    // Result example: "Button.Click [Bubble] (RoutedEventArgs)"
                    var lines = allEvents
                        .Distinct() // Safety check
                        .Select(e =>
                        {
                            var name = e.Name;
                            var owner = e.OwnerType.Name;
                            var args = e.EventArgsType.Name;
                            var strategy = e.RoutingStrategies.ToString();

                            return $"{owner}.{name} [{strategy}] ({args})";
                        })
                        .OrderBy(s => s);

                    _output = string.Join("\n", lines);
                }
                else
                {
                    _output = "";
                }

                _input = input;
            }

            output = _output;
        }
    }
}
