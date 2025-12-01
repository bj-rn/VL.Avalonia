using System.Xml.Linq;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Application = Avalonia.Application;

namespace VL.Avalonia.Skia
{
    public static class FluentThemeHelper
    {
        public static void LoadFluentEditorTheme(this Application application, string xamlThemeCode)
        {
            // 1. Inject default namespaces if they are missing, so XDocument can parse it.
            if (!xamlThemeCode.Contains("xmlns="))
            {
                xamlThemeCode = xamlThemeCode.Replace(
                    "<FluentTheme>",
                    "<FluentTheme xmlns='https://github.com/avaloniaui' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>"
                );
            }

            XDocument doc;
            try
            {
                doc = XDocument.Parse(xamlThemeCode);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to parse XAML string: {ex.Message}");
            }

            // 2. Create the theme manually (avoids missing constructor error)
            var fluentTheme = new FluentTheme(sp: null);

            XNamespace avaloniaNs = "https://github.com/avaloniaui";
            XNamespace xNs = "http://schemas.microsoft.com/winfx/2006/xaml";

            // 3. Find all ColorPaletteResources
            var palettes = doc.Descendants(avaloniaNs + "ColorPaletteResources");

            foreach (var paletteElement in palettes)
            {
                // Get the x:Key="Light" or "Dark"
                var keyAttr = paletteElement.Attribute(xNs + "Key");
                if (keyAttr == null)
                    continue;

                ThemeVariant variant = keyAttr.Value switch
                {
                    "Light" => ThemeVariant.Light,
                    "Dark" => ThemeVariant.Dark,
                    _ => null,
                };

                if (variant != null)
                {
                    // Remove x:Key attribute (invalid when parsing the object directly)
                    keyAttr.Remove();

                    // Convert back to string (XElement.ToString adds xmlns automatically)
                    var paletteXaml = paletteElement.ToString();

                    // Parse and assign
                    var resource = AvaloniaRuntimeXamlLoader.Parse<ColorPaletteResources>(
                        paletteXaml
                    );
                    fluentTheme.Palettes[variant] = resource;
                }
            }

            // 4. Add to application
            application.Styles.Add(fluentTheme);
        }
    }
}
