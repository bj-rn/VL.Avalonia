using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref= "ContentControl"/>
    [ProcessNode]
    public abstract partial class ContentControlNodeBase<TControl>
        : TemplatedControlNodeBase<TControl>
        where TControl : ContentControl, new()
    {
        /// <inheritdoc cref= "ContentControl.ContentProperty"/>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.ContentProperty),
            Order = PinOrder.Main
        )]
        private Optional<object> _content;

        /// <inheritdoc cref="ContentControl.ContentTemplateProperty"/>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.ContentTemplateProperty),
            Order = PinOrder.Secondary,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _contentTemplate;

        /// <inheritdoc cref="ContentControl.HorizontalContentAlignmentProperty"/>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.HorizontalContentAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalContentAlignment;

        /// <inheritdoc cref="ContentControl.VerticalContentAlignmentProperty"/>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.VerticalContentAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalContentAlignment;
    }
}
