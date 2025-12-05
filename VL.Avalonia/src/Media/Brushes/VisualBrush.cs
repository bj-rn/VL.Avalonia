using Avalonia;
using Avalonia.Media;
using VL.Avalonia.Media.Base;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Brushes;

[ProcessNode(Name = "VisualBrush")]
public partial class VisualBrushWrapper : TileBrushWrapperBase<VisualBrush>
{
    protected Optional<Visual> _visual;

    [Fragment(Order = PinOrder.Main)]
    public void SetVisual(Optional<Visual> visual)
    {
        if (_visual != visual)
        {
            if (visual.HasValue)
            {
                _output.SetValue(VisualBrush.VisualProperty, visual.Value);
            }
            else
            {
                _output.ClearValue(VisualBrush.VisualProperty);
            }

            _visual = visual;
        }
    }
}
