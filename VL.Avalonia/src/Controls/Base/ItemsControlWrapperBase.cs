using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ItemsControl</c> is a base control for displaying collections of data items. It provides the foundation for controls like ListBox, ComboBox, Menu, and TreeView by managing item templates, containers, and data binding. The control can display items from any IEnumerable source and provides extensive customization through templates and styling.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/itemscontrol">ItemsControl</see>
/// </summary>
[ProcessNode]
public abstract partial class ItemsControlWrapperBase<TControl, TValue>
    : ControlWrapperBase<TControl>
    where TControl : ItemsControl, new()
{
    #region Core Data Properties

    protected ChannelSpreadToItemsSourceBinding<TValue> _itemsSourceBinding;

    protected ItemsControlWrapperBase()
    {
        _itemsSourceBinding = new ChannelSpreadToItemsSourceBinding<TValue>(
            _output,
            ItemsControl.ItemsSourceProperty
        );
    }

    /// <param name="items">
    /// The collection of items
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public virtual void SetItems(Spread<TValue> items) => _itemsSourceBinding.SetItems(items);

    [Fragment(Order = PinOrder.Main)]
    public void SetItemsChannel(
        [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<Spread<TValue>> itemsChannel
    ) => _itemsSourceBinding.SetChannel(itemsChannel);

    /// <param name="itemTemplate">
    /// The template used to display each item in the collection
    /// </param>
    [ImplementProperty(
        "ItemsControl.ItemTemplateProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<IDataTemplate> _itemTemplate;

    /*  TODO: BREAKS CODEGEN
    /// <param name="displayMemberBinding">
    /// Binding used to extract display text from complex objects (alternative to ItemTemplate)
    /// </param>
    // [ImplementProperty("ItemsControl.DisplayMemberBindingProperty", PinVisibility = Model.PinVisibility.Optional)]
    // protected Optional<Data.IBinding> _displayMemberBinding;
    */

    #endregion

    #region Layout and Presentation Properties

    /// <param name="itemsPanel">
    /// The panel template used to arrange the items (e.g., StackPanel, WrapPanel, Grid)
    /// </param>
    [ImplementProperty(
        "ItemsControl.ItemsPanelProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<ITemplate<Panel>> _itemsPanel;

    /// <param name="itemContainerTheme">
    /// The theme applied to the container element generated for each item
    /// </param>
    [ImplementProperty(
        "ItemsControl.ItemContainerThemeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<ControlTheme> _itemContainerTheme;

    #endregion

    #region Read-Only State Properties

    // Note: These DirectProperties are read-only and represent the current state
    // They are not included as they cannot be set directly by users
    // - ItemCount (DirectProperty) - Number of items currently being displayed
    public static int ItemCount(ItemsControl input) => input.Items.Count;

    #endregion
}
