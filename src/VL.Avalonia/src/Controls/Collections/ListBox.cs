using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ListBox</c> is a selection control that displays a list of items where users can select one or multiple items. It extends SelectingItemsControl with full multi-selection support, keyboard navigation, and scrolling capabilities. ListBox is ideal for presenting lists of data where user selection is required, supporting both single and multiple selection modes with rich keyboard and mouse interaction.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/listbox">ListBox</see>
/// </summary>
[ProcessNode(Name = "ListBox (Spectral)")]
public partial class ListBoxSpectralWrapper<T> : SelectingItemsControlWrapperBase<ListBox, T>
{
    [ImplementProperty("ListBox.SelectionModeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<SelectionMode> _selectionMode;

    #region Read-Only Properties Note

    // ListBox exposes several read-only DirectProperties:
    // - Scroll (DirectProperty) - Access to the internal scroll viewer
    // - SelectedItems (DirectProperty) - Collection of currently selected 
    // - Selection (DirectProperty) - The underlying selection model

    // These are not included as StyledProperties since they represent state
    // rather than configuration and cannot be set directly by users

    #endregion

    #region Key Methods Available

    // ListBox provides these key methods:
    // - SelectAll() - Selects all items when in multiple selection mode
    // - UnselectAll() - Clears all selections
    // - UpdateSelectionFromPointerEvent() - Internal method for handling mouse/touch selection

    #endregion
}
/// <inheritdoc path="ListBoxSpectralWrapper"/>
[ProcessNode(Name = "ListBox")]
public partial class ListBoxWrapper<T> : ListBoxSpectralWrapper<T>
{
    [Fragment(Order = -10)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T> items)
    {
        base.SetItems(items);
    }

    /// <summary>
    ///  SelectedItems (DirectProperty) - Collection of currently selected 
    /// </summary>
    public static Spread<T> SelectedItems<T>(ListBox input)
    {
        var selectedItems = input.SelectedItems;
        if (selectedItems == null || selectedItems.Count == 0)
            return Spread<T>.Empty;

        return selectedItems.Cast<T>().ToSpread();
    }
}
