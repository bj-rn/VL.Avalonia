using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Animation
{
    /// <summary>
    /// Workaround interface to support transition building at runtime
    /// </summary>
    public interface IAvaloniaTransition
    {
        /// <summary>
        /// Function we gonna call in ControlNodeBase to get AvaloniaProperty by name from registry
        /// </summary>
        /// <param name="owner">Instance of Control</param>
        /// <param name="transition">Transition</param>
        public bool TryBuildTransition(AvaloniaObject owner, out TransitionBase transition);
    }

    /// <summary>Base wrapper for <see cref="TransitionBase"/></summary>
    [ProcessNode(HasStateOutput = true)]
    public abstract partial class TransitionBaseNodeBase<T> : IAvaloniaTransition
        where T : TransitionBase, new()
    {
        private readonly T _output = new();

        public Optional<string> Property { internal get; set; }

        /// <summary>Sets the duration.</summary>
        [ImplementProperty(typeof(TransitionBase), nameof(TransitionBase.DurationProperty))]
        private Optional<TimeSpan> _duration;

        /// <summary>Sets the delay.</summary>
        [ImplementProperty(typeof(TransitionBase), nameof(TransitionBase.DelayProperty))]
        private Optional<TimeSpan> _delay;

        private Optional<Easing> _easing;

        /// <param name="easing">The easing to set.</param>
        public void SetEasing([Pin(Visibility = PinVisibility.Visible)] Optional<Easing> easing)
        {
            if (_easing != easing)
            {
                _easing = easing;

                if (easing.HasValue)
                {
                    _output.SetValue(TransitionBase.EasingProperty, easing.Value);
                }
                else
                {
                    _output.SetValue(TransitionBase.EasingProperty, new LinearEasing());
                }
            }
        }

        [Fragment(IsHidden = true)]
        public bool TryBuildTransition(AvaloniaObject owner, out TransitionBase transition)
        {
            if (Property.HasValue && owner != null)
            {
                var property = AvaloniaPropertyRegistry.Instance.FindRegistered(
                    owner.GetType(),
                    Property.Value
                );

                if (property != null)
                {
                    _output.SetValue(TransitionBase.PropertyProperty, property);
                    transition = _output;

                    return true;
                }
            }

            transition = null;
            return false;
        }
    }
}
