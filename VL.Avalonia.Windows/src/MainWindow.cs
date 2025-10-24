using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Windows
{
    public class MainWindow : Window
    {
        public MainWindow()
        {

        }
    }

    [ProcessNode(Name = "MainWindow")]
    public class MainWindowWrapper
    {
        protected Window _output;
        public Window Output => _output;

        protected Optional<object> _content;
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

        public MainWindowWrapper()
        {
            GammaAvaloniaInitializer.Initialize();

            var instance = GammaAvaloniaInitializer.Instance;
            instance.RunWithMainWindow<MainWindow>();

            _output = (instance.ApplicationLifetime as GammaAvaloniaApplicationLifetime).MainWindow;
        }
    }
}
