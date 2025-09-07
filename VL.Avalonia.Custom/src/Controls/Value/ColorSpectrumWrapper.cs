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
        protected ChannelTwoWayBinding<Color, Color> _colorBinding;
        protected ChannelTwoWayBinding<HsvColor, HsvColor> _hsvColorBinding;

        public ColorSpectrumWrapper()
        {
            _colorBinding = new ChannelTwoWayBinding<Color, Color>(_output, ColorSpectrum.ColorProperty, (x) => x, (x) => x);
            _hsvColorBinding = new ChannelTwoWayBinding<HsvColor, HsvColor>(_output, ColorSpectrum.HsvColorProperty, (x) => x, (x) => x);
            //_output.Shape = ColorSpectrumShape.Ring;
        }


        /// <param name="colorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetValueChannel(IChannel<Color> colorChannel) =>
            _colorBinding.SetChannel(colorChannel);


        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetValueChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvColorBinding.SetChannel(hsvColorChannel);



        /// <summary>
        /// Defines the <see cref="Shape"/> property.
        /// </summary>
        [ImplementProperty("ColorSpectrum.ShapeProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<ColorSpectrumShape> _shape;


        /// <summary>
        /// Defines the <see cref="Components"/> property.
        /// </summary>
        [ImplementProperty("ColorSpectrum.ComponentsProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<object> _components;

        /// <summary>
        /// Defines the <see cref="ColorSpectrumShape"/> property.
        /// </summary>
        [ImplementProperty("ColorSpectrum.ColorSpectrumShapeProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<object> _colorSpectrumShape;
    }
}
