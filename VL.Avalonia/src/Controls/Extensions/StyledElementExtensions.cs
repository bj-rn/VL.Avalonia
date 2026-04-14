using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
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
    }
}
