using Avalonia.Collections;
using Avalonia.Controls;
using AvaOrientation = Avalonia.Layout.Orientation;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Slider"/>.
    /// </summary>
    public static class SliderExtensions
    {
        /// <inheritdoc cref="Slider.Orientation"/>
        public static AvaOrientation Orientation(this Slider slider) =>
            slider != null ? slider.Orientation : default(AvaOrientation);

        /// <inheritdoc cref="Slider.Orientation"/>
        public static void SetOrientation(this Slider slider, AvaOrientation orientation)
        {
            if (slider is not null)
                slider.Orientation = orientation;
        }

        /// <inheritdoc cref="Slider.IsDirectionReversed"/>
        public static bool IsDirectionReversed(this Slider slider) =>
            slider != null ? slider.IsDirectionReversed : false;

        /// <inheritdoc cref="Slider.IsDirectionReversed"/>
        public static void SetIsDirectionReversed(this Slider slider, bool isDirectionReversed)
        {
            if (slider is not null)
                slider.IsDirectionReversed = isDirectionReversed;
        }

        /// <inheritdoc cref="Slider.IsSnapToTickEnabled"/>
        public static bool IsSnapToTickEnabled(this Slider slider) =>
            slider != null ? slider.IsSnapToTickEnabled : false;

        /// <inheritdoc cref="Slider.IsSnapToTickEnabled"/>
        public static void SetIsSnapToTickEnabled(this Slider slider, bool isSnapToTickEnabled)
        {
            if (slider is not null)
                slider.IsSnapToTickEnabled = isSnapToTickEnabled;
        }

        /// <inheritdoc cref="Slider.TickFrequency"/>
        public static float TickFrequency(this Slider slider) =>
            slider != null ? (float)slider.TickFrequency : 0.0f;

        /// <inheritdoc cref="Slider.TickFrequency"/>
        public static void SetTickFrequency(this Slider slider, float tickFrequency)
        {
            if (slider is not null)
                slider.TickFrequency = tickFrequency;
        }

        /// <inheritdoc cref="Slider.TickPlacement"/>
        public static TickPlacement TickPlacement(this Slider slider) =>
            slider != null ? slider.TickPlacement : default(TickPlacement);

        /// <inheritdoc cref="Slider.TickPlacement"/>
        public static void SetTickPlacement(this Slider slider, TickPlacement tickPlacement)
        {
            if (slider is not null)
                slider.TickPlacement = tickPlacement;
        }

        /// <inheritdoc cref="Slider.Ticks"/>
        public static AvaloniaList<double>? Ticks(this Slider slider) =>
            slider != null ? slider.Ticks : null;

        /// <inheritdoc cref="Slider.Ticks"/>
        public static void SetTicks(this Slider slider, AvaloniaList<double>? ticks)
        {
            if (slider is not null)
                slider.Ticks = ticks;
        }
    }
}
