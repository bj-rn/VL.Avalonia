using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Input.Raw;
using VL.Avalonia.Skia;
using VL.Lib.IO.Notifications;
using Keys = VL.Lib.IO.Keys;
using MouseButtons = VL.Lib.IO.MouseButtons;

namespace VL.Avalonia.Tests;

public class GammaTopLevelInputModifierTests
{
    private static readonly Vector2 Pos = new(10, 10);
    private static readonly Vector2 ClientArea = new(100, 100);

    private readonly GammaTopLevelImpl _topLevel = new(null!);
    private readonly List<RawInputEventArgs> _received = new();

    private void Setup()
    {
        _topLevel.SetInputRoot(new Window());
        _topLevel.Input = _received.Add;
    }

    private void Send(INotification notification) =>
        _topLevel.HandleNotification(notification, new Point(Pos.X, Pos.Y));

    private void MouseDown(MouseButtons b) =>
        Send(new MouseDownNotification(Pos, ClientArea, b, Keys.None, null!));

    private void MouseUp(MouseButtons b) =>
        Send(new MouseUpNotification(Pos, ClientArea, b, Keys.None, null!));

    private RawInputModifiers MoveAndGetModifiers()
    {
        Send(new MouseMoveNotification(Pos, ClientArea, Keys.None, null!));
        var move = Assert.IsType<RawPointerEventArgs>(_received[^1]);
        Assert.Equal(RawPointerEventType.Move, move.Type);
        return move.InputModifiers;
    }

    private void Touch(TouchNotificationKind kind, int id) =>
        Send(new TouchNotification(kind, Pos, ClientArea, id, true, Vector2.One, 0, Keys.None, null!));

    [AvaloniaFact]
    public void Mouse_up_without_prior_down_does_not_set_button()
    {
        Setup();

        MouseUp(MouseButtons.Left);

        Assert.Equal(RawInputModifiers.None, MoveAndGetModifiers());
    }

    [AvaloniaFact]
    public void Key_press_during_drag_keeps_button_held()
    {
        Setup();

        MouseDown(MouseButtons.Left);
        Send(new KeyDownNotification(Keys.ShiftKey, Keys.Shift, null!));

        Assert.Equal(
            RawInputModifiers.LeftMouseButton | RawInputModifiers.Shift,
            MoveAndGetModifiers()
        );
    }

    [AvaloniaFact]
    public void Button_released_after_key_press_is_cleared()
    {
        Setup();

        MouseDown(MouseButtons.Left);
        Send(new KeyDownNotification(Keys.ShiftKey, Keys.Shift, null!));
        MouseUp(MouseButtons.Left);

        Assert.Equal(RawInputModifiers.Shift, MoveAndGetModifiers());
    }

    [AvaloniaFact]
    public void Key_up_clears_only_keyboard_bits()
    {
        Setup();

        MouseDown(MouseButtons.Left);
        Send(new KeyDownNotification(Keys.ShiftKey, Keys.Shift, null!));
        Send(new KeyUpNotification(Keys.ShiftKey, Keys.None, null!));

        Assert.Equal(RawInputModifiers.LeftMouseButton, MoveAndGetModifiers());
    }

    [AvaloniaFact]
    public void Releasing_one_button_keeps_the_other()
    {
        Setup();

        MouseDown(MouseButtons.Left);
        MouseDown(MouseButtons.Right);
        MouseUp(MouseButtons.Left);

        Assert.Equal(RawInputModifiers.RightMouseButton, MoveAndGetModifiers());
    }

    [AvaloniaFact]
    public void Completed_touch_does_not_mute_mouse_input()
    {
        Setup();

        Touch(TouchNotificationKind.TouchDown, 0);
        Touch(TouchNotificationKind.TouchMove, 0);
        Touch(TouchNotificationKind.TouchUp, 0);
        var countAfterTouch = _received.Count;

        MoveAndGetModifiers();

        Assert.Equal(countAfterTouch + 1, _received.Count);
    }
}
