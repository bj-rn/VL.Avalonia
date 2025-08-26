using Avalonia;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia
{
    /// <summary>
    /// Helper, that displays non-readonly properties of avalonia object
    /// </summary>
    [ProcessNode]
    public class PropertyInspector<T>
    {
        protected Optional<T> _input;
        protected string _output;

        /// <param name="input">Control to inspect</param>
        public void SetInput(Optional<T> input, out string output)
        {
            if (_input != input)
            {
                if (input.HasValue)
                {
                    var type = input.Value?.GetType();

                    if (type != null)
                    {
                        var properties = AvaloniaPropertyRegistry.Instance.GetRegistered(type);
                        _output = string.Join("\n", properties
                            .Where(x => !x.IsReadOnly)
                            .Select(x => x.Name)
                            .OrderBy(x => x));
                    }
                }

                _input = input;

            }

            output = _output;
        }
    }
}
