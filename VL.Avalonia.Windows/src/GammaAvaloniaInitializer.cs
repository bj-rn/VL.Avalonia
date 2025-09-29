using Avalonia;
using Avalonia.Logging;
using Avalonia.Themes.Fluent;
using VL.Core.CompilerServices;
using Application = Avalonia.Application;

[assembly: AssemblyInitializer(typeof(VL.Avalonia.Windows.GammaAvaloniaInitializer))]

namespace VL.Avalonia.Windows
{
    public sealed class GammaAvaloniaInitializer : AssemblyInitializer<GammaAvaloniaInitializer>
    {
        public static Application Instance;
        public static void Init() => Instance ??=
            AppBuilder
            .Configure<GammaAvaloniaApplication>()
            .UseWin32()
            .UseSkia()
#if(DEBUG)
            .LogToTrace(LogEventLevel.Verbose)
#endif
            .SetupWithLifetime(new GammaAvaloniaApplicationLifetime())
            .WithInterFont()
            .AfterSetup((builder) => builder.Instance.Styles.Add(new FluentTheme()))
            .Instance;
    }
}
