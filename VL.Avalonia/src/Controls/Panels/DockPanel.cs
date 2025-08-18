using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>DockPanel</c> arranges its children at the top, bottom, left, right, or center of the panel. Each child can be docked to a specified edge using the Dock attached property. Optionally, the last child can fill the remaining space, and spacing between child elements can be set horizontally and vertically.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/dockpanel">DockPanel</see>
/// </summary>
[ProcessNode(Name = "DockPanel (Spectral)")]
public partial class DockPanelSpectralWrapper : PanelWrapperBase<DockPanel>
{
    #region Layout Properties

    /// <param name="lastChildFill">
    /// Whether the last child fills the remaining space (default: true).
    /// </param>
    [ImplementProperty("DockPanel.LastChildFillProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _lastChildFill;

    /*
     * NOT IMPLEMENTED ON 11.2.1
     * 
    /// <param name="horizontalSpacing">
    /// The horizontal spacing (in device-independent pixels) between children.
    /// </param>
    [ImplementProperty("DockPanel.HorizontalSpacingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _horizontalSpacing;

    /// <param name="verticalSpacing">
    /// The vertical spacing (in device-independent pixels) between children.
    /// </param>
    [ImplementProperty("DockPanel.VerticalSpacingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _verticalSpacing;
    */
    #endregion
}


/// <inheritdoc cref="DockPanelSpectralWrapper"/>
[ProcessNode(Name = "DockPanel")]
public partial class DockPanelWrapper : DockPanelSpectralWrapper
{
    /// <inheritdoc cref="SetChildren(Spread{Control})"/>
    [Fragment(Order = -10)]
    public override void SetChildren([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<Control> children) =>
        base.SetChildren(children);
}

/// <summary>
/// The edge (Left, Right, Top, Bottom) to which the child is docked.
/// Attached Property
/// </summary>
[ProcessNode(Name = "Dock (DockPanel)")]
public partial class DockPanelDock : AttachedPropertyBase
{
    private Optional<Dock> _dock;
    /// <param name="dock">
    /// (Attached) The edge (Left, Right, Top, Bottom) to which the child is docked.
    /// </param>
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
        if (_input.HasNoValue)
        {
            return;
        }

        if (_dock.HasValue)
        {
            DockPanel.SetDock(_input.Value, _dock.Value);
        }
        else
        {
            _input.Value?.ClearValue(DockPanel.DockProperty);
        }
    }
}