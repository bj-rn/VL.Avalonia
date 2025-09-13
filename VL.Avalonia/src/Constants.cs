namespace VL.Avalonia;

/// <summary>
/// Internal constant to control order of pins
/// </summary>
public struct PinOrder
{
    public const int None = 0;

    public const int Exclusive = -20;

    public const int Main = -10;

    public const int Secondary = -9;

    public const int Style = -7;

    public const int Action = -5;
}
