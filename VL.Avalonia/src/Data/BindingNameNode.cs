using Avalonia;
using VL.Core.Import;

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

                var prop = AvaloniaPropertyRegistry.Instance.FindRegistered(
                    _input?.GetType(),
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
