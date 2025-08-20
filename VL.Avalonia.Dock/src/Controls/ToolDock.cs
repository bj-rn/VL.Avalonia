using Dock.Model.Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Dock.Controls.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Dock.Controls
{
    [ProcessNode(Name = "ToolDock")]
    public partial class ToolDockWrapper : DockBaseWrapper<ToolDock>
    {
    }

    [ProcessNode(Name = "Tool")]
    public partial class ToolWrapper : DockableBaseWrapper<Tool>
    {
        [ImplementProperty("Tool.ContentProperty")]
        protected Optional<object> _content;
    }
}
