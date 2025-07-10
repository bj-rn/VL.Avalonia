using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for panel wrappers, (StackPanel, Grid etc.)
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class PanelWrapperBase<T> : ControlWrapperBase<T> where T : Panel, new()
{
    private Spread<Control> _children;
    /// <param name="children">
    /// Children collection (of type Controls) which holds its child controls.
    /// </param>
    [Fragment(Order = -10)]
    public virtual void SetChildren(Spread<Control> children)
    {
        if (_children != children)
        {
            _children = children;

            _output.Children.Clear();

            foreach (var child in _children)
            {
                if (child is Control control)
                {
                    // TODO: Handle VisualParent

                    _output.Children.Add(control);
                }
            }
        }
    }

    [ImplementProperty("Panel.BackgroundProperty", PinVisibility = Model.PinVisibility.Optional)]
    public Optional<IBrush> _background;
}
