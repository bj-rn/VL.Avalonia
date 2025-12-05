using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The DatePicker has three 'spinner' controls to allow the user to pick a date value. The spinners display when the control is clicked.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/datepicker">Slider</see>
/// <br/><br/>PseudoClasses: :hasnodate
/// <br/><br/>TemplateParts: PART_ButtonContentGrid, PART_DayTextBlock, PART_FirstSpacer, PART_FlyoutButton, PART_MonthTextBlock, PART_PickerPresenter, PART_Popup, PART_SecondSpacer, PART_YearTextBlock
/// </summary>
[ProcessNode(Name = "DatePicker")]
public partial class DatePickerWrapper : ControlWrapperBase<DatePicker>
{
    #region Core

    protected ChannelTwoWayBinding<DateTime?, DateTimeOffset?> _selectedDateBinding;

    public DatePickerWrapper()
    {
        _selectedDateBinding = new ChannelTwoWayBinding<DateTime?, DateTimeOffset?>(
            _output,
            DatePicker.SelectedDateProperty,
            (dt) => dt == null ? null : DateTime.SpecifyKind(dt.Value, DateTimeKind.Local),
            dts => dts?.DateTime
        );
    }

    /// <param name="selectedDateChannel">
    /// Selected Date for the picker, can be null
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetSelectedDateChannel(IChannel<DateTime?> selectedDateChannel) =>
        _selectedDateBinding.SetChannel(selectedDateChannel);

    #endregion

    #region Format

    /// <param name="dayFormat">
    /// Format string for the day part of the date.
    /// </param>
    [ImplementProperty(
        "DatePicker.DayFormatProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<string> _dayFormat;

    /// <param name="dayVisible">
    /// Sets if the day column is visible.
    /// </param>
    [ImplementProperty(
        "DatePicker.DayVisibleProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _dayVisible;

    /// <param name="monthFormat">
    /// Format string for the month part of the date.
    /// </param>
    [ImplementProperty(
        "DatePicker.MonthFormatProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<string> _monthFormat;

    /// <param name="monthVisible">
    /// Sets if the month column is visible.
    /// </param>
    [ImplementProperty(
        "DatePicker.MonthVisibleProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _monthVisible;

    /// <param name="yearFormat">
    /// Format string for the yar part of the date.
    /// </param>
    [ImplementProperty(
        "DatePicker.YearFormatProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<string> _yearFormat;

    /// <param name="yearVisible">
    /// Sets if the year column is visible.
    /// </param>
    [ImplementProperty(
        "DatePicker.YearVisibleProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _yearVisible;

    #endregion

    #region Range

    protected Optional<DateTime> _minYear;

    /// <param name="minYear">
    /// Sets the minimum year for the picker.
    /// </param>
    [Fragment(Order = 0)]
    public void SetMinYear(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<DateTime> minYear
    )
    {
        if (_minYear != minYear)
        {
            _minYear = minYear;

            if (minYear.HasValue)
            {
                _output.SetValue(
                    DatePicker.MinYearProperty,
                    DateTime.SpecifyKind(minYear.Value, DateTimeKind.Local)
                );
            }
            else
            {
                _output.ClearValue(DatePicker.MinYearProperty);
            }
        }
    }

    protected Optional<DateTime> _maxYear;

    /// <param name="maxYear">
    /// Sets the maximum year for the picker.
    /// </param>
    [Fragment(Order = 0)]
    public void SetMaxYear(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<DateTime> maxYear
    )
    {
        if (_maxYear != maxYear)
        {
            _maxYear = maxYear;

            if (maxYear.HasValue)
            {
                _output.SetValue(
                    DatePicker.MaxYearProperty,
                    DateTime.SpecifyKind(maxYear.Value, DateTimeKind.Local)
                );
            }
            else
            {
                _output.ClearValue(DatePicker.MaxYearProperty);
            }
        }
    }

    #endregion
}
