using VL.Core.Import;

namespace VL.Avalonia.Interactivity
{
    [ProcessNode]
    public abstract class StandardEventListenerBase<TControl, TArgs> : IDisposable
        where TControl : class
        where TArgs : EventArgs
    {
        protected TControl? Input;
        protected Action<TControl, TArgs>? Handler;

        [Fragment(Order = PinOrder.Main)]
        public void SetInput(TControl? input)
        {
            if (input != Input)
            {
                if (Input != null)
                    Unsubscribe(Input);

                Input = input;

                if (Input != null)
                    Subscribe(Input);
            }
        }

        [Fragment(Order = PinOrder.Action)]
        public void SetHandler(Action<TControl, TArgs>? handler)
        {
            Handler = handler;
        }

        protected void InvokeHandler(TControl sender, TArgs args)
        {
            Handler?.Invoke(sender, args);
        }

        protected abstract void Subscribe(TControl source);
        protected abstract void Unsubscribe(TControl source);

        public virtual void Dispose()
        {
            if (Input != null)
            {
                Unsubscribe(Input);
                Input = null;
            }
        }
    }
}
