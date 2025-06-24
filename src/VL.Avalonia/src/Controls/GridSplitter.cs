using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "GridSplitter")]
public partial class GridSplitterWrapper : ControlWrapperBase<GridSplitter>
{
    [ImplementProperty("GridSplitter.BackgroundProperty")]
    private Optional<IBrush> _background;

    [ImplementProperty("GridSplitter.ResizeDirectionProperty")]
    private Optional<GridResizeDirection> _resizeDirection;
}
