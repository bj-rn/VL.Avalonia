using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

[ProcessNode]
public abstract class AttachedPropertyBase
{
    protected internal Optional<Control> _input;

    public void SetInput(Optional<Control> input)
    {
        if (_input != input)
        {
            _input = input;

            UpdateSetters();
        }
    }

    public Control? Output => _input.Value;

    protected abstract void UpdateSetters();
}
