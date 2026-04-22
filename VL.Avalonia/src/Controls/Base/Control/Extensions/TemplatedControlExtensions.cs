using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="TemplatedControlExtensions"/>.
    /// </summary>
    public static class TemplatedControlExtensions
    {
        /// <inheritdoc cref="TemplatedControl.Background"/>
        public static IBrush? Background(this TemplatedControl control) => control?.Background;

        /// <inheritdoc cref="TemplatedControl.Background"/>
        public static void SetBackground(this TemplatedControl control, IBrush? background)
        {
            if (control is not null)
                control.Background = background;
        }

        /// <inheritdoc cref="TemplatedControl.BackgroundSizing"/>
        public static BackgroundSizing BackgroundSizing(this TemplatedControl control) =>
            control?.BackgroundSizing ?? default;

        /// <inheritdoc cref="TemplatedControl.BackgroundSizing"/>
        public static void SetBackgroundSizing(
            this TemplatedControl control,
            BackgroundSizing backgroundSizing
        )
        {
            if (control is not null)
                control.BackgroundSizing = backgroundSizing;
        }

        /// <inheritdoc cref="TemplatedControl.BorderBrush"/>
        public static IBrush? BorderBrush(this TemplatedControl control) => control?.BorderBrush;

        /// <inheritdoc cref="TemplatedControl.BorderBrush"/>
        public static void SetBorderBrush(this TemplatedControl control, IBrush? borderBrush)
        {
            if (control is not null)
                control.BorderBrush = borderBrush;
        }

        /// <inheritdoc cref="TemplatedControl.BorderThickness"/>
        public static Thickness BorderThickness(this TemplatedControl control) =>
            control?.BorderThickness ?? default;

        /// <inheritdoc cref="TemplatedControl.BorderThickness"/>
        public static void SetBorderThickness(
            this TemplatedControl control,
            Thickness borderThickness
        )
        {
            if (control is not null)
                control.BorderThickness = borderThickness;
        }

        /// <inheritdoc cref="TemplatedControl.CornerRadius"/>
        public static CornerRadius CornerRadius(this TemplatedControl control) =>
            control?.CornerRadius ?? default;

        /// <inheritdoc cref="TemplatedControl.CornerRadius"/>
        public static void SetCornerRadius(this TemplatedControl control, CornerRadius cornerRadius)
        {
            if (control is not null)
                control.CornerRadius = cornerRadius;
        }

        /// <inheritdoc cref="TemplatedControl.FontFamily"/>
        public static FontFamily FontFamily(this TemplatedControl control) =>
            control?.FontFamily ?? default!;

        /// <inheritdoc cref="TemplatedControl.FontFamily"/>
        public static void SetFontFamily(this TemplatedControl control, FontFamily fontFamily)
        {
            if (control is not null)
                control.FontFamily = fontFamily;
        }

        /// <inheritdoc cref="TemplatedControl.FontFeatures"/>
        public static FontFeatureCollection? FontFeatures(this TemplatedControl control) =>
            control?.FontFeatures;

        /// <inheritdoc cref="TemplatedControl.FontFeatures"/>
        public static void SetFontFeatures(
            this TemplatedControl control,
            FontFeatureCollection? fontFeatures
        )
        {
            if (control is not null)
                control.FontFeatures = fontFeatures;
        }

        /// <inheritdoc cref="TemplatedControl.FontSize"/>
        public static float FontSize(this TemplatedControl control) =>
            (float)(control?.FontSize ?? 0.0);

        /// <inheritdoc cref="TemplatedControl.FontSize"/>
        public static void SetFontSize(this TemplatedControl control, float fontSize)
        {
            if (control is not null)
                control.FontSize = fontSize;
        }

        /// <inheritdoc cref="TemplatedControl.FontStyle"/>
        public static FontStyle FontStyle(this TemplatedControl control) =>
            control?.FontStyle ?? default;

        /// <inheritdoc cref="TemplatedControl.FontStyle"/>
        public static void SetFontStyle(this TemplatedControl control, FontStyle fontStyle)
        {
            if (control is not null)
                control.FontStyle = fontStyle;
        }

        /// <inheritdoc cref="TemplatedControl.FontWeight"/>
        public static FontWeight FontWeight(this TemplatedControl control) =>
            control?.FontWeight ?? default;

        /// <inheritdoc cref="TemplatedControl.FontWeight"/>
        public static void SetFontWeight(this TemplatedControl control, FontWeight fontWeight)
        {
            if (control is not null)
                control.FontWeight = fontWeight;
        }

        /// <inheritdoc cref="TemplatedControl.FontStretch"/>
        public static FontStretch FontStretch(this TemplatedControl control) =>
            control?.FontStretch ?? default;

        /// <inheritdoc cref="TemplatedControl.FontStretch"/>
        public static void SetFontStretch(this TemplatedControl control, FontStretch fontStretch)
        {
            if (control is not null)
                control.FontStretch = fontStretch;
        }

        /// <inheritdoc cref="TemplatedControl.Foreground"/>
        public static IBrush? Foreground(this TemplatedControl control) => control?.Foreground;

        /// <inheritdoc cref="TemplatedControl.Foreground"/>
        public static void SetForeground(this TemplatedControl control, IBrush? foreground)
        {
            if (control is not null)
                control.Foreground = foreground;
        }

        /// <inheritdoc cref="TemplatedControl.Padding"/>
        public static Thickness Padding(this TemplatedControl control) =>
            control?.Padding ?? default;

        /// <inheritdoc cref="TemplatedControl.Padding"/>
        public static void SetPadding(this TemplatedControl control, Thickness padding)
        {
            if (control is not null)
                control.Padding = padding;
        }

        /// <inheritdoc cref="TemplatedControl.Template"/>
        public static IControlTemplate? Template(this TemplatedControl control) =>
            control?.Template;

        /// <inheritdoc cref="TemplatedControl.Template"/>
        public static void SetTemplate(this TemplatedControl control, IControlTemplate? template)
        {
            if (control is not null)
                control.Template = template;
        }
    }
}
