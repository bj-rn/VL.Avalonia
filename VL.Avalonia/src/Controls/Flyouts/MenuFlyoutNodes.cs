using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Ungeneric wrapper for <see cref="MenuFlyout"/>
    /// </summary>
    [ProcessNode(Name = "MenuFlyout")]
    public class MenuFlyoutNode : MenuFlyoutNodeBase<MenuFlyout, object>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuFlyoutNode"/>
    [ProcessNode(Name = "MenuFlyout (Spectral)")]
    public class MenuFlyoutSpectralNode : MenuFlyoutNodeBase<MenuFlyout, object>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="MenuFlyout"/>
    /// </summary>
    [ProcessNode(Name = "MenuFlyout (Advanced)")]
    public class MenuFlyoutNode<T> : MenuFlyoutNodeBase<MenuFlyout, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuFlyoutNode{T}"/>
    [ProcessNode(Name = "MenuFlyout (Advanced Spectral)")]
    public class MenuFlyoutSpectralNode<T> : MenuFlyoutNodeBase<MenuFlyout, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuFlyoutNode{T}"/>
    [ProcessNode(Name = "MenuFlyout (Advanced Reactive)")]
    public class MenuFlyoutReactiveNode<T> : MenuFlyoutNodeBase<MenuFlyout, T>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
