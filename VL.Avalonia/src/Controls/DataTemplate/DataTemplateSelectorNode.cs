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
        TState InvokeRegion(TState state, TIn input, out Control output);
    }

    /// <summary>
    /// Builds data with the first of its templates that matches it.
    /// </summary>
    /// <remarks>
    /// A change of templates, including a template handing out a new instance when it is
    /// invalidated, or invalidating the selector itself, hands out a new <see cref="Output"/>,
    /// so the controls using it build their content again.
    /// </remarks>
    [ProcessNode(Name = "DataTemplateSelector", FragmentSelection = FragmentSelection.Explicit)]
    public class DataTemplateSelectorNode
    {
        private Selector _output = new(Spread<IDataTemplate>.Empty);

        [Fragment]
        public DataTemplateSelectorNode() { }

        [Fragment]
        public IDataTemplate Output => _output;

        [Fragment]
        public void Update(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<IDataTemplate> templates,
            bool invalidate = false
        )
        {
            templates ??= Spread<IDataTemplate>.Empty;
            if (invalidate || !_output.Holds(templates))
                _output = new Selector(templates);
        }

        private sealed class Selector(IReadOnlyList<IDataTemplate> templates) : IDataTemplate
        {
            /// <summary>
            /// Whether it holds the same templates in the same order. A pin group may hand over a
            /// new spread with the same templates, which must not rebuild the content.
            /// </summary>
            public bool Holds(IReadOnlyList<IDataTemplate> other)
            {
                if (ReferenceEquals(templates, other))
                    return true;
                if (templates.Count != other.Count)
                    return false;
                for (var i = 0; i < templates.Count; i++)
                {
                    if (!ReferenceEquals(templates[i], other[i]))
                        return false;
                }
                return true;
            }

            public Control? Build(object? param)
            {
                foreach (var template in templates)
                {
                    if (template != null && template.Match(param))
                        return template.Build(param);
                }

                return null;
            }

            public bool Match(object? data)
            {
                foreach (var template in templates)
                {
                    if (template != null && template.Match(data))
                        return true;
                }

                return false;
            }
        }
    }
}
