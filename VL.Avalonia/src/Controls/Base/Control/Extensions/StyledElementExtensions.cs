using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using VL.Avalonia.Styles;
using VL.Lib.Collections;
using AvaStyles = Avalonia.Styling.Styles;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="StyledElement"/>.
    /// </summary>
    public static class StyledElementExtensions
    {
        /// <inheritdoc cref="StyledElement.Name"/>
        public static string? Name(this StyledElement styledElement) => styledElement?.Name;

        /// <inheritdoc cref="StyledElement.Name"/>
        public static void SetName(this StyledElement styledElement, string? name)
        {
            if (styledElement is not null)
                styledElement.Name = name;
        }

        /// <inheritdoc cref="StyledElement.Classes"/>
        public static Classes? Classes(this StyledElement styledElement) => styledElement?.Classes;

        /// <inheritdoc cref="StyledElement.DataContext"/>
        public static object? DataContext(this StyledElement styledElement) =>
            styledElement?.DataContext;

        /// <inheritdoc cref="StyledElement.DataContext"/>
        public static void SetDataContext(this StyledElement styledElement, object? dataContext)
        {
            if (styledElement is not null)
                styledElement.DataContext = dataContext;
        }

        /// <inheritdoc cref="StyledElement.IsInitialized"/>
        public static bool IsInitialized(this StyledElement styledElement) =>
            styledElement?.IsInitialized ?? false;

        /// <inheritdoc cref="StyledElement.Styles"/>
        public static AvaStyles? Styles(this StyledElement styledElement) => styledElement?.Styles;

        /// <inheritdoc cref="StyledElement.StyleKey"/>
        public static Type? StyleKey(this StyledElement styledElement) => styledElement?.StyleKey;

        /// <inheritdoc cref="StyledElement.Resources"/>
        public static
        // -----------------------/
        // TODO: NEEDS CONVERSIONS
        // -----------------------/
        IResourceDictionary? Resources(this StyledElement styledElement) =>
            styledElement?.Resources;

        /// <inheritdoc cref="StyledElement.Resources"/>
        public static void SetResources(
            this StyledElement styledElement,
            // -----------------------/
            // TODO: NEEDS CONVERSIONS
            // -----------------------/
            IResourceDictionary resources
        )
        {
            if (styledElement is not null && resources is not null)
                styledElement.Resources = resources;
        }

        /// <inheritdoc cref="StyledElement.TemplatedParent"/>
        public static AvaloniaObject? TemplatedParent(this StyledElement styledElement) =>
            styledElement?.TemplatedParent;

        /// <inheritdoc cref="StyledElement.Theme"/>
        public static ControlTheme? Theme(this StyledElement styledElement) => styledElement?.Theme;

        /// <inheritdoc cref="StyledElement.Theme"/>
        public static void SetTheme(this StyledElement styledElement, ControlTheme? theme)
        {
            if (styledElement is not null)
                styledElement.Theme = theme;
        }

        /// <inheritdoc cref="StyledElement.Parent"/>
        public static StyledElement? Parent(this StyledElement styledElement) =>
            styledElement?.Parent;

        /// <inheritdoc cref="StyledElement.ActualThemeVariant"/>
        public static ThemeVariant? ActualThemeVariant(this StyledElement styledElement) =>
            styledElement?.ActualThemeVariant;

        /// <inheritdoc cref="StyledElement.BeginInit"/>
        public static void BeginInit(this StyledElement styledElement)
        {
            styledElement?.BeginInit();
        }

        /// <inheritdoc cref="StyledElement.EndInit"/>
        public static void EndInit(this StyledElement styledElement)
        {
            styledElement?.EndInit();
        }

        /// <inheritdoc cref="StyledElement.ApplyStyling"/>
        public static bool ApplyStyling(this StyledElement styledElement)
        {
            return styledElement?.ApplyStyling() ?? false;
        }

        /// <inheritdoc cref="StyledElement.TryGetResource(object, ThemeVariant?, out object?)"/>
        public static bool TryGetResource(
            this StyledElement styledElement,
            object key,
            ThemeVariant? theme,
            out object? value
        )
        {
            if (styledElement is not null)
            {
                return styledElement.TryGetResource(key, theme, out value);
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Replaces the element's existing styles with the specified style.
        /// </summary>
        /// <remarks>
        /// All current styles are cleared. If <paramref name="style"/> is <see langword="null"/>, the styles collection is left empty.
        /// </remarks>
        /// <param name="element">The target element.</param>
        /// <param name="style">The new style to apply, or <see langword="null"/> to clear all styles.</param>
        public static void TryUpdateStyles(this StyledElement element, IAvaloniaStyle? style)
        {
            element.Styles.Clear();

            var s = style?.BuildStyle(element, new Style());
            if (s != null)
            {
                element.Styles.Add(s);
            }
        }

        /// <summary>
        /// Replaces the element's existing classes with the specified space-separated class names.
        /// </summary>
        /// <remarks>
        /// All current classes are cleared unconditionally. If <paramref name="classes"/> is <see langword="null"/>, empty, or invalid, the element is left with no classes.
        /// </remarks>
        /// <param name="element">The target element.</param>
        /// <param name="classes">A space-separated string of class names to apply, or <see langword="null"/> to clear all classes.</param>
        public static void TryUpdateClasses(this StyledElement element, string? classes)
        {
            element.Classes.Clear();
            if (classes.TryParseClasses(out var parsedClasses))
            {
                foreach (var cls in parsedClasses)
                {
                    element.Classes.Add(cls);
                }
            }
        }

        /// <summary>
        /// Attempts to parse a space-separated string into individual class names.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="classes">When this method returns, contains the sequence of parsed class names, or an empty sequence if the input is <see langword="null"/> or empty.</param>
        /// <returns><see langword="true"/> if at least one class name was successfully parsed; otherwise, <see langword="false"/>.</returns>
        internal static bool TryParseClasses(this string? input, out IEnumerable<string> classes)
        {
            classes =
                input?.Split(
                    " ",
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                ) ?? [];

            return classes.Any();
        }

        /// <summary>
        /// Attempts to get a registered Avalonia property by name for the specified element's type.
        /// </summary>
        /// <param name="owner">The element whose registered properties to search.</param>
        /// <param name="propertyName">The name of the property to find.</param>
        /// <param name="property">When this method returns, contains the matching property if found; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the property was found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetProperty(
            this StyledElement owner,
            string propertyName,
            [NotNullWhen(true)] out AvaloniaProperty? property
        )
        {
            property = AvaloniaPropertyRegistry
                .Instance.GetRegistered(owner.GetType())
                .FirstOrDefault(p => p.Name == propertyName);

            return property != null;
        }
    }
}
