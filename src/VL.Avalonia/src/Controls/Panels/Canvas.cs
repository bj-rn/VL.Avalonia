using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>Canvas</c> panel allows absolute positioning of child controls via attached coordinates. Each child can specify its location using Canvas.Left, Canvas.Top, Canvas.Right, and Canvas.Bottom attached properties. Canvas is ideal for scenarios requiring manual placement, drawing, or overlays.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/canvas">Canvas</see>
/// </summary>
[ProcessNode(Name = "Canvas (Spectral)")]
public partial class CanvasSpectralWrapper : PanelWrapperBase<Canvas>
{
}

/// <inheritdoc cref="CanvasSpectralWrapper"/>
[ProcessNode(Name = "Canvas")]
public partial class CanvasWrapper : CanvasSpectralWrapper
{
    /// <inheritdoc cref="SetChildren(Spread{Control})"/>
    [Fragment(Order = -10)]
    public override void SetChildren([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<Control> children) =>
        base.SetChildren(children);
}

/// <summary>
/// Defines the Left attached property.
/// Attached Property Canvas
/// </summary>
[ProcessNode(Name = "Left (Canvas)")]
public partial class CanvasLeftProperty : AttachedPropertyBase
{
    private Optional<float> _left;
    /// <param name="left">
    /// (Attached) The distance from the left edge of the Canvas to the child's left edge (NaN for unset).
    /// </param>
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
        if (_input.HasNoValue)
        {
            return;
        }

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
/// Defines the Right attached property.
/// Attached Property Canvas
/// </summary>
[ProcessNode(Name = "Right (Canvas)")]
public partial class CanvasRightProperty : AttachedPropertyBase
{
    private Optional<float> _right;
    /// <param name="right">
    /// (Attached) The distance from the right edge of the Canvas to the child's right edge (NaN for unset).
    /// </param>
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
        if (_input.HasNoValue)
        {
            return;
        }

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
/// Defines the Top attached property.
/// Attached Property Canvas
/// </summary>
[ProcessNode(Name = "Top (Canvas)")]
public partial class CanvasTopProperty : AttachedPropertyBase
{
    private Optional<float> _top;
    /// <param name="top">
    /// (Attached) The distance from the top edge of the Canvas to the child's top edge (NaN for unset).
    /// </param>
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
        if (_input.HasNoValue)
        {
            return;
        }

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
/// Defines the Bottom attached property.
/// Attached Property Canvas
/// </summary>
[ProcessNode(Name = "Bottom (Canvas)")]
public partial class CanvasBottomProperty : AttachedPropertyBase
{
    private Optional<float> _bottom;
    /// <param name="bottom">
    /// (Attached) The distance from the bottom edge of the Canvas to the child's bottom edge (NaN for unset).
    /// </param>
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
        if (_input.HasNoValue)
        {
            return;
        }

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