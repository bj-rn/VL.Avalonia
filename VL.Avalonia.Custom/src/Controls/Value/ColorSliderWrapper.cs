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
    /// ColorSlider
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorSlider</see>
    /// </summary>
    [ProcessNode(Name = "ColorSlider")]
    public partial class ColorSliderWrapper : RangeBaseWrapper<ColorSlider>
    {
        protected ChannelTwoWayBinding<HsvColor> _hsvColorBinding;
        public ColorSliderWrapper()
        {
            _hsvColorBinding = new ChannelTwoWayBinding<HsvColor>(_output, ColorSlider.HsvColorProperty);
        }

        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvColorBinding.SetChannel(hsvColorChannel);




        protected Optional<ColorComponent> _colorComponent;
        [Fragment(Order = PinOrder.Main)]
        public void SetColorComponent([Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorComponent> colorComponent)
        {
            if (_colorComponent != colorComponent)
            {
                if (colorComponent.HasValue)
                {
                    _output.SetValue(ColorSlider.ColorComponentProperty, colorComponent.Value);
                }
                else
                {
                    _output.ClearValue(ColorSlider.ColorComponentProperty);
                }

                _colorComponent = colorComponent;
            }
        }

        protected Optional<ColorModel> _colorModel;
        [Fragment(Order = PinOrder.Main)]
        public void SetColorModel([Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorModel> colorModel)
        {
            if (_colorModel != colorModel)
            {
                if (colorModel.HasValue)
                {
                    _output.SetValue(ColorSlider.ColorModelProperty, colorModel.Value);
                }
                else
                {
                    _output.ClearValue(ColorSlider.ColorModelProperty);
                }

                _colorModel = colorModel;
            }
        }
    }
}
