using Avalonia;

namespace VL.Avalonia.Custom.Controls.Value
{
    public partial class EditableSlider
    {
        public bool? IsEditing
        {
            get => GetValue(IsEditingProperty);
            set => SetValue(IsEditingProperty, value);
        }

        public static readonly StyledProperty<bool?> IsEditingProperty = AvaloniaProperty.Register<
            EditableSlider,
            bool?
        >(nameof(IsEditing));
    }
}
