using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for <see href="https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/Primitives/HeaderedItemsControl.cs">HeaderedItemsControl</see>
/// </summary>
[ProcessNode]
public abstract partial class HeaderedItemsControlWrapperBase<TControl, TValue>
    : ItemsControlWrapperBase<TControl, TValue>
    where TControl : HeaderedItemsControl, new()
{
    [ImplementProperty("HeaderedContentControl.HeaderProperty", Order = PinOrder.Action)]
    private Optional<object?> _header;

    [ImplementProperty(
        "HeaderedContentControl.HeaderTemplateProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<IDataTemplate> _headerTemplate;
}
