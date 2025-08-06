using Avalonia;

namespace VL.Avalonia.Custom.Controls
{
    public partial class TemplatableSlider
    {
        public object? Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly StyledProperty<object?> ContentProperty =
            AvaloniaProperty.Register<TemplatableSlider, object?>(nameof(Content));
    }
}
