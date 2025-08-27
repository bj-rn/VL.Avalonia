using Avalonia;
using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using VL.Avalonia.Media.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Brushes;

[ProcessNode(Name = "LinearGradientBrush")]
public partial class LinearGradientBrushWrapper : GradientBrushWrapperBase<LinearGradientBrush>
{
    protected Optional<Vector2> _startPoint;
    protected Optional<RelativeUnit> _startPointRelativeUnit;
    protected Optional<Vector2> _endPoint;
    protected Optional<RelativeUnit> _endPointRelativeUnit;

    public void SetStartPoint(Optional<Vector2> startPoint, Optional<RelativeUnit> startPointRelativeUnit)
    {
        if (_startPoint != startPoint || _startPointRelativeUnit != startPointRelativeUnit)
        {
            if (startPoint.HasValue)
            {
                _output.SetValue(LinearGradientBrush.StartPointProperty,
                    startPoint.Value.ToRelativePoint(startPointRelativeUnit.HasValue ? startPointRelativeUnit.Value : RelativeUnit.Relative));
            }
            else
            {
                _output.ClearValue(LinearGradientBrush.StartPointProperty);
            }

            _startPoint = startPoint;
            _startPointRelativeUnit = startPointRelativeUnit;
        }
    }

    public void SetEndPoint(Optional<Vector2> endPoint, Optional<RelativeUnit> endPointRelativeUnit)
    {
        if (_endPoint != endPoint || _endPointRelativeUnit != endPointRelativeUnit)
        {
            if (endPoint.HasValue)
            {
                _output.SetValue(LinearGradientBrush.EndPointProperty,
                    endPoint.Value.ToRelativePoint(endPointRelativeUnit.HasValue ? endPointRelativeUnit.Value : RelativeUnit.Relative));
            }
            else
            {
                _output.ClearValue(LinearGradientBrush.EndPointProperty);
            }

            _endPoint = endPoint;
            _endPointRelativeUnit = endPointRelativeUnit;
        }
    }
}
