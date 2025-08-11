using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Media.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Brushes;

[ProcessNode(Name = "SolidColorBrush")]
public class SolidColorBrushWrapper : BrushWrapperBase<SolidColorBrush>
{
    private Optional<Color4> _color;
    public void SetColor(Optional<Color4> color)
    {
        if (_color != color)
        {
            if (color.HasValue)
            {
                _output.SetValue(SolidColorBrush.ColorProperty, color.Value.ToColor());
            }
            else
            {
                _output.ClearValue(SolidColorBrush.ColorProperty);
            }

            _color = color;
        }
    }
}
