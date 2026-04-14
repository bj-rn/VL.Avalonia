using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    // TODO:
    internal abstract class ContentControlNodeBase<TControl, TValue> : ControlWrapperBase<TControl>
        where TControl : ContentControl, new() { }
}
