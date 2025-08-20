using Avalonia;
using Dock.Avalonia.Themes;
using Dock.Model.Avalonia;

namespace VL.Avalonia.Dock
{
    public static class AvaloniaDockInitializer
    {
        public static Application DockInitilizer(Application application)
        {
            GC.KeepAlive(typeof(Factory).Assembly);

            application.Styles.Add(new DockFluentTheme());
            //application.Resources.Add(ControlRecycling.TryToUseIdAsKeyProperty(true)));

            return application;
        }
    }
}
