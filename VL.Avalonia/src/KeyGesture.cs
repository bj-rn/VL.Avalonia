using Avalonia.Input;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia
{
    [ProcessNode(Name = "KeyGesture")]
    public partial class KeyGestureWrapper
    {
        protected KeyGesture _output = KeyGestureExtensions.Default();
        public KeyGesture Output => _output;

        private Optional<string> _gesture;
        public void SetGesture(Optional<string> gesture)
        {
            if (_gesture != gesture)
            {
                if (gesture.HasValue)
                {
                    _output = KeyGesture.Parse(gesture.Value);
                }
                else
                {
                    _output = KeyGestureExtensions.Default();
                }

                _gesture = gesture;
            }
        }
    }
}
