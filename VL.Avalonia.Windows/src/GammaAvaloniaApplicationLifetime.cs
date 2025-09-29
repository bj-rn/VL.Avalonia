using Avalonia.Controls.ApplicationLifetimes;

namespace VL.Avalonia.Windows
{
    sealed class GammaAvaloniaApplicationLifetime : ClassicDesktopStyleApplicationLifetime
    {
        public event EventHandler<ControlledApplicationLifetimeStartupEventArgs>? Startup;
        public event EventHandler<ControlledApplicationLifetimeExitEventArgs>? Exit;

        public GammaAvaloniaApplicationLifetime()
        {
            Application.ApplicationExit += (s, a) => Shutdown();
        }
        public void Shutdown(int exitCode = 0)
        {
            Exit?.Invoke(this, new ControlledApplicationLifetimeExitEventArgs(exitCode));
        }
    }
}
