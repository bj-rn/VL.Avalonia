using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using VL.Avalonia.Attributes;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref= "ContentControl"/>
    public abstract class ContentControlNodeBase<TControl, TValue> : ControlNodeBase<TControl>
        where TControl : ContentControl, new()
    {
        /// <inheritdoc cref= "ContentControl.ContentProperty"/>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.Content),
            Order = PinOrder.Main
        )]
        private Optional<object> _content;

        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.ContentTemplate),
            Order = PinOrder.Secondary,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _contentTemplate;

        /// <inheritdoc cref="ContentControl.HorizontalContentAlignment"/>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.HorizontalContentAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalContentAlignment;

        /// <inheritdoc cref="ContentControl.VerticalContentAlignment"/>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.VerticalContentAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalContentAlignment;
    }
}
