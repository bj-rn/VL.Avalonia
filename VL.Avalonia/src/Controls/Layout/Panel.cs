using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>Panel</c> is a base class for all controls that can contain multiple child elements. It provides a flexible container for arranging, measuring, and rendering groups of controls. Common panel types include StackPanel, Grid, DockPanel, and Canvas. Panels are fundamental to Avalonia's layout system, supporting custom arrangement and backgrounds.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/panel">Panel</see>
/// </summary>
[ProcessNode(Name = "Panel (Spectral)")]
public partial class PanelSpectralWrapper : PanelWrapperBase<Panel> { }

/// <inheritdoc cref="PanelSpectralWrapper"/>
[ProcessNode(Name = "Panel")]
public partial class PanelWrapper : PanelSpectralWrapper
{
    /// <inheritdoc/>
    [Fragment(Order = -10)]
    public override void SetChildren(
        [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
            Spread<Control> children
    ) => base.SetChildren(children);
}
