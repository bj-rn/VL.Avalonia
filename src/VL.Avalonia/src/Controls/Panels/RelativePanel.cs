using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls.Base;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "RelativePanel (Spectral)")]
public partial class RelativePanelWrapperSpectral
{
    [ImplementOutput]
    protected readonly RelativePanel _output = new RelativePanel();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("Control.NameProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;
}

[ProcessNode(Name = "RelativePanel")]
public partial class RelativePanelWrapper : RelativePanelWrapperSpectral
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}

/// <summary>
/// Identifies the RightOfProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "RightOf (RelativePanel)")]
public partial class RelativePanelRightOfProperty : AttachedPropertyBase
{
    private Optional<object> _rightOf;
    public void SetRightOf(Optional<object> rightOf)
    {
        if (_rightOf != rightOf)
        {
            _rightOf = rightOf;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_rightOf.HasValue)
        {
            RelativePanel.SetRightOf(_input.Value, _rightOf.Value);
        }
        else
        {
            _input.Value.ClearValue(RelativePanel.RightOfProperty);
        }
    }
}

/// <summary>
/// Identifies the LeftOfProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "LeftOf (RelativePanel)")]
public partial class RelativePanelLeftOfProperty : AttachedPropertyBase
{
    private Optional<object> _leftOf;
    public void SetLeftOf(Optional<object> leftOf)
    {
        if (_leftOf != leftOf)
        {
            _leftOf = leftOf;
            UpdateSetters();
        }
    }
    protected override void UpdateSetters()
    {
        if (_leftOf.HasValue)
        {
            RelativePanel.SetLeftOf(_input.Value, _leftOf.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.LeftOfProperty);
        }
    }
}

/// <summary>
/// Identifies the AboveProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "Above (RelativePanel)")]
public partial class RelativePanelAboveProperty : AttachedPropertyBase
{
    private Optional<object> _above;
    public void SetAbove(Optional<object> above)
    {
        if (_above != above)
        {
            _above = above;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_above.HasValue)
        {
            RelativePanel.SetAbove(_input.Value, _above.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AboveProperty);
        }
    }
}

/// <summary>
/// Identifies the BelowProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "Below (RelativePanel)")]
public partial class RelativePanelBelowProperty : AttachedPropertyBase
{
    private Optional<object> _below;
    public void SetBelow(Optional<object> below)
    {
        if (_below != below)
        {
            _below = below;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_below.HasValue)
        {
            RelativePanel.SetBelow(_input.Value, _below.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.BelowProperty);
        }
    }
}

/// <summary>
/// Identifies the AlignLeftWithProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignLeftWith (RelativePanel)")]
public partial class RelativePanelAlignLeftWithProperty : AttachedPropertyBase
{
    private Optional<object> _alignLeftWith;
    public void SetAlignLeftWith(Optional<object> alignLeftWith)
    {
        if (_alignLeftWith != alignLeftWith)
        {
            _alignLeftWith = alignLeftWith;
            UpdateSetters();
        }
    }
    protected override void UpdateSetters()
    {
        if (_alignLeftWith.HasValue)
        {
            RelativePanel.SetAlignLeftWith(_input.Value, _alignLeftWith.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignLeftWithProperty);
        }
    }
}

/// <summary>
/// Identifies the AlignRightWithProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignRightWith (RelativePanel)")]
public partial class RelativePanelAlignRightWithProperty : AttachedPropertyBase
{
    private Optional<object> _alignRightWith;
    public void SetAlignRightWith(Optional<object> alignRightWith)
    {
        if (_alignRightWith != alignRightWith)
        {
            _alignRightWith = alignRightWith;
            UpdateSetters();
        }
    }
    protected override void UpdateSetters()
    {
        if (_alignRightWith.HasValue)
        {
            RelativePanel.SetAlignRightWith(_input.Value, _alignRightWith.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignRightWithProperty);
        }
    }
}

/// <summary>
/// Identifies the AlignTopWithProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignTopWith (RelativePanel)")]
public partial class RelativePanelAlignTopWith : AttachedPropertyBase
{
    private Optional<object> _alignTopWith;
    public void SetAlignTopWith(Optional<object> alignTopWith)
    {
        if (_alignTopWith != alignTopWith)
        {
            _alignTopWith = alignTopWith;
            UpdateSetters();
        }
    }
    protected override void UpdateSetters()
    {
        if (_alignTopWith.HasValue)
        {
            RelativePanel.SetAlignTopWith(_input.Value, _alignTopWith.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignTopWithProperty);
        }
    }
}

/// <summary>
/// Identifies the AlignBottomWithProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignBottomWith (RelativePanel)")]
public partial class RelativePanelAlignBottomWith : AttachedPropertyBase
{
    private Optional<object> _alignBottomWith;
    public void SetAlignBottomWith(Optional<object> alignBottomWith)
    {
        if (_alignBottomWith != alignBottomWith)
        {
            _alignBottomWith = alignBottomWith;
            UpdateSetters();
        }
    }
    protected override void UpdateSetters()
    {
        if (_alignBottomWith.HasValue)
        {
            RelativePanel.SetAlignBottomWith(_input.Value, _alignBottomWith.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignBottomWithProperty);
        }
    }
}

/// <summary>
/// Identifies the AlignHorizontalCenterWithProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignHorizontalCenterWith (RelativePanel)")]
public partial class RelativePanelAlignHorizontalCenterWith : AttachedPropertyBase
{
    private Optional<object> _alignHorizontalCenterWith;
    public void SetAlignHorizontalCenterWith(Optional<object> alignHorizontalCenterWith)
    {
        if (_alignHorizontalCenterWith != alignHorizontalCenterWith)
        {
            _alignHorizontalCenterWith = alignHorizontalCenterWith;
            UpdateSetters();
        }
    }
    protected override void UpdateSetters()
    {
        if (_alignHorizontalCenterWith.HasValue)
        {
            RelativePanel.SetAlignHorizontalCenterWith(_input.Value, _alignHorizontalCenterWith.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignHorizontalCenterWithProperty);
        }
    }
}

/// <summary>
/// Identifies the AlignVerticalCenterWithProperty
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignVerticalCenterWith (RelativePanel)")]
public partial class RelativePanelAlignVerticalCenterWith : AttachedPropertyBase
{
    private Optional<object> _alignVerticalCenterWith;
    public void SetAlignVerticalCenterWith(Optional<object> alignVerticalCenterWith)
    {
        if (_alignVerticalCenterWith != alignVerticalCenterWith)
        {
            _alignVerticalCenterWith = alignVerticalCenterWith;
            UpdateSetters();
        }
    }
    protected override void UpdateSetters()
    {
        if (_alignVerticalCenterWith.HasValue)
        {
            RelativePanel.SetAlignVerticalCenterWith(_input.Value, _alignVerticalCenterWith.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignVerticalCenterWithProperty);
        }
    }
}

/// <summary>
/// Boolean. Align the top edge of the child control with the top edge of the panel.
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignTopWithPanel (RelativePanel)")]
public partial class RelativePanelAlignTopWithPanel : AttachedPropertyBase
{
    private Optional<bool> _alignTopWithPanel;
    public void SetAlignTopWithPanel(Optional<bool> alignTopWithPanel)
    {
        if (_alignTopWithPanel != alignTopWithPanel)
        {
            _alignTopWithPanel = alignTopWithPanel;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_alignTopWithPanel.HasValue)
        {
            RelativePanel.SetAlignTopWithPanel(_input.Value, _alignTopWithPanel.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignTopWithPanelProperty);
        }
    }
}

/// <summary>
/// Boolean. Align the bottom edge of the child control with the bottom edge of the panel.
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignBottomWithPanel (RelativePanel)")]
public partial class RelativePanelAlignBottomWithPanel : AttachedPropertyBase
{
    private Optional<bool> _alignBottomWithPanel;
    public void SetAlignBottomWithPanel(Optional<bool> alignBottomWithPanel)
    {
        if (_alignBottomWithPanel != alignBottomWithPanel)
        {
            _alignBottomWithPanel = alignBottomWithPanel;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_alignBottomWithPanel.HasValue)
        {
            RelativePanel.SetAlignBottomWithPanel(_input.Value, _alignBottomWithPanel.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignBottomWithPanelProperty);
        }
    }
}

/// <summary>
/// Boolean. Align the left edge of the child control with the left edge of the panel.
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignLeftWithPanel (RelativePanel)")]
public partial class RelativePanelAlignLeftWithPanel : AttachedPropertyBase
{
    private Optional<bool> _alignLeftWithPanel;
    public void SetAlignLeftWithPanel(Optional<bool> alignLeftWithPanel)
    {
        if (_alignLeftWithPanel != alignLeftWithPanel)
        {
            _alignLeftWithPanel = alignLeftWithPanel;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_alignLeftWithPanel.HasValue)
        {
            RelativePanel.SetAlignLeftWithPanel(_input.Value, _alignLeftWithPanel.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignLeftWithPanelProperty);
        }
    }
}

/// <summary>
/// Boolean. Align the right edge of the child control with the right edge of the panel.
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignRightWithPanel (RelativePanel)")]
public partial class RelativePanelAlignRightWithPanel : AttachedPropertyBase
{
    private Optional<bool> _alignRightWithPanel;
    public void SetAlignRightWithPanel(Optional<bool> alignRightWithPanel)
    {
        if (_alignRightWithPanel != alignRightWithPanel)
        {
            _alignRightWithPanel = alignRightWithPanel;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_alignRightWithPanel.HasValue)
        {
            RelativePanel.SetAlignRightWithPanel(_input.Value, _alignRightWithPanel.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignRightWithPanelProperty);
        }
    }
}

/// <summary>
/// Boolean. Align the horizontal center of the child control with the horizontal center of the panel.
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignHorizontalCenterWithPanel (RelativePanel)")]
public partial class RelativePanelAlignHorizontalCenterWithPanel : AttachedPropertyBase
{
    private Optional<bool> _alignHorizontalCenterWithPanel;
    public void SetAlignHorizontalCenterWithPanel(Optional<bool> alignHorizontalCenterWithPanel)
    {
        if (_alignHorizontalCenterWithPanel != alignHorizontalCenterWithPanel)
        {
            _alignHorizontalCenterWithPanel = alignHorizontalCenterWithPanel;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_alignHorizontalCenterWithPanel.HasValue)
        {
            RelativePanel.SetAlignHorizontalCenterWithPanel(_input.Value, _alignHorizontalCenterWithPanel.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignHorizontalCenterWithPanelProperty);
        }
    }
}

/// <summary>
/// Boolean. Align the vertical center of the child control with the vertical center of the panel.
/// RelativePanel AttachedProperty.
/// </summary>
[ProcessNode(Name = "AlignVerticalCenterWithPanel (RelativePanel)")]
public partial class RelativePanelAlignVerticalCenterWithPanel : AttachedPropertyBase
{
    private Optional<bool> _alignVerticalCenterWithPanel;
    public void SetAlignVerticalCenterWithPanel(Optional<bool> alignVerticalCenterWithPanel)
    {
        if (_alignVerticalCenterWithPanel != alignVerticalCenterWithPanel)
        {
            _alignVerticalCenterWithPanel = alignVerticalCenterWithPanel;
            UpdateSetters();
        }
    }

    protected override void UpdateSetters()
    {
        if (_alignVerticalCenterWithPanel.HasValue)
        {
            RelativePanel.SetAlignVerticalCenterWithPanel(_input.Value, _alignVerticalCenterWithPanel.Value);
        }
        else
        {
            _input.Value?.ClearValue(RelativePanel.AlignVerticalCenterWithPanelProperty);
        }
    }
}