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
        /// <inheritdoc cref="HeaderedItemsControl.Header"/>
        [ImplementProperty(
            typeof(HeaderedItemsControl),
            nameof(HeaderedItemsControl.HeaderProperty),
            Order = PinOrder.Main
        )]
        private Optional<object> _header;

        /// <inheritdoc cref="HeaderedItemsControl.HeaderTemplate"/>
        [ImplementProperty(
            typeof(HeaderedItemsControl),
            nameof(HeaderedItemsControl.HeaderTemplateProperty),
            Order = PinOrder.Secondary,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _headerTemplate;
    }
}
