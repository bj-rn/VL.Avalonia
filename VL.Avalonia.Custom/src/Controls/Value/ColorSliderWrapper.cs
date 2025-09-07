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
        protected ChannelTwoWayBinding<HsvColor, HsvColor> _hsvColorBinding;
        public ColorSliderWrapper()
        {
            _hsvColorBinding = new ChannelTwoWayBinding<HsvColor, HsvColor>(_output, ColorSlider.HsvColorProperty, (x) => x, (x) => x);
        }

        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetValueChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvColorBinding.SetChannel(hsvColorChannel);

        /// <summary>
        /// Defines the <see cref="ColorComponent"/> property.
        /// </summary>
        [ImplementProperty("ColorSlider.ColorComponentProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<object> _colorComponent;

        /// <param name="colorModel">
        /// Defines the <see cref="ColorModel"/> property.
        /// </param>
        [ImplementProperty("ColorSlider.ColorModelProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<object> _colorModel;
    }
}
