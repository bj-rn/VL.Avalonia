using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>SelectingItemsControl</c> is a base class for ItemsControls that maintain a selection state. It provides the foundation for controls like ListBox, ComboBox, and TabControl by managing single or multiple item selection. The control handles selection logic, keyboard navigation, and automatically updates visual states of selected items.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/selecting-items-control">SelectingItemsControl</see>
/// </summary>
/// <summary>
[ProcessNode]
public abstract partial class SelectingItemsControlWrapperBase<TControl, TValue> : ItemsControlWrapperBase<TControl, TValue> where TControl : SelectingItemsControl, new()
{
    #region Core Selection Properties

    protected ChannelTwoWayBinding<TValue?, object?> _selectedValueBinding;
    public SelectingItemsControlWrapperBase()
    {
        _selectedValueBinding = new(_output, SelectingItemsControl.SelectedItemProperty, (x) => (object?)x, (x) => (TValue?)x);
    }

    /// <param name="selectedValue">
    /// The value of the selected item, obtained using SelectedValueBinding
    /// </param>
    public virtual void SetSelectedValueChannel(IChannel<TValue?> selectedValueChannel) =>
        _selectedValueBinding.SetChannel(selectedValueChannel);

    #endregion

    #region Selection Behavior Properties

    /// <param name="autoScrollToSelectedItem">
    /// Whether to automatically scroll to newly selected items
    /// </param>
    [ImplementProperty("SelectingItemsControl.AutoScrollToSelectedItemProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _autoScrollToSelectedItem;

    /// <param name="wrapSelection">
    /// Whether to wrap around when the first or last item is reached during navigation
    /// </param>
    [ImplementProperty("SelectingItemsControl.WrapSelectionProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _wrapSelection;

    /// <param name="isTextSearchEnabled">
    /// Whether users can jump to items by typing the first few characters
    /// </param>
    [ImplementProperty("SelectingItemsControl.IsTextSearchEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isTextSearchEnabled;

    #endregion

    #region Selection State Properties (Read-Only)

    // Note: These DirectProperties are read-only and represent the current selection state
    // They are not included as they cannot be set directly by users
    // - SelectedIndex (DirectProperty) - Index of the currently selected item
    // - SelectedItem (DirectProperty) - The currently selected item object
    // - SelectedItems (DirectProperty) - Collection of selected items (for multi-select)
    // - Selection (DirectProperty) - The underlying selection model

    #endregion
}
