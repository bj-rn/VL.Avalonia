using Avalonia.Controls;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "AutoCompleteBox (Spectral)")]
public partial class AutoCompleteBoxSpectralWrapper<T> : ControlNodeBase<AutoCompleteBox>
{
    #region Core Content Properties

    protected ChannelTwoWayBinding<string> _textBinding;
    protected ChannelTwoWayBinding<object?> _selectedItemBinding;
    protected ChannelSpreadToItemsSourceBinding<T> _itemsSourceBinding;

    public AutoCompleteBoxSpectralWrapper()
    {
        _textBinding = new ChannelTwoWayBinding<string>(_output, AutoCompleteBox.TextProperty);
        _selectedItemBinding = new ChannelTwoWayBinding<object?>(
            _output,
            AutoCompleteBox.SelectedItemProperty
        );
        _itemsSourceBinding = new ChannelSpreadToItemsSourceBinding<T>(
            _output,
            AutoCompleteBox.ItemsSourceProperty
        );
    }

    /// <param name="textChannel">
    /// The text content of the TextBox
    /// </param>
    [Fragment(Order = -10)]
    public void SetTextChannel(IChannel<string>? textChannel) =>
        _textBinding.SetChannel(textChannel);

    /// <param name="itemsSource">
    /// The collection of items that provides the data for the dropdown suggestions
    /// </param>
    protected Spread<T?> _items;

    [Fragment(Order = -5)]
    public void SetItems(Spread<T?> items) => _itemsSourceBinding.SetItems(items);

    /// <param name="selectedItem">
    /// The currently selected item from the dropdown
    /// </param>
    public void SetSelectedItemChannel(IChannel<T?>? itemChannel) =>
        _selectedItemBinding.SetChannel(itemChannel as IChannel<object?>);

    #endregion

    #region Filter and Search Properties

    /// <param name="filterMode">
    /// The filtering mode used to determine which items match the typed text
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.FilterModeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<AutoCompleteFilterMode> _filterMode;

    /// <param name="minimumPrefixLength">
    /// Minimum number of characters that must be typed before suggestions appear
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.MinimumPrefixLengthProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<int> _minimumPrefixLength;

    /// <param name="minimumPopulateDelay">
    /// Delay before showing suggestions after typing stops
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.MinimumPopulateDelayProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<TimeSpan> _minimumPopulateDelay;

    /// <param name="itemFilter">
    /// Custom filter function for advanced filtering scenarios
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.ItemFilterProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<AutoCompleteFilterPredicate<object>> _itemFilter;

    /// <param name="textFilter">
    /// String-based filter function for text filtering
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.TextFilterProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<AutoCompleteFilterPredicate<string>> _textFilter;

    #endregion

    #region Dropdown Behavior Properties

    /// <param name="isDropDownOpen">
    /// Whether the dropdown is currently open
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.IsDropDownOpenProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _isDropDownOpen;

    /// <param name="maxDropDownHeight">
    /// Maximum height of the dropdown when open
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.MaxDropDownHeightProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _maxDropDownHeight;

    /// <param name="isTextCompletionEnabled">
    /// Whether text completion is enabled (auto-completes text as you type)
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.IsTextCompletionEnabledProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _isTextCompletionEnabled;

    #endregion

    #region Display and Template Properties

    /// <param name="itemTemplate">
    /// Template used to display items in the dropdown
    /// </param>
    [ImplementProperty("AutoCompleteBox.ItemTemplateProperty")]
    protected Optional<IDataTemplate> _itemTemplate;

    ///// <param name="valueMemberBinding">
    ///// Binding used to extract display text from complex objects
    ///// </param>
    //[ImplementProperty("AutoCompleteBox.ValueMemberBindingProperty", PinVisibility = Model.PinVisibility.Optional)]
    //protected Optional<IBinding> _valueMemberBinding;

    /// <param name="watermark">
    /// Placeholder text shown when the text box is empty
    /// </param>
    [ImplementProperty("AutoCompleteBox.WatermarkProperty")]
    protected Optional<string> _watermark;

    #endregion

    #region Advanced Selection Properties

    /// <param name="itemSelector">
    /// Custom selector for how selected items modify the text
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.ItemSelectorProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<AutoCompleteSelector<object>> _itemSelector;

    /// <param name="textSelector">
    /// String-based selector for text modification
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.TextSelectorProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<AutoCompleteSelector<string>> _textSelector;

    #endregion

    #region Async Population Properties

    /// <param name="asyncPopulator">
    /// Async function for populating suggestions from external sources
    /// </param>
    [ImplementProperty(
        "AutoCompleteBox.AsyncPopulatorProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<Func<string, CancellationToken, Task<IEnumerable<object>>>> _asyncPopulator;

    #endregion
}

