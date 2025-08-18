using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>MenuItem</c> is a selectable item in a menu, supporting headers, icons, gestures, commands, submenus, toggle/check states, and grouping (radio).
/// MenuItem is highly flexible and can represent a clickable action, a parent for a submenu, a separator, or a toggleable/radio option.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/menuitem">MenuItem</see>
/// </summary>
[ProcessNode(Name = "MenuItem (Spectral)")]
public partial class MenuItemSpectralWrapper : MenuItemSpectralWrapper<object?>
{
}

/// <inheritdoc cref="MenuItemSpectralWrapper"/>
[ProcessNode(Name = "MenuItem")]
public partial class MenuItemWrapper : MenuItemSpectralWrapper
{
    /// <param name="items">
    /// The collection of items
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<object?> items)
       => base.SetItems(items);
}

/// <inheritdoc cref="MenuItemSpectralWrapper"/>
[ProcessNode(Name = "MenuItem (Spectral Advanced Experimental)")]
public partial class MenuItemSpectralWrapper<T> : MenuItemWrapperBase<T>
{
}

/// <inheritdoc cref="MenuItemSpectralWrapper"/>
[ProcessNode(Name = "MenuItem (Advanced Experimental)")]
public partial class MenuItemWrapper<T> : MenuItemSpectralWrapper<T>
{
    /// <param name="items">
    /// The collection of items
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T> items)
       => base.SetItems(items);
}








