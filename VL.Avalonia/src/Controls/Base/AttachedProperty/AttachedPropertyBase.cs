using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

[ProcessNode]
public abstract class AttachedPropertyBase
{
    internal Optional<Control> _input;

    public void SetInput(Optional<Control> input)
    {
        // An upstream node without input outputs null, which VL turns into an Optional holding null.
        if (input.HasValue && input.Value is null)
            input = default;

        if (_input != input)
        {
            if (_input.HasValue)
                ClearSetters(_input.Value);

            _input = input;

            UpdateSetters();
        }
    }

    public Control? Output => _input.Value;

    protected abstract void UpdateSetters();

    /// <summary>
    /// Removes what this node attached to a control it no longer receives as input.
    /// </summary>
    protected abstract void ClearSetters(Control control);
}
