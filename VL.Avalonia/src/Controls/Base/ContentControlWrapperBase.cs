using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// Wrapper for controls implementing Content
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class ContentControlWrapperBase<T> : ControlWrapperBase<T> where T : ContentControl, new()
{
    #region Core Content Properties

    /// <param name="content">
    /// The content to display inside the control (can be any object - text, controls, data objects, etc.)
    /// </param>
    [ImplementProperty("ContentControl.ContentProperty", Order = -10)]
    protected Optional<object> _content;

    /// <param name="contentTemplate">
    /// The data template used to display the content when it's not a visual element
    /// </param>
    [ImplementProperty("ContentControl.ContentTemplateProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IDataTemplate> _contentTemplate;

    #endregion

    #region Content Alignment Properties

    /// <param name="horizontalContentAlignment">
    /// Horizontal alignment of the content within the control bounds
    /// </param>
    [ImplementProperty("ContentControl.HorizontalContentAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<HorizontalAlignment> _horizontalContentAlignment;

    /// <param name="verticalContentAlignment">
    /// Vertical alignment of the content within the control bounds
    /// </param>
    [ImplementProperty("ContentControl.VerticalContentAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<VerticalAlignment> _verticalContentAlignment;

    #endregion

    #region Inherited Properties Note

    // ContentControl inherits all properties from TemplatedControl and Control:
    // - Background, Foreground, BorderBrush, BorderThickness - Visual appearance
    // - Padding, Margin - Layout spacing
    // - FontFamily, FontSize, FontWeight, FontStyle - Text formatting
    // - Width, Height, MinWidth, MaxWidth, MinHeight, MaxHeight - Sizing
    // - HorizontalAlignment, VerticalAlignment - Control positioning
    // - IsEnabled, IsVisible, Opacity - State and visibility
    // - Template - Control template for visual structure
    // - Theme, Styles, Classes - Styling and theming

    #endregion
}
