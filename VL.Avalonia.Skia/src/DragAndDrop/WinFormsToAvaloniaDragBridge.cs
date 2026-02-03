using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Control = Avalonia.Controls.Control;
using DragDropEffects = Avalonia.Input.DragDropEffects;
using Point = Avalonia.Point;

namespace VL.Avalonia.Skia.DragAndDrop
{
    public class WinFormsToAvaloniaDragBridge : IDisposable
    {
        private readonly Form _winFormsSource;
        private readonly TopLevel _avaloniaTarget;
        private readonly IInputManager _inputManager;
        private readonly IDragDropDevice _dragDevice;

        public WinFormsToAvaloniaDragBridge(Form winFormsSource, Control avaloniaControl)
        {
            _winFormsSource = winFormsSource;

            // Find the TopLevel (Window or generic InputRoot) of the Avalonia control
            _avaloniaTarget =
                TopLevel.GetTopLevel(avaloniaControl)
                ?? throw new InvalidOperationException("Control must be attached to a TopLevel");

            _inputManager = AvaloniaLocator.Current.GetService<IInputManager>()!;
            _dragDevice = AvaloniaLocator.Current.GetService<IDragDropDevice>()!;

            // Hook WinForms events
            _winFormsSource.AllowDrop = true;
            _winFormsSource.DragEnter += OnDragEnter;
            _winFormsSource.DragOver += OnDragOver;
            _winFormsSource.DragLeave += OnDragLeave;
            _winFormsSource.DragDrop += OnDragDrop;
        }

        private void OnDragEnter(object? sender, System.Windows.Forms.DragEventArgs e) =>
            ProcessEvent(RawDragEventType.DragEnter, e);

        private void OnDragOver(object? sender, System.Windows.Forms.DragEventArgs e) =>
            ProcessEvent(RawDragEventType.DragOver, e);

        private void OnDragDrop(object? sender, System.Windows.Forms.DragEventArgs e) =>
            ProcessEvent(RawDragEventType.Drop, e);

        private void OnDragLeave(object? sender, EventArgs e)
        {
            // Create an empty dummy wrapper for the Leave event
            var dummyData = new WinFormsAvaloniaDataObject(new System.Windows.Forms.DataObject());

            var args = new RawDragEvent(
                _dragDevice,
                RawDragEventType.DragLeave,
                _avaloniaTarget,
                new Point(-1, -1),
                dummyData,
                DragDropEffects.None,
                RawInputModifiers.None
            );

            _inputManager.ProcessInput(args);
        }

        private void ProcessEvent(RawDragEventType type, System.Windows.Forms.DragEventArgs e)
        {
            // 1. Coordinates: Screen -> Client
            var screenPt = new PixelPoint(e.X, e.Y);
            var clientPt = _avaloniaTarget.PointToClient(screenPt);

            // 2. Modifiers: WinForms -> Avalonia
            var modifiers = ConvertModifiers(e.KeyState);

            // 3. Data: Wrap the WinForms IDataObject
            var avaloniaData = new WinFormsAvaloniaDataObject(e.Data);

            // 4. Construct RawDragEvent
            // NOTE: We use the constructor that accepts IDataObject here.
            var rawEvent = new RawDragEvent(
                _dragDevice,
                type,
                _avaloniaTarget,
                clientPt,
                avaloniaData,
                (DragDropEffects)e.AllowedEffect,
                modifiers
            );

            // 5. Inject into Avalonia
            _inputManager.ProcessInput(rawEvent);

            // 6. Update WinForms Cursor
            e.Effect = (System.Windows.Forms.DragDropEffects)rawEvent.Effects;
        }

        private static RawInputModifiers ConvertModifiers(int keyState)
        {
            var mods = RawInputModifiers.None;
            if ((keyState & 1) != 0)
                mods |= RawInputModifiers.LeftMouseButton;
            if ((keyState & 2) != 0)
                mods |= RawInputModifiers.RightMouseButton;
            if ((keyState & 4) != 0)
                mods |= RawInputModifiers.Shift;
            if ((keyState & 8) != 0)
                mods |= RawInputModifiers.Control;
            if ((keyState & 16) != 0)
                mods |= RawInputModifiers.MiddleMouseButton;
            if ((keyState & 32) != 0)
                mods |= RawInputModifiers.Alt;
            return mods;
        }

        public void Dispose()
        {
            _winFormsSource.DragEnter -= OnDragEnter;
            _winFormsSource.DragOver -= OnDragOver;
            _winFormsSource.DragLeave -= OnDragLeave;
            _winFormsSource.DragDrop -= OnDragDrop;
        }
    }
}
