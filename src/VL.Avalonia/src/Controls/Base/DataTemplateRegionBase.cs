using Avalonia.Controls.Templates;
using VL.Core.Import;
using VL.Core.PublicAPI;

namespace VL.Avalonia.Controls.Base
{
    public interface IDataTemplateRegion<TState, TIn, TOut>
    {
        public void InvokeRegion(in TState inputState, in TIn input, out TState outputState, out TOut output);
        // public bool IsChanged();
    }


    public abstract class DataTemplateRegionBase<T> : IRegion<T>
    {
        protected Func<T>? _patchInlayFactory;
        protected readonly Dictionary<InputDescription, object> _inputValues = new();
        protected readonly Dictionary<OutputDescription, object> _outputValues = new();

        public void AcknowledgeInput(in InputDescription description, object outerValue)
        {
            _inputValues[description] = outerValue;
        }

        public void AcknowledgeOutput(in OutputDescription description, T patchInlay, object innerValue)
        {
            _outputValues[description] = innerValue;
        }

        public void RetrieveInput(in InputDescription description, T patchInlay, out object innerValue)
        {
            _inputValues.TryGetValue(description, out innerValue);
        }

        public void RetrieveOutput(in OutputDescription description, out object outerValue)
        {
            _outputValues.TryGetValue(description, out outerValue);
        }

        public void SetPatchInlayFactory(Func<T> patchInlayFactory)
        {
            this._patchInlayFactory = patchInlayFactory;
        }
    }


    [ProcessNode(HasStateOutput = true)]

    public class DataTemplateRegion : DataTemplateRegionBase<FuncDataTemplate>
    {

    }
}
