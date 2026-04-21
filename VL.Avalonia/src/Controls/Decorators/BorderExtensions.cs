using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaBackgroundSizing = Avalonia.Media.BackgroundSizing;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Border"/>.
    /// </summary>
    public static class BorderExtensions
    {
        /// <inheritdoc cref="Border.Background"/>
        public static IBrush? Background(this Border border) =>
            border != null ? border.Background : null;

        /// <inheritdoc cref="Border.Background"/>
        public static void SetBackground(this Border border, IBrush? background)
        {
            if (border is not null)
                border.Background = background;
        }

        /// <inheritdoc cref="Border.BackgroundSizing"/>
        public static AvaBackgroundSizing BackgroundSizing(this Border border) =>
            border != null ? border.BackgroundSizing : AvaBackgroundSizing.CenterBorder;

        /// <inheritdoc cref="Border.BackgroundSizing"/>
        public static void SetBackgroundSizing(
            this Border border,
            AvaBackgroundSizing backgroundSizing
        )
        {
            if (border is not null)
                border.BackgroundSizing = backgroundSizing;
        }

        /// <inheritdoc cref="Border.BorderBrush"/>
        public static IBrush? BorderBrush(this Border border) =>
            border != null ? border.BorderBrush : null;

        /// <inheritdoc cref="Border.BorderBrush"/>
        public static void SetBorderBrush(this Border border, IBrush? borderBrush)
        {
            if (border is not null)
                border.BorderBrush = borderBrush;
        }

        /// <inheritdoc cref="Border.BorderThickness"/>
        public static Thickness BorderThickness(this Border border) =>
            border != null ? border.BorderThickness : new Thickness();

        /// <inheritdoc cref="Border.BorderThickness"/>
        public static void SetBorderThickness(this Border border, Thickness borderThickness)
        {
            if (border is not null)
                border.BorderThickness = borderThickness;
        }

        /// <inheritdoc cref="Border.CornerRadius"/>
        public static CornerRadius CornerRadius(this Border border) =>
            border != null ? border.CornerRadius : new CornerRadius();

        /// <inheritdoc cref="Border.CornerRadius"/>
        public static void SetCornerRadius(this Border border, CornerRadius cornerRadius)
        {
            if (border is not null)
                border.CornerRadius = cornerRadius;
        }

        /// <inheritdoc cref="Border.BoxShadow"/>
        public static BoxShadows BoxShadow(this Border border) =>
            border != null ? border.BoxShadow : new BoxShadows();

        /// <inheritdoc cref="Border.BoxShadow"/>
        public static void SetBoxShadow(this Border border, BoxShadows boxShadow)
        {
            if (border is not null)
                border.BoxShadow = boxShadow;
        }

        /// <inheritdoc cref="Border.ClipToBoundsRadius"/>
        public static CornerRadius ClipToBoundsRadius(this Border border) =>
            border != null ? border.ClipToBoundsRadius : new CornerRadius();
    }
}
