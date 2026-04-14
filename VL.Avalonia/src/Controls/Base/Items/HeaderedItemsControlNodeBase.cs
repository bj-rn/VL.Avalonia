using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="HeaderedItemsControl"/>
    [ProcessNode]
    public abstract partial class HeaderedItemsControlBaseNode<TControl, TValue>
        : ItemsControlNodeBase<TControl, TValue>
        where TControl : HeaderedItemsControl, new()
    {
        [ImplementProperty("HeaderedContentControl.HeaderProperty")]
        protected Optional<object> _header;

        [ImplementProperty(
            "HeaderedContentControl.HeaderTemplateProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<IDataTemplate> _headerTemplate;
    }
}
