using Avalonia.Animation;
using Avalonia.Animation.Easings;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Animation.PageTransitions;

[ProcessNode(Name = "CrossFade")]
public partial class CrossFadeWrapper<T>
    where T : CrossFade, new()
{
    protected readonly T _output = new();
    public T Output => _output;

    protected Optional<TimeSpan> _duration;

    public void SetDuration(Optional<TimeSpan> duration)
    {
        if (_duration != duration)
        {
            if (duration.HasValue)
            {
                _output.Duration = duration.Value;
            }
            else
            {
                _output.Duration = default;
            }

            _duration = duration;
        }
    }

    protected Optional<Easing> _fadeInEasing;

    public void SetFadeInEasing(Optional<Easing> fadeInEasing)
    {
        if (_fadeInEasing != fadeInEasing)
        {
            if (fadeInEasing.HasValue)
            {
                _output.FadeInEasing = fadeInEasing.Value;
            }
            else
            {
                _output.FadeInEasing = new LinearEasing();
            }

            _fadeInEasing = fadeInEasing;
        }
    }

    protected Optional<Easing> _fadeOutEasing;

    public void SetFadeOutEasing(Optional<Easing> fadeOutEasing)
    {
        if (_fadeOutEasing != fadeOutEasing)
        {
            if (fadeOutEasing.HasValue)
            {
                _output.FadeOutEasing = fadeOutEasing.Value;
            }
            else
            {
                _output.FadeOutEasing = new LinearEasing();
            }

            _fadeOutEasing = fadeOutEasing;
        }
    }
}
