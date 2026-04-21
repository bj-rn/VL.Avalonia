using Avalonia.Controls.Primitives;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="RangeBase"/>.
    /// </summary>
    public static class RangeBaseExtensions
    {
        /// <inheritdoc cref="RangeBase.Minimum"/>
        public static float Minimum(this RangeBase rangeBase) =>
            rangeBase != null ? (float)rangeBase.Minimum : 0f;

        /// <inheritdoc cref="RangeBase.Minimum"/>
        public static void SetMinimum(this RangeBase rangeBase, float minimum)
        {
            if (rangeBase is not null)
                rangeBase.Minimum = minimum;
        }

        /// <inheritdoc cref="RangeBase.Maximum"/>
        public static float Maximum(this RangeBase rangeBase) =>
            rangeBase != null ? (float)rangeBase.Maximum : 100f;

        /// <inheritdoc cref="RangeBase.Maximum"/>
        public static void SetMaximum(this RangeBase rangeBase, float maximum)
        {
            if (rangeBase is not null)
                rangeBase.Maximum = maximum;
        }

        /// <inheritdoc cref="RangeBase.Value"/>
        public static float Value(this RangeBase rangeBase) =>
            rangeBase != null ? (float)rangeBase.Value : 0f;

        /// <inheritdoc cref="RangeBase.Value"/>
        public static void SetValue(this RangeBase rangeBase, float value)
        {
            if (rangeBase is not null)
                rangeBase.Value = value;
        }

        /// <inheritdoc cref="RangeBase.SmallChange"/>
        public static float SmallChange(this RangeBase rangeBase) =>
            rangeBase != null ? (float)rangeBase.SmallChange : 1f;

        /// <inheritdoc cref="RangeBase.SmallChange"/>
        public static void SetSmallChange(this RangeBase rangeBase, float smallChange)
        {
            if (rangeBase is not null)
                rangeBase.SmallChange = smallChange;
        }

        /// <inheritdoc cref="RangeBase.LargeChange"/>
        public static float LargeChange(this RangeBase rangeBase) =>
            rangeBase != null ? (float)rangeBase.LargeChange : 10f;

        /// <inheritdoc cref="RangeBase.LargeChange"/>
        public static void SetLargeChange(this RangeBase rangeBase, float largeChange)
        {
            if (rangeBase is not null)
                rangeBase.LargeChange = largeChange;
        }
    }
}
