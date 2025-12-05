using System.Reactive.Subjects;
using VL.Core.Import;
using VL.Lib.IO.Notifications;
using VL.Skia;
using RectangleF = Stride.Core.Mathematics.RectangleF;

namespace VL.Avalonia.Skia
{
    [ProcessNode(HasStateOutput = true, FragmentSelection = FragmentSelection.Explicit)]
    public class CallerInfoHook : LinkedLayerBase, IDisposable, ILayer
    {
        private CallerInfo _caller = CallerInfo.Default;
        public RectangleF? Bounds => Input?.Bounds;

        private readonly Subject<INotification> _notificationsSoure = new();

        [Fragment]
        public IObservable<INotification> NotificationsSource => _notificationsSoure;

        [Fragment]
        public CallerInfo CallerInfo => _caller;

        [Fragment]
        public CallerInfoHook() { }

        [Fragment]
        public void SetInput(ILayer? input)
        {
            Input = input;
        }

        public bool Notify(INotification notification, CallerInfo caller)
        {
            if (!caller.Equals(_caller))
            {
                _caller = caller;
            }

            _notificationsSoure.OnNext(notification);

            return Input?.Notify(notification, caller) ?? false;
        }

        public void Render(CallerInfo caller)
        {
            if (!caller.Equals(_caller))
            {
                _caller = caller;
            }

            Input?.Render(caller);
        }

        public void Dispose()
        {
            _notificationsSoure.Dispose();
        }
    }
}
