using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>StackPanel</c> arranges its children in a single line, either horizontally or vertically, with optional spacing between them.
/// It is a fundamental layout container, ideal for aligning controls in a stack.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/stackpanel">StackPanel</see>
/// </summary>
[ProcessNode(Name = "StackPanel (Spectral)")]
public partial class StackPanelSpectralWrapper : PanelWrapperBase<StackPanel>
{
    /// <param name="orientation">
    /// The orientation in which child controls will be arranged (Vertical or Horizontal). Default is Vertical.
    /// </param>
    [ImplementProperty("StackPanel.OrientationProperty")]
    protected Optional<Orientation> _orientation;

    /// <param name="spacing">
    /// The amount of space (in device-independent pixels) to place between stacked children.
    /// </param>
    [ImplementProperty("StackPanel.SpacingProperty")]
    protected Optional<float> _spacing;
}


/// <inheritdoc cref="StackPanelSpectralWrapper"/>
[ProcessNode(Name = "StackPanel")]
public partial class StackPanelWrapper : StackPanelSpectralWrapper
{
    /// <inheritdoc cref="SetChildren(Spread{Control})"/>
    [Fragment(Order = -10)]
    public override void SetChildren([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<Control> children) =>
        base.SetChildren(children);
}
