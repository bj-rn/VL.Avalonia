using Avalonia.Controls;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>MenuBase</c> is an abstract base class for menu controls (such as Menu and ContextMenu) in Avalonia. It provides selection management, open/close state, and event hooks, and is designed to be extended for specific menu implementations.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/menu">MenuBase</see>
/// </summary>
[ProcessNode]
public abstract partial class MenuBaseWrapper<TControl, TValue> : SelectingItemsControlWrapperBase<TControl, TValue> where TControl : MenuBase, new()
{
}