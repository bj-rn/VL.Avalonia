using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>RadioButton</c> allows users to select a single option from a group of mutually exclusive choices. It extends ToggleButton with grouping functionality, where only one radio button in a group can be selected at a time. RadioButtons automatically manage group exclusivity and provide the classic radio button interaction pattern for single-choice scenarios.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/radiobutton">RadioButton</see>
/// </summary>
[ProcessNode(Name = "RadioButton")]
public partial class RadioButtonWrapper : ToggleButtonWrapperBase<RadioButton>
{
    [ImplementProperty("RadioButton.GroupNameProperty")]
    protected Optional<string> _groupName;
}
