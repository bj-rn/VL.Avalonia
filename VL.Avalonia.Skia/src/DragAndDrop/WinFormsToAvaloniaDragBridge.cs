using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Control = Avalonia.Controls.Control;

namespace VL.Avalonia.Skia.DragAndDrop
{
    public class WinFormsToAvaloniaDragBridge : IDisposable
    {
        private readonly Form _form;
        private readonly OleDropTarget _dropTarget;
        private bool _disposed;

        public WinFormsToAvaloniaDragBridge(Form form, Control control)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form));
            var targetControl = control ?? throw new ArgumentNullException(nameof(control));

            var topLevel = TopLevel.GetTopLevel(targetControl);
            if (topLevel == null)
                throw new InvalidOperationException("Control is not attached to a TopLevel.");

            var platformImpl = topLevel.PlatformImpl;
            var inputRoot = topLevel as IInputRoot;

            if (platformImpl == null || inputRoot == null)
                throw new InvalidOperationException(
                    "Could not resolve Avalonia PlatformImpl or InputRoot."
                );

            var dragDevice = AvaloniaLocator.Current.GetService<IDragDropDevice>();
            if (dragDevice == null)
                throw new InvalidOperationException(
                    "IDragDropDevice not found in AvaloniaLocator."
                );

            if (!OleContext.EnsureInitialized())
                throw new InvalidOperationException("Failed to initialize OLE. Ensure STAThread.");

            if (!_form.IsHandleCreated)
            {
                var _ = _form.Handle;
            }
            _form.AllowDrop = true;

            _dropTarget = new OleDropTarget(topLevel, inputRoot, dragDevice);

            var res = OleInterop.RegisterDragDrop(_form.Handle, _dropTarget);
            if (res != 0 && res != -2147221247) // S_OK or DRAGDROP_E_ALREADYREGISTERED
            {
                throw new Exception($"RegisterDragDrop failed with code {res}");
            }

            _form.HandleDestroyed += OnFormHandleDestroyed;
        }

        private void OnFormHandleDestroyed(object? sender, EventArgs e) => Dispose();

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            _form.HandleDestroyed -= OnFormHandleDestroyed;

            if (_form.Handle != IntPtr.Zero)
            {
                OleInterop.RevokeDragDrop(_form.Handle);
            }
        }
    }
}
