namespace VL.Avalonia.Skia.DragAndDrop
{
    public static class OleContext
    {
        private static bool _initialized;

        public static bool EnsureInitialized()
        {
            if (_initialized)
                return true;

            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
                return false;

            int hr = OleInterop.OleInitialize(IntPtr.Zero);
            _initialized = (hr == 0 || hr == 1); // S_OK or S_FALSE
            return _initialized;
        }
    }
}
