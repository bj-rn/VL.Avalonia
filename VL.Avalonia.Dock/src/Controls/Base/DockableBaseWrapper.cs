using Dock.Model.Avalonia.Core;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Dock.Controls.Base
{
    [ProcessNode]
    public abstract partial class DockableBaseWrapper<T> where T : DockableBase, new()
    {
        protected readonly T _output = new T();
        public T Output => _output;

        [ImplementProperty("DockableBase.IdProperty")]
        protected Optional<string> _id;

        [ImplementProperty("DockableBase.TitleProperty")]
        protected Optional<string> _title;
    }
}
