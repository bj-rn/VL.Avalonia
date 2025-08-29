using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>WrapPanel</c> arranges its children in a sequence, wrapping them to a new line (or column) when there is not enough space. It supports optional fixed item width/height, customizable spacing between items and lines, and alignment for items within each line. WrapPanel is ideal for flowing layouts like tag clouds, toolbars, or galleries.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/wrappanel">WrapPanel</see>
/// </summary>
[ProcessNode(Name = "WrapPanel (Spectral)")]
public partial class WrapPanelSpectralWrapper : PanelWrapperBase<WrapPanel>
{
    #region Layout Properties

    /* NOTIMPLEMENTED ON 11.2.1
    /// <param name="itemSpacing">
    /// The spacing (in device-independent pixels) between individual items in the panel.
    /// </param>
    [ImplementProperty("WrapPanel.ItemSpacingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _itemSpacing;

    /// <param name="lineSpacing">
    /// The spacing (in device-independent pixels) between lines (or columns, for vertical orientation).
    /// </param>
    [ImplementProperty("WrapPanel.LineSpacingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _lineSpacing;
    */

    /// <param name="orientation">
    /// The primary direction items are laid out (Horizontal or Vertical; default is Horizontal).
    /// </param>
    [ImplementProperty("WrapPanel.OrientationProperty")]
    protected Optional<Orientation> _orientation;

    /* NOTIMPLEMENTED ON 11.2.1
    /// <param name="itemsAlignment">
    /// Alignment of items within each line (Start, Center, End).
    /// </param>
    [ImplementProperty("WrapPanel.ItemsAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<WrapPanelItemsAlignment> _itemsAlignment;
    */

    /// <param name="itemWidth">
    /// Fixed width for all items in the panel (NaN for automatic sizing).
    /// </param>
    [ImplementProperty("WrapPanel.ItemWidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _itemWidth;

    /// <param name="itemHeight">
    /// Fixed height for all items in the panel (NaN for automatic sizing).
    /// </param>
    [ImplementProperty("WrapPanel.ItemHeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _itemHeight;

    #endregion
}

[ProcessNode(Name = "WrapPanel")]
public partial class WrapPanelWrapper : WrapPanelSpectralWrapper
{
    /// <inheritdoc cref="SetChildren(Spread{Control})"/>
    [Fragment(Order = -10)]
    public override void SetChildren([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<Control> children) =>
        base.SetChildren(children);
}


