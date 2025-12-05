using Avalonia.Controls;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>SplitButton</c> combines a primary button with a secondary dropdown button in a single control. The primary part behaves like a standard Button and executes commands or raises Click events, while the secondary part opens a flyout with additional options. This design pattern is common in applications where a default action needs quick access alongside related alternative actions.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/splitbutton">SplitButton</see>
/// </summary>
[ProcessNode(Name = "SplitButton")]
public partial class SplitButtonWrapper : SplitButtonWrapperBase<SplitButton> { }
