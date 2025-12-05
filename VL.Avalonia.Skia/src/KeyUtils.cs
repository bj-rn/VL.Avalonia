using Avalonia.Input;
using Avalonia.Input.Raw;
using VL.Lib.IO.Notifications;
using Keys = VL.Lib.IO.Keys;
using MouseButtons = VL.Lib.IO.MouseButtons;

namespace VL.Avalonia.Skia;

static class KeyUtils
{
    public static RawInputModifiers ToModifier(this Keys key)
    {
        var modifiers = RawInputModifiers.None;
        if (key.HasFlag(Keys.Shift))
            modifiers |= RawInputModifiers.Shift;
        if (key.HasFlag(Keys.Control))
            modifiers |= RawInputModifiers.Control;
        if (key.HasFlag(Keys.Alt))
            modifiers |= RawInputModifiers.Alt;
        return modifiers;
    }

    public static RawInputModifiers ToModifier(this MouseButtons button)
    {
        var modifiers = RawInputModifiers.None;
        if (button.HasFlag(MouseButtons.Left))
            modifiers |= RawInputModifiers.LeftMouseButton;
        if (button.HasFlag(MouseButtons.Middle))
            modifiers |= RawInputModifiers.MiddleMouseButton;
        if (button.HasFlag(MouseButtons.Right))
            modifiers |= RawInputModifiers.RightMouseButton;
        if (button.HasFlag(MouseButtons.XButton1))
            modifiers |= RawInputModifiers.XButton1MouseButton;
        if (button.HasFlag(MouseButtons.XButton2))
            modifiers |= RawInputModifiers.XButton2MouseButton;
        return modifiers;
    }

    public static RawPointerEventType ToEventType(this MouseButtons button, bool isUp)
    {
        switch (button)
        {
            case MouseButtons.Left:
                return isUp ? RawPointerEventType.LeftButtonUp : RawPointerEventType.LeftButtonDown;
            case MouseButtons.Right:
                return isUp
                    ? RawPointerEventType.RightButtonUp
                    : RawPointerEventType.RightButtonDown;
            case MouseButtons.Middle:
                return isUp
                    ? RawPointerEventType.MiddleButtonUp
                    : RawPointerEventType.MiddleButtonDown;
            case MouseButtons.XButton1:
                return isUp ? RawPointerEventType.XButton1Up : RawPointerEventType.XButton1Down;
            case MouseButtons.XButton2:
                return isUp ? RawPointerEventType.XButton2Up : RawPointerEventType.XButton2Down;
            default:
                return RawPointerEventType.Move;
        }
    }

    public static RawPointerEventType GetTouchPointerEventType(this TouchNotificationKind kind)
    {
        switch (kind)
        {
            case TouchNotificationKind.TouchDown:
                return RawPointerEventType.TouchBegin;
            case TouchNotificationKind.TouchUp:
                return RawPointerEventType.TouchEnd;
            case TouchNotificationKind.TouchMove:
                return RawPointerEventType.TouchUpdate;
            default:
                return RawPointerEventType.TouchCancel;
        }
    }

    public static Key ToKey(this Keys key)
    {
        switch (key & ~Keys.Modifiers)
        {
            case Keys.Cancel:
                return Key.Cancel;
            case Keys.Back:
                return Key.Back;
            case Keys.Tab:
                return Key.Tab;
            case Keys.LineFeed:
                return Key.LineFeed;
            case Keys.Clear:
                return Key.Clear;
            case Keys.Return:
                return Key.Return;
            case Keys.Pause:
                return Key.Pause;
            case Keys.Capital:
                return Key.Capital;
            case Keys.KanaMode:
                return Key.KanaMode;
            case Keys.JunjaMode:
                return Key.JunjaMode;
            case Keys.FinalMode:
                return Key.FinalMode;
            case Keys.HanjaMode:
                return Key.HanjaMode;
            case Keys.Escape:
                return Key.Escape;
            case Keys.IMEConvert:
                return Key.ImeConvert;
            case Keys.IMENonconvert:
                return Key.ImeNonConvert;
            case Keys.IMEAccept:
                return Key.ImeAccept;
            case Keys.IMEModeChange:
                return Key.ImeModeChange;
            case Keys.Space:
                return Key.Space;
            case Keys.Prior:
                return Key.Prior;
            case Keys.Next:
                return Key.Next;
            case Keys.End:
                return Key.End;
            case Keys.Home:
                return Key.Home;
            case Keys.Left:
                return Key.Left;
            case Keys.Up:
                return Key.Up;
            case Keys.Right:
                return Key.Right;
            case Keys.Down:
                return Key.Down;
            case Keys.Select:
                return Key.Select;
            case Keys.Print:
                return Key.Print;
            case Keys.Execute:
                return Key.Execute;
            case Keys.Snapshot:
                return Key.Snapshot;
            case Keys.Insert:
                return Key.Insert;
            case Keys.Delete:
                return Key.Delete;
            case Keys.Help:
                return Key.Help;
            case Keys.D0:
                return Key.D0;
            case Keys.D1:
                return Key.D1;
            case Keys.D2:
                return Key.D2;
            case Keys.D3:
                return Key.D3;
            case Keys.D4:
                return Key.D4;
            case Keys.D5:
                return Key.D5;
            case Keys.D6:
                return Key.D6;
            case Keys.D7:
                return Key.D7;
            case Keys.D8:
                return Key.D8;
            case Keys.D9:
                return Key.D9;
            case Keys.A:
                return Key.A;
            case Keys.B:
                return Key.B;
            case Keys.C:
                return Key.C;
            case Keys.D:
                return Key.D;
            case Keys.E:
                return Key.E;
            case Keys.F:
                return Key.F;
            case Keys.G:
                return Key.G;
            case Keys.H:
                return Key.H;
            case Keys.I:
                return Key.I;
            case Keys.J:
                return Key.J;
            case Keys.K:
                return Key.K;
            case Keys.L:
                return Key.L;
            case Keys.M:
                return Key.M;
            case Keys.N:
                return Key.N;
            case Keys.O:
                return Key.O;
            case Keys.P:
                return Key.P;
            case Keys.Q:
                return Key.Q;
            case Keys.R:
                return Key.R;
            case Keys.S:
                return Key.S;
            case Keys.T:
                return Key.T;
            case Keys.U:
                return Key.U;
            case Keys.V:
                return Key.V;
            case Keys.W:
                return Key.W;
            case Keys.X:
                return Key.X;
            case Keys.Y:
                return Key.Y;
            case Keys.Z:
                return Key.Z;
            case Keys.LWin:
                return Key.LWin;
            case Keys.RWin:
                return Key.RWin;
            case Keys.Apps:
                return Key.Apps;
            case Keys.Sleep:
                return Key.Sleep;
            case Keys.NumPad0:
                return Key.NumPad0;
            case Keys.NumPad1:
                return Key.NumPad1;
            case Keys.NumPad2:
                return Key.NumPad2;
            case Keys.NumPad3:
                return Key.NumPad3;
            case Keys.NumPad4:
                return Key.NumPad4;
            case Keys.NumPad5:
                return Key.NumPad5;
            case Keys.NumPad6:
                return Key.NumPad6;
            case Keys.NumPad7:
                return Key.NumPad7;
            case Keys.NumPad8:
                return Key.NumPad8;
            case Keys.NumPad9:
                return Key.NumPad9;
            case Keys.Multiply:
                return Key.Multiply;
            case Keys.Add:
                return Key.Add;
            case Keys.Separator:
                return Key.Separator;
            case Keys.Subtract:
                return Key.Subtract;
            case Keys.Decimal:
                return Key.Decimal;
            case Keys.Divide:
                return Key.Divide;
            case Keys.F1:
                return Key.F1;
            case Keys.F2:
                return Key.F2;
            case Keys.F3:
                return Key.F3;
            case Keys.F4:
                return Key.F4;
            case Keys.F5:
                return Key.F5;
            case Keys.F6:
                return Key.F6;
            case Keys.F7:
                return Key.F7;
            case Keys.F8:
                return Key.F8;
            case Keys.F9:
                return Key.F9;
            case Keys.F10:
                return Key.F10;
            case Keys.F11:
                return Key.F11;
            case Keys.F12:
                return Key.F12;
            case Keys.F13:
                return Key.F13;
            case Keys.F14:
                return Key.F14;
            case Keys.F15:
                return Key.F15;
            case Keys.F16:
                return Key.F16;
            case Keys.F17:
                return Key.F17;
            case Keys.F18:
                return Key.F18;
            case Keys.F19:
                return Key.F19;
            case Keys.F20:
                return Key.F20;
            case Keys.F21:
                return Key.F21;
            case Keys.F22:
                return Key.F22;
            case Keys.F23:
                return Key.F23;
            case Keys.F24:
                return Key.F24;
            case Keys.NumLock:
                return Key.NumLock;
            case Keys.Scroll:
                return Key.Scroll;
            case Keys.LShiftKey:
                return Key.LeftShift;
            case Keys.RShiftKey:
                return Key.RightShift;
            case Keys.LControlKey:
                return Key.LeftCtrl;
            case Keys.RControlKey:
                return Key.RightCtrl;
            case Keys.LMenu:
                return Key.LeftAlt;
            case Keys.RMenu:
                return Key.RightAlt;
            case Keys.BrowserBack:
                return Key.BrowserBack;
            case Keys.BrowserForward:
                return Key.BrowserForward;
            case Keys.BrowserRefresh:
                return Key.BrowserRefresh;
            case Keys.BrowserStop:
                return Key.BrowserStop;
            case Keys.BrowserSearch:
                return Key.BrowserSearch;
            case Keys.BrowserFavorites:
                return Key.BrowserFavorites;
            case Keys.BrowserHome:
                return Key.BrowserHome;
            case Keys.VolumeMute:
                return Key.VolumeMute;
            case Keys.VolumeDown:
                return Key.VolumeDown;
            case Keys.VolumeUp:
                return Key.VolumeUp;
            case Keys.MediaNextTrack:
                return Key.MediaNextTrack;
            case Keys.MediaPreviousTrack:
                return Key.MediaPreviousTrack;
            case Keys.MediaStop:
                return Key.MediaStop;
            case Keys.MediaPlayPause:
                return Key.MediaPlayPause;
            case Keys.LaunchMail:
                return Key.LaunchMail;
            case Keys.SelectMedia:
                return Key.SelectMedia;
            case Keys.LaunchApplication1:
                return Key.LaunchApplication1;
            case Keys.LaunchApplication2:
                return Key.LaunchApplication2;
            case Keys.Oem1:
                return Key.Oem1;
            case Keys.Oemplus:
                return Key.OemPlus;
            case Keys.Oemcomma:
                return Key.OemComma;
            case Keys.OemMinus:
                return Key.OemMinus;
            case Keys.OemPeriod:
                return Key.OemPeriod;
            case Keys.Oem2:
                return Key.Oem2;
            case Keys.Oem3:
                return Key.Oem3;
            case Keys.Oem4:
                return Key.Oem4;
            case Keys.Oem5:
                return Key.Oem5;
            case Keys.Oem6:
                return Key.Oem6;
            case Keys.Oem7:
                return Key.Oem7;
            case Keys.Oem8:
                return Key.Oem8;
            case Keys.Oem102:
                return Key.Oem102;
            case Keys.Attn:
                return Key.Attn;
            case Keys.Crsel:
                return Key.CrSel;
            case Keys.Exsel:
                return Key.ExSel;
            case Keys.EraseEof:
                return Key.EraseEof;
            case Keys.Play:
                return Key.Play;
            case Keys.Zoom:
                return Key.Zoom;
            case Keys.NoName:
                return Key.NoName;
            case Keys.Pa1:
                return Key.Pa1;
            case Keys.OemClear:
                return Key.OemClear;
            default:
                return Key.None;
        }
    }

    public static PhysicalKey ToPhysicalKey(this Keys key) =>
        (key & Keys.KeyCode) switch
        {
            // No key
            Keys.None => PhysicalKey.None,

            // Writing System Keys - Digits
            Keys.D0 => PhysicalKey.Digit0,
            Keys.D1 => PhysicalKey.Digit1,
            Keys.D2 => PhysicalKey.Digit2,
            Keys.D3 => PhysicalKey.Digit3,
            Keys.D4 => PhysicalKey.Digit4,
            Keys.D5 => PhysicalKey.Digit5,
            Keys.D6 => PhysicalKey.Digit6,
            Keys.D7 => PhysicalKey.Digit7,
            Keys.D8 => PhysicalKey.Digit8,
            Keys.D9 => PhysicalKey.Digit9,

            // Writing System Keys - Letters
            Keys.A => PhysicalKey.A,
            Keys.B => PhysicalKey.B,
            Keys.C => PhysicalKey.C,
            Keys.D => PhysicalKey.D,
            Keys.E => PhysicalKey.E,
            Keys.F => PhysicalKey.F,
            Keys.G => PhysicalKey.G,
            Keys.H => PhysicalKey.H,
            Keys.I => PhysicalKey.I,
            Keys.J => PhysicalKey.J,
            Keys.K => PhysicalKey.K,
            Keys.L => PhysicalKey.L,
            Keys.M => PhysicalKey.M,
            Keys.N => PhysicalKey.N,
            Keys.O => PhysicalKey.O,
            Keys.P => PhysicalKey.P,
            Keys.Q => PhysicalKey.Q,
            Keys.R => PhysicalKey.R,
            Keys.S => PhysicalKey.S,
            Keys.T => PhysicalKey.T,
            Keys.U => PhysicalKey.U,
            Keys.V => PhysicalKey.V,
            Keys.W => PhysicalKey.W,
            Keys.X => PhysicalKey.X,
            Keys.Y => PhysicalKey.Y,
            Keys.Z => PhysicalKey.Z,

            // Writing System Keys - Punctuation
            Keys.OemSemicolon or Keys.Oem1 => PhysicalKey.Semicolon,
            Keys.Oemplus => PhysicalKey.Equal,
            Keys.Oemcomma => PhysicalKey.Comma,
            Keys.OemMinus => PhysicalKey.Minus,
            Keys.OemPeriod => PhysicalKey.Period,
            Keys.OemQuestion or Keys.Oem2 => PhysicalKey.Slash,
            Keys.Oemtilde or Keys.Oem3 => PhysicalKey.Backquote,
            Keys.OemOpenBrackets or Keys.Oem4 => PhysicalKey.BracketLeft,
            Keys.OemPipe or Keys.Oem5 => PhysicalKey.Backslash,
            Keys.OemCloseBrackets or Keys.Oem6 => PhysicalKey.BracketRight,
            Keys.OemQuotes or Keys.Oem7 => PhysicalKey.Quote,
            Keys.OemBackslash or Keys.Oem102 => PhysicalKey.IntlBackslash,

            // Functional Keys
            Keys.Menu => PhysicalKey.AltLeft,
            Keys.LMenu => PhysicalKey.AltLeft,
            Keys.RMenu => PhysicalKey.AltRight,
            Keys.Back => PhysicalKey.Backspace,
            Keys.Capital or Keys.CapsLock => PhysicalKey.CapsLock,
            Keys.Apps => PhysicalKey.ContextMenu,
            Keys.ControlKey => PhysicalKey.ControlLeft,
            Keys.LControlKey => PhysicalKey.ControlLeft,
            Keys.RControlKey => PhysicalKey.ControlRight,
            Keys.Enter or Keys.Return => PhysicalKey.Enter,
            Keys.LWin => PhysicalKey.MetaLeft,
            Keys.RWin => PhysicalKey.MetaRight,
            Keys.ShiftKey => PhysicalKey.ShiftLeft,
            Keys.LShiftKey => PhysicalKey.ShiftLeft,
            Keys.RShiftKey => PhysicalKey.ShiftRight,
            Keys.Space => PhysicalKey.Space,
            Keys.Tab => PhysicalKey.Tab,

            // Language/IME Keys
            Keys.IMEConvert => PhysicalKey.Convert,
            Keys.KanaMode => PhysicalKey.KanaMode,
            Keys.HanjaMode or Keys.KanjiMode => PhysicalKey.Lang2,
            Keys.JunjaMode => PhysicalKey.Lang3,
            Keys.FinalMode => PhysicalKey.Lang4,
            Keys.IMENonconvert => PhysicalKey.NonConvert,

            // Control Pad Section
            Keys.Delete => PhysicalKey.Delete,
            Keys.End => PhysicalKey.End,
            Keys.Help => PhysicalKey.Help,
            Keys.Home => PhysicalKey.Home,
            Keys.Insert => PhysicalKey.Insert,
            Keys.PageDown or Keys.Next => PhysicalKey.PageDown,
            Keys.PageUp or Keys.Prior => PhysicalKey.PageUp,

            // Arrow Pad Section
            Keys.Down => PhysicalKey.ArrowDown,
            Keys.Left => PhysicalKey.ArrowLeft,
            Keys.Right => PhysicalKey.ArrowRight,
            Keys.Up => PhysicalKey.ArrowUp,

            // Numeric Keypad Section
            Keys.NumLock => PhysicalKey.NumLock,
            Keys.NumPad0 => PhysicalKey.NumPad0,
            Keys.NumPad1 => PhysicalKey.NumPad1,
            Keys.NumPad2 => PhysicalKey.NumPad2,
            Keys.NumPad3 => PhysicalKey.NumPad3,
            Keys.NumPad4 => PhysicalKey.NumPad4,
            Keys.NumPad5 => PhysicalKey.NumPad5,
            Keys.NumPad6 => PhysicalKey.NumPad6,
            Keys.NumPad7 => PhysicalKey.NumPad7,
            Keys.NumPad8 => PhysicalKey.NumPad8,
            Keys.NumPad9 => PhysicalKey.NumPad9,
            Keys.Add => PhysicalKey.NumPadAdd,
            Keys.Clear => PhysicalKey.NumPadClear,
            Keys.Separator => PhysicalKey.NumPadComma,
            Keys.Decimal => PhysicalKey.NumPadDecimal,
            Keys.Divide => PhysicalKey.NumPadDivide,
            Keys.Multiply => PhysicalKey.NumPadMultiply,
            Keys.Subtract => PhysicalKey.NumPadSubtract,

            // Function Section
            Keys.Escape => PhysicalKey.Escape,
            Keys.F1 => PhysicalKey.F1,
            Keys.F2 => PhysicalKey.F2,
            Keys.F3 => PhysicalKey.F3,
            Keys.F4 => PhysicalKey.F4,
            Keys.F5 => PhysicalKey.F5,
            Keys.F6 => PhysicalKey.F6,
            Keys.F7 => PhysicalKey.F7,
            Keys.F8 => PhysicalKey.F8,
            Keys.F9 => PhysicalKey.F9,
            Keys.F10 => PhysicalKey.F10,
            Keys.F11 => PhysicalKey.F11,
            Keys.F12 => PhysicalKey.F12,
            Keys.F13 => PhysicalKey.F13,
            Keys.F14 => PhysicalKey.F14,
            Keys.F15 => PhysicalKey.F15,
            Keys.F16 => PhysicalKey.F16,
            Keys.F17 => PhysicalKey.F17,
            Keys.F18 => PhysicalKey.F18,
            Keys.F19 => PhysicalKey.F19,
            Keys.F20 => PhysicalKey.F20,
            Keys.F21 => PhysicalKey.F21,
            Keys.F22 => PhysicalKey.F22,
            Keys.F23 => PhysicalKey.F23,
            Keys.F24 => PhysicalKey.F24,
            Keys.PrintScreen or Keys.Snapshot => PhysicalKey.PrintScreen,
            Keys.Scroll => PhysicalKey.ScrollLock,
            Keys.Pause => PhysicalKey.Pause,

            // Media Keys
            Keys.BrowserBack => PhysicalKey.BrowserBack,
            Keys.BrowserFavorites => PhysicalKey.BrowserFavorites,
            Keys.BrowserForward => PhysicalKey.BrowserForward,
            Keys.BrowserHome => PhysicalKey.BrowserHome,
            Keys.BrowserRefresh => PhysicalKey.BrowserRefresh,
            Keys.BrowserSearch => PhysicalKey.BrowserSearch,
            Keys.BrowserStop => PhysicalKey.BrowserStop,
            Keys.LaunchApplication1 => PhysicalKey.LaunchApp1,
            Keys.LaunchApplication2 => PhysicalKey.LaunchApp2,
            Keys.LaunchMail => PhysicalKey.LaunchMail,
            Keys.MediaPlayPause => PhysicalKey.MediaPlayPause,
            Keys.SelectMedia => PhysicalKey.MediaSelect,
            Keys.MediaStop => PhysicalKey.MediaStop,
            Keys.MediaNextTrack => PhysicalKey.MediaTrackNext,
            Keys.MediaPreviousTrack => PhysicalKey.MediaTrackPrevious,
            Keys.Sleep => PhysicalKey.Sleep,
            Keys.VolumeMute => PhysicalKey.AudioVolumeMute,
            Keys.VolumeDown => PhysicalKey.AudioVolumeDown,
            Keys.VolumeUp => PhysicalKey.AudioVolumeUp,

            // Legacy Keys - mapped to available equivalents
            Keys.Select => PhysicalKey.Select,
            Keys.Print => PhysicalKey.PrintScreen, // Best approximation
            Keys.Execute => PhysicalKey.None, // No equivalent
            Keys.LineFeed => PhysicalKey.Enter, // Best approximation

            // Keys with no direct PhysicalKey equivalent
            Keys.Cancel => PhysicalKey.None,
            Keys.LButton => PhysicalKey.None, // Mouse button
            Keys.RButton => PhysicalKey.None, // Mouse button
            Keys.MButton => PhysicalKey.None, // Mouse button
            Keys.XButton1 => PhysicalKey.None, // Mouse button
            Keys.XButton2 => PhysicalKey.None, // Mouse button
            Keys.IMEAccept or Keys.IMEAceept => PhysicalKey.None, // IMEAceept is obsolete version of IMEAccept
            Keys.IMEModeChange => PhysicalKey.None,
            Keys.ProcessKey => PhysicalKey.None,
            Keys.Packet => PhysicalKey.None,
            Keys.Attn => PhysicalKey.None,
            Keys.Crsel => PhysicalKey.None,
            Keys.Exsel => PhysicalKey.None,
            Keys.EraseEof => PhysicalKey.None,
            Keys.Play => PhysicalKey.None,
            Keys.Zoom => PhysicalKey.None,
            Keys.NoName => PhysicalKey.None,
            Keys.Pa1 => PhysicalKey.None,
            Keys.OemClear => PhysicalKey.None,
            Keys.Oem8 => PhysicalKey.None,

            // Default case
            _ => PhysicalKey.None,
        };

    public static string? ToKeySymbol(this Keys key)
    {
        var baseKey = key & Keys.KeyCode;

        return baseKey switch
        {
            // Letters
            Keys.A => "A",
            Keys.B => "B",
            Keys.C => "C",
            Keys.D => "D",
            Keys.E => "E",
            Keys.F => "F",
            Keys.G => "G",
            Keys.H => "H",
            Keys.I => "I",
            Keys.J => "J",
            Keys.K => "K",
            Keys.L => "L",
            Keys.M => "M",
            Keys.N => "N",
            Keys.O => "O",
            Keys.P => "P",
            Keys.Q => "Q",
            Keys.R => "R",
            Keys.S => "S",
            Keys.T => "T",
            Keys.U => "U",
            Keys.V => "V",
            Keys.W => "W",
            Keys.X => "X",
            Keys.Y => "Y",
            Keys.Z => "Z",

            // Digits
            Keys.D0 => "0",
            Keys.D1 => "1",
            Keys.D2 => "2",
            Keys.D3 => "3",
            Keys.D4 => "4",
            Keys.D5 => "5",
            Keys.D6 => "6",
            Keys.D7 => "7",
            Keys.D8 => "8",
            Keys.D9 => "9",

            // NumPad digits
            Keys.NumPad0 => "0",
            Keys.NumPad1 => "1",
            Keys.NumPad2 => "2",
            Keys.NumPad3 => "3",
            Keys.NumPad4 => "4",
            Keys.NumPad5 => "5",
            Keys.NumPad6 => "6",
            Keys.NumPad7 => "7",
            Keys.NumPad8 => "8",
            Keys.NumPad9 => "9",

            // Basic symbols and punctuation
            Keys.Space => " ",
            Keys.Tab => "\t",
            Keys.Enter or Keys.Return => "\n",
            Keys.Back => "\b",

            // OEM/Symbol keys (US keyboard layout)
            Keys.OemSemicolon or Keys.Oem1 => ";",
            Keys.Oemplus => "=",
            Keys.Oemcomma => ",",
            Keys.OemMinus => "-",
            Keys.OemPeriod => ".",
            Keys.OemQuestion or Keys.Oem2 => "/",
            Keys.Oemtilde or Keys.Oem3 => "`",
            Keys.OemOpenBrackets or Keys.Oem4 => "[",
            Keys.OemPipe or Keys.Oem5 => "\\",
            Keys.OemCloseBrackets or Keys.Oem6 => "]",
            Keys.OemQuotes or Keys.Oem7 => "'",

            // NumPad operations
            Keys.Add => "+",
            Keys.Subtract => "-",
            Keys.Multiply => "*",
            Keys.Divide => "/",
            Keys.Decimal => ".",
            Keys.Separator => ",",

            // Function keys and special keys return null as they don't have printable symbols
            Keys.F1
            or Keys.F2
            or Keys.F3
            or Keys.F4
            or Keys.F5
            or Keys.F6
            or Keys.F7
            or Keys.F8
            or Keys.F9
            or Keys.F10
            or Keys.F11
            or Keys.F12
            or Keys.F13
            or Keys.F14
            or Keys.F15
            or Keys.F16
            or Keys.F17
            or Keys.F18
            or Keys.F19
            or Keys.F20
            or Keys.F21
            or Keys.F22
            or Keys.F23
            or Keys.F24 => null,

            // Modifier keys
            Keys.ShiftKey
            or Keys.LShiftKey
            or Keys.RShiftKey
            or Keys.ControlKey
            or Keys.LControlKey
            or Keys.RControlKey
            or Keys.Menu
            or Keys.LMenu
            or Keys.RMenu
            or Keys.LWin
            or Keys.RWin => null,

            // Navigation and control keys
            Keys.Left
            or Keys.Right
            or Keys.Up
            or Keys.Down
            or Keys.Home
            or Keys.End
            or Keys.PageUp
            or Keys.PageDown
            or Keys.Insert
            or Keys.Delete
            or Keys.Escape
            or Keys.CapsLock
            or Keys.NumLock
            or Keys.Scroll
            or Keys.Pause
            or Keys.PrintScreen => null,

            // Everything else
            _ => null,
        };
    }
}
