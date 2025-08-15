using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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

            var uri = new Uri("avares://VL.Avalonia.Custom/Resources/Resources.axaml");
            var resource = AvaloniaXamlLoader.Load(uri) as ResourceDictionary;

            if (resource == null)
                throw new Exception($"Failed to load resource dictionary from: {uri}");

            app.Resources ??= new ResourceDictionary();
            app.Resources.MergedDictionaries.Add(resource);

            _isLoaded = true;
        }
    }
}