using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

// reference: https://docs.avaloniaui.net/docs/reference/controls/dockpanel

[ProcessNode(Name = "DockPanel (Spectral)")]
public partial class DockPanelSpectralWrapper : ControlWrapperBase<DockPanel>
{
    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("DockPanel.HorizontalAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<HorizontalAlignment> _horizontalAlignment;

    [ImplementProperty("DockPanel.VerticalAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<VerticalAlignment> _verticalAlignment;
}

[ProcessNode(Name = "DockPanel")]
public partial class DockPanelWrapper : DockPanelSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}

[ProcessNode(Name = "Dock (DockPanel)")]
public partial class DockPanelDock : AttachedPropertyBase
{
    private Optional<Dock> _dock;
    public void SetDock(Optional<Dock> dock)
    {
        if (_dock != dock)
        {
            _dock = dock;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_dock.HasValue)
        {
            DockPanel.SetDock(_input.Value, _dock.Value);
        }
        else
        {
            _input.Value.ClearValue(DockPanel.DockProperty);
        }
    }
}