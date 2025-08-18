using Avalonia.Rendering;

namespace VL.Avalonia.Skia
{
    sealed class GammaRenderTimer : IRenderTimer
    {
        public static readonly GammaRenderTimer Instance = new GammaRenderTimer();

        public bool RunsInBackground => false;

        public event Action<TimeSpan> Tick;

        public void TriggerTick(TimeSpan time)
        {
            Tick?.Invoke(time);
        }
    }
}