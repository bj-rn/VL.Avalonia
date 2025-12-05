using Avalonia;
using Avalonia.Styling;

namespace VL.Avalonia.Styles
{
    /// <summary>
    /// Helper abstraction to generate Style with Selector using immutability.
    /// </summary>
    /// <param name="Input">Upstream style setters</param>
    /// <param name="Selector">Style selector</param>
    /// <param name="SelectorStyle"></param>
    public record struct ImmutableSelector(
        IAvaloniaStyle? Input,
        Selector? Selector,
        IAvaloniaStyle? SelectorStyle
    ) : IAvaloniaStyle
    {
        public Style BuildStyle(StyledElement owner, Style style)
        {
            if (SelectorStyle != null)
            {
                var selectorStyle = SelectorStyle.BuildStyle(
                    owner,
                    new Style() { Selector = Selector }
                );

                style.Add(selectorStyle);
            }

            Input?.BuildStyle(owner, style);

            return style;
        }
    }
}
