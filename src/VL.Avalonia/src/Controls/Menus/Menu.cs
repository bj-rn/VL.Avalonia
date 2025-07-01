using Avalonia.Controls;
using VL.Avalonia.Controls.Menus;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>Menu</c> control is a top-level menu bar (such as a main window menu) in Avalonia. It hosts menu items and handles open/close state, selection, access keys, and interaction with the access key system. Menu is the root of the menu hierarchy and supports horizontal layout by default.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/menu">Menu</see>
/// </summary>
[ProcessNode(Name = "Menu (Spectral)")]
public partial class MenuSpectralWrapper : MenuSpectralAdvancedWrapper<object?>
{
}

/// <inheritdoc cref="MenuSpectralWrapper"/>
[ProcessNode(Name = "Menu")]
public partial class MenuWrapper : MenuSpectralWrapper
{
    /// <param name="items">
    /// The collection of items
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<object?> items)
    {
        base.SetItems(items);
    }
}

/// <inheritdoc cref="MenuSpectralWrapper"/>
[ProcessNode(Name = "Menu (Spectral Advanced)")]
public partial class MenuSpectralAdvancedWrapper<T> : MenuBaseWrapper<Menu, T>
{
}

/// <inheritdoc cref="MenuSpectralWrapper"/>
[ProcessNode(Name = "Menu (Advanced)")]
public partial class MenuAdvancedWrapper<T> : MenuSpectralAdvancedWrapper<T>
{
    [Fragment(Order = PinOrder.Main)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T> items)
    {
        base.SetItems(items);
    }
}

