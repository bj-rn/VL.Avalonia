using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="SelectingItemsControl">SelectingItemsControl</see>
    /// </summary>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract partial class SelectingItemsControlNodeBase<TControl, TValue>
        : ItemsControlNodeBase<TControl, TValue>,
            IDisposable
        where TControl : SelectingItemsControl, new()
    {
        private IChannel<TValue>? _selectedItemChannel;
        private IDisposable? _subscription;

        [Fragment]
        public SelectingItemsControlNodeBase() { }

        public virtual void SetSelectedItemChannel(IChannel<TValue> selectedItemChannel)
        {
            if (!ReferenceEquals(_selectedItemChannel, selectedItemChannel))
            {
                _subscription?.Dispose();

                if (selectedItemChannel != null)
                {
                    var channelObservable = selectedItemChannel;
                    var controlObservable = _output
                        .GetObservable(SelectingItemsControl.SelectedItemProperty)
                        .Select(obj => (TValue?)obj)
                        .Skip(1);

                    _subscription = Observable
                        .Merge(channelObservable, controlObservable)
                        .StartWith(selectedItemChannel.Value)
                        .DistinctUntilChanged()
                        .Subscribe(value =>
                        {
                            selectedItemChannel.EnsureValue(value);

                            var currentControlValue = _output.SelectedItem is TValue val
                                ? val
                                : default;

                            if (
                                !EqualityComparer<TValue?>.Default.Equals(
                                    currentControlValue,
                                    value
                                )
                            )
                            {
                                _output.SelectedItem = value;
                            }
                        });
                }

                _selectedItemChannel = selectedItemChannel;
            }
        }

        /// <inheritdoc cref="SelectingItemsControl.AutoScrollToSelectedItem"/>
        [ImplementProperty(
            typeof(SelectingItemsControl),
            nameof(SelectingItemsControl.AutoScrollToSelectedItemProperty),
            Order = PinOrder.Styled,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _autoScrollToSelectedItem;

        /// <inheritdoc cref="SelectingItemsControl.WrapSelection"/>
        [ImplementProperty(
            typeof(SelectingItemsControl),
            nameof(SelectingItemsControl.WrapSelectionProperty),
            Order = PinOrder.Styled,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _wrapSelection;

        /// <inheritdoc cref="SelectingItemsControl.IsTextSearchEnabled"/>
        [ImplementProperty(
            typeof(SelectingItemsControl),
            nameof(SelectingItemsControl.IsTextSearchEnabledProperty),
            Order = PinOrder.Styled,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isTextSearchEnabled;

        public override void Dispose()
        {
            _subscription?.Dispose();
            _selectedItemChannel = null;

            base.Dispose();
        }
    }
}
