using System.Reactive.Linq;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls.Collections
{
    /// <inheritdoc cref="TreeView"/>
    [ProcessNode]
    public abstract partial class TreeViewNodeBase<T> : ItemsControlNodeBase<TreeView, T>
    {
        /// <inheritdoc cref="TreeView.AutoScrollToSelectedItemProperty"/>
        [ImplementProperty(
            "TreeView.AutoScrollToSelectedItemProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<bool> _autoScrollToSelectedItem;

        /// <inheritdoc cref="TreeView.SelectionModeProperty"/>
        [ImplementProperty(
            "TreeView.SelectionModeProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<SelectionMode> _selectionMode;
    }

    /// <inheritdoc cref="TreeView"/>
    [ProcessNode(Name = "TreeView")]
    public class TreeViewNode<T> : TreeViewNodeBase<T>
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

    /// <inheritdoc cref="TreeView"/>
    [ProcessNode(Name = "TreeView (Spectral)")]
    public class TreeViewSpectralNode<T> : TreeViewNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeView"/>
    [ProcessNode(Name = "TreeView (Reactive)")]
    public class TreeViewNodeReactive<T> : TreeViewNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }

    [ProcessNode(Name = "SelectedItems (TreeView)")]
    public class TreeViewSelectedItemsHandler<T> : IDisposable
    {
        private TreeView? _input;
        private IDisposable? _subscription;
        private IReadOnlyList<T>? _output;

        public void SetInput(TreeView? input)
        {
            if (ReferenceEquals(_input, input))
                return;

            _subscription?.Dispose();
            _input = input;

            if (_input is not null)
            {
                // Create an observable stream from the SelectionChanged event
                _subscription = Observable
                    .FromEventPattern<SelectionChangedEventArgs>(
                        h => _input.SelectionChanged += h,
                        h => _input.SelectionChanged -= h
                    )
                    .Subscribe(_ => _output = null);
            }

            _output = null;
        }

        public IReadOnlyList<T> Output
        {
            get
            {
                if (_output == null)
                {
                    _output = _input?.SelectedItems?.OfType<T>().ToSpread() ?? Spread<T>.Empty;
                }

                return _output;
            }
        }

        public void Dispose() => _subscription?.Dispose();
    }
}
