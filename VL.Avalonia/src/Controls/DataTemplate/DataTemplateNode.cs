using Avalonia.Controls;
using Avalonia.Controls.Templates;
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
    public abstract class DataTemplateNodeBase<T> : TemplateRegionBase<IDataTemplateInlay<T>, T>
    {
        private Template _output;

        protected DataTemplateNodeBase()
        {
            _output = new Template(this);
        }

        [Fragment]
        public IDataTemplate Output => _output;

        protected void Invalidate() => _output = new Template(this);

        private Control? Build(object? param)
        {
            if (param is not T data || CreateInlay() is not { } inlay)
                return null;

            var control = inlay.Update(data);
            if (control is null)
            {
                DisposeInlay(inlay);
                return null;
            }

            Track(control, inlay, data);
            return control;
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
                    instance.Inlay.Update(instance.State);
            }
        }
    }
}