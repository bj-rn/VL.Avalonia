using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ComboBox</c> is a selection control that combines a text box with a drop-down list, allowing users to either select from a list of predefined options or enter their own value. It displays the selected item in a compact form and shows the full list of options when activated. The ComboBox is ideal for scenarios where screen space is limited but many options need to be available.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/combobox">ComboBox</see>
/// </summary>
[ProcessNode(Name = "ComboBox (Spectral)")]
public partial class ComboBoxSpectralWrapper<T> : SelectingItemsControlWrapperBase<ComboBox, T>
{
    #region Dropdown Behavior Properties

    protected ChannelTwoWayBinding<bool> _isDropDownOpenBinding;
    public ComboBoxSpectralWrapper() : base()
    {
        _isDropDownOpenBinding = new(_output, ComboBox.IsDropDownOpenProperty);
    }

    /// <param name="isDropDownOpenChannel">
    /// Whether the dropdown list is currently open and visible
    /// </param>
    public virtual void SetIsDropDownOpenChannel([Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isDropDownOpenChannel) =>
        _isDropDownOpenBinding.SetChannel(isDropDownOpenChannel);


    /// <param name="maxDropDownHeight">
    /// Maximum height of the dropdown list when opened
    /// </param>
    [ImplementProperty("ComboBox.MaxDropDownHeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _maxDropDownHeight;

    #endregion

    #region Selection Display Properties

    /// <param name="selectionBoxItemTemplate">
    /// Template used to display the selected item in the closed ComboBox
    /// </param>
    [ImplementProperty("ComboBox.SelectionBoxItemTemplateProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IDataTemplate> _selectionBoxItemTemplate;

    #endregion

    #region Placeholder Properties

    /// <param name="placeholderText">
    /// Text displayed when no item is selected
    /// </param>
    [ImplementProperty("ComboBox.PlaceholderTextProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _placeholderText;

    /// <param name="placeholderForeground">
    /// Brush used to render the placeholder text
    /// </param>
    [ImplementProperty("ComboBox.PlaceholderForegroundProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _placeholderForeground;

    #endregion

    #region Content Alignment Properties

    /// <param name="horizontalContentAlignment">
    /// Horizontal alignment of the selected item content within the ComboBox
    /// </param>
    [ImplementProperty("ComboBox.HorizontalContentAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<HorizontalAlignment> _horizontalContentAlignment;

    /// <param name="verticalContentAlignment">
    /// Vertical alignment of the selected item content within the ComboBox
    /// </param>
    [ImplementProperty("ComboBox.VerticalContentAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<VerticalAlignment> _verticalContentAlignment;

    #endregion

    #region Read-Only State Properties

    // Note: These DirectProperties are read-only and represent the current state
    // They are not included as they cannot be set directly by users
    // - SelectionBoxItem (DirectProperty) - The actual item displayed in the closed ComboBox

    #endregion

    #region Inherited Properties Note

    // ComboBox inherits all properties from SelectingItemsControl:
    // - SelectedValue, SelectedValueBinding - Value extraction from selected item
    // - AutoScrollToSelectedItem, WrapSelection, IsTextSearchEnabled - Selection behavior
    //
    // And from ItemsControl:
    // - ItemsSource - Collection of items to display
    // - ItemTemplate - Template for items in the dropdown
    // - DisplayMemberBinding - Simple text extraction from objects
    // - ItemsPanel - Layout panel for dropdown items
    // - ItemContainerTheme - Styling for ComboBoxItem containers
    //
    // And from Control:
    // - FontFamily, FontSize, Foreground, Background, Padding, Margin, etc.

    #endregion
}

[ProcessNode(Name = "ComboBox")]
public partial class ComboBoxWrapper<T> : ComboBoxSpectralWrapper<T>
{
    [Fragment(Order = -10)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T> items)
        => base.SetItems(items);
}
