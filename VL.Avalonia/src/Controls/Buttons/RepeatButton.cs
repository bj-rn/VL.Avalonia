using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>RepeatButton</c> extends Button to provide continuous clicking functionality when pressed and held. It fires Click events repeatedly at specified intervals after an initial delay, making it ideal for increment/decrement operations, scrolling, or any scenario requiring repeated actions. RepeatButton maintains all standard button functionality while adding timer-based repetition.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/repeatbutton">RepeatButton</see>
/// </summary>
[ProcessNode(Name = "RepeatButton")]
public partial class RepeatButtonWrapper : ButtonWrapperBase<RepeatButton>
{
    #region Timing Properties

    /// <param name="interval">
    /// Time interval in milliseconds between repeated Click events after initial delay (default: 100ms)
    /// </param>
    [ImplementProperty("RepeatButton.IntervalProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _interval;

    /// <param name="delay">
    /// Initial delay in milliseconds before repeating begins (default: 300ms)
    /// </param>
    [ImplementProperty("RepeatButton.DelayProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _delay;

    #endregion
}
