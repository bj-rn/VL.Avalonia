using Avalonia.Collections;
using Dock.Model.Avalonia.Core;
using Dock.Model.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Dock.Controls.Base
{
    [ProcessNode]
    public abstract class DockBaseWrapper<T> where T : DockBase, new()
    {
        protected readonly T _output = new T();
        public T Output => _output;

        protected Spread<IDockable> _visibleDockables;
        public void SetVisibleDocakbales(Spread<IDockable> visibleDockables)
        {
            if (_visibleDockables != visibleDockables)
            {
                if (visibleDockables != null)
                {
                    var list = new AvaloniaList<IDockable>();
                    foreach (var dockable in visibleDockables)
                    {
                        if (dockable != null)
                            list.Add(dockable);
                    }

                    _output.SetValue(DockBase.VisibleDockablesProperty, list);
                }
                else
                {
                    _output.ClearValue(DockBase.VisibleDockablesProperty);
                }

                _visibleDockables = visibleDockables;
            }
        }
    }
}
