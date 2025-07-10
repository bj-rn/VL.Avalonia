using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>HeaderedSelectingItemsControl</c> is an <see cref="SelectingItemsControl"/> with an optional header area. The header can display any object (text, UI, etc.), with a customizable template. This pattern is useful for grouped lists or controls with a labeled items area.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/headeredselectingitemscontrol">HeaderedSelectingItemsControl</see>
/// </summary>
[ProcessNode]
public abstract partial class HeaderedSelectingItemsControlWrapperBase<TControl, TValue> : SelectingItemsControlWrapperBase<TControl, TValue> where TControl : SelectingItemsControl, new()
{
    #region Header Properties

    /// <param name="header">
    /// The header content (can be a string, UI element, or any object).
    /// </param>
    [ImplementProperty("HeaderedSelectingItemsControl.HeaderProperty", Order = PinOrder.Secondary)]
    protected Optional<object> _header;

    /// <param name="headerTemplate">
    /// The data template used to display the header content.
    /// </param>
    [ImplementProperty("HeaderedSelectingItemsControl.HeaderTemplateProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IDataTemplate> _headerTemplate;

    #endregion
}

