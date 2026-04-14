using Avalonia;
using Avalonia.Layout;
using Stride.Core.Mathematics;
using AvaHorizontalAlignment = Avalonia.Layout.HorizontalAlignment;
using AvaVerticalAlignment = Avalonia.Layout.VerticalAlignment;

namespace VL.Avalonia.Layout
{
    /// <summary>
    /// Extension methods for <see cref="Layoutable"/>.
    /// </summary>
    public static class LayoutableExtensions
    {
        /// <inheritdoc cref="Layoutable.Width"/>
        public static float Width(this Layoutable layoutable) =>
            layoutable != null ? (float)layoutable.Width : float.NaN;

        /// <inheritdoc cref="Layoutable.Width"/>
        public static void SetWidth(this Layoutable layoutable, float width)
        {
            if (layoutable is not null)
                layoutable.Width = width;
        }

        /// <inheritdoc cref="Layoutable.Height"/>
        public static float Height(this Layoutable layoutable) =>
            layoutable != null ? (float)layoutable.Height : float.NaN;

        /// <inheritdoc cref="Layoutable.Height"/>
        public static void SetHeight(this Layoutable layoutable, float height)
        {
            if (layoutable is not null)
                layoutable.Height = height;
        }

        /// <inheritdoc cref="Layoutable.MinWidth"/>
        public static float MinWidth(this Layoutable layoutable) =>
            layoutable != null ? (float)layoutable.MinWidth : 0f;

        /// <inheritdoc cref="Layoutable.MinWidth"/>
        public static void SetMinWidth(this Layoutable layoutable, float minWidth)
        {
            if (layoutable is not null)
                layoutable.MinWidth = minWidth;
        }

        /// <inheritdoc cref="Layoutable.MaxWidth"/>
        public static float MaxWidth(this Layoutable layoutable) =>
            layoutable != null ? (float)layoutable.MaxWidth : float.PositiveInfinity;

        /// <inheritdoc cref="Layoutable.MaxWidth"/>
        public static void SetMaxWidth(this Layoutable layoutable, float maxWidth)
        {
            if (layoutable is not null)
                layoutable.MaxWidth = maxWidth;
        }

        /// <inheritdoc cref="Layoutable.MinHeight"/>
        public static float MinHeight(this Layoutable layoutable) =>
            layoutable != null ? (float)layoutable.MinHeight : 0f;

        /// <inheritdoc cref="Layoutable.MinHeight"/>
        public static void SetMinHeight(this Layoutable layoutable, float minHeight)
        {
            if (layoutable is not null)
                layoutable.MinHeight = minHeight;
        }

        /// <inheritdoc cref="Layoutable.MaxHeight"/>
        public static float MaxHeight(this Layoutable layoutable) =>
            layoutable != null ? (float)layoutable.MaxHeight : float.PositiveInfinity;

        /// <inheritdoc cref="Layoutable.MaxHeight"/>
        public static void SetMaxHeight(this Layoutable layoutable, float maxHeight)
        {
            if (layoutable is not null)
                layoutable.MaxHeight = maxHeight;
        }

        /// <inheritdoc cref="Layoutable.Margin"/>
        public static Thickness Margin(this Layoutable layoutable) => layoutable?.Margin ?? default;

        /// <inheritdoc cref="Layoutable.Margin"/>
        public static void SetMargin(this Layoutable layoutable, Thickness margin)
        {
            if (layoutable is not null)
                layoutable.Margin = margin;
        }

        /// <inheritdoc cref="Layoutable.HorizontalAlignment"/>
        public static AvaHorizontalAlignment HorizontalAlignment(this Layoutable layoutable) =>
            layoutable?.HorizontalAlignment ?? AvaHorizontalAlignment.Stretch;

        /// <inheritdoc cref="Layoutable.HorizontalAlignment"/>
        public static void SetHorizontalAlignment(
            this Layoutable layoutable,
            AvaHorizontalAlignment horizontalAlignment
        )
        {
            if (layoutable is not null)
                layoutable.HorizontalAlignment = horizontalAlignment;
        }

        /// <inheritdoc cref="Layoutable.VerticalAlignment"/>
        public static AvaVerticalAlignment VerticalAlignment(this Layoutable layoutable) =>
            layoutable?.VerticalAlignment ?? AvaVerticalAlignment.Stretch;

        /// <inheritdoc cref="Layoutable.VerticalAlignment"/>
        public static void SetVerticalAlignment(
            this Layoutable layoutable,
            AvaVerticalAlignment verticalAlignment
        )
        {
            if (layoutable is not null)
                layoutable.VerticalAlignment = verticalAlignment;
        }

        /// <inheritdoc cref="Layoutable.UseLayoutRounding"/>
        public static bool UseLayoutRounding(this Layoutable layoutable) =>
            layoutable?.UseLayoutRounding ?? true;

        /// <inheritdoc cref="Layoutable.UseLayoutRounding"/>
        public static void SetUseLayoutRounding(this Layoutable layoutable, bool useLayoutRounding)
        {
            if (layoutable is not null)
                layoutable.UseLayoutRounding = useLayoutRounding;
        }

        /// <inheritdoc cref="Layoutable.DesiredSize"/>
        public static Vector2 DesiredSize(this Layoutable layoutable)
        {
            if (layoutable == null)
                return Vector2.Zero;

            return new Vector2(
                (float)layoutable.DesiredSize.Width,
                (float)layoutable.DesiredSize.Height
            );
        }

        /// <inheritdoc cref="Layoutable.IsMeasureValid"/>
        public static bool IsMeasureValid(this Layoutable layoutable) =>
            layoutable?.IsMeasureValid ?? false;

        /// <inheritdoc cref="Layoutable.IsArrangeValid"/>
        public static bool IsArrangeValid(this Layoutable layoutable) =>
            layoutable?.IsArrangeValid ?? false;

        /// <inheritdoc cref="Layoutable.UpdateLayout"/>
        public static void UpdateLayout(this Layoutable layoutable)
        {
            layoutable?.UpdateLayout();
        }

        /// <inheritdoc cref="Layoutable.Measure(Size)"/>
        public static void Measure(this Layoutable layoutable, Vector2 availableSize)
        {
            layoutable?.Measure(new Size(availableSize.X, availableSize.Y));
        }

        /// <inheritdoc cref="Layoutable.Arrange(Rect)"/>
        public static void Arrange(this Layoutable layoutable, RectangleF rect)
        {
            layoutable?.Arrange(new Rect(rect.X, rect.Y, rect.Width, rect.Height));
        }

        /// <inheritdoc cref="Layoutable.InvalidateMeasure"/>
        public static void InvalidateMeasure(this Layoutable layoutable)
        {
            layoutable?.InvalidateMeasure();
        }

        /// <inheritdoc cref="Layoutable.InvalidateArrange"/>
        public static void InvalidateArrange(this Layoutable layoutable)
        {
            layoutable?.InvalidateArrange();
        }
    }
}
