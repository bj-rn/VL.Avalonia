using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media;

/// <summary>
/// Represents the geometry of an ellipse or circle.
/// </summary>
[ProcessNode(Name = "EllipseGeometry")]
public partial class EllipseGeometryWrapper : GeometryWrapperBase<EllipseGeometry>
{
    protected Optional<Vector2> _radius;
    /// <param name="radius">Sets a Vector2 that defines the radius in the XY-axis of the ellipse.</param>
    public void SetRadius(Optional<Vector2> radius)
    {
        if (_radius != radius)
        {
            if (radius.HasValue)
            {
                _output.SetValue(EllipseGeometry.RadiusXProperty, radius.Value.X);
                _output.SetValue(EllipseGeometry.RadiusYProperty, radius.Value.Y);
            }
            else
            {
                _output.ClearValue(EllipseGeometry.RadiusXProperty);
                _output.ClearValue(EllipseGeometry.RadiusYProperty);
            }

            _radius = radius;
        }
    }

    protected Optional<Vector2> _center;
    /// <param name="center">Sets a point that defines the center of the ellipse</param>
    public void SetCenter(Optional<Vector2> center)
    {
        if (_center != center)
        {
            if (center.HasValue)
            {
                _output.SetValue(EllipseGeometry.CenterProperty, center.Value.ToPoint());
            }
            else
            {
                _output.ClearValue(EllipseGeometry.CenterProperty);
            }

            _center = center;
        }
    }

    protected Optional<RectangleF> _rect;
    /// <param name="rect">Sets a rect that defines the bounds</param>
    public void SetRect(Optional<RectangleF> rect)
    {
        if (_rect != rect)
        {
            if (rect.HasValue)
            {
                // TODO: 
                // _output = new EllipseGeometry(Rect)

                _output.SetValue(EllipseGeometry.RectProperty, rect.Value.ToRect());
            }
            else
            {
                _output.ClearValue(EllipseGeometry.RectProperty);
            }

            _rect = rect;
        }
    }
}
