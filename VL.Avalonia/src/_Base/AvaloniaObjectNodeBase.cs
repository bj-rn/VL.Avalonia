using Avalonia;
using VL.Core.Import;

namespace VL.Avalonia
{
    /// <summary>
    /// Base wrapper for <see cref="AvaloniaObject"/>
    /// </summary>
    [ProcessNode]
    public abstract class AvaloniaObjectNodeBase<T> : IDisposable
        where T : AvaloniaObject, new()
    {
        protected readonly T _output = new();
        public T Output => _output;

        public virtual void Dispose()
        {
            // Disposing base
        }
    }
}
