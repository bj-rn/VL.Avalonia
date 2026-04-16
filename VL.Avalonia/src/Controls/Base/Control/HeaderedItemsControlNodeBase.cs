using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="HeaderedItemsControl"/>
    [ProcessNode]
    public abstract partial class HeaderedItemsControlNodeBase<TControl, TValue>
        : ItemsControlNodeBase<TControl, TValue>
        where TControl : HeaderedItemsControl, new()
    {
        /// <summary>Sets the content of the control's header.</summary>
        [ImplementProperty(
            typeof(HeaderedItemsControl),
            nameof(HeaderedItemsControl.HeaderProperty),
            Order = PinOrder.Main
        )]
        private Optional<object> _header;

        /// <summary>Sets the data template used to display the header content of the control.</summary>
        [ImplementProperty(
            typeof(HeaderedItemsControl),
            nameof(HeaderedItemsControl.HeaderTemplateProperty),
            Order = PinOrder.Secondary,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _headerTemplate;
    }
}
