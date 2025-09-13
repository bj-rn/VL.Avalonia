using Avalonia.Controls;
using VL.Core.Import;


namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// Custom Colorpicker
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">Custom Color Picker</see>
    /// </summary>
    [ProcessNode(Name = "ColorPicker")]
    public partial class ColorPickerWrapper : ColorViewWrapper<ColorPicker>
    {
    }
}
