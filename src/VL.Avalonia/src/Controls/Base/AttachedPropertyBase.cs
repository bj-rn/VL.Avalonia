using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls.Base;

[ProcessNode]
public abstract class AttachedPropertyBase
{
    internal Optional<Control> _input;
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
