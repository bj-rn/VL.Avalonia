using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Interactivity;
using VL.Avalonia.Interactivity;

namespace VL.Avalonia.Tests;

public class RoutedEventListenerTests
{
    private sealed class TestControl : Border
    {
        public static readonly RoutedEvent<RoutedEventArgs> EventA =
            RoutedEvent.Register<TestControl, RoutedEventArgs>(nameof(EventA), RoutingStrategies.Bubble);

        public static readonly RoutedEvent<RoutedEventArgs> EventB =
            RoutedEvent.Register<TestControl, RoutedEventArgs>(nameof(EventB), RoutingStrategies.Bubble);
    }

    private readonly TestControl _control = new();
    private readonly List<RoutedEvent> _received = new();

    private Func<Interactive, RoutedEventArgs, bool> Recorder() =>
        (_, args) =>
        {
            _received.Add(args.RoutedEvent!);
            return false;
        };

    private void Raise(RoutedEvent routedEvent) =>
        _control.RaiseEvent(new RoutedEventArgs(routedEvent, _control));

    private RoutedEventListener<RoutedEventArgs> Listener(RoutedEvent? routedEvent)
    {
        var listener = new RoutedEventListener<RoutedEventArgs>();
        listener.SetInput(_control);
        listener.SetHandler(Recorder());
        listener.SetRoutedEvent(routedEvent);
        return listener;
    }

    [AvaloniaFact]
    public void Handler_receives_the_event()
    {
        using var listener = Listener(TestControl.EventA);

        Raise(TestControl.EventA);

        Assert.Equal(new[] { TestControl.EventA }, _received);
    }

    [AvaloniaFact]
    public void Switching_the_event_stops_listening_to_the_previous_one()
    {
        using var listener = Listener(TestControl.EventA);

        listener.SetRoutedEvent(TestControl.EventB);
        Raise(TestControl.EventA);
        Raise(TestControl.EventB);

        Assert.Equal(new[] { TestControl.EventB }, _received);
    }

    [AvaloniaFact]
    public void Clearing_the_event_stops_listening()
    {
        using var listener = Listener(TestControl.EventA);

        listener.SetRoutedEvent(null);
        Raise(TestControl.EventA);

        Assert.Empty(_received);
    }

    [AvaloniaFact]
    public void Switching_back_listens_once()
    {
        using var listener = Listener(TestControl.EventA);

        listener.SetRoutedEvent(TestControl.EventB);
        listener.SetRoutedEvent(TestControl.EventA);
        Raise(TestControl.EventA);

        Assert.Single(_received);
    }

    [AvaloniaFact]
    public void Switching_the_event_name_stops_listening_to_the_previous_one()
    {
        using var listener = new RoutedEventListenerName<RoutedEventArgs>();
        listener.SetInput(_control);
        listener.SetHandler(Recorder());
        listener.SetRoutedEvent("TestControl.EventA");

        listener.SetRoutedEvent("TestControl.EventB");
        Raise(TestControl.EventA);
        Raise(TestControl.EventB);

        Assert.Equal(new[] { TestControl.EventB }, _received);
    }

    [AvaloniaFact]
    public void Unknown_event_name_stops_listening()
    {
        using var listener = new RoutedEventListenerName<RoutedEventArgs>();
        listener.SetInput(_control);
        listener.SetHandler(Recorder());
        listener.SetRoutedEvent("TestControl.EventA");

        listener.SetRoutedEvent("TestControl.NoSuchEvent");
        Raise(TestControl.EventA);

        Assert.Empty(_received);
    }

    [AvaloniaFact]
    public void Dispose_stops_listening()
    {
        var listener = Listener(TestControl.EventA);

        listener.Dispose();
        Raise(TestControl.EventA);

        Assert.Empty(_received);
    }
}
