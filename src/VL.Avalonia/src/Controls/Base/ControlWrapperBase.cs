using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for all controls, seems more flexible then using CodeGen for each prop.
/// Implements: <c>Output</c>, <c>Style</c>, <c>Classes</c>, <c>Name</c>.
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class ControlWrapperBase<T> where T : Control, new()
{
    protected readonly T _output = new();
    public T Output => _output;

    protected Optional<IAvaloniaStyle> _style;
    [Fragment(Order = -4)]
    /// <param name="style">
    /// Style Setters
    /// </param>
    public void SetStyle(Optional<IAvaloniaStyle> style)
    {
        if (_style != style)
        {
            _style = style;
            _output.TryUpdateStyles(style.Value);
        }
    }

    #region StyledElement Properties

    protected Optional<string> _classes;
    /// <param name="classes">
    /// Collection of CSS-like class names for styling purposes
    /// </param>
    [Fragment(Order = -3)]
    public void SetClasses([Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes)
    {
        if (_classes != classes)
        {
            _classes = classes;
            _output.TryUpdateClasses(classes.Value);
        }
    }

    /// <param traget="name">
    /// Sets name of control
    /// </param>
    [ImplementProperty("Control.NameProperty", Order = -2, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;

    #endregion

    #region Layoutable Properties

    /// <param name="horizontalAlignment">
    /// How the control is positioned horizontally within its parent
    /// </param>
    [ImplementProperty("Control.HorizontalAlignmentProperty", Order = 100, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<HorizontalAlignment> _horizontalAlignment;

    /// <param name="verticalAlignment">
    /// How the control is positioned vertically within its parent
    /// </param>
    [ImplementProperty("Control.VerticalAlignmentProperty", Order = 100, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<VerticalAlignment> _verticalAlignment;

    #endregion

    #region Visual Properties

    /// <param name="zIndex">
    /// The z-order (layering) of the control relative to its siblings
    /// </param>
    [ImplementProperty("Control.ZIndexProperty", Order = 900, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _zIndex;

    #endregion

    #region InputElement Properties

    /// <param name="isEnabled">
    /// Whether the control responds to user interaction
    /// </param>
    [ImplementProperty("Control.IsEnabledProperty", Order = 1000, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isEnabled;
    #endregion
}
