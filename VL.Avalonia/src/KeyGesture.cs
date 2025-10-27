using Avalonia.Input;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia
{
    /// <summary>
    /// Provides functionality for configuring key gesture input, including support for specifying
    /// keyboard keys and modifiers.
    /// </summary>
    [ProcessNode]
    public abstract class KeyGestureWrapperBase
    {
        protected KeyGesture _output = KeyGestureExtensions.Default();
        public KeyGesture Output => _output;
    }

    /// <inheritdoc cref="KeyGestureWrapperBase"/>
    [ProcessNode(Name = "KeyGesture (String)")]
    public partial class KeyGestureStringWrapper : KeyGestureWrapperBase
    {
        private Optional<string> _gesture;

        /// <param name="gesture">The key gesture string to parse. See <see cref="KeyGesture.Parse(string)"/> for format details.
        /// <remarks>Ctrl + X</remarks>
        /// </param>
        public void SetGesture(Optional<string> gesture)
        {
            if (_gesture != gesture)
            {
                if (gesture.HasValue)
                {
                    try
                    {
                        _output = KeyGesture.Parse(gesture.Value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _output = KeyGestureExtensions.Default();
                    }
                }
                else
                {
                    _output = KeyGestureExtensions.Default();
                }

                _gesture = gesture;
            }
        }
    }

    /// <inheritdoc cref="KeyGestureWrapperBase"/>
    [ProcessNode(Name = "KeyGesture (Advanced)")]
    public partial class KeyGestureWrapper : KeyGestureWrapperBase
    {
        private Key _key = Key.None;
        private KeyModifiers _modifiers = KeyModifiers.None;

        /// <param name="key">The keyboard key to use. See <see cref="Avalonia.Input.Key"/> for possible values</param>
        /// <param name="modifiers">The keyboard modifiers to use. See <see cref="Avalonia.Input.Key"/> for possible values</param>
        public void SetValue(Key key = Key.None, KeyModifiers modifiers = KeyModifiers.None)
        {
            if (_key != key || _modifiers != modifiers)
            {
                _output = new KeyGesture(key, modifiers);

                _key = key;
                _modifiers = modifiers;
            }
        }
    }
}
