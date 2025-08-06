using VL.Avalonia.Attributes;
using VL.Avalonia.Custom.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls.Value
{
    /// <summary>
    /// A slider with content presenter to customize its inner content
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Slider</see>
    /// </summary>
    [ProcessNode(Name = "TemplatableSlider")]
    public partial class TemplatableSliderWrapper : RangeBaseWrapper<TemplatableSlider>
    {
        /// <param name="content">
        /// Dialog Button. Whether this button is the cancel button (triggered by Escape key)
        /// </param>
        [ImplementProperty("TemplatableSlider.ContentProperty")]
        protected Optional<object> _content;
    }
}
