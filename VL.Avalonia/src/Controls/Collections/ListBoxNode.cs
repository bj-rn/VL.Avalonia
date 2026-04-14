using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="ListBox"/>
    [ProcessNode]
    public abstract partial class ListBoxNodeBase<T> : SelectingItemsControlNodeBase<ListBox, T>
    {
        [ImplementProperty(
            "ListBox.SelectionModeProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<SelectionMode> _selectionMode;

        [Fragment(Order = PinOrder.Secondary)]
        public override void SetSelectedItemChannel(IChannel<T> selectedItemChannel)
        {
            base.SetSelectedItemChannel(selectedItemChannel);
        }
    }

    /// <inheritdoc cref="ListBox"/>
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

    /// <inheritdoc cref="ListBox"/>
    [ProcessNode(Name = "ListBox (Spectral)")]
    public class ListBoxSpectralNode<T> : ListBoxNodeBase<T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ListBox"/>
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
