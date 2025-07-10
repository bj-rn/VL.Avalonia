using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// A control that lets the user select from a range of values by moving a Thumb control along a Track.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Slider</see>
/// <br/>PseudoClasses: :vertical, :horizontal, :pressed
/// <br/>TemplateParts: PART_DecreaseButton, PART_IncreaseButton, PART_Track
/// </summary>
[ProcessNode(Name = "Slider")]
public partial class SliderWrapper : RangeBaseWrapper<Slider>
{
    #region Orientation

    /// <param name="orientation">
    /// Sets the orientation of a Slider
    /// </param>
    [ImplementProperty("Slider.OrientationProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<Orientation> _orientation;

    /// <param name="isDirectionReversed">
    /// Sets the direction of increasing value.
    /// true if the direction of increasing value is to the left for a horizontal slider or
    /// down for a vertical slider; otherwise, false. The default is false.
    /// </param>
    [ImplementProperty("Slider.IsDirectionReversedProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<bool> _isDirectionReversed;

    #endregion

    #region Ticks

    /// <param name="isSnapToTickEnabled">
    /// Sets a value that indicates whether the Slider automatically moves the Thumb to the closest tick mark.
    /// </param>
    [ImplementProperty("Slider.IsSnapToTickEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<bool> _isSnapToTickEnabled;

    /// <param name="tickFrequency">
    /// Sets the interval between tick marks.
    /// </param>
    [ImplementProperty("Slider.TickFrequencyProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<float> _tickFrequency;

    /// <param name="tickPlacement">
    /// Sets a value that indicates where to draw 
    /// tick marks in relation to the track.
    /// </param>
    [ImplementProperty("Slider.TickPlacementProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<TickPlacement> _tickPlacement;

    private Optional<Spread<float>> _ticks;
    /// <param name="ticks">
    /// Defines the ticks to be drawn on the tick bar.
    /// </param>
    public void SetTicks([Pin(Visibility = Model.PinVisibility.Optional)] Optional<Spread<float>> ticks)
    {
        if (_ticks != ticks)
        {
            if (ticks.HasValue && ticks.Value.Count > 0)
            {
                var list = new AvaloniaList<double>(ticks.Value.Select(x => (double)x));

                _output.SetValue(Slider.TicksProperty, list);
            }
            else
            {
                _output.ClearValue(Slider.TicksProperty);
            }

            _ticks = ticks;
        }
    }

    #endregion
}
