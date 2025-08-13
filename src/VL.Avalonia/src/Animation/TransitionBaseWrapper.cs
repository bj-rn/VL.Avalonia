using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Animation.Transitions
{
    /// <summary>
    /// Workaround interface to support transition building at runtime
    /// </summary>
    public interface IAvaloniaTransition
    {
        /// <summary>
        /// Function we gonna call in ControlWrapperBase to get AvaloniaProperty by name from registry
        /// </summary>
        /// <param name="owner">Instance of Control</param>
        /// <param name="transition">Transition</param>
        public bool TryBuildTransition(AvaloniaObject owner, out TransitionBase transition);
    }

    [ProcessNode(HasStateOutput = true)]
    public abstract partial class TransitionBaseWrapper<T> : IAvaloniaTransition where T : TransitionBase, new()
    {
        protected readonly T _output = new();

        public Optional<string> Property { internal get; set; }

        [ImplementProperty("TransitionBase.DurationProperty")]
        protected Optional<TimeSpan> _duration;

        [ImplementProperty("TransitionBase.DelayProperty")]
        protected Optional<TimeSpan> _delay;

        protected Optional<Easing> _easing;
        public void SetEasing([Pin(Visibility = Model.PinVisibility.Visible)] Optional<Easing> easing)
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
                var property = AvaloniaPropertyRegistry.Instance.FindRegistered(owner.GetType(), Property.Value);

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
