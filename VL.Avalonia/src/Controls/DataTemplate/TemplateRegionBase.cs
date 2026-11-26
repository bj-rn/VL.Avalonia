using Avalonia.Controls;
using Avalonia.LogicalTree;
using VL.Core.Import;
using VL.Core.PublicAPI;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Shared part of the template regions: hands the border values to the patch and keeps one
    /// patch instance per built control, disposed when the control leaves the logical tree, or
    /// with the region.
    /// </summary>
    /// <typeparam name="TInlay">The patch inside the region.</typeparam>
    /// <typeparam name="TState">What is kept with each built control, e.g. its data item.</typeparam>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract class TemplateRegionBase<TInlay, TState> : IRegion<TInlay>, IDisposable
        where TInlay : class
    {
        private readonly Dictionary<InputDescription, object?> _inputs = new();
        private readonly Dictionary<OutputDescription, object?> _outputs = new();
        private readonly Dictionary<Control, Instance> _instances = new();
        private Instance[]? _snapshot;
        private Func<TInlay>? _factory;

        protected readonly record struct Instance(Control Control, TInlay Inlay, TState State);

        /// <summary>
        /// The live instances, rebuilt only when one is added or removed. An instance released
        /// while the array is walked stays in it; check <see cref="IsLive"/> before using it.
        /// </summary>
        protected Instance[] Instances => _snapshot ??= _instances.Values.ToArray();

        protected bool IsLive(in Instance instance) =>
            _instances.TryGetValue(instance.Control, out var live) && live.Inlay == instance.Inlay;

        /// <summary>
        /// A new patch instance, or null while the region has none to hand out.
        /// </summary>
        protected TInlay? CreateInlay() => _factory?.Invoke();

        protected static void DisposeInlay(TInlay inlay) => (inlay as IDisposable)?.Dispose();

        /// <summary>
        /// Keeps the patch instance with the control until the control leaves the logical tree.
        /// </summary>
        protected void Track(Control control, TInlay inlay, TState state)
        {
            Release(control);
            _instances[control] = new Instance(control, inlay, state);
            _snapshot = null;
            control.DetachedFromLogicalTree += OnDetached;
        }

        /// <summary>
        /// Called after an instance is released, before its patch is disposed.
        /// </summary>
        protected virtual void OnReleased(in Instance instance) { }

        private void OnDetached(object? sender, LogicalTreeAttachmentEventArgs e)
        {
            if (sender is Control control)
                Release(control);
        }

        private void Release(Control control)
        {
            control.DetachedFromLogicalTree -= OnDetached;
            if (_instances.Remove(control, out var instance))
            {
                _snapshot = null;
                OnReleased(instance);
                DisposeInlay(instance.Inlay);
            }
        }

        void IRegion<TInlay>.SetPatchInlayFactory(Func<TInlay> patchInlayFactory) =>
            _factory = patchInlayFactory;

        void IRegion<TInlay>.AcknowledgeInput(in InputDescription description, object? outerValue) =>
            _inputs[description] = outerValue;

        void IRegion<TInlay>.RetrieveInput(
            in InputDescription description,
            TInlay patchInlay,
            out object? innerValue
        ) => _inputs.TryGetValue(description, out innerValue);

        void IRegion<TInlay>.AcknowledgeOutput(
            in OutputDescription description,
            TInlay patchInlay,
            object? innerValue
        ) => _outputs[description] = innerValue;

        void IRegion<TInlay>.RetrieveOutput(in OutputDescription description, out object? outerValue) =>
            _outputs.TryGetValue(description, out outerValue);

        public virtual void Dispose()
        {
            foreach (var control in _instances.Keys.ToList())
                Release(control);
        }
    }
}
