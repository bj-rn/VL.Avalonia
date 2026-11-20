using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using VL.Core.Import;
using VL.Core.PublicAPI;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// The patch inside the <c>DataTemplate</c> region: one instance per built control, called
    /// with that control's data item when it is built. The control it returns is the one placed.
    /// </summary>
    public interface IDataTemplateInlay<T>
    {
        Control? Update(T input);
    }

    /// <summary>
    /// A data template that matches data of type <typeparamref name="T"/> and builds its control
    /// with the patch inside the region. Null and data of other types never match, so the patch
    /// is only ever called with a <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// Each built control has its own patch instance, disposed when the control leaves the
    /// logical tree, or with the region. Invalidate hands out a new <see cref="Output"/>, so the
    /// controls using it build their content again.
    /// </remarks>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract class DataTemplateNodeBase<T> : IRegion<IDataTemplateInlay<T>>, IDisposable
    {
        private readonly Dictionary<InputDescription, object?> _inputs = new();
        private readonly Dictionary<OutputDescription, object?> _outputs = new();
        private readonly Dictionary<Control, Instance> _instances = new();
        private Instance[]? _snapshot;
        private Func<IDataTemplateInlay<T>>? _factory;
        private Template _output;

        protected DataTemplateNodeBase()
        {
            _output = new Template(this);
        }

        protected readonly record struct Instance(
            Control Control,
            IDataTemplateInlay<T> Inlay,
            T Data
        );

        [Fragment]
        public IDataTemplate Output => _output;

        /// <summary>
        /// The live instances, rebuilt only when one is added or removed. An instance released
        /// while the array is walked stays in it; check <see cref="IsLive"/> before using it.
        /// </summary>
        protected Instance[] Instances => _snapshot ??= _instances.Values.ToArray();

        protected bool IsLive(in Instance instance) =>
            _instances.TryGetValue(instance.Control, out var live) && live.Inlay == instance.Inlay;

        protected void Invalidate() => _output = new Template(this);

        private Control? Build(object? param)
        {
            if (param is not T data || _factory is null)
                return null;

            var inlay = _factory();
            var control = inlay.Update(data);
            if (control is null)
            {
                (inlay as IDisposable)?.Dispose();
                return null;
            }

            Release(control);
            _instances[control] = new Instance(control, inlay, data);
            _snapshot = null;
            control.DetachedFromLogicalTree += OnDetached;
            return control;
        }

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
                (instance.Inlay as IDisposable)?.Dispose();
            }
        }

        void IRegion<IDataTemplateInlay<T>>.SetPatchInlayFactory(
            Func<IDataTemplateInlay<T>> patchInlayFactory
        ) => _factory = patchInlayFactory;

        void IRegion<IDataTemplateInlay<T>>.AcknowledgeInput(
            in InputDescription description,
            object? outerValue
        ) => _inputs[description] = outerValue;

        void IRegion<IDataTemplateInlay<T>>.RetrieveInput(
            in InputDescription description,
            IDataTemplateInlay<T> patchInlay,
            out object? innerValue
        ) => _inputs.TryGetValue(description, out innerValue);

        void IRegion<IDataTemplateInlay<T>>.AcknowledgeOutput(
            in OutputDescription description,
            IDataTemplateInlay<T> patchInlay,
            object? innerValue
        ) => _outputs[description] = innerValue;

        void IRegion<IDataTemplateInlay<T>>.RetrieveOutput(
            in OutputDescription description,
            out object? outerValue
        ) => _outputs.TryGetValue(description, out outerValue);

        public void Dispose()
        {
            foreach (var control in _instances.Keys.ToList())
                Release(control);
        }

        private sealed class Template(DataTemplateNodeBase<T> node) : IDataTemplate
        {
            public bool Match(object? data) => data is T;

            public Control? Build(object? param) => node.Build(param);
        }
    }

    /// <summary>
    /// Runs the patch once per control, when it is built. Values that should change afterwards
    /// reach the control through channels; or invalidate to build again.
    /// </summary>
    [ProcessNode(Name = "DataTemplate")]
    [Region(SupportedBorderControlPoints = ControlPointType.None)]
    public class DataTemplateNode<T> : DataTemplateNodeBase<T>, IRegion<IDataTemplateInlay<T>>
    {
        [Fragment]
        public DataTemplateNode() { }

        public void Update(bool invalidate = false)
        {
            if (invalidate)
                Invalidate();
        }
    }

    /// <summary>
    /// Runs the patch of every built control again each frame, with that control's data item,
    /// so nodes that need more than one frame work inside. Later returns of the patch are
    /// ignored: the control placed is the one from the first call.
    /// </summary>
    [ProcessNode(Name = "DataTemplate (Advanced Experimental)")]
    [Region(SupportedBorderControlPoints = ControlPointType.None)]
    public class DataTemplateExperimentalNode<T>
        : DataTemplateNodeBase<T>,
            IRegion<IDataTemplateInlay<T>>
    {
        [Fragment]
        public DataTemplateExperimentalNode() { }

        public void Update()
        {
            foreach (var instance in Instances)
            {
                if (IsLive(instance))
                    instance.Inlay.Update(instance.Data);
            }
        }
    }
}
