using Avalonia;
using Avalonia.Controls;

namespace VL.Avalonia.Custom.Controls.Value
{
    public partial class NumberField : NumericUpDown
    {
        public bool? IsEditing
        {
            get => GetValue(IsEditingProperty);
            set => SetValue(IsEditingProperty, value);
        }

        public static readonly StyledProperty<bool?> IsEditingProperty = AvaloniaProperty.Register<
            NumberField,
            bool?
        >(nameof(IsEditing));
    }
}
