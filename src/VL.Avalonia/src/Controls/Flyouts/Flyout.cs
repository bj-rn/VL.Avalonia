using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls.Flyouts;

/// <summary>
/// Flyouts are dismissible containers that can be attached to some classes of 'host' control; although flyouts themselves are not controls. They show when their host control receives the focus, and are hidden again in a number of different ways.
/// </summary>
[ProcessNode(Name = "Flyout")]
public partial class FlyoutWrapper : FlyoutWrapperBase<Flyout>
{
    [ImplementProperty("Flyout.ContentProperty", Order = -10)]
    protected Optional<object> _content;
}
