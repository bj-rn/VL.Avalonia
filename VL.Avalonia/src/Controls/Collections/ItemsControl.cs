using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="ItemsControl"/>
    [ProcessNode(Name = "ItemsControl")]
    public partial class ItemsControlNode<T> : ItemsControlNodeBase<ItemsControl, T>
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

    /// <inheritdoc cref="ItemsControl"/>
    [ProcessNode(Name = "ItemsControl (Spectral)")]
    public partial class ItemControlSpectralNode<T> : ItemsControlNodeBase<ItemsControl, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ItemsControl"/>
    [ProcessNode(Name = "ItemsControl (Reactive)")]
    public partial class ItemsControlNodeReactive<T> : ItemsControlNodeBase<ItemsControl, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
