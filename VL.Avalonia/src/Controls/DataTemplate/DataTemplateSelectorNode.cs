using Avalonia.Controls;
using Avalonia.Controls.Templates;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base interface for DataTemplate region in VL
    /// You can check DataTemplate in vl definitions
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TIn"></typeparam>
    public interface IDataTemplateRegion<TState, TIn>
    {
        /// <summary>
        /// Used internally to convert patch to delegate
        /// </summary>
        public TState InvokeRegion(TState state, TIn input, out Control output);
    }

    [ProcessNode(
        Name = "DataTemplateSelector",
        HasStateOutput = true,
        FragmentSelection = FragmentSelection.Explicit
    )]
    public class DataTemplateSelectorNode : IDataTemplate
    {
        protected IReadOnlyList<IDataTemplate> Templates { get; set; } =
            Spread<IDataTemplate>.Empty;

        [Fragment]
        public DataTemplateSelectorNode() { }

        [Fragment]
        public virtual void SetTemplates(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<IDataTemplate> templates
        )
        {
            if (!ReferenceEquals(Templates, templates))
            {
                Templates = templates ?? Spread<IDataTemplate>.Empty;
            }
        }

        public Control? Build(object? param)
        {
            foreach (var template in Templates)
            {
                if (template != null && template.Match(param))
                {
                    return template.Build(param);
                }
            }

            return null;
        }

        public bool Match(object? data)
        {
            foreach (var template in Templates)
            {
                if (template != null && template.Match(data))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
