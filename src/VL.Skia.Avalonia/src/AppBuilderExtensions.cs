using Avalonia;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Rendering.Composition;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using VL.Core;
using VL.Lib.Animation;

namespace VL.Skia.Avalonia;

public static class AppBuilderExtensions
{
    /// <summary>
    /// Initializes app with VL.Skia platform render interface 
    /// </summary>
    /// <param name="appBuilder">AppBuilder</param>
    /// <returns>AppBuilder</returns>
    public static AppBuilder UseGammaSkia(this AppBuilder appBuilder) =>
        appBuilder
        .UseStandardRuntimePlatformSubsystem()
        .UseSkia()
        .UseWindowingSubsystem(() =>
        {
            AvaloniaSynchronizationContext.AutoInstall = false;

            var platformGraphics = new GammaPlatformGraphics();
            AvaloniaLocator.CurrentMutable
                .Bind<ICursorFactory>().ToConstant(new GammaSkiaCursorFactory())
                .Bind<IKeyboardDevice>().ToConstant(GammaDevices.KeyboardDevice)
                .Bind<IPlatformSettings>().ToConstant(new GammaPlatformSettings())
                .Bind<PlatformHotkeyConfiguration>().ToConstant(CreatePlatformHotKeyConfiguration())
                .Bind<IDispatcherImpl>().ToConstant(new GammaDispatcherImpl(Thread.CurrentThread, AppHost.Current.Services.GetService(typeof(IClock)) as IClock, AppHost.Current?.SynchronizationContext))
                .Bind<IPlatformGraphics>().ToConstant(platformGraphics)
                .Bind<IRenderTimer>().ToConstant(GammaRenderTimer.Instance)
                .Bind<Compositor>().ToConstant(new Compositor(platformGraphics, useUiThreadForSynchronousCommits: true));
        })
        .AfterPlatformServicesSetup(_ =>
        {
            var renderInterface = new GammaSkiaPlatformRenderInterface();
            AvaloniaLocator.CurrentMutable.Bind<IPlatformRenderInterface>()
                .ToConstant(renderInterface);
        });

    private static PlatformHotkeyConfiguration CreatePlatformHotKeyConfiguration()
        => OperatingSystem.IsMacOS()
            ? new PlatformHotkeyConfiguration(commandModifiers: KeyModifiers.Meta, wholeWordTextActionModifiers: KeyModifiers.Alt)
            : new PlatformHotkeyConfiguration(commandModifiers: KeyModifiers.Control);

    public static AppBuilder UseGammaSkiaDefaults(this AppBuilder appBuilder) =>
        appBuilder
        .WithInterFont()
        .AfterSetup((_) => appBuilder?.Instance?.Styles.Add(new FluentTheme()))
        .AfterSetup((_) =>
        {
            appBuilder?.Instance?.Resources.MergedDictionaries.Add(new ResourceInclude(new Uri("resm:Styles?assembly=Vl.Avalonia.Custom"))
            {
                Source = new Uri("avares://VL.Avalonia.Custom/Styles/EditableSliderStyles.axaml")
            });
        });
}