using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ListBox"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ListBoxNodeBase<T> : SelectingItemsControlNodeBase<ListBox, T>
    {
        /// <summary>Sets the selection mode.</summary>
        [ImplementProperty(
            typeof(ListBox),
            nameof(ListBox.SelectionModeProperty),
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<SelectionMode> _selectionMode;
    }

    /// <summary>
    /// Wrapper for <see cref="ListBox"/>
    /// </summary>
    [ProcessNode(Name = "ListBox")]
    public class ListBoxNode<T> : ListBoxNodeBase<T>
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

    /// <inheritdoc cref="ListBoxNode{T}"/>
    [ProcessNode(Name = "ListBox (Spectral)")]
    public class ListBoxSpectralNode<T> : ListBoxNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ListBoxNode{T}"/>
    [ProcessNode(Name = "ListBox (Reactive)")]
    public class ListBoxReactiveNode<T> : ListBoxNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
