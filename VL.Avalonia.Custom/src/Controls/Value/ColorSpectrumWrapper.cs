using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
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
        protected ChannelTwoWayBinding<Color> _colorBinding;
        protected ChannelTwoWayBinding<HsvColor> _hsvColorBinding;

        public ColorSpectrumWrapper()
        {
            _colorBinding = new ChannelTwoWayBinding<Color>(_output, ColorSpectrum.ColorProperty);
            _hsvColorBinding = new ChannelTwoWayBinding<HsvColor>(_output, ColorSpectrum.HsvColorProperty);
        }

        /// <param name="colorChannel">
        /// Gets or sets the colorChannel
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetColorChannel(IChannel<Color>? colorChannel) =>
            _colorBinding.SetChannel(colorChannel);

        /// <param name="hsvColorChannel">
        /// Gets or sets the hsvColorChannel
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvColorBinding.SetChannel(hsvColorChannel);


        protected Optional<ColorSpectrumShape> _shape;
        [Fragment(Order = PinOrder.Main)]
        public void SetColorSpectrumShape([Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorSpectrumShape> shape)
        {
            if (_shape != shape)
            {
                if (shape.HasValue)
                {
                    _output.SetValue(ColorSpectrum.ShapeProperty, shape.Value);
                }
                else
                {
                    _output.ClearValue(ColorSpectrum.ShapeProperty);
                }

                _shape = shape;
            }
        }
    }
}
