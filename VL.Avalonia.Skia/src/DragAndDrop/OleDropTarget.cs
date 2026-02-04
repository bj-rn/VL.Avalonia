using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using AvaloniaDragDropEffects = Avalonia.Input.DragDropEffects;
// Aliases to resolve ambiguities
using AvaloniaPoint = Avalonia.Point;
using IComDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;
using Input = Avalonia.Input;

namespace VL.Avalonia.Skia.DragAndDrop
{
    [ComVisible(true)]
    internal class OleDropTarget : OleInterop.IDropTarget
    {
        private readonly TopLevel _topLevel;
        private readonly IInputRoot _inputRoot;
        private readonly IDragDropDevice _dragDevice;

        // Explicitly use Avalonia.Input.IDataObject
        private Input.IDataObject? _currentData;

        public OleDropTarget(TopLevel topLevel, IInputRoot inputRoot, IDragDropDevice dragDevice)
        {
            _topLevel = topLevel;
            _inputRoot = inputRoot;
            _dragDevice = dragDevice;
        }

        public void DragEnter(
            IComDataObject pDataObj,
            int grfKeyState,
            OleInterop.POINT pt,
            ref int pdwEffect
        )
        {
            _currentData = new OleDataObject(pDataObj);
            var effect = (AvaloniaDragDropEffects)pdwEffect;
            var modifiers = ConvertKeyState(grfKeyState);
            var location = GetLocation(pt);

            var args = new RawDragEvent(
                _dragDevice,
                RawDragEventType.DragEnter,
                _inputRoot,
                location,
                _currentData,
                effect,
                modifiers
            );

            _topLevel.PlatformImpl?.Input?.Invoke(args);
            pdwEffect = (int)args.Effects;
        }

        public void DragOver(int grfKeyState, OleInterop.POINT pt, ref int pdwEffect)
        {
            if (_currentData == null)
                return;

            var effect = (AvaloniaDragDropEffects)pdwEffect;
            var modifiers = ConvertKeyState(grfKeyState);
            var location = GetLocation(pt);

            var args = new RawDragEvent(
                _dragDevice,
                RawDragEventType.DragOver,
                _inputRoot,
                location,
                _currentData,
                effect,
                modifiers
            );

            _topLevel.PlatformImpl?.Input?.Invoke(args);
            pdwEffect = (int)args.Effects;
        }

        public void DragLeave()
        {
            if (_currentData != null)
            {
                var args = new RawDragEvent(
                    _dragDevice,
                    RawDragEventType.DragLeave,
                    _inputRoot,
                    default,
                    _currentData,
                    AvaloniaDragDropEffects.None,
                    RawInputModifiers.None
                );

                _topLevel.PlatformImpl?.Input?.Invoke(args);
                _currentData = null;
            }
        }

        public void Drop(
            IComDataObject pDataObj,
            int grfKeyState,
            OleInterop.POINT pt,
            ref int pdwEffect
        )
        {
            if (_currentData == null)
                _currentData = new OleDataObject(pDataObj);

            var effect = (AvaloniaDragDropEffects)pdwEffect;
            var modifiers = ConvertKeyState(grfKeyState);
            var location = GetLocation(pt);

            var args = new RawDragEvent(
                _dragDevice,
                RawDragEventType.Drop,
                _inputRoot,
                location,
                _currentData,
                effect,
                modifiers
            );

            _topLevel.PlatformImpl?.Input?.Invoke(args);
            pdwEffect = (int)args.Effects;
            _currentData = null;
        }

        private AvaloniaPoint GetLocation(OleInterop.POINT pt)
        {
            if (_topLevel is Visual v)
            {
                // Convert screen coordinates to client coordinates
                var screenPoint = new PixelPoint(pt.X, pt.Y);
                return v.PointToClient(screenPoint);
            }
            return new AvaloniaPoint(0, 0);
        }

        private static RawInputModifiers ConvertKeyState(int keys)
        {
            var modifiers = RawInputModifiers.None;
            if ((keys & 1) != 0)
                modifiers |= RawInputModifiers.LeftMouseButton;
            if ((keys & 2) != 0)
                modifiers |= RawInputModifiers.RightMouseButton;
            if ((keys & 4) != 0)
                modifiers |= RawInputModifiers.Shift;
            if ((keys & 8) != 0)
                modifiers |= RawInputModifiers.Control;
            if ((keys & 16) != 0)
                modifiers |= RawInputModifiers.MiddleMouseButton;
            if ((keys & 32) != 0)
                modifiers |= RawInputModifiers.Alt;
            return modifiers;
        }
    }
}
