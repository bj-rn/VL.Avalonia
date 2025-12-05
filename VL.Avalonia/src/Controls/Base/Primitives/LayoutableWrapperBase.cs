using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;

namespace VL.Avalonia.Controls.Primitives;

/// <summary>
/// Layoutable base, based on https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Base/Layout/Layoutable.cs.
/// Gonna comment few props for convenience, use Style Setters instead.
/// </summary>
[Obsolete("Overcomplicated")]
public abstract partial class LayoutableWrapperBase<T> : VisualWrapperBase<T>
    where T : Layoutable
{
    #region Layout Properties (from Layoutable)

    /*
    /// <param name="width">
    /// The width of the control in device-independent pixels
    /// </param>
    [ImplementProperty("Layoutable.WidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _width;
    */

    /*
    /// <param name="height">
    /// The height of the control in device-independent pixels
    /// </param>
    [ImplementProperty("Layoutable.HeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _height;
    */

    /*
    /// <param name="minWidth">
    /// The minimum width constraint for the control
    /// </param>
    [ImplementProperty("Layoutable.MinWidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _minWidth;
    */

    /*
    /// <param name="minHeight">
    /// The minimum height constraint for the control
    /// </param>
    [ImplementProperty("Layoutable.MinHeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _minHeight;
    */

    /*
    /// <param name="maxWidth">
    /// The maximum width constraint for the control
    /// </param>
    [ImplementProperty("Layoutable.MaxWidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _maxWidth;
    */

    /*
    /// <param name="maxHeight">
    /// The maximum height constraint for the control
    /// </param>
    [ImplementProperty("Layoutable.MaxHeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _maxHeight;
    */

    /*
    /// <param name="margin">
    /// The margin space around the outside of the control
    /// </param>
    [ImplementProperty("Layoutable.MarginProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<Thickness> _margin;
    */

    /// <param name="horizontalAlignment">
    /// How the control is positioned horizontally within its parent
    /// </param>
    [ImplementProperty(
        "Layoutable.HorizontalAlignmentProperty",
        Order = 100,
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<HorizontalAlignment> _horizontalAlignment;

    /// <param name="verticalAlignment">
    /// How the control is positioned vertically within its parent
    /// </param>
    [ImplementProperty(
        "Layoutable.VerticalAlignmentProperty",
        Order = 100,
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<VerticalAlignment> _verticalAlignment;

    #endregion
}
