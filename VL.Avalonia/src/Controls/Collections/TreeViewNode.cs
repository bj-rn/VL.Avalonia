using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TreeView"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TreeViewNodeBase<TControl, TValue>
        : ItemsControlNodeBase<TControl, TValue>,
            IDisposable
        where TControl : TreeView, new()
    {
        private readonly TwoWayBinding<TValue?, object?> _selectedItemBinding;

        public TreeViewNodeBase()
        {
            _selectedItemBinding = new TwoWayBinding<TValue?, object?>(
                _output,
                TreeView.SelectedItemProperty,
                x => (object?)x,
                y => (TValue?)y
            );
        }

        /// <param name="selectedItemChannel">Binds <see cref="TreeView.SelectedItem"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetSelectedItemChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<TValue?> selectedItemChannel
        ) => _selectedItemBinding.Bind(selectedItemChannel);

        /// <summary>Sets a value indicating whether to automatically scroll to newly selected items.</summary>
        [ImplementProperty(
            typeof(TreeView),
            nameof(TreeView.AutoScrollToSelectedItemProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _autoScrollToSelectedItem;

        /// <summary>Sets the selection mode.</summary>
        [ImplementProperty(
            typeof(TreeView),
            nameof(TreeView.SelectionModeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SelectionMode> _selectionMode;

        public override void Dispose()
        {
            _selectedItemBinding.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="TreeView"/>
    /// </summary>
    [ProcessNode(Name = "TreeView")]
    public class TreeViewNode<T> : TreeViewNodeBase<TreeView, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewNode{T}"/>
    [ProcessNode(Name = "TreeView (Spectral)")]
    public class TreeViewSpectralNode<T> : TreeViewNodeBase<TreeView, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewNode{T}"/>
    [ProcessNode(Name = "TreeView (Reactive)")]
    public class TreeViewNodeReactive<T> : TreeViewNodeBase<TreeView, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
