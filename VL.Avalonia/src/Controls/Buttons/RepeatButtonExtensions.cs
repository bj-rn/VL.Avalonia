using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="RepeatButton"/>.
    /// </summary>
    public static class RepeatButtonExtensions
    {
        /// <inheritdoc cref="RepeatButton.Interval"/>
        public static int Interval(this RepeatButton repeatButton) =>
            repeatButton != null ? repeatButton.Interval : 100;

        /// <inheritdoc cref="RepeatButton.Interval"/>
        public static void SetInterval(this RepeatButton repeatButton, int interval)
        {
            if (repeatButton is not null)
                repeatButton.Interval = interval;
        }

        /// <inheritdoc cref="RepeatButton.Delay"/>
        public static int Delay(this RepeatButton repeatButton) =>
            repeatButton != null ? repeatButton.Delay : 300;

        /// <inheritdoc cref="RepeatButton.Delay"/>
        public static void SetDelay(this RepeatButton repeatButton, int delay)
        {
            if (repeatButton is not null)
                repeatButton.Delay = delay;
        }
    }
}
