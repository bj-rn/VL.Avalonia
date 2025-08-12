using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Animation
{
    [ProcessNode]
    public abstract partial class TransitionBaseWrapper<T> where T : TransitionBase, new()
    {
        protected readonly T _output = new();
        public T Output => _output;

        [ImplementProperty("TransitionBase.DurationProperty")]
        protected Optional<TimeSpan> _duration;

        [ImplementProperty("TransitionBase.DelayProperty")]
        protected Optional<TimeSpan> _delay;

        [ImplementProperty("TransitionBase.EasingProperty")]
        protected Optional<Easing> _easing;

        protected Optional<string> _property;
        /// <param name="property">Name of property. Should be set in advance</param>
        [Fragment(Order = PinOrder.Main)]
        public void SetProperty(Optional<string> property)
        {
            if (_property != property)
            {
                if (property.HasValue)
                {
                    var prop = AvaloniaPropertyRegistry.Instance.FindRegistered(typeof(Control), property.Value);

                    _output.SetValue(TransitionBase.PropertyProperty, prop);

                }
                else
                {
                    _output.ClearValue(TransitionBase.PropertyProperty);
                }

                _property = property;
            }
        }
    }
}
