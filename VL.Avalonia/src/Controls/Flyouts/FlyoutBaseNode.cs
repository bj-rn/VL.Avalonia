using Avalonia.Controls.Primitives;
using VL.Avalonia.Data;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="FlyoutBase"/>
    /// </summary>
    [ProcessNode]
    public abstract class FlyoutBaseNode<TControl> : AvaloniaObjectNodeBase<TControl>, IDisposable
        where TControl : FlyoutBase, new()
    {
        private OneWayBinding<bool> _isOpenBinding;

        public FlyoutBaseNode()
        {
            _isOpenBinding = new OneWayBinding<bool>(_output, FlyoutBase.IsOpenProperty);
        }

        /// <param name="isOpenChannel">Provides a one-way binding to the read-only <see cref="FlyoutBase.IsOpen"/> property. The channel is updated when the control state changes, but cannot be used to modify it.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsOpenChannel(IChannel<bool> isOpenChannel) =>
            _isOpenBinding.Bind(isOpenChannel);

        public override void Dispose()
        {
            _isOpenBinding?.Dispose();
            base.Dispose();
        }
    }
}
