using Avalonia.Controls.Primitives;
using VL.Core.Import;

namespace VL.Avalonia.Controls;



/// <summary>
/// The <c>ToggleButton</c> is a button control that can be toggled between checked and unchecked states. Unlike a regular button, it maintains its state after being clicked. It supports three states when IsThreeState is enabled: checked, unchecked, and indeterminate. This control serves as the base class for CheckBox and other toggleable controls.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/buttons/togglebutton">ToggleButton</see>
/// </summary>
[ProcessNode(Name = "ToggleButton")]
public partial class ToggleButtonWrapper : ToggleButtonWrapperBase<ToggleButton>
{

}
