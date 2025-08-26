using Avalonia;
using Avalonia.Styling;

namespace VL.Avalonia.Styles
{
    /// <summary>
    /// Interface to manage style setters via chain
    /// </summary>
    public interface IAvaloniaStyle
    {
        public Style BuildStyle(StyledElement owner, Style style);
    }
}
