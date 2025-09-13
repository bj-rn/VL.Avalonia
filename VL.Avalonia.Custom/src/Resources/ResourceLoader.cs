using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace VL.Avalonia.Custom.Resources
{
    public sealed class ResourceLoader
    {
        private static bool _isLoaded;
        public static void IncludeCustomResources(Application app)
        {
            if (_isLoaded)
                return;

            if (app == null)
                throw new ArgumentNullException(nameof(app), "You must provide a valid Avalonia Application instance.");

            // Load resources
            var uri = new Uri("avares://VL.Avalonia.Custom/Resources/Resources.axaml");
            var resource = AvaloniaXamlLoader.Load(uri) as ResourceDictionary;

            if (resource == null)
                throw new Exception($"Failed to load resource dictionary from: {uri}");


            // Load styles
            var styles = CreateStyle("avares://VL.Avalonia.Custom/Styles/Styles.axaml");
            //var stylesUri = new Uri("avares://VL.Avalonia.Custom/Styles/Styles.axaml");

            //var styles = AvaloniaXamlLoader.Load(styleUri) as Styles;
            if (styles == null)
                throw new Exception($"Failed to load styles dictionary from: {styles}");


            app.Resources ??= new ResourceDictionary();
            app.Resources.MergedDictionaries.Add(resource);
            app.Styles.Add(styles);
            //app.Resources.MergedDictionaries.Add(styles);

            _isLoaded = true;
        }

        private static StyleInclude CreateStyle(string url)
        {
            var self = new Uri("resm:Styles?assembly=YourAssembly");
            return new StyleInclude(self)
            {
                Source = new Uri(url)
            };
        }
    }
}