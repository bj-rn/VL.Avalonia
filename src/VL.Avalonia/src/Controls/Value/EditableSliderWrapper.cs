using VL.Avalonia.Attributes;
using VL.Avalonia.Custom.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls.Value
{
    public partial class EditableSliderWrapper
    {
        /// <summary>
        /// A slider with editable value
        /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Slider</see>
        /// </summary>
        [ProcessNode(Name = "EditableSlider")]
        public partial class CustomSliderWrapper : RangeBaseWrapper<EditableSlider>
        {

        }
    }
}
