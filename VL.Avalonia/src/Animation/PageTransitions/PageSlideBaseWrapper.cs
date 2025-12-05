using Avalonia.Animation;
using Avalonia.Animation.Easings;
using VL.Core;
using VL.Core.Import;
using static Avalonia.Animation.PageSlide;

namespace VL.Avalonia.Animation.PageTransitions;

[ProcessNode]
public abstract partial class PageSlideBaseWrapper<T>
    where T : PageSlide, new()
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

    protected Optional<SlideAxis> _orientation;

    public void SetOrientation(Optional<SlideAxis> orientation)
    {
        if (_orientation != orientation)
        {
            if (orientation.HasValue)
            {
                _output.Orientation = orientation.Value;
            }
            else
            {
                _output.Orientation = default;
            }

            _orientation = orientation;
        }
    }

    protected Optional<Easing> _slideInEasing;

    public void SetSlideInEasing(Optional<Easing> slideInEasing)
    {
        if (_slideInEasing != slideInEasing)
        {
            if (slideInEasing.HasValue)
            {
                _output.SlideInEasing = slideInEasing.Value;
            }
            else
            {
                _output.SlideInEasing = new LinearEasing();
            }

            _slideInEasing = slideInEasing;
        }
    }

    protected Optional<Easing> _slideOutEasing;

    public void SetSlideOutEasing(Optional<Easing> slideOutEasing)
    {
        if (_slideOutEasing != slideOutEasing)
        {
            if (slideOutEasing.HasValue)
            {
                _output.SlideOutEasing = slideOutEasing.Value;
            }
            else
            {
                _output.SlideOutEasing = new LinearEasing();
            }

            _slideOutEasing = slideOutEasing;
        }
    }
}
