using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="DatePicker"/>.
    /// </summary>
    public static class DatePickerExtensions
    {
        /// <inheritdoc cref="DatePicker.DayFormat"/>
        public static string DayFormat(this DatePicker datePicker) =>
            datePicker != null ? datePicker.DayFormat : "%d";

        /// <inheritdoc cref="DatePicker.DayFormat"/>
        public static void SetDayFormat(this DatePicker datePicker, string dayFormat)
        {
            if (datePicker is not null)
                datePicker.DayFormat = dayFormat;
        }

        /// <inheritdoc cref="DatePicker.DayVisible"/>
        public static bool DayVisible(this DatePicker datePicker) =>
            datePicker != null ? datePicker.DayVisible : true;

        /// <inheritdoc cref="DatePicker.DayVisible"/>
        public static void SetDayVisible(this DatePicker datePicker, bool dayVisible)
        {
            if (datePicker is not null)
                datePicker.DayVisible = dayVisible;
        }

        /// <inheritdoc cref="DatePicker.MaxYear"/>
        public static DateTimeOffset MaxYear(this DatePicker datePicker) =>
            datePicker != null ? datePicker.MaxYear : DateTimeOffset.MaxValue;

        /// <inheritdoc cref="DatePicker.MaxYear"/>
        public static void SetMaxYear(this DatePicker datePicker, DateTimeOffset maxYear)
        {
            if (datePicker is not null)
                datePicker.MaxYear = maxYear;
        }

        /// <inheritdoc cref="DatePicker.MinYear"/>
        public static DateTimeOffset MinYear(this DatePicker datePicker) =>
            datePicker != null ? datePicker.MinYear : DateTimeOffset.MinValue;

        /// <inheritdoc cref="DatePicker.MinYear"/>
        public static void SetMinYear(this DatePicker datePicker, DateTimeOffset minYear)
        {
            if (datePicker is not null)
                datePicker.MinYear = minYear;
        }

        /// <inheritdoc cref="DatePicker.MonthFormat"/>
        public static string MonthFormat(this DatePicker datePicker) =>
            datePicker != null ? datePicker.MonthFormat : "MMMM";

        /// <inheritdoc cref="DatePicker.MonthFormat"/>
        public static void SetMonthFormat(this DatePicker datePicker, string monthFormat)
        {
            if (datePicker is not null)
                datePicker.MonthFormat = monthFormat;
        }

        /// <inheritdoc cref="DatePicker.MonthVisible"/>
        public static bool MonthVisible(this DatePicker datePicker) =>
            datePicker != null ? datePicker.MonthVisible : true;

        /// <inheritdoc cref="DatePicker.MonthVisible"/>
        public static void SetMonthVisible(this DatePicker datePicker, bool monthVisible)
        {
            if (datePicker is not null)
                datePicker.MonthVisible = monthVisible;
        }

        /// <inheritdoc cref="DatePicker.YearFormat"/>
        public static string YearFormat(this DatePicker datePicker) =>
            datePicker != null ? datePicker.YearFormat : "yyyy";

        /// <inheritdoc cref="DatePicker.YearFormat"/>
        public static void SetYearFormat(this DatePicker datePicker, string yearFormat)
        {
            if (datePicker is not null)
                datePicker.YearFormat = yearFormat;
        }

        /// <inheritdoc cref="DatePicker.YearVisible"/>
        public static bool YearVisible(this DatePicker datePicker) =>
            datePicker != null ? datePicker.YearVisible : true;

        /// <inheritdoc cref="DatePicker.YearVisible"/>
        public static void SetYearVisible(this DatePicker datePicker, bool yearVisible)
        {
            if (datePicker is not null)
                datePicker.YearVisible = yearVisible;
        }

        /// <inheritdoc cref="DatePicker.SelectedDate"/>
        public static DateTimeOffset? SelectedDate(this DatePicker datePicker) =>
            datePicker != null ? datePicker.SelectedDate : null;

        /// <inheritdoc cref="DatePicker.SelectedDate"/>
        public static void SetSelectedDate(this DatePicker datePicker, DateTimeOffset? selectedDate)
        {
            if (datePicker is not null)
                datePicker.SelectedDate = selectedDate;
        }

        /// <inheritdoc cref="DatePicker.Clear"/>
        public static void Clear(this DatePicker datePicker)
        {
            datePicker?.Clear();
        }

        // AttachedProperties:
        // No attached properties were found in the provided file.
    }
}
