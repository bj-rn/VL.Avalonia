using Avalonia;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia
{
    [ProcessNode(Name = "TryGetResource (Experimental)")]
    public class TryGetResource<T>
    {
        protected T _output;
        public T Output => _output;
        public bool IsFound { get; protected set; }

        protected Optional<string> _key;

        public void SetKey(Optional<string> key, bool refresh = false)
        {
            if (_key != key || refresh)
            {
                if (key.HasValue)
                {
                    if (
                        Application.Current?.TryGetResource(
                            key.Value,
                            Application.Current.ActualThemeVariant,
                            out var resource
                        ) ?? false
                    )
                    {
                        try
                        {
                            _output = (T)resource!;
                            IsFound = true;
                        }
                        catch
                        {
                            IsFound = false;
                            _output = default;
                        }
                    }
                    else
                    {
                        IsFound = false;
                        _output = default;
                    }
                }

                _key = key;
            }
        }
    }
}
