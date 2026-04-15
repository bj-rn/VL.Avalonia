using Avalonia;
using Avalonia.Styling;
using VL.Avalonia.Controls;

namespace VL.Avalonia.Styles
{
    public static class StyleExtensions
    {
        /// <summary>
        /// Adds a new setter to the style if the property exists and isn't already set.
        /// </summary>
        /// <param name="style">The style to modify.</param>
        /// <param name="owner">The element type that registered the property.</param>
        /// <param name="propertyName">The name of the registered property.</param>
        /// <param name="value">The value to assign to the property.</param>
        /// <returns>The original style instance to allow method chaining.</returns>
        public static Style TryAddSetter(
            this Style style,
            StyledElement owner,
            string propertyName,
            object? value
        )
        {
            if (owner.TryGetProperty(propertyName, out var property))
            {
                // Avoids duplicate setter
                if (!style.Setters.Any((s) => (s as Setter)?.Property == property))
                    style.Add(new Setter(property, value));
            }

            return style;
        }
    }
}
