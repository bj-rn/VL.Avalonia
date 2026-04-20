using System.Reactive.Linq;
using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Specialized handler for <see cref="ListBox.SelectedItems"/>.
    /// </summary>
    [ProcessNode(Name = "SelectedItems (ListBox)")]
    public class ListBoxSelectedItemsHandler<T>
        : SelectingItemsControlSelectedItemsHandler<ListBox, T>
    {
        public ListBoxSelectedItemsHandler()
            : base(input => input.SelectedItems?.OfType<T>().ToSpread() ?? Spread<T>.Empty) { }

        public override void SetInput(ListBox? input)
        {
            base.SetInput(input);
        }
    }
}
