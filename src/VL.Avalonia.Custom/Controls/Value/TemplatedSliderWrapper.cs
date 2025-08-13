using VL.Avalonia.Attributes;
using VL.Avalonia.Custom.Controls;
using VL.Core;
using VL.Core.Import;
using VL.Avalonia.Custom.Controls.Value;

namespace VL.Avalonia.Controls.Value
{
    /// <summary>
    /// A slider with content presenter to customize its inner content
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Slider</see>
    /// </summary>
    [ProcessNode(Name = "TemplatedSlider")]
    public partial class TemplatedliderWrapper : RangeBaseWrapper<TemplatedSlider>
    {
        /// <param name="content">
        /// Dialog Button. Whether this button is the cancel button (triggered by Escape key)
        /// </param>
        [ImplementProperty("TemplatedSlider.ContentProperty")]
        protected Optional<object> _content;
    }
}
