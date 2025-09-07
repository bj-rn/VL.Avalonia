using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// ColorSlider
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorSlider</see>
    /// </summary>
    [ProcessNode(Name = "ColorSlider")]
    public partial class ColorSliderWrapper : RangeBaseWrapper<ColorSlider>
    {
        /// <summary>
        /// Defines the <see cref="ColorComponent"/> property.
        /// </summary>
        [ImplementProperty("ColorSlider.ColorComponentProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<ColorComponent> _colorComponent;

        /// <param name="colorModel">
        /// Defines the <see cref="ColorModel"/> property.
        /// </param>
        [ImplementProperty("ColorSlider.ColorModelProperty", PinVisibility = Model.PinVisibility.Visible)]
        protected Optional<ColorModel> _colorModel;
    }
}
