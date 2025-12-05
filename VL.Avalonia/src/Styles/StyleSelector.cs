using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Styles
{
    [ProcessNode]
    public class StyleSelector
    {
        protected ImmutableSelector _output = new();
        public IAvaloniaStyle Output => _output;

        protected Optional<IAvaloniaStyle> _input;

        public void SetInput(Optional<IAvaloniaStyle> input)
        {
            if (_input != input)
            {
                if (input.HasValue)
                {
                    _output = _output with { Input = input.Value };
                }
                else
                {
                    _output = _output with { Input = default };
                }

                _input = input;
            }
        }

        protected Optional<IAvaloniaStyle> _selectorStyle;

        public void SetSelectorStyle(Optional<IAvaloniaStyle> selectorStyle)
        {
            if (_selectorStyle != selectorStyle)
            {
                if (selectorStyle.HasValue)
                {
                    _output = _output with { SelectorStyle = selectorStyle.Value };
                }
                else
                {
                    _output = _output with { SelectorStyle = default };
                }

                _selectorStyle = selectorStyle;
            }
        }

        protected Optional<string> _selectorString;

        public void SetSelector(Optional<string> selectorString)
        {
            if (_selectorString != selectorString)
            {
                if (selectorString.HasValue)
                {
                    var selector = SelectorHelper.ParseSelector(selectorString.Value);

                    _output = _output with { Selector = selector };
                }
                else
                {
                    _output = _output with { Selector = default };
                }

                _selectorString = selectorString;
            }
        }
    }
}
