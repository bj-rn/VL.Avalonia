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
    /// <typeparam name="T">Type of value</typeparam>
    /// <param name="Input">Next style in chain</param>
    /// <param name="StyleValue">Value of style</param>
    /// <param name="StyleName">Name of property</param>
    public record struct ImmutableStyle<T>(IAvaloniaStyle? Input, T? StyleValue, string StyleName) : IAvaloniaStyle
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
