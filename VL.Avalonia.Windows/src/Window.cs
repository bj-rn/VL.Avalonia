using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Embedding;
using Avalonia.Styling;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Windows
{
    [ProcessNode(Name = "Window")]
    public class WindowWrapper
    {
        private Window _output;

        private Optional<object> _content;
        public void SetContent(Optional<object> content)
        {
            if (_content != content)
            {
                if (content.HasValue)
                {
                    _output.SetValue(Window.ContentProperty, content.Value);
                }
                else
                {
                    _output.ClearValue(Window.ContentProperty);
                }
                _content = content;
            }
        }

        private Optional<ThemeVariant> _requestedThemeVariant;
        /// <param name="requestedThemeVariant">Requested Theme Variant</param>
        [Fragment(Order = -10)]
        public void SetRequestedThemeVariant(Optional<ThemeVariant> requestedThemeVariant)
        {
            if (_requestedThemeVariant != requestedThemeVariant)
            {
                if (requestedThemeVariant.HasValue)
                {
                    var theme = requestedThemeVariant.Value;

                    _output.SetValue(EmbeddableControlRoot.RequestedThemeVariantProperty, requestedThemeVariant.Value);
                }
                else
                {
                    _output.ClearValue(EmbeddableControlRoot.RequestedThemeVariantProperty);
                }

                _requestedThemeVariant = requestedThemeVariant;
            }
        }

        public WindowWrapper()
        {
            GammaAvaloniaInitializer.Init();

            var app = GammaAvaloniaInitializer.Instance.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;

            //app.Start();

            _output = app.MainWindow;
            _output?.Show();
        }
    }
}
