using Dock.Model.Avalonia.Controls;
using Dock.Model.Core;
using VL.Avalonia.Attributes;
using VL.Avalonia.Dock.Controls.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Dock.Controls
{
    [ProcessNode(Name = "ProportionalDock")]
    public partial class ProportionalDockWrapper : DockBaseWrapper<ProportionalDock>
    {
        [ImplementProperty("ProportionalDock.OrientationProperty")]
        protected Optional<Orientation> _orientation;
    }

    [ProcessNode(Name = "ProportionalDockSplitter")]
    public partial class ProportionalDockSplitterWrapper : DockBaseWrapper<ProportionalDockSplitter>
    {

    }
}
