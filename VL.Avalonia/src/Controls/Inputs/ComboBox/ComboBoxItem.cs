using Avalonia.Controls;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ComboBoxItem"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ComboBoxItemNodeBase<T> : ListBoxItemNodeBase<T>
        where T : ComboBoxItem, new() { }

    /// <summary>
    /// Wrapper for <see cref="ComboBoxItem"/>
    /// </summary>
    [ProcessNode(Name = "ComboBoxItem")]
    public class ComboBoxItemNode : ComboBoxItemNodeBase<ComboBoxItem> { }
}
