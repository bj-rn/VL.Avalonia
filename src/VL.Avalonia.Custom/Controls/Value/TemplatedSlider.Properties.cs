using Avalonia;

namespace VL.Avalonia.Custom.Controls.Value
{
    public partial class TemplatedSlider
    {
        public object? Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly StyledProperty<object?> ContentProperty =
            AvaloniaProperty.Register<TemplatedSlider, object?>(nameof(Content));
    }
}
