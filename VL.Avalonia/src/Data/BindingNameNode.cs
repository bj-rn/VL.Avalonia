using Avalonia;
using VL.Core.Import;

// TODO: REFACTOR

namespace VL.Avalonia.Data
{
    [ProcessNode(Name = "Binding (Name)")]
    public class BindingNameNode<TControl, TValue> : BindingNode<TControl, AvaloniaProperty, TValue>
        where TControl : AvaloniaObject
    {
        private string _propertyName;

        [Fragment(Order = PinOrder.Main)]
        public void SetProperty(string propertyName)
        {
            if (_propertyName != propertyName)
            {
                _propertyName = propertyName;

                var inputType = _input?.GetType();

                if (inputType != null)
                {
                    var prop = AvaloniaPropertyRegistry.Instance.FindRegistered(
                        inputType,
                        propertyName
                    );

                    if (prop != null)
                    {
                        SetProperty(prop);
                    }
                }
            }
        }
    }
}
