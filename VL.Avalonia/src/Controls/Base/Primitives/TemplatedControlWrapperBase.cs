using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>TemplatedControl</c> is a lookless control whose visual appearance is completely defined by its control template. It serves as the base class for most complex controls in Avalonia, providing a clean separation between control logic and visual presentation. The template system allows for complete customization of appearance while maintaining the control's behavior and functionality.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/templatedcontrol">TemplatedControl</see>
/// </summary>
[Obsolete]
public abstract class TemplatedControlWrapperBase<T> : ControlWrapperBase<T>
    where T : TemplatedControl, new()
{
    #region Template Properties

    /*
    /// <param name="template">
    /// The control template that defines the visual structure and appearance of the control
    /// </param>
    [ImplementProperty("TemplatedControl.TemplateProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IControlTemplate> _template;
    */

    #endregion

    #region Background and Border Properties

    /// <param name="background">
    /// The brush used to fill the control's background
    /// </param>
    [ImplementProperty(
        "TemplatedControl.BackgroundProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<IBrush> _background;

    /// <param name="backgroundSizing">
    /// How the background is drawn relative to the control's border (InnerBorderEdge, OuterBorderEdge, CenterBorder)
    /// </param>
    [ImplementProperty(
        "TemplatedControl.BackgroundSizingProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<BackgroundSizing> _backgroundSizing;

    /// <param name="borderBrush">
    /// The brush used to draw the control's border
    /// </param>
    [ImplementProperty(
        "TemplatedControl.BorderBrushProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<IBrush> _borderBrush;

    /// <param name="borderThickness">
    /// The thickness of the control's border on all sides
    /// </param>
    [ImplementProperty(
        "TemplatedControl.BorderThicknessProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<Thickness> _borderThickness;

    /// <param name="cornerRadius">
    /// The radius of the control's rounded corners
    /// </param>
    [ImplementProperty(
        "TemplatedControl.CornerRadiusProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<CornerRadius> _cornerRadius;

    #endregion

    #region Typography Properties

    /// <param name="fontFamily">
    /// The font family used for text within the control
    /// </param>
    [ImplementProperty(
        "TemplatedControl.FontFamilyProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontFamily> _fontFamily;

    /// <param name="fontSize">
    /// The size of the font used for text within the control
    /// </param>
    [ImplementProperty(
        "TemplatedControl.FontSizeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _fontSize;

    /// <param name="fontWeight">
    /// The weight (boldness) of the font used for text within the control
    /// </param>
    [ImplementProperty(
        "TemplatedControl.FontWeightProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontWeight> _fontWeight;

    /// <param name="fontStyle">
    /// The style (normal, italic, oblique) of the font used for text within the control
    /// </param>
    [ImplementProperty(
        "TemplatedControl.FontStyleProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontStyle> _fontStyle;

    /// <param name="fontStretch">
    /// The stretch (condensed, normal, expanded) of the font used for text within the control
    /// </param>
    [ImplementProperty(
        "TemplatedControl.FontStretchProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontStretch> _fontStretch;

    /// <param name="fontFeatures">
    /// Advanced font features (ligatures, stylistic sets, etc.) applied to text within the control
    /// </param>
    [ImplementProperty(
        "TemplatedControl.FontFeaturesProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontFeatureCollection> _fontFeatures;

    /// <param name="foreground">
    /// The brush used to draw text and other foreground elements within the control
    /// </param>
    [ImplementProperty(
        "TemplatedControl.ForegroundProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<IBrush> _foreground;

    #endregion

    #region Layout Properties

    /// <param name="padding">
    /// The space between the control's border and its content
    /// </param>
    [ImplementProperty(
        "TemplatedControl.PaddingProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<Thickness> _padding;

    #endregion

    #region Inherited Properties Note

    // TemplatedControl inherits all properties from Control:
    // - Width, Height, MinWidth, MaxWidth, MinHeight, MaxHeight - Sizing constraints
    // - HorizontalAlignment, VerticalAlignment - Positioning within parent
    // - Margin - External spacing around the control
    // - IsEnabled, IsVisible, Opacity - State and visibility
    // - Focusable, IsTabStop, TabIndex - Focus and tab navigation
    // - Cursor - Mouse cursor when hovering
    // - ToolTip - Tooltip content
    // - ContextMenu - Right-click context menu
    // - Classes - Style classes for CSS-like styling
    // - Theme - Control theme for consistent styling
    // - Tag - User data storage

    #endregion
}
