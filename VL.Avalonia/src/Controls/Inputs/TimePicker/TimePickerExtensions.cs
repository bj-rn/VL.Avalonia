using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="TimePicker"/>.
    /// </summary>
    public static class TimePickerExtensions
    {
        /// <inheritdoc cref="TimePicker.MinuteIncrement"/>
        public static int MinuteIncrement(this TimePicker timePicker) =>
            timePicker != null ? timePicker.MinuteIncrement : 1;

        /// <inheritdoc cref="TimePicker.MinuteIncrement"/>
        public static void SetMinuteIncrement(this TimePicker timePicker, int minuteIncrement)
        {
            if (timePicker is not null)
                timePicker.MinuteIncrement = minuteIncrement;
        }

        /// <inheritdoc cref="TimePicker.SecondIncrement"/>
        public static int SecondIncrement(this TimePicker timePicker) =>
            timePicker != null ? timePicker.SecondIncrement : 1;

        /// <inheritdoc cref="TimePicker.SecondIncrement"/>
        public static void SetSecondIncrement(this TimePicker timePicker, int secondIncrement)
        {
            if (timePicker is not null)
                timePicker.SecondIncrement = secondIncrement;
        }

        /// <inheritdoc cref="TimePicker.ClockIdentifier"/>
        public static string ClockIdentifier(this TimePicker timePicker) =>
            timePicker != null ? timePicker.ClockIdentifier : "12HourClock";

        /// <inheritdoc cref="TimePicker.ClockIdentifier"/>
        public static void SetClockIdentifier(this TimePicker timePicker, string clockIdentifier)
        {
            if (timePicker is not null)
                timePicker.ClockIdentifier = clockIdentifier;
        }

        /// <inheritdoc cref="TimePicker.UseSeconds"/>
        public static bool UseSeconds(this TimePicker timePicker) =>
            timePicker != null ? timePicker.UseSeconds : false;

        /// <inheritdoc cref="TimePicker.UseSeconds"/>
        public static void SetUseSeconds(this TimePicker timePicker, bool useSeconds)
        {
            if (timePicker is not null)
                timePicker.UseSeconds = useSeconds;
        }

        /// <inheritdoc cref="TimePicker.SelectedTime"/>
        public static TimeSpan? SelectedTime(this TimePicker timePicker) =>
            timePicker != null ? timePicker.SelectedTime : null;

        /// <inheritdoc cref="TimePicker.SelectedTime"/>
        public static void SetSelectedTime(this TimePicker timePicker, TimeSpan? selectedTime)
        {
            if (timePicker is not null)
                timePicker.SelectedTime = selectedTime;
        }

        /// <inheritdoc cref="TimePicker.Clear"/>
        public static void Clear(this TimePicker timePicker)
        {
            timePicker?.Clear();
        }
    }
}
