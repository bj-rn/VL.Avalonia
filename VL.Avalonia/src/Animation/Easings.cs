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
public class BackEaseInOutWrapper : EasingWrapperBase<BackEaseInOut> { }

[ProcessNode(Name = "BackEaseOut")]
public class BackEaseOutWrapper : EasingWrapperBase<BackEaseOut> { }

[ProcessNode(Name = "BounceEaseIn")]
public class BounceEaseInWrapper : EasingWrapperBase<BounceEaseIn> { }

[ProcessNode(Name = "BounceEaseInOut")]
public class BounceEaseInOutWrapper : EasingWrapperBase<BounceEaseInOut> { }

[ProcessNode(Name = "BounceEaseOut")]
public class BounceEaseOutWrapper : EasingWrapperBase<BounceEaseOut> { }

[ProcessNode(Name = "CircularEaseIn")]
public class CircularEaseInWrapper : EasingWrapperBase<CircularEaseIn> { }

[ProcessNode(Name = "CircularEaseInOut")]
public class CircularEaseInOutWrapper : EasingWrapperBase<CircularEaseInOut> { }

[ProcessNode(Name = "CircularEaseOut")]
public class CircularEaseOutWrapper : EasingWrapperBase<CircularEaseOut> { }

[ProcessNode(Name = "CubicEaseIn")]
public class CubicEaseInWrapper : EasingWrapperBase<CubicEaseIn> { }

[ProcessNode(Name = "CubicEaseInOut")]
public class CubicEaseInOutWrapper : EasingWrapperBase<CubicEaseInOut> { }

[ProcessNode(Name = "CubicEaseOut")]
public class CubicEaseOutWrapper : EasingWrapperBase<CubicEaseOut> { }

[ProcessNode(Name = "ElasticEaseIn")]
public class ElasticEaseInWrapper : EasingWrapperBase<ElasticEaseIn> { }

[ProcessNode(Name = "ElasticEaseInOut")]
public class ElasticEaseInOutWrapper : EasingWrapperBase<ElasticEaseInOut> { }

[ProcessNode(Name = "ElasticEaseOut")]
public class ElasticEaseOutWrapper : EasingWrapperBase<ElasticEaseOut> { }

[ProcessNode(Name = "ExponentialEaseIn")]
public class ExponentialEaseInWrapper : EasingWrapperBase<ExponentialEaseIn> { }

[ProcessNode(Name = "ExponentialEaseInOut")]
public class ExponentialEaseInOutWrapper : EasingWrapperBase<ExponentialEaseInOut> { }

[ProcessNode(Name = "ExponentialEaseOut")]
public class ExponentialEaseOutWrapper : EasingWrapperBase<ExponentialEaseOut> { }

[ProcessNode(Name = "LinearEasing")]
public class LinearEasingWrapper : EasingWrapperBase<LinearEasing> { }

[ProcessNode(Name = "QuadraticEaseIn")]
public class QuadraticEaseInWrapper : EasingWrapperBase<QuadraticEaseIn> { }

[ProcessNode(Name = "QuadraticEaseInOut")]
public class QuadraticEaseInOutWrapper : EasingWrapperBase<QuadraticEaseInOut> { }

[ProcessNode(Name = "QuadraticEaseOut")]
public class QuadraticEaseOutWrapper : EasingWrapperBase<QuadraticEaseOut> { }

[ProcessNode(Name = "QuarticEaseIn")]
public class QuarticEaseInWrapper : EasingWrapperBase<QuarticEaseIn> { }

[ProcessNode(Name = "QuarticEaseInOut")]
public class QuarticEaseInOutWrapper : EasingWrapperBase<QuarticEaseInOut> { }

[ProcessNode(Name = "QuarticEaseOut")]
public class QuarticEaseOutWrapper : EasingWrapperBase<QuarticEaseOut> { }

[ProcessNode(Name = "QuinticEaseIn")]
public class QuinticEaseInWrapper : EasingWrapperBase<QuinticEaseIn> { }

[ProcessNode(Name = "QuinticEaseInOut")]
public class QuinticEaseInOutWrapper : EasingWrapperBase<QuinticEaseInOut> { }

[ProcessNode(Name = "QuinticEaseOut")]
public class QuinticEaseOutWrapper : EasingWrapperBase<QuinticEaseOut> { }

[ProcessNode(Name = "SineEaseIn")]
public class SineEaseInWrapper : EasingWrapperBase<SineEaseIn> { }

[ProcessNode(Name = "SineEaseInOut")]
public class SineEaseInOutWrapper : EasingWrapperBase<SineEaseInOut> { }

[ProcessNode(Name = "SineEaseOut")]
public class SineEaseOutWrapper : EasingWrapperBase<SineEaseOut> { }
