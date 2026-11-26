using Avalonia.Labs.Panels;
using VL.Avalonia.Styles;
using VL.Core.Import;

namespace VL.Avalonia.Labs.Panels.Styles
{
    // RowSpacing and ColumnSpacing are covered by SetRowSpacing / SetColumnSpacing in VL.Avalonia.Styles,
    // which resolve properties by name and therefore also apply to FlexPanel.

    #region FlexPanel

    [ProcessNode(Name = "SetDirection")]
    public class SetDirection : StyleSetter<FlexDirection>
    {
        public SetDirection()
            : base("Direction") { }
    }

    [ProcessNode(Name = "SetJustifyContent")]
    public class SetJustifyContent : StyleSetter<JustifyContent>
    {
        public SetJustifyContent()
            : base("JustifyContent") { }
    }

    [ProcessNode(Name = "SetAlignItems")]
    public class SetAlignItems : StyleSetter<AlignItems>
    {
        public SetAlignItems()
            : base("AlignItems") { }
    }

    [ProcessNode(Name = "SetAlignContent")]
    public class SetAlignContent : StyleSetter<AlignContent>
    {
        public SetAlignContent()
            : base("AlignContent") { }
    }

    [ProcessNode(Name = "SetWrap")]
    public class SetWrap : StyleSetter<FlexWrap>
    {
        public SetWrap()
            : base("Wrap") { }
    }

    #endregion
}
