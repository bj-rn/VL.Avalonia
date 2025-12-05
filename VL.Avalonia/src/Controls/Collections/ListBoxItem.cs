using Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for ListBoxItemWrapper
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class ListBoxItemWrapperBase<T> : ContentControlWrapperBase<T>
    where T : ListBoxItem, new()
{
    #region Selection Properties
    protected ChannelTwoWayBinding<bool> _isSelectedBinding;

    public ListBoxItemWrapperBase()
    {
        _isSelectedBinding = new(_output, ListBoxItem.IsSelectedProperty);
    }

    /// <param name="isSelectedChannel">
    /// Whether this list item is currently selected
    /// </param>
    public void SetIsSelectedChannel(
        [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isSelectedChannel
    ) => _isSelectedBinding.SetChannel(isSelectedChannel);

    #endregion
}

[ProcessNode(Name = "ListBoxItem")]
/// <summary>
/// The <c>ListBoxItem</c> represents a selectable item within a ListBox control. It extends ContentControl to display content while adding selection functionality and appropriate visual states. Each ListBoxItem can hold any type of content and provides visual feedback when selected, pressed, or focused, making it the building block for list-based user interfaces.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/listboxitem">ListBoxItem</see>
/// </summary>
public partial class ListBoxItemWrapper : ListBoxItemWrapperBase<ListBoxItem> { }
