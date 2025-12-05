using Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>TreeViewItem</c> an item of TreeView.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/treeview-1">ListBox</see>
/// </summary>
[ProcessNode(Name = "TreeViewItem")]
public partial class TreeViewItemWrapper : TreeViewItemSpectralAdvancedWrapper<object?>
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

/// <inheritdoc cref="TreeViewItemWrapper"/>
[ProcessNode(Name = "TreeViewItem (Spectral)")]
public partial class TreeViewItemSpectralWrapper : TreeViewItemSpectralAdvancedWrapper<object?> { }

/// <inheritdoc cref="TreeViewItemWrapper"/>
[ProcessNode(Name = "TreeViewItem (Advanced Experimental)")]
public partial class TreeViewItemAdvancedWrapper<T> : TreeViewItemSpectralAdvancedWrapper<T>
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

/// <inheritdoc cref="TreeViewItemWrapper"/>
[ProcessNode(Name = "TreeViewItem (Spectral Advanced Experimental)")]
public partial class TreeViewItemSpectralAdvancedWrapper<T>
    : HeaderedItemsControlWrapperBase<TreeViewItem, T>
{
    #region Selection Properties

    protected ChannelTwoWayBinding<bool> _isExpandedBinding;
    protected ChannelTwoWayBinding<bool> _isSelectedBinding;

    public TreeViewItemSpectralAdvancedWrapper()
    {
        _isExpandedBinding = new ChannelTwoWayBinding<bool>(
            _output,
            TreeViewItem.IsExpandedProperty
        );
        _isSelectedBinding = new ChannelTwoWayBinding<bool>(
            _output,
            TreeViewItem.IsSelectedProperty
        );
    }

    /// <param name="isExpandedChannel">
    /// Whether this list item is currently expanded
    /// </param>
    public void SetIsExpandedChannel(
        [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isExpandedChannel
    ) => _isExpandedBinding.SetChannel(isExpandedChannel);

    /// <param name="isSelectedChannel">
    /// Whether this list item is currently selected
    /// </param>
    public void SetIsSelectedChannel(
        [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isSelectedChannel
    ) => _isSelectedBinding.SetChannel(isSelectedChannel);

    #endregion
}
