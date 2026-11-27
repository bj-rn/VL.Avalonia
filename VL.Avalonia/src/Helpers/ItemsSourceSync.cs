using System.Collections;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Threading;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers
{
    /// <summary>
    /// Feeds the items of a node to an items property (e.g. <c>ItemsControl.ItemsSource</c>)
    /// through one persistent list, so a new list of items only adds and removes the items that
    /// changed instead of rebuilding all item containers. A list that reports its own changes
    /// (e.g. an ObservableCollection) is handed on as is.
    /// </summary>
    internal sealed class ItemsSourceSync<T> : IDisposable
    {
        private readonly AvaloniaObject _target;
        private readonly AvaloniaProperty _property;
        private readonly ItemsList<T> _list = new();
        private readonly object _gate = new();

        private IChannel<IReadOnlyList<T>>? _channel;
        private IReadOnlyList<T>? _items;
        private IDisposable? _subscription;

        private IReadOnlyList<T>? _applied;
        private bool _hasApplied;

        private IReadOnlyList<T>? _pending;
        private bool _posted;
        private int _generation;

        public ItemsSourceSync(AvaloniaObject target, AvaloniaProperty property)
        {
            _target = target;
            _property = property;
        }

        public void SetChannel(IChannel<IReadOnlyList<T>>? channel)
        {
            if (ReferenceEquals(_channel, channel))
                return;

            Unsubscribe();
            _channel = channel;
            _items = null;

            if (channel is null)
            {
                Clear();
                return;
            }

            var generation = _generation;
            _subscription = channel.Subscribe(items => OnNext(items, generation));
            OnNext(channel.Value, generation);
        }

        public void SetItems(IReadOnlyList<T>? items)
        {
            if (ReferenceEquals(_items, items))
                return;

            Unsubscribe();
            _items = items;
            _channel = null;

            if (items is null)
                Clear();
            else
                Apply(items);
        }

        public void Dispose()
        {
            Unsubscribe();
            _channel = null;
            _items = null;
        }

        private void OnNext(IReadOnlyList<T>? items, int generation)
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                if (generation == _generation)
                    Apply(items);
                return;
            }

            lock (_gate)
            {
                if (generation != _generation)
                    return;

                _pending = items;
                if (_posted)
                    return;
                _posted = true;
            }

            Dispatcher.UIThread.Post(() => ApplyPending(generation));
        }

        private void ApplyPending(int generation)
        {
            IReadOnlyList<T>? items;
            lock (_gate)
            {
                _posted = false;
                if (generation != _generation)
                    return;
                items = _pending;
                _pending = null;
            }

            Apply(items);
        }

        private void Apply(IReadOnlyList<T>? items)
        {
            if (_hasApplied && ReferenceEquals(_applied, items))
                return;

            _applied = items;
            _hasApplied = true;

            if (items is null)
            {
                _target.ClearValue(_property);
                _list.Clear();
                return;
            }

            if (items is IList and INotifyCollectionChanged)
            {
                SetSource(items);
                _list.Clear();
                return;
            }

            var assigned = ReferenceEquals(_target.GetValue(_property), _list);
            ItemsReconciler.Sync(_list, items);
            if (!assigned)
                SetSource(_list);
        }

        private void SetSource(IEnumerable source)
        {
            if (!ReferenceEquals(_target.GetValue(_property), source))
                _target.SetValue(_property, source);
        }

        private void Clear()
        {
            _applied = null;
            _hasApplied = false;
            _target.ClearValue(_property);
            _list.Clear();
        }

        private void Unsubscribe()
        {
            lock (_gate)
            {
                _generation++;
                _pending = null;
            }

            _subscription?.Dispose();
            _subscription = null;
        }
    }
}
