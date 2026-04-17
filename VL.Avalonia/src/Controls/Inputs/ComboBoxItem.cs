using Avalonia.Controls;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ComboBoxItem</c> represents a selectable item within a ComboBox dropdown list. It extends ListBoxItem with specialized behavior for dropdown scenarios, including automatic focus notification to the parent ComboBox and proper automation support. ComboBoxItems are typically created automatically by the ComboBox when displaying data-bound items.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/comboboxitem">ComboBoxItem</see>
/// </summary>
[ProcessNode(Name = "ComboBoxItem")]
public partial class ComboBoxItemWrapper : ListBoxItemNodeBase<ComboBoxItem> { }
