using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ItemsControl</c> is a base control for displaying collections of data items. It provides the foundation for controls like ListBox, ComboBox, Menu, and TreeView by managing item templates, containers, and data binding. The control can display items from any IEnumerable source and provides extensive customization through templates and styling.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/itemscontrol">ItemsControl</see>
/// </summary>
/// <summary>
[ProcessNode(Name = "ItemsControl (Spectral)")]
public partial class ItemsControlSpectralWrapper<T> : ItemsControlWrapperBase<ItemsControl, T>
{
}

/// <inheritdoc cref="ItemsControlSpectralWrapper{T}"/>
[ProcessNode(Name = "ItemsControl")]
public partial class ItemsControlWrapper<T> : ItemsControlSpectralWrapper<T>
{
    /// <param name="items">
    /// The collection of items
    /// </param>
    [Fragment(Order = -10)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T> items) =>
         _itemsSourceBinding.SetItems(items);
}
