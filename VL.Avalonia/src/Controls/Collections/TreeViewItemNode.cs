using Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="TreeViewItem"/>
    [ProcessNode]
    public abstract class TreeViewItemNodeBase<T>
        : HeaderedItemsControlBaseNode<TreeViewItem, T>,
            IDisposable
    {
        protected TwoWayBinding<bool> IsExpandedBinding;
        protected TwoWayBinding<bool> IsSelectedBinding;

        public TreeViewItemNodeBase()
        {
            IsExpandedBinding = new(_output, TreeViewItem.IsExpandedProperty);
            IsSelectedBinding = new(_output, TreeViewItem.IsSelectedProperty);
        }

        /// <inheritdoc cref="TreeViewItem.IsExpandedProperty"/>
        public void SetIsExpandedChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<bool> isExpandedChannel
        )
        {
            IsExpandedBinding.Bind(isExpandedChannel);
        }

        /// <inheritdoc cref="TreeViewItem.IsSelectedProperty"/>
        public void SetIsSelectedChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<bool> isSelectedChannel
        )
        {
            IsSelectedBinding.Bind(isSelectedChannel);
        }

        public override void Dispose()
        {
            IsExpandedBinding?.Dispose();
            IsSelectedBinding?.Dispose();

            base.Dispose();
        }
    }

    [ProcessNode(Name = "TreeViewItem")]
    public class TreeViewItemNode<T> : TreeViewItemNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T> items
        )
        {
            base.SetItems(items);
        }
    }

    [ProcessNode(Name = "TreeViewItem (Spectral)")]
    public class TreeViewItemSpectralNode<T> : TreeViewItemNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T> items)
        {
            base.SetItems(items);
        }
    }

    [ProcessNode(Name = "TreeViewItem (Reactive)")]
    public class TreeViewItemReactiveNode<T> : TreeViewItemNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
