using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using VL.Avalonia.Attributes;
using VL.Avalonia.Custom.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls.Value
{
    /// <summary>
    /// A slider with editable value
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Slider</see>
    /// </summary>
    [ProcessNode(Name = "EditableSlider")]
    public partial class EditableSliderWrapper : RangeBaseWrapper<EditableSlider>
    {
    }
}
