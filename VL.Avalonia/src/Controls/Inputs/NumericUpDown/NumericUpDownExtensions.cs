using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Media;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="NumericUpDown"/>.
    /// </summary>
    public static class NumericUpDownExtensions
    {
        /// <inheritdoc cref="NumericUpDown.AllowSpin"/>
        public static bool AllowSpin(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.AllowSpin : true;

        /// <inheritdoc cref="NumericUpDown.AllowSpin"/>
        public static void SetAllowSpin(this NumericUpDown numericUpDown, bool allowSpin)
        {
            if (numericUpDown is not null)
                numericUpDown.AllowSpin = allowSpin;
        }

        /// <inheritdoc cref="NumericUpDown.ButtonSpinnerLocation"/>
        public static Location ButtonSpinnerLocation(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.ButtonSpinnerLocation : Location.Right;

        /// <inheritdoc cref="NumericUpDown.ButtonSpinnerLocation"/>
        public static void SetButtonSpinnerLocation(
            this NumericUpDown numericUpDown,
            Location buttonSpinnerLocation
        )
        {
            if (numericUpDown is not null)
                numericUpDown.ButtonSpinnerLocation = buttonSpinnerLocation;
        }

        /// <inheritdoc cref="NumericUpDown.ShowButtonSpinner"/>
        public static bool ShowButtonSpinner(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.ShowButtonSpinner : true;

        /// <inheritdoc cref="NumericUpDown.ShowButtonSpinner"/>
        public static void SetShowButtonSpinner(
            this NumericUpDown numericUpDown,
            bool showButtonSpinner
        )
        {
            if (numericUpDown is not null)
                numericUpDown.ShowButtonSpinner = showButtonSpinner;
        }

        /// <inheritdoc cref="NumericUpDown.ClipValueToMinMax"/>
        public static bool ClipValueToMinMax(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.ClipValueToMinMax : false;

        /// <inheritdoc cref="NumericUpDown.ClipValueToMinMax"/>
        public static void SetClipValueToMinMax(
            this NumericUpDown numericUpDown,
            bool clipValueToMinMax
        )
        {
            if (numericUpDown is not null)
                numericUpDown.ClipValueToMinMax = clipValueToMinMax;
        }

        /// <inheritdoc cref="NumericUpDown.NumberFormat"/>
        public static NumberFormatInfo? NumberFormat(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.NumberFormat : NumberFormatInfo.CurrentInfo;

        /// <inheritdoc cref="NumericUpDown.NumberFormat"/>
        public static void SetNumberFormat(
            this NumericUpDown numericUpDown,
            NumberFormatInfo? numberFormat
        )
        {
            if (numericUpDown is not null)
                numericUpDown.NumberFormat = numberFormat;
        }

        /// <inheritdoc cref="NumericUpDown.FormatString"/>
        public static string FormatString(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.FormatString : string.Empty;

        /// <inheritdoc cref="NumericUpDown.FormatString"/>
        public static void SetFormatString(this NumericUpDown numericUpDown, string formatString)
        {
            if (numericUpDown is not null)
                numericUpDown.FormatString = formatString;
        }

        /// <inheritdoc cref="NumericUpDown.Increment"/>
        public static decimal Increment(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.Increment : 1.0m;

        /// <inheritdoc cref="NumericUpDown.Increment"/>
        public static void SetIncrement(this NumericUpDown numericUpDown, decimal increment)
        {
            if (numericUpDown is not null)
                numericUpDown.Increment = increment;
        }

        /// <inheritdoc cref="NumericUpDown.IsReadOnly"/>
        public static bool IsReadOnly(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.IsReadOnly : false;

        /// <inheritdoc cref="NumericUpDown.IsReadOnly"/>
        public static void SetIsReadOnly(this NumericUpDown numericUpDown, bool isReadOnly)
        {
            if (numericUpDown is not null)
                numericUpDown.IsReadOnly = isReadOnly;
        }

        /// <inheritdoc cref="NumericUpDown.Maximum"/>
        public static decimal Maximum(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.Maximum : decimal.MaxValue;

        /// <inheritdoc cref="NumericUpDown.Maximum"/>
        public static void SetMaximum(this NumericUpDown numericUpDown, decimal maximum)
        {
            if (numericUpDown is not null)
                numericUpDown.Maximum = maximum;
        }

        /// <inheritdoc cref="NumericUpDown.Minimum"/>
        public static decimal Minimum(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.Minimum : decimal.MinValue;

        /// <inheritdoc cref="NumericUpDown.Minimum"/>
        public static void SetMinimum(this NumericUpDown numericUpDown, decimal minimum)
        {
            if (numericUpDown is not null)
                numericUpDown.Minimum = minimum;
        }

        /// <inheritdoc cref="NumericUpDown.ParsingNumberStyle"/>
        public static NumberStyles ParsingNumberStyle(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.ParsingNumberStyle : NumberStyles.Any;

        /// <inheritdoc cref="NumericUpDown.ParsingNumberStyle"/>
        public static void SetParsingNumberStyle(
            this NumericUpDown numericUpDown,
            NumberStyles parsingNumberStyle
        )
        {
            if (numericUpDown is not null)
                numericUpDown.ParsingNumberStyle = parsingNumberStyle;
        }

        /// <inheritdoc cref="NumericUpDown.Text"/>
        public static string? Text(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.Text : null;

        /// <inheritdoc cref="NumericUpDown.Text"/>
        public static void SetText(this NumericUpDown numericUpDown, string? text)
        {
            if (numericUpDown is not null)
                numericUpDown.Text = text;
        }

        /// <inheritdoc cref="NumericUpDown.TextConverter"/>
        public static IValueConverter? TextConverter(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.TextConverter : null;

        /// <inheritdoc cref="NumericUpDown.TextConverter"/>
        public static void SetTextConverter(
            this NumericUpDown numericUpDown,
            IValueConverter? textConverter
        )
        {
            if (numericUpDown is not null)
                numericUpDown.TextConverter = textConverter;
        }

        /// <inheritdoc cref="NumericUpDown.Value"/>
        public static decimal? Value(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.Value : null;

        /// <inheritdoc cref="NumericUpDown.Value"/>
        public static void SetValue(this NumericUpDown numericUpDown, decimal? value)
        {
            if (numericUpDown is not null)
                numericUpDown.Value = value;
        }

        /// <inheritdoc cref="NumericUpDown.Watermark"/>
        public static string? Watermark(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.Watermark : null;

        /// <inheritdoc cref="NumericUpDown.Watermark"/>
        public static void SetWatermark(this NumericUpDown numericUpDown, string? watermark)
        {
            if (numericUpDown is not null)
                numericUpDown.Watermark = watermark;
        }

        /// <inheritdoc cref="NumericUpDown.HorizontalContentAlignment"/>
        public static HorizontalAlignment HorizontalContentAlignment(
            this NumericUpDown numericUpDown
        ) =>
            numericUpDown != null
                ? numericUpDown.HorizontalContentAlignment
                : default(HorizontalAlignment);

        /// <inheritdoc cref="NumericUpDown.HorizontalContentAlignment"/>
        public static void SetHorizontalContentAlignment(
            this NumericUpDown numericUpDown,
            HorizontalAlignment horizontalContentAlignment
        )
        {
            if (numericUpDown is not null)
                numericUpDown.HorizontalContentAlignment = horizontalContentAlignment;
        }

        /// <inheritdoc cref="NumericUpDown.VerticalContentAlignment"/>
        public static VerticalAlignment VerticalContentAlignment(
            this NumericUpDown numericUpDown
        ) =>
            numericUpDown != null
                ? numericUpDown.VerticalContentAlignment
                : default(VerticalAlignment);

        /// <inheritdoc cref="NumericUpDown.VerticalContentAlignment"/>
        public static void SetVerticalContentAlignment(
            this NumericUpDown numericUpDown,
            VerticalAlignment verticalContentAlignment
        )
        {
            if (numericUpDown is not null)
                numericUpDown.VerticalContentAlignment = verticalContentAlignment;
        }

        /// <inheritdoc cref="NumericUpDown.TextAlignment"/>
        public static TextAlignment TextAlignment(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.TextAlignment : default(TextAlignment);

        /// <inheritdoc cref="NumericUpDown.TextAlignment"/>
        public static void SetTextAlignment(
            this NumericUpDown numericUpDown,
            TextAlignment textAlignment
        )
        {
            if (numericUpDown is not null)
                numericUpDown.TextAlignment = textAlignment;
        }

        /// <inheritdoc cref="NumericUpDown.InnerLeftContent"/>
        public static object? InnerLeftContent(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.InnerLeftContent : null;

        /// <inheritdoc cref="NumericUpDown.InnerLeftContent"/>
        public static void SetInnerLeftContent(
            this NumericUpDown numericUpDown,
            object? innerLeftContent
        )
        {
            if (numericUpDown is not null)
                numericUpDown.InnerLeftContent = innerLeftContent;
        }

        /// <inheritdoc cref="NumericUpDown.InnerRightContent"/>
        public static object? InnerRightContent(this NumericUpDown numericUpDown) =>
            numericUpDown != null ? numericUpDown.InnerRightContent : null;

        /// <inheritdoc cref="NumericUpDown.InnerRightContent"/>
        public static void SetInnerRightContent(
            this NumericUpDown numericUpDown,
            object? innerRightContent
        )
        {
            if (numericUpDown is not null)
                numericUpDown.InnerRightContent = innerRightContent;
        }
    }
}
