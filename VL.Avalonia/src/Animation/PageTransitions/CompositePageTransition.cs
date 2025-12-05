using Avalonia.Animation;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Animation.PageTransitions
{
    [ProcessNode(Name = "CompositePageTransition")]
    public class CompositePageTransitionWrapper<T>
        where T : CompositePageTransition, new()
    {
        protected readonly T _output = new();
        public T Output => _output;

        protected Spread<IPageTransition> _pageTransitions;

        public void SetPageTransitions(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<IPageTransition> pageTransitions
        )
        {
            if (_pageTransitions != pageTransitions)
            {
                if (pageTransitions != null)
                {
                    var transitions = new List<IPageTransition>();
                    foreach (var transition in pageTransitions)
                    {
                        if (transition != null)
                            transitions.Add(transition);
                    }

                    _output.PageTransitions = transitions;
                }
                else
                {
                    _output.PageTransitions.Clear();
                }
            }
        }
    }
}
