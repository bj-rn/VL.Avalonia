using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// Currently works only for <c>Button</c>
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode(Name = "MenuFlyout (Spectral)")]
public partial class MenuFlyoutSpectralWrapper : FlyoutWrapperBase<MenuFlyout>
{
    protected IChannel<Spread<object?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<
        Spread<object?>
    >();
    protected Spread<object?> _items;

    [Fragment(Order = -10)]
    public virtual void SetItems(Spread<object?> items)
    {
        if (_items != items)
        {
            _items = items;

            _itemsChannel.OnNext(items);
        }
    }

    public MenuFlyoutSpectralWrapper()
    {
        _output.Bind(MenuFlyout.ItemsSourceProperty, _itemsChannel);
    }
}

[ProcessNode(Name = "MenuFlyout")]
public partial class MenuFlyoutWrapper : MenuFlyoutSpectralWrapper
{
    public MenuFlyoutWrapper()
        : base() { }

    [Fragment(Order = -5)]
    public override void SetItems(
        [Pin(PinGroupKind = VL.Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
            Spread<object?> items
    ) => base.SetItems(items);
}
