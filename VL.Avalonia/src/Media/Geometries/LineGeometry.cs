using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media;

/// <summary>
/// Represents the geometry of a line.
/// </summary>
[ProcessNode(Name = "LineGeometry")]
public partial class LineGeometryWrapper : GeometryWrapperBase<LineGeometry>
{
    private Optional<Vector2> _startPoint;
    /// <param name="startPoint">Sets the start point of the line.</param>
    public void SetStartPoint(Optional<Vector2> startPoint)
    {
        if (_startPoint != startPoint)
        {
            if (startPoint.HasValue)
            {
                _output.SetValue(LineGeometry.StartPointProperty, startPoint.Value.ToPoint());
            }
            else
            {
                _output.ClearValue(LineGeometry.StartPointProperty);
            }
            _startPoint = startPoint;
        }
    }

    private Optional<Vector2> _endPoint;
    /// <param name="endPoint">Sets the end point of the line.</param>
    public void SetEndPoint(Optional<Vector2> endPoint)
    {
        if (_endPoint != endPoint)
        {
            if (endPoint.HasValue)
            {
                _output.SetValue(LineGeometry.EndPointProperty, endPoint.Value.ToPoint());
            }
            else
            {
                _output.ClearValue(LineGeometry.EndPointProperty);
            }
            _endPoint = endPoint;
        }
    }
}

