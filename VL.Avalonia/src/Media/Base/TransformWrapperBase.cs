using Avalonia.Media;
using VL.Core.Import;

namespace VL.Avalonia.Media.Base
{
    [ProcessNode]
    [Obsolete("No reason to port transforms if we can use them directly")]
    public abstract class TransformWrapperBase<T>
        where T : Transform, new()
    {
        protected readonly T _output = new();
        public T Output => _output;
    }
}
