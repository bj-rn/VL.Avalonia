using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    [ProcessNode]
    public abstract class SelectingItemsControlSelectedItemsHandler<TControl, T>
        where TControl : SelectingItemsControl
    {
        private TControl? _input;
        private IDisposable? _subscription;
        private IReadOnlyList<T> _output = Spread<T>.Empty;

        protected abstract Func<TControl, IReadOnlyList<T>> Selector { get; }

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
                    .Subscribe(ev => _output = Selector(_input));
            }
        }

        public IReadOnlyList<T> Output => _output;

        public virtual void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
