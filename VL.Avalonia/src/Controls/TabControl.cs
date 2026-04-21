using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>TabControl</c> displays a tab strip and the content of the selected tab. It supports setting the tab strip's position, custom content templates, and alignment for content. TabControl automatically manages selection and container generation for tab items.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/tabcontrol">TabControl</see>
/// </summary>
[ProcessNode(Name = "TabControl (Spectral)")]
public partial class TabControlSpectralWrapper<T> : SelectingItemsControlNodeBase<TabControl, T>
{
    #region TabControl Properties

    /// <param name="tabStripPlacement">
    /// The position of the tab strip: Top (default), Bottom, Left, or Right.
    /// </param>
    [ImplementProperty("TabControl.TabStripPlacementProperty")]
    protected Optional<Dock> _tabStripPlacement;

    /// <param name="horizontalContentAlignment">
    /// Horizontal alignment of the content within the tab (default: Stretch).
    /// </param>
    [ImplementProperty(
        "TabControl.HorizontalContentAlignmentProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<HorizontalAlignment> _horizontalContentAlignment;

    /// <param name="verticalContentAlignment">
    /// Vertical alignment of the content within the tab (default: Stretch).
    /// </param>
    [ImplementProperty(
        "TabControl.VerticalContentAlignmentProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<VerticalAlignment> _verticalContentAlignment;

    /// <param name="contentTemplate">
    /// The default data template used to display the content of the selected tab. (Can be overridden per TabItem)
    /// </param>
    [ImplementProperty(
        "TabControl.ContentTemplateProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<IDataTemplate> _contentTemplate;

    #endregion
}

/// <inheritdoc cref="TabControlSpectralWrapper{T}"/>
[ProcessNode(Name = "TabControl")]
public partial class TabControlWrapper<T> : TabControlSpectralWrapper<T>
{
    /// <param name="items">
    /// The collection of items
    /// </param>
    [Fragment(Order = -10)]
    public override void SetItems(
        [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
            Spread<T> items
    )
    {
        base.SetItems(items);
    }
}
