namespace VL.Avalonia;

/// <summary>
/// Internal constant to control order of pins
/// </summary>
public struct PinOrder
{
    public const int None = 0;

    public const int Exclusive = -100;

    public const int Main = -50;

    public const int Secondary = -40;

    public const int Action = -35;

    public const int Styled = -30;

    public const int Animatable = -20;

    public const int Layoutable = -15;

    public const int Control = -10;

    public const int DataTemplate = -7;

    public const int Visual = -5;

    public const int InputElement = -4;
}
