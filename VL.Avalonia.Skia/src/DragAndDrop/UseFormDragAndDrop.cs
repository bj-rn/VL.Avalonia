using VL.Core.Import;
using Control = Avalonia.Controls.Control;

namespace VL.Avalonia.Skia.DragAndDrop
{
    [ProcessNode]
    public class UseFormDragAndDrop
    {
        private Form _form;
        private Control _control;

        private WinFormsToAvaloniaDragBridge _bridge;

        private bool _invalidate;

        public void SetInput(Form form, Control control)
        {
            if (_form != form || _control != control)
            {
                _form = form;
                _control = control;

                _invalidate = true;
            }
        }

        public void Update()
        {
            if (_invalidate)
            {
                if (_bridge != null)
                {
                    _bridge.Dispose();
                }

                bool canRegister = _form != null && _control != null;

                if (canRegister)
                {
                    try
                    {
                        _bridge = new WinFormsToAvaloniaDragBridge(_form, _control);
                    }
                    catch (Exception ex) { }
                }
            }
        }
    }
}
