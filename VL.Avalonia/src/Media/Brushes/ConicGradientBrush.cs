using Avalonia;
using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using VL.Avalonia.Media.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Brushes;

[ProcessNode(Name = "ConicGradientBrush")]
public partial class ConicGradientBrushWrapper : GradientBrushWrapperBase<ConicGradientBrush>
{
    protected Optional<Vector2> _center;
    protected Optional<RelativeUnit> _relativeUnit;
    public void SetCenter(Optional<Vector2> center, Optional<RelativeUnit> relativeUnit)
    {
        if (_center != center || _relativeUnit != relativeUnit)
        {
            if (center.HasValue)
            {
                _output.SetValue(ConicGradientBrush.CenterProperty,
                    center.Value.ToRelativePoint(relativeUnit.HasValue ? relativeUnit.Value : RelativeUnit.Relative));
            }
            else
            {
                _output.ClearValue(ConicGradientBrush.CenterProperty);
            }

            _center = center;
            _relativeUnit = relativeUnit;
        }
    }

    protected Optional<float> _angle;
    public void SetAngle(Optional<float> angle)
    {
        if (_angle != angle)
        {
            if (angle.HasValue)
            {
                _output.SetValue(ConicGradientBrush.AngleProperty, (double)angle.Value * 360.0);
            }
            else
            {
                _output.ClearValue(ConicGradientBrush.AngleProperty);
            }

            _angle = angle;
        }
    }
}
