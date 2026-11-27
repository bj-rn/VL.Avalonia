using System.Collections;
using System.Collections.Specialized;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Core.PublicAPI;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// The patch inside the <c>TreeDataTemplate</c> region: one instance per built control, called
    /// with that control's data item. The control it returns is the one placed; the children it
    /// returns are the child items of the data item. No children (null) means a leaf.
    /// </summary>
    public interface ITreeDataTemplateInlay<T, TChild>
    {
        Control? Update(T input, out IReadOnlyList<TChild>? children);
    }

    /// <summary>
    /// A tree data template that matches data of type <typeparamref name="T"/>, builds its control
    /// with the patch inside the region and takes the child items from the patch as well. Use
    /// <typeparamref name="TChild"/> = <typeparamref name="T"/> for a tree of one type, or a
    /// common base type and a DataTemplateSelector for a tree of several types.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A tree item asks for its children before its control is built, so the children arrive
    /// once the patch has run for that data item. They only arrive through this template: an
    /// item with its own HeaderTemplate gets no children from it.
    /// </para>
    /// <para>
    /// Each built control has its own patch instance, disposed when the control leaves the
    /// logical tree, or with the region. Invalidate hands out a new <see cref="Output"/>, so the
    /// controls using it build their content again.
    /// </para>
    /// </remarks>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract class TreeDataTemplateNodeBase<T, TChild>
        : TemplateRegionBase<ITreeDataTemplateInlay<T, TChild>, T>
    {
        private readonly Dictionary<object, ChildrenSource> _sources = new();
        private Template _output;

        protected TreeDataTemplateNodeBase()
        {
            _output = new Template(this);
        }

        [Fragment]
        public ITreeDataTemplate Output => _output;

        protected void Invalidate() => _output = new Template(this);

        /// <summary>
        /// Runs the patch of every built control again and hands on the children of those whose
        /// list changed. Lists are compared by reference.
        /// </summary>
        protected void UpdateInstances()
        {
            foreach (var instance in Instances)
            {
                if (!IsLive(instance))
                    continue;

                instance.Inlay.Update(instance.State, out var children);
                if (_sources.TryGetValue(instance.State!, out var source))
                    source.Push(children);
            }
        }

        private Control? Build(object? param)
        {
            if (param is not T data || CreateInlay() is not { } inlay)
                return null;

            var control = inlay.Update(data, out var children);
            if (control is null)
            {
                DisposeInlay(inlay);
                return null;
            }

            Track(control, inlay, data);
            var source = GetSource(data);
            source.Built++;
            source.Push(children);
            return control;
        }

        private InstancedBinding? SelectItems(object item) =>
            item is T data ? InstancedBinding.OneWay(GetSource(data)) : null;

        protected override void OnReleased(in Instance instance)
        {
            if (_sources.TryGetValue(instance.State!, out var source))
            {
                source.Built--;
                DropIfIdle(source);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _sources.Clear();
        }

        private ChildrenSource GetSource(T data)
        {
            if (!_sources.TryGetValue(data!, out var source))
                _sources[data!] = source = new ChildrenSource(this, data!);
            return source;
        }

        private void DropIfIdle(ChildrenSource source)
        {
            if (
                source.IsIdle
                && _sources.TryGetValue(source.Key, out var current)
                && current == source
            )
                _sources.Remove(source.Key);
        }

        /// <summary>
        /// The children of one data item, shared by the tree items showing it. They see one list
        /// that follows the children from the patch, so a new list only adds and removes the
        /// child items that changed. A list that reports its own changes is handed on as is.
        /// </summary>
        private sealed class ChildrenSource(TreeDataTemplateNodeBase<T, TChild> owner, object key)
            : IObservable<object?>
        {
            private readonly List<IObserver<object?>> _observers = new();
            private readonly ItemsList<TChild> _list = new();
            private IReadOnlyList<TChild>? _children;
            private bool _hasChildren;
            private IEnumerable? _current;

            public object Key => key;

            /// <summary>
            /// How many built controls show this data item.
            /// </summary>
            public int Built;

            public bool IsIdle => Built <= 0 && _observers.Count == 0;

            private IEnumerable Current => _current ?? _list;

            public void Push(IReadOnlyList<TChild>? children)
            {
                if (_hasChildren && ReferenceEquals(_children, children))
                    return;

                _children = children;
                _hasChildren = true;

                IEnumerable current;
                if (children is IList and INotifyCollectionChanged)
                {
                    current = children;
                    _list.Clear();
                }
                else
                {
                    ItemsReconciler.Sync(_list, children);
                    current = _list;
                }

                if (ReferenceEquals(current, Current))
                    return;

                _current = current;
                foreach (var observer in _observers.ToArray())
                    observer.OnNext(current);
            }

            public IDisposable Subscribe(IObserver<object?> observer)
            {
                _observers.Add(observer);
                observer.OnNext(Current);
                return new Subscription(this, observer);
            }

            private void Unsubscribe(IObserver<object?> observer)
            {
                if (_observers.Remove(observer))
                    owner.DropIfIdle(this);
            }

            private sealed class Subscription(ChildrenSource source, IObserver<object?> observer)
                : IDisposable
            {
                private bool _disposed;

                public void Dispose()
                {
                    if (_disposed)
                        return;
                    _disposed = true;
                    source.Unsubscribe(observer);
                }
            }
        }

        private sealed class Template(TreeDataTemplateNodeBase<T, TChild> node) : ITreeDataTemplate
        {
            public bool Match(object? data) => data is T;

            public Control? Build(object? param) => node.Build(param);

            public InstancedBinding? ItemsSelector(object item) => node.SelectItems(item);
        }
    }

    /// <summary>
    /// Runs the patch once per control, when it is built, and takes the children from that call.
    /// Values that should change afterwards reach the control through channels; or invalidate to
    /// build again.
    /// </summary>
    [ProcessNode(Name = "TreeDataTemplate")]
    [Region(SupportedBorderControlPoints = ControlPointType.None)]
    public class TreeDataTemplateNode<T, TChild>
        : TreeDataTemplateNodeBase<T, TChild>,
            IRegion<ITreeDataTemplateInlay<T, TChild>>
    {
        [Fragment]
        public TreeDataTemplateNode() { }

        public void Update(bool invalidate = false)
        {
            if (invalidate)
                Invalidate();
        }
    }

    /// <summary>
    /// Runs the patch of every built control again each frame, with that control's data item,
    /// so nodes that need more than one frame work inside. The children follow the patch: a new
    /// list (by reference) is compared with the shown one, and only the child items that changed
    /// are added or removed. Later returned controls are ignored: the control placed is the one
    /// from the first call.
    /// </summary>
    [ProcessNode(Name = "TreeDataTemplate (Advanced Experimental)")]
    [Region(SupportedBorderControlPoints = ControlPointType.None)]
    public class TreeDataTemplateExperimentalNode<T, TChild>
        : TreeDataTemplateNodeBase<T, TChild>,
            IRegion<ITreeDataTemplateInlay<T, TChild>>
    {
        [Fragment]
        public TreeDataTemplateExperimentalNode() { }

        public void Update() => UpdateInstances();
    }
}
