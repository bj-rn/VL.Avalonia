using Avalonia.Animation;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "TransitioningContentControl")]
public partial class TransitioningContentControlWrapper
    : ContentControlWrapperBase<TransitioningContentControl>
{
    [ImplementProperty("TransitioningContentControl.PageTransitionProperty")]
    protected Optional<IPageTransition> _pageTransition;

    [ImplementProperty("TransitioningContentControl.IsTransitionReversedProperty")]
    protected Optional<bool> _isTransitionReversedProperty;
}
