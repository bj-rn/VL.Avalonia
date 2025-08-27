using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media;

[ProcessNode(Name = "GradientStop")]
public partial class GradientStopWrapper
{
    protected readonly GradientStop _output = new();
    public GradientStop Output => _output;

    [ImplementProperty<float, double>("GradientStop.OffsetProperty")]
    protected Optional<float> _offset;

    private Optional<Color4> _color;
    public void SetColor(Optional<Color4> color)
    {
        if (_color != color)
        {
            if (color.HasValue)
            {
                _output.SetValue(GradientStop.ColorProperty, color.Value.ToColor());
            }
            else
            {
                _output.ClearValue(GradientStop.ColorProperty);
            }

            _color = color;
        }
    }
}
