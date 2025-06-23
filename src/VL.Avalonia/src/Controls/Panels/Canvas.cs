using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls.Base;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls.Panels;

/// <summary>
/// A panel that displays child controls at arbitrary locations.
/// </summary>
/// <remarks>
/// Unlike other <see cref="Panel"/> implementations, the <see cref="Canvas"/> doesn't lay out
/// its children in any particular layout. Instead, the positioning of each child control is
/// defined by the <c>Canvas.Left</c>, <c>Canvas.Top</c>, <c>Canvas.Right</c>
/// and <c>Canvas.Bottom</c> attached properties.
/// </remarks>
[ProcessNode(Name = "Canvas (Spectral)")]
public partial class CanvasSpectralWrapper
{
    [ImplementOutput]
    protected readonly Canvas _output = new Canvas();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("Control.NameProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;
}

/// <inheritdoc cref="CanvasSpectralWrapper"/>
[ProcessNode(Name = "Canvas")]
public partial class CanvasWrapper : CanvasSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}

/// <summary>
/// Canvas Left AttachedProperty. 
/// Defines the Left attached property.
/// </summary>
[ProcessNode(Name = "Left (Canvas)")]
public partial class CanvasLeftProperty : AttachedPropertyBase
{
    private Optional<float> _left;
    public void SetLeft(Optional<float> left)
    {
        if (_left != left)
        {
            _left = left;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_left.HasValue)
        {
            Canvas.SetLeft(_input.Value, (double)_left.Value);
        }
        else
        {
            _input.Value?.ClearValue(Canvas.LeftProperty);
        }
    }
}

/// <summary>
/// Canvas Right AttachedProperty. 
/// Defines the Right attached property.
/// </summary>
[ProcessNode(Name = "Right (Canvas)")]
public partial class CanvasRightProperty : AttachedPropertyBase
{
    private Optional<float> _right;
    public void SetRight(Optional<float> right)
    {
        if (_right != right)
        {
            _right = right;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_right.HasValue)
        {
            Canvas.SetRight(_input.Value, (double)_right.Value);
        }
        else
        {
            _input.Value?.ClearValue(Canvas.RightProperty);
        }
    }
}

/// <summary>
/// Canvas Top AttachedProperty. 
/// Defines the Top attached property.
/// </summary>
[ProcessNode(Name = "Top (Canvas)")]
public partial class CanvasTopProperty : AttachedPropertyBase
{
    private Optional<float> _top;
    public void SetTop(Optional<float> top)
    {
        if (_top != top)
        {
            _top = top;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_top.HasValue)
        {
            Canvas.SetTop(_input.Value, (double)_top.Value);
        }
        else
        {
            _input.Value?.ClearValue(Canvas.TopProperty);
        }
    }
}

/// <summary>
/// Canvas Bottom AttachedProperty. 
/// Defines the Bottom attached property.
/// </summary>
[ProcessNode(Name = "Bottom (Canvas)")]
public partial class CanvasBottomProperty : AttachedPropertyBase
{
    private Optional<float> _bottom;
    public void SetBottom(Optional<float> bottom)
    {
        if (_bottom != bottom)
        {
            _bottom = bottom;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_bottom.HasValue)
        {
            Canvas.SetBottom(_input.Value, (double)_bottom.Value);
        }
        else
        {
            _input.Value?.ClearValue(Canvas.BottomProperty);
        }
    }
}