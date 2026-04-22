using Avalonia.Controls;
using VL.Avalonia.Data;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ToggleSplitButton"/>
    /// </summary>
    [ProcessNode]
    public abstract class ToggleSplitButtonNodeBase<T> : SplitButtonNodeBase<T>, IDisposable
        where T : ToggleSplitButton, new()
    {
        private readonly TwoWayBinding<bool> _isCheckedBinding;

        public ToggleSplitButtonNodeBase()
        {
            _isCheckedBinding = new TwoWayBinding<bool>(
                _output,
                ToggleSplitButton.IsCheckedProperty
            );
        }

        /// <param name="isCheckedChannel">Binds <see cref="ToggleSplitButton.IsChecked"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsCheckedChannel(IChannel<bool> isCheckedChannel) =>
            _isCheckedBinding.Bind(isCheckedChannel);

        public override void Dispose()
        {
            _isCheckedBinding.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="ToggleSplitButton"/>
    /// </summary>
    [ProcessNode(Name = "ToggleSplitButton")]
    public class ToggleSplitButtonNode : ToggleSplitButtonNodeBase<ToggleSplitButton> { }
}
