using Avalonia.Animation;
using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="TransitioningContentControl"/>.
    /// </summary>
    public static class TransitioningContentControlExtensions
    {
        /// <inheritdoc cref="TransitioningContentControl.PageTransition"/>
        public static IPageTransition? PageTransition(
            this TransitioningContentControl transitioningContentControl
        ) =>
            transitioningContentControl != null ? transitioningContentControl.PageTransition : null;

        /// <inheritdoc cref="TransitioningContentControl.PageTransition"/>
        public static void SetPageTransition(
            this TransitioningContentControl transitioningContentControl,
            IPageTransition? pageTransition
        )
        {
            if (transitioningContentControl is not null)
                transitioningContentControl.PageTransition = pageTransition;
        }

        /// <inheritdoc cref="TransitioningContentControl.IsTransitionReversed"/>
        public static bool IsTransitionReversed(
            this TransitioningContentControl transitioningContentControl
        ) =>
            transitioningContentControl != null
                ? transitioningContentControl.IsTransitionReversed
                : false;

        /// <inheritdoc cref="TransitioningContentControl.IsTransitionReversed"/>
        public static void SetIsTransitionReversed(
            this TransitioningContentControl transitioningContentControl,
            bool isTransitionReversed
        )
        {
            if (transitioningContentControl is not null)
                transitioningContentControl.IsTransitionReversed = isTransitionReversed;
        }
    }
}
