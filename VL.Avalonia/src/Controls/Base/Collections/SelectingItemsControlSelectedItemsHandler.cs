using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    [ProcessNode]
    public abstract class SelectingItemsControlSelectedItemsHandler<TControl, T> : IDisposable
        where TControl : SelectingItemsControl
    {
        private TControl? _input;
        private IDisposable? _subscription;

        // Null means stale: recomputed from the current input on the next read, so a new input's
        // existing selection shows up without waiting for a SelectionChanged.
        private IReadOnlyList<T>? _output;

        protected abstract Func<TControl, IReadOnlyList<T>> Selector { get; }

        public virtual void SetInput(TControl? input)
        {
            if (ReferenceEquals(_input, input))
                return;

            _subscription?.Dispose();
            _subscription = null;
            _input = input;
            _output = null;

            if (input is not null)
            {
                // Create an observable stream from the SelectionChanged event
                _subscription = Observable
                    .FromEventPattern<SelectionChangedEventArgs>(
                        h => input.SelectionChanged += h,
                        h => input.SelectionChanged -= h
                    )
                    .Subscribe(_ => _output = null);
            }
        }

        public IReadOnlyList<T> Output =>
            _output ??= _input is null ? Spread<T>.Empty : Selector(_input);

        public virtual void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
            _input = null;
            _output = null;
        }
    }
}
