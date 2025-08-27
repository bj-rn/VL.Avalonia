using Avalonia.Input;

namespace VL.Avalonia.Extensions
{
    public static partial class KeyGestureExtensions
    {
        /// <summary>Creates Empty KeyGesture</summary>
        public static KeyGesture Default() => new KeyGesture(Key.None, KeyModifiers.None);
    }
}
