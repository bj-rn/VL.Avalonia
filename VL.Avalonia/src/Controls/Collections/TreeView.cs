using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>TreeView</c> control can present hierarchical data and allows item selection. The items are templated so you can customise how they are displayed.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/treeview-1">ListBox</see>
/// </summary>
[ProcessNode(Name = "TreeView (Spectral)")]
public partial class TreeViewSpectralWrapper : TreeViewSpectralAdvancedWrapper<object?> { }

/// <inheritdoc cref="TreeViewSpectralWrapper"/>
[ProcessNode(Name = "TreeView")]
public partial class TreeViewWrapper : TreeViewAdvancedWrapper<object?>
{
    [Fragment(Order = -10)]
    public override void SetItems(
        [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
            Spread<object?> items
    )
    {
        base.SetItems(items);
    }
}

/// <inheritdoc cref="TreeViewSpectralWrapper"/>
[ProcessNode(Name = "TreeView (Spectral Advanced)")]
public partial class TreeViewSpectralAdvancedWrapper<T> : ItemsControlWrapperBase<TreeView, T>
{
    /// <param name="autoScrollToSelectedItem">
    /// Whether to automatically scroll to newly selected items
    /// </param>
    [ImplementProperty(
        "TreeView.AutoScrollToSelectedItemProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _autoScrollToSelectedItem;

    /// <param name="selectionMode">
    /// Sets the selection mode.
    /// </param>
    [ImplementProperty(
        "TreeView.SelectionModeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<SelectionMode> _selectionMode;
}

/// <inheritdoc cref="TreeViewSpectralWrapper"/>
[ProcessNode(Name = "TreeView (Advanced Experimental)")]
public partial class TreeViewAdvancedWrapper<T> : TreeViewSpectralAdvancedWrapper<T>
{
    [Fragment(Order = -10)]
    public override void SetItems(
        [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
            Spread<T> items
    )
    {
        base.SetItems(items);
    }
}
