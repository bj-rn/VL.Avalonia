using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Platform;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using System.Diagnostics;
using VL.Lib.IO.Notifications;
using Point = Avalonia.Point;
using Size = Avalonia.Size;
using Vector2 = Stride.Core.Mathematics.Vector2;

namespace VL.Skia.Avalonia
{
    // source
    // https://github.com/vvvv/VL.StandardLibs/blob/dev/azeno/avalonia/VL.AvaloniaUI/src/RootElementImpl.cs
    sealed class GammaTopLevelImpl : ITopLevelImpl
    {
        public static IKeyboardDevice Keyboard { get; } = new KeyboardDevice();

        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();


        public GammaTopLevelImpl(Compositor compositor)
        {
            MouseDevice = new MouseDevice();
            //https://github.com/AvaloniaUI/Avalonia/blob/master/src/Windows/Avalonia.Win32/Input/WindowsKeyboardDevice.cs#L7
            //KeyboardDevice = WindowsKeyboardDevice.Instance

            _touchDevice = new TouchDevice();

            Compositor = compositor;
        }


        // That's something important throws without it
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/AvaloniaControl.cs#L130
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotPlatform.cs#L48
        public Compositor Compositor { get; set; }

        // TODO: DPI
        public double DesktopScaling => 1.0;

        private double _renderScaling = 1;
        /// <summary>
        /// Render Scaling
        /// </summary>
        public double RenderScaling
        {
            get => _renderScaling;
            set
            {
                if (_renderScaling != value)
                {
                    _renderScaling = value;
                    ScalingChanged?.Invoke(value);
                }
            }
        }

        private Size _clientSize;
        /// <summary>
        /// Client Size
        /// </summary>
        public Size ClientSize
        {
            get => _clientSize;
            set
            {
                if (_clientSize != value)
                {
                    _clientSize = value;
                    Resized?.Invoke(value, WindowResizeReason.Unspecified);
                }
            }
        }


        private readonly List<CallerInfo> _callerInfos = new List<CallerInfo>();
        /// <summary>
        /// Caller Info's
        /// </summary>
        public IEnumerable<object> Surfaces => _callerInfos;

        // TODO: Implement
        // NEEDS WINDO HANDLE HERE
        public IPlatformHandle? Handle => new PlatformHandle(IntPtr.Zero, "STUB");

        public IKeyboardDevice KeyboardDevice { get; } = GammaDevices.KeyboardDevice;
        public IMouseDevice MouseDevice { get; }

        // not sure about it being private. did it this way becaus that's how it is handled in Estragonia
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L24
        private readonly TouchDevice _touchDevice;

        public IInputRoot InputRoot { get; set; }

        private RawInputModifiers _inputModifiers;
        public Action<RawInputEventArgs>? Input { get; set; }
        public void SetInputRoot(IInputRoot inputRoot)
           => InputRoot = inputRoot;


        internal bool Notify(INotification notification, CallerInfo caller)
        {
            var position = new Point(0, 0);

            if (notification is NotificationWithPosition n)
                position = n.Position.ToPoint();

            return HandleNotification(notification, position);
        }

        internal bool SendNotification(INotification notification, Func<NotificationWithPosition, Vector2> getPosition)
        {

            var position = new Point(0, 0);

            if (notification is NotificationWithPosition n)
                position = getPosition(n).ToPoint();

            return HandleNotification(notification, position);
        }

        internal bool HandleNotification(INotification notification, Point position)
        {
            if (InputRoot is null || Input is not { } input)
            {
                return false;
            }

            if (notification is MouseNotification mouseNotification)
                return HandleMouseNotification(mouseNotification, input, position);
            if (notification is KeyNotification keyNotification)
                return HandleKeyNotification(keyNotification, input);
            if (notification is TouchNotification touchNotification)
                return HandleTouchNotification(touchNotification, input, position);
            return false;

        }


        private bool HandleMouseNotification(MouseNotification notification, Action<RawInputEventArgs> input, Point position)
        {
            var e = default(RawInputEventArgs);

            if (notification is MouseButtonNotification m)
            {
                if (m.Kind == MouseNotificationKind.MouseDown)
                    _inputModifiers |= m.Buttons.ToModifier();
                else if (m.Kind == MouseNotificationKind.MouseUp)
                    _inputModifiers ^= m.Buttons.ToModifier();
            }

            if (notification is MouseDownNotification mouseDown)
                input(e = new RawPointerEventArgs(MouseDevice, Timestamp, InputRoot, mouseDown.Buttons.ToEventType(false), position, _inputModifiers));
            else if (notification is MouseUpNotification mouseUp)
                input(e = new RawPointerEventArgs(MouseDevice, Timestamp, InputRoot, mouseUp.Buttons.ToEventType(true), position, _inputModifiers));
            else if (notification is MouseMoveNotification mouseMove)
                input(e = new RawPointerEventArgs(MouseDevice, Timestamp, InputRoot, RawPointerEventType.Move, position, _inputModifiers));
            else if (notification is MouseWheelNotification mouseWheel)
                input(e = new RawMouseWheelEventArgs(MouseDevice, Timestamp, InputRoot, position, new Vector(mouseWheel.WheelDelta, 0), _inputModifiers));
         
            if (e != null)
                return e.Handled;

            return false;
        }


        private bool HandleKeyNotification(KeyNotification notification, Action<RawInputEventArgs> input)
        {
            var e = default(RawInputEventArgs);

            if (notification is KeyCodeNotification keyCode)
            {
                _inputModifiers = keyCode.KeyData.ToModifier();
            }

            if (notification is KeyDownNotification keyDown)
                input(e = new RawKeyEventArgs(KeyboardDevice, Timestamp, InputRoot, RawKeyEventType.KeyDown, keyDown.KeyData.ToKey(), _inputModifiers, keyDown.KeyData.ToPhysicalKey(), keyDown.KeyData.ToKeySymbol()));
            else if (notification is KeyUpNotification keyUp)
                input(e = new RawKeyEventArgs(KeyboardDevice, Timestamp, InputRoot, RawKeyEventType.KeyUp, keyUp.KeyData.ToKey(), _inputModifiers, keyUp.KeyData.ToPhysicalKey(), keyUp.KeyData.ToKeySymbol()));
            else if (notification is KeyPressNotification keyPress && !char.IsControl(keyPress.KeyChar))
                input(e = new RawTextInputEventArgs(KeyboardDevice, Timestamp, InputRoot, keyPress.KeyChar.ToString()));

            if (e != null)
                return e.Handled;

            return false;
        }

        private bool HandleTouchNotification(TouchNotification notification, Action<RawInputEventArgs> input, Point position)
        {
            var e = default(RawInputEventArgs);

            if (notification.Kind == TouchNotificationKind.TouchDown)
                input(e = new RawPointerEventArgs(_touchDevice, Timestamp, InputRoot, RawPointerEventType.TouchBegin, position, _inputModifiers));
            else if (notification.Kind == TouchNotificationKind.TouchUp)
                input(e = new RawPointerEventArgs(_touchDevice, Timestamp, InputRoot, RawPointerEventType.TouchEnd, position, _inputModifiers));
            else if (notification.Kind == TouchNotificationKind.TouchMove)
                input(e = new RawPointerEventArgs(_touchDevice, Timestamp, InputRoot, RawPointerEventType.TouchUpdate, position, _inputModifiers));

            if (e != null)
                return e.Handled;

            return false;
        }

        public Action<Rect>? Paint { get; set; }

        public Action<Size, WindowResizeReason>? Resized { get; set; }

        // Wonder is it DesktopScaling or RenderScaling
        public Action<double>? ScalingChanged { get; set; }


        public Action<WindowTransparencyLevel>? TransparencyLevelChanged { get; set; }


        public Action? Closed { get; set; }
        public Action? LostFocus { get; set; }

        public WindowTransparencyLevel TransparencyLevel { get; set; } = WindowTransparencyLevel.None;

        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L76
        public AcrylicPlatformCompensationLevels AcrylicCompensationLevels { get; } = new(1.0, 1.0, 1.0);

        private ulong Timestamp => (ulong)_stopwatch.ElapsedMilliseconds;

        public IPopupImpl? CreatePopup()
        {
            return null;
        }

        internal void Render(CallerInfo caller)
        {
            var bounds = caller.ViewportBounds.ToAvaloniaRect();
            ClientSize = bounds.Size;

            _callerInfos.Clear();
            _callerInfos.Add(caller);

            caller.Canvas.Save();
            try
            {
                Paint?.Invoke(bounds);
            }
            finally
            {
                caller.Canvas.Restore();
            }
        }

        public void Dispose()
        {
            // TODO
        }

        public Point PointToClient(PixelPoint point) => point.ToPoint(RenderScaling);
        public PixelPoint PointToScreen(Point point) => PixelPoint.FromPoint(point, RenderScaling);

        // example from Godot
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L364
        public void SetCursor(ICursorImpl? cursor)
        {
            //
        }

        public void SetFrameThemeVariant(PlatformThemeVariant themeVariant)
        {
            // throw new NotImplementedException();
        }





        // copy paste from here
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L376
        public void SetTransparencyLevelHint(IReadOnlyList<WindowTransparencyLevel> transparencyLevels)
        {
            foreach (var transparencyLevel in transparencyLevels)
            {
                if (transparencyLevel == WindowTransparencyLevel.Transparent || transparencyLevel == WindowTransparencyLevel.None)
                {
                    TransparencyLevel = transparencyLevel;
                    return;
                }
            }
        }

        // example from Godot
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L388
        public object? TryGetFeature(Type featureType)
        {
            // throws 
            return null;
        }

        internal bool SendNotification(INotification notification, bool v1, bool v2)
        {
            throw new NotImplementedException();
        }
    }
}
