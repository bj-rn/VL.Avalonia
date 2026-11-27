using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(typeof(VL.Avalonia.Tests.TestApp))]

namespace VL.Avalonia.Tests;

/// <summary>
/// The headless Avalonia app behind [AvaloniaFact] tests: they run on its UI thread.
/// </summary>
public sealed class TestApp : Application
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<TestApp>().UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
