using Dock.Avalonia.Controls;
using Dock.Model.Avalonia;
using Dock.Model.Core;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Dock.Controls
{
    [ProcessNode(Name = "DockControl")]
    public partial class DockControlWrapper : TemplatedControlWrapperBase<DockControl>
    {
        public DockControlWrapper()
        {
            _output.Factory = new Factory();
        }

        private Optional<IDock> _layout;
        [Fragment(Order = PinOrder.Main)]
        public void SetLayout(Optional<IDock> layout)
        {
            if (_layout != layout)
            {
                if (layout.HasValue)
                {
                    _output.SetValue(DockControl.LayoutProperty, layout.Value);
                }
                else
                {
                    _output.ClearValue(DockControl.LayoutProperty);
                }

                _layout = layout;
            }
        }
    }
}
