using VL.Avalonia.Controls;
using VL.Core.Import;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// A control that lets the user change value.
    /// <br/>NumberField<br/>
    /// </summary>
    [ProcessNode(Name = "NumberField")]
    public partial class NumberFieldWrapper : NumericUpDownNodeBase<NumberField> { }
}
