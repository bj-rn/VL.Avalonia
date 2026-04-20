using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    [ProcessNode]
    public abstract class SelectingItemsControlSelectedItemsHandler<TControl, TValue>
        where TControl : SelectingItemsControl
    {
        private TControl? _input;
        private IDisposable? _subscription;
        private IReadOnlyList<TValue> _output = Spread<TValue>.Empty;

        private Func<TControl, IReadOnlyList<TValue>> _selector;

        public SelectingItemsControlSelectedItemsHandler(
            Func<TControl, IReadOnlyList<TValue>> selector
        )
        {
            _selector = selector;
        }

        public virtual void SetInput(TControl? input)
        {
            if (ReferenceEquals(_input, input))
                return;

            _subscription?.Dispose();
            _input = input;

            if (_input is not null)
            {
                // Create an observable stream from the SelectionChanged event
                _subscription = Observable
                    .FromEventPattern<SelectionChangedEventArgs>(
                        h => _input.SelectionChanged += h,
                        h => _input.SelectionChanged -= h
                    )
                    .Subscribe(ev => _output = _selector(_input));
            }
        }

        public IReadOnlyList<TValue> Output => _output;

        public virtual void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
