using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Media;

/// <summary>
/// Represents the geometry of an polyline or polygon.
/// </summary>
[ProcessNode(Name = "PolylineGeometry")]
public partial class PolyLineGeometrySpectralWrapper : GeometryWrapperBase<PolylineGeometry>
{
    protected Spread<Vector2> _points;
    /// <param name="points">The points.</param>
    public void SetPoints(Spread<Vector2> points)
    {
        if (_points != points)
        {
            if (points != null)
            {
                var list = points.Select(p => p.ToPoint()).ToList();

                _output.SetValue(PolylineGeometry.PointsProperty, list);

                // TODO: REMOVE
                // Workaround for a bug in 11.2.1
                // https://github.com/AvaloniaUI/Avalonia/issues/3623
                _output.SetValue(PolylineGeometry.IsFilledProperty, !_output.IsFilled);
                _output.SetValue(PolylineGeometry.IsFilledProperty, !_output.IsFilled);
            }
            else
            {
                _output.ClearValue(PolylineGeometry.PointsProperty);
            }

            _points = points;
        }
    }

    /// <param name="isFilled">Is polyline filled</param>
    [ImplementProperty("PolylineGeometry.IsFilledProperty")]
    protected Optional<bool> _isFilled;
}
