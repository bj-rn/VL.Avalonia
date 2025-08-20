using Dock.Model.Avalonia.Controls;
using Dock.Model.Core;
using VL.Avalonia.Dock.Controls.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Dock.Controls
{
    [ProcessNode(Name = "RootDock")]
    public partial class RootDockWrapper : DockBaseWrapper<RootDock>
    {
        private Optional<IDockable> _defaultDockable;
        public void SetDefaultDockable(Optional<IDockable> defaultDockable)
        {
            if (_defaultDockable != defaultDockable)
            {
                if (defaultDockable.HasValue)
                {
                    _output.SetValue(RootDock.DefaultDockableProperty, defaultDockable.Value);
                }
                else
                {
                    _output.ClearValue(RootDock.DefaultDockableProperty);
                }

                _defaultDockable = defaultDockable;
            }
        }
    }
}
