using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>HeaderedContentControl</c> extends <see cref="ContentControl"/> to add a header area above the main content. This allows controls like GroupBox, Expander, and TabItem to show a label, title, or any other UI element as a header, with its own template and content model.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/headeredcontentcontrol">HeaderedContentControl</see>
/// </summary>
[ProcessNode]
public abstract partial class HeaderedControlWrapperBase<T> : ContentControlWrapperBase<T> where T : HeaderedContentControl, new()
{
    /// <param name="header">
    /// The header content (can be a string, UI element, or any object).
    /// </param>
    [ImplementProperty("HeaderedContentControl.HeaderProperty")]
    protected Optional<object> _header;

    /// <param name="headerTemplate">
    /// The data template used to display the header content.
    /// </param>
    [ImplementProperty("HeaderedContentControl.HeaderTemplateProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IDataTemplate> _headerTemplate;
}
