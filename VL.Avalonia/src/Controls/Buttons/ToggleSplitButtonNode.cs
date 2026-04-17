using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Data;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ToggleSplitButton"/>
    /// </summary>
    [ProcessNode]
    public abstract class ToggleSplitButtonNode<T> : SplitButtonNodeBase<T>, IDisposable
        where T : ToggleSplitButton, new()
    {
        private readonly TwoWayBinding<bool, bool> _isCheckedBinding;

        public ToggleSplitButtonNode()
        {
            _isCheckedBinding = new TwoWayBinding<bool, bool>(
                _output,
                ToggleSplitButton.IsCheckedProperty
            );
        }

        /// <param name="isCheckedChannel">Binds <see cref="ToggleButton.IsChecked"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsCheckedChannel(IChannel<bool> isCheckedChannel) =>
            _isCheckedBinding.Bind(isCheckedChannel);
    }

    /// <summary>
    /// Wrapper for <see cref="ToggleSplitButton"/>
    /// </summary>
    [ProcessNode(Name = "ToggleSplitButton")]
    public class ToggleSplitButtonNode : ToggleSplitButtonNode<ToggleSplitButton> { }
}
