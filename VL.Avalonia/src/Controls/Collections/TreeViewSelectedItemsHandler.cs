using System.Reactive.Linq;
using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Specialized handler for <see cref="TreeView.SelectedItems"/>.
    /// </summary>
    [ProcessNode(Name = "SelectedItems (TreeView)")]
    public class TreeViewSelectedItemsHandler<T> : IDisposable
    {
        private TreeView? _input;
        private IDisposable? _subscription;
        private IReadOnlyList<T>? _output;

        public void SetInput(TreeView? input)
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
                    .Subscribe(_ => _output = null);
            }

            _output = null;
        }

        public IReadOnlyList<T> Output
        {
            get
            {
                if (_output == null)
                {
                    _output = _input?.SelectedItems?.OfType<T>().ToSpread() ?? Spread<T>.Empty;
                }

                return _output;
            }
        }

        public void Dispose() => _subscription?.Dispose();
    }
}
