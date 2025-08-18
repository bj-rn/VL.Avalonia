using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media;

/// <summary>
/// Represents the geometry of a rectangle.
/// </summary>
[ProcessNode(Name = "RectangleGeometry")]
public partial class RectangleGeometryWrapper : GeometryWrapperBase<RectangleGeometry>
{
    protected Optional<RectangleF> _rect;
    /// <param name="rect">Sets a rect that defines the bounds</param>
    public void SetRect(Optional<RectangleF> rect)
    {
        if (_rect != rect)
        {
            if (rect.HasValue)
            {
                _output.SetValue(RectangleGeometry.RectProperty, rect.Value.ToRect());
            }
            else
            {
                _output.ClearValue(RectangleGeometry.RectProperty);
            }

            _rect = rect;
        }
    }

    protected Optional<Vector2> _radius;
    /// <param name="radius">Sets a rounded corners that defines the radius in the XY-axis of the box.</param>
    public void SetRadius(Optional<Vector2> radius)
    {
        if (_radius != radius)
        {
            if (radius.HasValue)
            {
                _output.SetValue(RectangleGeometry.RadiusXProperty, (double)radius.Value.X);
                _output.SetValue(RectangleGeometry.RadiusYProperty, (double)radius.Value.Y);
            }
            else
            {
                _output.ClearValue(RectangleGeometry.RadiusXProperty);
                _output.ClearValue(RectangleGeometry.RadiusYProperty);
            }

            _radius = radius;
        }
    }



}
