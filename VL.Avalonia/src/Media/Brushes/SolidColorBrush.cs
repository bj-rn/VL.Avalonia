using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using VL.Avalonia.Media.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Brushes;

[ProcessNode(Name = "SolidColorBrush")]
public class SolidColorBrush : BrushWrapperBase<global::Avalonia.Media.SolidColorBrush>
{
    protected Optional<Color4> _color;
    [Fragment(Order = PinOrder.Main)]
    public void SetColor(Optional<Color4> color)
    {
        if (_color != color)
        {
            if (color.HasValue)
            {
                _output.SetValue(global::Avalonia.Media.SolidColorBrush.ColorProperty, color.Value.ToColor());
            }
            else
            {
                _output.ClearValue(global::Avalonia.Media.SolidColorBrush.ColorProperty);
            }

            _color = color;
        }
    }
}
