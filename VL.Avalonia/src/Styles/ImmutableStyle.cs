using Avalonia;
using Avalonia.Animation;
using Avalonia.Styling;
using VL.Avalonia.Animation.Transitions;
using VL.Avalonia.Helpers;
using VL.Lib.Collections;

namespace VL.Avalonia.Styles
{
    /// <summary>
    /// Helper abstraction to manage style chain update using immutability.
    /// </summary>
    /// <typeparam name="T">Type of property</typeparam>
    /// <param name="Input">Upstream style setters</param>
    /// <param name="StyleValue">Setter property value</param>
    /// <param name="StyleName">Setter property name</param>
    public record struct ImmutableSetter<T>(IAvaloniaStyle? Input, T? StyleValue, string StyleName)
        : IAvaloniaStyle
    {
        public Style BuildStyle(StyledElement owner, Style style)
        {
            if (StyleValue != null)
            {
                // Transitions should be built for control
                // so seems only place to add special case
                if (StyleValue is Spread<IAvaloniaTransition> transitions)
                {
                    var transitionsCollection = new Transitions();

                    if (transitions != null)
                    {
                        foreach (var transition in transitions)
                        {
                            if (transition != null)
                            {
                                if (transition.TryBuildTransition(owner, out var trs))
                                    transitionsCollection.Add(trs);
                            }
                        }

                        style.TryAddSetter(owner, StyleName, transitionsCollection);

                        Input?.BuildStyle(owner, style);
                        return style;
                    }
                }

                style.TryAddSetter(owner, StyleName, StyleValue);
            }

            Input?.BuildStyle(owner, style);
            return style;
        }
    }
}
