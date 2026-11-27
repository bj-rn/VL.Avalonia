using Avalonia.Input;
using VL.Avalonia.Skia;
using Keys = VL.Lib.IO.Keys;

namespace VL.Avalonia.Tests;

public class KeyUtilsTests
{
    [Theory]
    // WinForms reports modifier presses with the generic codes plus the modifier flag.
    [InlineData(Keys.ShiftKey | Keys.Shift, Key.LeftShift)]
    [InlineData(Keys.ControlKey | Keys.Control, Key.LeftCtrl)]
    [InlineData(Keys.Menu | Keys.Alt, Key.LeftAlt)]
    [InlineData(Keys.LShiftKey, Key.LeftShift)]
    [InlineData(Keys.RShiftKey, Key.RightShift)]
    [InlineData(Keys.RControlKey, Key.RightCtrl)]
    [InlineData(Keys.RMenu, Key.RightAlt)]
    [InlineData(Keys.A | Keys.Shift, Key.A)]
    public void ToKey_maps_modifier_keys(Keys keys, Key expected)
    {
        Assert.Equal(expected, keys.ToKey());
    }
}
