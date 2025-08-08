using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Stride.Core.Mathematics;
using System.Reactive.Subjects;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;


using static VL.Avalonia.Styles;
using VL.Lib.Mathematics;

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

  
    public RectangleF Bounds => new RectangleF((int)_output.Bounds.Position.X, (int)_output.Bounds.Position.Y,  (int)_output.Bounds.Size.Width, (int)_output.Bounds.Size.Height);

    protected ControlWrapperBase()
    {
        _output.PointerEntered += _output_PointerEntered;
        _output.PointerExited += _output_PointerExited;
    }

    [Fragment(Order = 10)]
    public IObservable<PointerEventArgs> PointerEntered => _pointerEntered;

    private Subject<PointerEventArgs> _pointerEntered = new Subject<PointerEventArgs>();
    private void _output_PointerEntered(object? sender, PointerEventArgs e)
    {
        _pointerEntered.OnNext(e);
    }


    [Fragment(Order = 10)]
    public IObservable<PointerEventArgs> PointerExited => _pointerExited;

    private Subject<PointerEventArgs> _pointerExited = new Subject<PointerEventArgs>();
    private void _output_PointerExited(object? sender, PointerEventArgs e)
    {
        _pointerExited.OnNext(e);
    }


    protected Optional<IAvaloniaStyle> _style;
    [Fragment(Order = PinOrder.Style)]
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
    [Fragment(Order = PinOrder.Style)]
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
    [ImplementProperty("Control.NameProperty", Order = PinOrder.Style, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;

    #endregion

    #region Focus Context Properties

    /// <param name="focusAdorner">
    /// Template for a visual adorner shown when the control is focused.
    /// </param>
    [ImplementProperty("Control.FocusAdornerProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ITemplate<Control>> _focusAdorner;

    /// <param name="contextMenu">
    /// The context menu shown for this control (ContextMenu).
    /// </param>
    [ImplementProperty("Control.ContextMenuProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ContextMenu> _contextMenu;

    /// <param name="contextFlyout">
    /// The context flyout shown for this control (FlyoutBase).
    /// </param>
    [ImplementProperty("Control.ContextFlyoutProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<FlyoutBase> _contextFlyout;

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

    /// <param name="focusable">
    /// Whether the control can receive keyboard focus
    /// </param>
    [ImplementProperty("Control.FocusableProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _focusable;

    /// <param name="isEnabled">
    /// Whether the control responds to user interaction
    /// </param>
    [ImplementProperty("Control.IsEnabledProperty", Order = 1000, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isEnabled;
    #endregion
}
