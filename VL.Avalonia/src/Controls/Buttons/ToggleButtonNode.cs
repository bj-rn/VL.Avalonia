using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls.Buttons
{
    /// <summary>
    /// Base wrapper for <see cref="ToggleButton"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ToggleButtonNodeBase<T> : ButtonNodeBase<T>, IDisposable
        where T : ToggleButton, new()
    {
        private readonly TwoWayBinding<bool, bool?> _isCheckedBinding;

        public ToggleButtonNodeBase()
        {
            _isCheckedBinding = new TwoWayBinding<bool, bool?>(
                _output,
                ToggleButton.IsCheckedProperty,
                x => x,
                y => y ?? false
            );
        }

        /// <param name="isCheckedChannel">Binds <see cref="ToggleButton.IsChecked"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsCheckedChannel(IChannel<bool> isCheckedChannel) =>
            _isCheckedBinding.Bind(isCheckedChannel);

        /// <summary>Sets a value that indicates whether the control supports three states. </summary>
        [ImplementProperty(
            typeof(ToggleButton),
            nameof(ToggleButton.IsThreeStateProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isThreeState;
    }

    /// <summary>
    /// Wrapper for <see cref="ToggleButton"/>
    /// </summary>
    [ProcessNode(Name = "ToggleButton")]
    public partial class ToggleButtonNode : ToggleButtonNodeBase<ToggleButton> { }
}
