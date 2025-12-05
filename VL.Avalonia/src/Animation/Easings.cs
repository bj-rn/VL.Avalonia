using Avalonia.Animation.Easings;
using VL.Core.Import;

namespace VL.Avalonia.Animation.Easings;

[ProcessNode]
public abstract class EasingWrapperBase<T>
    where T : Easing, new()
{
    protected readonly T _output = new();
    public T Output => _output;
}

[ProcessNode(Name = "BackEaseIn")]
public class BackEaseInWrapper : EasingWrapperBase<BackEaseIn> { }

[ProcessNode(Name = "BackEaseInOut")]
public class BackEaseInOutWrapper : EasingWrapperBase<BackEaseIn> { }

[ProcessNode(Name = "BackEaseOut")]
public class BackEaseOutWrapper : EasingWrapperBase<BackEaseIn> { }

[ProcessNode(Name = "BounceEaseIn")]
public class BounceEaseInWrapper : EasingWrapperBase<BounceEaseIn> { }

[ProcessNode(Name = "BounceEaseInOut")]
public class BounceEaseInOutWrapper : EasingWrapperBase<BounceEaseIn> { }

[ProcessNode(Name = "BounceEaseOut")]
public class BounceEaseOutWrapper : EasingWrapperBase<BounceEaseIn> { }

[ProcessNode(Name = "CircularEaseIn")]
public class CircularEaseInWrapper : EasingWrapperBase<CircularEaseIn> { }

[ProcessNode(Name = "CircularEaseInOut")]
public class CircularEaseInOutWrapper : EasingWrapperBase<CircularEaseIn> { }

[ProcessNode(Name = "CircularEaseOut")]
public class CircularEaseOutWrapper : EasingWrapperBase<CircularEaseIn> { }

[ProcessNode(Name = "CubicEaseIn")]
public class CubicEaseInWrapper : EasingWrapperBase<CubicEaseIn> { }

[ProcessNode(Name = "CubicEaseInOut")]
public class CubicEaseInOutWrapper : EasingWrapperBase<CubicEaseIn> { }

[ProcessNode(Name = "CubicEaseOut")]
public class CubicEaseOutWrapper : EasingWrapperBase<CubicEaseIn> { }

[ProcessNode(Name = "ElasticEaseIn")]
public class ElasticEaseInWrapper : EasingWrapperBase<ElasticEaseIn> { }

[ProcessNode(Name = "ElasticEaseInOut")]
public class ElasticEaseInOutWrapper : EasingWrapperBase<ElasticEaseIn> { }

[ProcessNode(Name = "ElasticEaseOut")]
public class ElasticEaseOutWrapper : EasingWrapperBase<ElasticEaseIn> { }

[ProcessNode(Name = "ExponentialEaseIn")]
public class ExponentialEaseInWrapper : EasingWrapperBase<ExponentialEaseIn> { }

[ProcessNode(Name = "ExponentialEaseInOut")]
public class ExponentialEaseInOutWrapper : EasingWrapperBase<ExponentialEaseIn> { }

[ProcessNode(Name = "ExponentialEaseOut")]
public class ExponentialEaseOutWrapper : EasingWrapperBase<ExponentialEaseIn> { }

[ProcessNode(Name = "LinearEasing")]
public class LinearEasingWrapper : EasingWrapperBase<LinearEasing> { }

[ProcessNode(Name = "QuadraticEaseIn")]
public class QuadraticEaseInWrapper : EasingWrapperBase<QuadraticEaseIn> { }

[ProcessNode(Name = "QuadraticEaseInOut")]
public class QuadraticEaseInOutWrapper : EasingWrapperBase<QuadraticEaseIn> { }

[ProcessNode(Name = "QuadraticEaseOut")]
public class QuadraticEaseOutWrapper : EasingWrapperBase<QuadraticEaseIn> { }

[ProcessNode(Name = "QuarticEaseIn")]
public class QuarticEaseInWrapper : EasingWrapperBase<QuarticEaseIn> { }

[ProcessNode(Name = "QuarticEaseInOut")]
public class QuarticEaseInOutWrapper : EasingWrapperBase<QuarticEaseIn> { }

[ProcessNode(Name = "QuarticEaseOut")]
public class QuarticEaseOutWrapper : EasingWrapperBase<QuarticEaseIn> { }

[ProcessNode(Name = "QuinticEaseIn")]
public class QuinticEaseInWrapper : EasingWrapperBase<QuinticEaseIn> { }

[ProcessNode(Name = "QuinticEaseInOut")]
public class QuinticEaseInOutWrapper : EasingWrapperBase<QuinticEaseIn> { }

[ProcessNode(Name = "QuinticEaseOut")]
public class QuinticEaseOutWrapper : EasingWrapperBase<QuinticEaseIn> { }

[ProcessNode(Name = "SineEaseIn")]
public class SineEaseInWrapper : EasingWrapperBase<SineEaseIn> { }

[ProcessNode(Name = "SineEaseInOut")]
public class SineEaseInOutWrapper : EasingWrapperBase<SineEaseIn> { }

[ProcessNode(Name = "SineEaseOut")]
public class SineEaseOutWrapper : EasingWrapperBase<SineEaseIn> { }
