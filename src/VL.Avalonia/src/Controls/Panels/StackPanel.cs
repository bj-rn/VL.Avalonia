using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

// https://docs.avaloniaui.net/docs/reference/controls/stackpanel
// http://reference.avaloniaui.net/api/Avalonia.Controls/StackPanel/

[ProcessNode(Name = "StackPanel (Spectral)")]
public partial class StackPanelSpectralWrapper : ControlWrapperBase<StackPanel>
{
    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("StackPanel.OrientationProperty")]
    protected Optional<Orientation> _orientation;

    [ImplementProperty("StackPanel.SpacingProperty")]
    protected Optional<float> _spacing;

    [ImplementProperty("StackPanel.HorizontalAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<HorizontalAlignment> _horizontalAlignment;

    [ImplementProperty("StackPanel.VerticalAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<VerticalAlignment> _verticalAlignment;
}

[ProcessNode(Name = "StackPanel")]
public partial class StackPanelWrapper : StackPanelSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}
