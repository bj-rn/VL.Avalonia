using System.Reactive;
using System.Windows.Input;
using Avalonia;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    public abstract class CommandBinding<T> : IDisposable
    {
        private readonly AvaloniaObject _control;
        private readonly AvaloniaProperty<ICommand?> _property;
        private IChannel<T>? _channel;
        private ChannelCommand? _command;

        public CommandBinding(AvaloniaObject control, AvaloniaProperty<ICommand?> property)
        {
            _control = control;
            _property = property;
        }

        public void Bind(IChannel<T> channel)
        {
            if (ReferenceEquals(_channel, channel))
                return;

            _channel = channel;

            if (_channel is not null)
            {
                _command = new ChannelCommand(channel);
                _control.SetValue(_property, _command);
            }
            else
            {
                _control.ClearValue(_property);
                _command = null;
            }
        }

        public void Dispose()
        {
            _control.ClearValue(_property);
            _command = null;
            _channel = null;
        }

        public class ChannelCommand : ICommand
        {
            private readonly IChannel<T> _channel;

            public ChannelCommand(IChannel<T> channel)
            {
                _channel = channel;
            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter)
            {
                return _channel.Enabled;
            }

            public void Execute(object? parameter)
            {
                if (_channel.Enabled is false)
                    return;

                if (_channel is null)
                    return;

                if (parameter is T paramT)
                {
                    _channel.Value = paramT;
                }
                else
                {
                    _channel.Value = default;
                }
            }

            public void RaiseCanExecuteChanged() =>
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class UnitCommandBinding : CommandBinding<Unit>
    {
        public UnitCommandBinding(AvaloniaObject control, AvaloniaProperty<ICommand?> property)
            : base(control, property) { }
    }
}
