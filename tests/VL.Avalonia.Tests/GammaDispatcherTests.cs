using VL.Avalonia.Skia;
using VL.Lib.Animation;

namespace VL.Avalonia.Tests;

public class GammaDispatcherTests
{
    private sealed class FakeClock : IClock
    {
        public Time Time { get; set; }
    }

    private readonly FakeClock _clock = new();
    private readonly GammaDispatcherImpl _dispatcher;

    public GammaDispatcherTests()
    {
        _dispatcher = new GammaDispatcherImpl(
            Thread.CurrentThread,
            _clock,
            new SynchronizationContext()
        );
    }

    [Theory]
    [InlineData(0.0, 0L)]
    [InlineData(0.016, 16L)]
    [InlineData(1.2345, 1234L)]
    [InlineData(3600.5, 3600500L)]
    public void Now_is_the_clock_time_in_milliseconds(double seconds, long expected)
    {
        _clock.Time = seconds;

        Assert.Equal(expected, _dispatcher.Now);
    }

    [Fact]
    public void Now_advances_within_a_second()
    {
        _clock.Time = 2.0;
        var before = _dispatcher.Now;

        _clock.Time = 2.1;

        Assert.Equal(100L, _dispatcher.Now - before);
    }
}
