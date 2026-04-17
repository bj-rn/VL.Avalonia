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
    public class ListBoxSelectedItemsHandler<T> : IDisposable
    {
        private ListBox? _input;
        private IDisposable? _subscription;
        private IReadOnlyList<T> _output = Spread<T>.Empty;

        public void SetInput(ListBox? input)
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
                    .Subscribe(ev =>
                        _output = _input?.SelectedItems?.OfType<T>().ToSpread() ?? Spread<T>.Empty
                    );
            }
        }

        public IReadOnlyList<T> Output => _output;

        public virtual void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
