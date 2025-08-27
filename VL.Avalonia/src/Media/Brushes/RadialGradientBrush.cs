using Avalonia;
using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using VL.Avalonia.Media.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Brushes;

[ProcessNode(Name = "RadialGradientBrush")]
public partial class RadialGradientBrushWrapper : GradientBrushWrapperBase<RadialGradientBrush>
{
    protected Optional<Vector2> _center;
    protected Optional<RelativeUnit> _centerRelativeUnit;
    public void SetCenter(Optional<Vector2> center, Optional<RelativeUnit> centerRelativeUnit)
    {
        if (_center != center || _centerRelativeUnit != centerRelativeUnit)
        {
            if (center.HasValue)
            {
                _output.SetValue(RadialGradientBrush.CenterProperty,
                    center.Value.ToRelativePoint(centerRelativeUnit.HasValue ? centerRelativeUnit.Value : RelativeUnit.Relative));
            }
            else
            {
                _output.ClearValue(RadialGradientBrush.CenterProperty);
            }

            _center = center;
            _centerRelativeUnit = centerRelativeUnit;
        }
    }

    protected Optional<Vector2> _gradientOrigin;
    protected Optional<RelativeUnit> _gradientOriginRelativeUnit;
    public void SetGradientOrigin(Optional<Vector2> gradientOrigin, Optional<RelativeUnit> gradientOriginRelativeUnit)
    {
        if (_gradientOrigin != gradientOrigin || _gradientOriginRelativeUnit != gradientOriginRelativeUnit)
        {
            if (gradientOrigin.HasValue)
            {
                _output.SetValue(RadialGradientBrush.GradientOriginProperty,
                    gradientOrigin.Value.ToRelativePoint(gradientOriginRelativeUnit.HasValue ? gradientOriginRelativeUnit.Value : RelativeUnit.Relative));
            }
            else
            {
                _output.ClearValue(RadialGradientBrush.GradientOriginProperty);
            }

            _gradientOrigin = gradientOrigin;
            _gradientOriginRelativeUnit = gradientOriginRelativeUnit;
        }
    }

    protected Optional<Vector2> _radius;
    protected Optional<RelativeUnit> _radiusRelativeUnit;

    public void SetRadius(Optional<Vector2> radius, Optional<RelativeUnit> radiusRelativeUnit)
    {
        if (_radius != radius || _radiusRelativeUnit != radiusRelativeUnit)
        {
            if (radius.HasValue)
            {
                _output.SetValue(RadialGradientBrush.RadiusXProperty, radius.Value.X.ToRelativeScalar(radiusRelativeUnit.HasValue ? radiusRelativeUnit.Value : RelativeUnit.Relative));
                _output.SetValue(RadialGradientBrush.RadiusYProperty, radius.Value.Y.ToRelativeScalar(radiusRelativeUnit.HasValue ? radiusRelativeUnit.Value : RelativeUnit.Relative));
            }
            else
            {
                _output.ClearValue(RadialGradientBrush.RadiusXProperty);
                _output.ClearValue(RadialGradientBrush.RadiusYProperty);
            }

            _radius = radius;
            _radiusRelativeUnit = radiusRelativeUnit;
        }
    }
}
