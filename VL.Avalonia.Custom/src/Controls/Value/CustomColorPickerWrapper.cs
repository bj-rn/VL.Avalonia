using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// Custom Colorpicker
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Custom Color Picker</see>
    /// </summary>
    [ProcessNode(Name = "ColorPicker")]
    public partial class CustomColorPickerWrapper : ControlWrapperBase<ColorPicker>
    {
        protected ChannelTwoWayBinding<Color, Color> _valueBinding;
        public CustomColorPickerWrapper()
        {
            _valueBinding = new ChannelTwoWayBinding<Color, Color>(_output, ColorPicker.ColorProperty, (x) => (Color)x, (x) => (Color)x);
        }

        /// <param name="colorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = 0/*PinOrder.Main*/)]
        public void SetValueChannel(IChannel<Color> colorChannel) =>
            _valueBinding.SetChannel(colorChannel);
    }
}
