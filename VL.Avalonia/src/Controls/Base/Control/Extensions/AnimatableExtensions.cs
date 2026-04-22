using Avalonia.Animation;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Animatable"/>.
    /// </summary>
    public static class AnimatableExtensions
    {
        /// <inheritdoc cref="Animatable.Transitions"/>
        public static
        // -----------------------/
        // TODO: NEEDS CONVERSIONS
        // -----------------------/
        Transitions? Transitions(this Animatable animatable) => animatable?.Transitions;

        /// <inheritdoc cref="Animatable.Transitions"/>
        public static void SetTransitions(
            this Animatable animatable,
            // -----------------------/
            // TODO: NEEDS CONVERSIONS
            // -----------------------/
            Transitions? transitions
        )
        {
            if (animatable is not null)
                animatable.Transitions = transitions;
        }
    }
}
