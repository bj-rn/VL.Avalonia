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
    /// ColorSpectrum
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorSpectrum</see>
    /// </summary>
    [ProcessNode(Name = "ColorSpectrum")]
    public partial class ColorSpectrumWrapper : ControlWrapperBase<ColorSpectrum>
    {
        protected ChannelTwoWayBinding<Color, Color> _valueBinding;
        public ColorSpectrumWrapper()
        {
            _valueBinding = new ChannelTwoWayBinding<Color, Color>(_output, ColorSpectrum.ColorProperty, (x) => x, (x) => x);
        }

        /// <param name="colorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = 0 /*PinOrder.Main*/)]
        public void SetValueChannel(IChannel<Color> colorChannel) =>
            _valueBinding.SetChannel(colorChannel);


        /// <summary>
        /// Defines the <see cref="Components"/> property.
        /// </summary>
        [ImplementProperty("ColorSpectrum.ComponentsProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<ColorSpectrumComponents> _components;
    }
}
