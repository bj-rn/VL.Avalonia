using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Media.Base;

[ProcessNode]
public abstract partial class GradientBrushWrapperBase<T> : BrushWrapperBase<T> where T : GradientBrush, new()
{
    [ImplementProperty("GradientBrush.SpreadMethodProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<GradientSpreadMethod> _spreadMethod;

    protected Spread<global::Avalonia.Media.GradientStop> _gradientStops;
    [Fragment(Order = PinOrder.Secondary)]
    public void SetGradientStops(Spread<global::Avalonia.Media.GradientStop> gradientStops)
    {
        if (_gradientStops != gradientStops)
        {
            if (gradientStops != null)
            {
                var stops = new GradientStops();
                foreach (var stop in gradientStops)
                {
                    if (stop != null)
                        stops.Add(stop);
                }

                _output.SetValue(GradientBrush.GradientStopsProperty, stops);
            }
            else
            {
                _output.ClearValue(GradientBrush.GradientStopsProperty);
            }

            _gradientStops = gradientStops;
        }
    }
}
