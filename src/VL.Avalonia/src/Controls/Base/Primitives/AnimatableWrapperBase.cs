using Avalonia.Animation;
using Avalonia.Controls;
using VL.Avalonia.Animation.Transitions;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls.Base.Primitives
{
    [ProcessNode]
    public abstract class AnimatableWrapperBase<T> where T : Animatable, new()
    {
        protected readonly T _output = new();
        public T Output => _output;

        #region Animatable

        private Spread<IAvaloniaTransition> _transitions;
        public void SetTransition([Pin(Visibility = Model.PinVisibility.Optional)] Spread<IAvaloniaTransition> transitions)
        {
            if (_transitions != transitions)
            {
                if (transitions != null)
                {
                    var t = new Transitions();
                    foreach (IAvaloniaTransition builder in transitions)
                        if (builder != null)
                        {
                            var success = builder.TryBuildTransition(_output, out var transition);
                            if (success)
                            {
                                t.Add(transition);
                            }
                        }


                    _output.SetValue(Control.TransitionsProperty, t);
                }
                else
                {
                    _output.ClearValue(Control.TransitionsProperty);
                }

                _transitions = transitions;
            }
        }
        #endregion
    }
}
