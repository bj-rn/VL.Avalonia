using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The TimePicker has two or three 'spinner' controls to allow the user to pick a time value. The time picker can work in 24 or 12 hour formats. The picker controls display when the control is clicked.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/timepicker">Slider</see>
/// <br/><br/>PseudoClasses: :hasnotime
/// <br/><br/>TemplateParts: "PART_FirstColumnDivider", PART_FirstPickerHost", PART_FlyoutButton", PART_FlyoutButtonContentGrid", PART_HourTextBlock", PART_MinuteTextBlock", PART_SecondTextBlock", PART_PeriodTextBlock", PART_PickerPresenter", PART_Popup", PART_SecondColumnDivider", PART_SecondPickerHost",  PART_ThirdColumnDivider", PART_ThirdPickerHost", PART_FourthPickerHost".
/// </summary>
[ProcessNode(Name = "TimePicker")]
public partial class TimePickerWrapper : ControlNodeBase<TimePicker>
{
    #region Core

    protected ChannelTwoWayBinding<TimeSpan?> _selectedTimeBinding;

    public TimePickerWrapper()
    {
        _selectedTimeBinding = new ChannelTwoWayBinding<TimeSpan?>(
            _output,
            TimePicker.SelectedTimeProperty
        );
    }

    /// <param name="selectedTimeChannel">
    /// Selected Time for the picker, can be null
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetSelectedTimeChannel(IChannel<TimeSpan?> selectedTimeChannel) =>
        _selectedTimeBinding.SetChannel(selectedTimeChannel);

    #endregion

    #region Additional Properties

    /// <param name="minuteIncrement">
    /// Sets the minute increment in the picker
    /// </param>
    [ImplementProperty(
        "TimePicker.MinuteIncrementProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<int> _minuteIncrement;

    /// <param name="secondIncrement">
    /// Sets the second increment in the picker
    /// </param>
    [ImplementProperty(
        "TimePicker.SecondIncrementProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<int> _secondIncrement;

    /// <param name="clockIdentifier">
    /// Sets the clock identifier, either 12HourClock or 24HourClock
    /// </param>
    [ImplementProperty(
        "TimePicker.ClockIdentifierProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<string> _clockIdentifier;

    /// <param name="useSeconds">
    /// Sets the use seconds switch, either true or false
    /// </param>
    [ImplementProperty(
        "TimePicker.UseSecondsProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<bool> _useSeconds;

    #endregion
}

