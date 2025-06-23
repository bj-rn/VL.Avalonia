using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls.Base;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

// reference: https://docs.avaloniaui.net/docs/reference/controls/dockpanel

[ProcessNode(Name = "DockPanel (Spectral)")]
public partial class DockPanelSpectralWrapper
{
    [ImplementOutput]
    protected readonly DockPanel _output = new DockPanel();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("Control.NameProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;

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