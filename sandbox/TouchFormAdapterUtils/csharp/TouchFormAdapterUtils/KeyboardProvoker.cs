using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TouchFormAdapterUtils;

/// <summary>
/// Shows or hides the Windows touch keyboard programmatically.
/// Use with VL.Avalonia TextBox focus events to trigger the keyboard on touch devices.
/// </summary>
[ProcessNode]
public class TouchKeyboard : IDisposable
{
    [ComImport, Guid("4ce576fa-83dc-4F88-951c-9d0782b4e376")]
    private class UIHostNoLaunch { }

    [ComImport, Guid("37c994e7-432b-4834-a2f7-dce1f13b834b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface ITipInvocation
    {
        void Toggle(IntPtr hwnd);
    }

    [ComImport, Guid("d5120aa3-46ba-44c5-822d-ca8092c1fc72")]
    private class FrameworkInputPane { }

    [ComImport, Guid("5752238b-24f0-495a-82f1-2fd593056796")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IFrameworkInputPane
    {
        int Advise(IntPtr pWindow, IntPtr pHandler, out int pdwCookie);
        int AdviseWithHWND(IntPtr hwnd, IntPtr pHandler, out int pdwCookie);
        int Unadvise(int dwCookie);
        int Location(out RECT prcInputPaneScreenLocation);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [DllImport("user32.dll", SetLastError = false)]
    private static extern IntPtr GetDesktopWindow();

    private bool _lastShow;

    public void Update(
        out bool success,
        out string status,
        bool show = false,
        bool hide = false)
    {
        success = false;
        status = "Idle";

        // Rising edge detection
        bool doShow = show && !_lastShow;
        _lastShow = show;

        if (hide)
        {
            // Try to hide by toggling if currently visible
            try
            {
                if (IsKeyboardVisible())
                {
                    ToggleKeyboard();
                    success = true;
                    status = "Keyboard hidden";
                }
            }
            catch (Exception ex)
            {
                status = $"Hide failed: {ex.Message}";
            }
            return;
        }

        if (!doShow) return;

        try
        {
            // Only show if not already visible
            if (!IsKeyboardVisible())
            {
                ToggleKeyboard();
            }
            success = true;
            status = "Keyboard shown";
        }
        catch (COMException ex) when (ex.HResult == unchecked((int)0x80040154))
        {
            // UIHostNoLaunch not registered — TabTip not running, start it
            try
            {
                var psi = new ProcessStartInfo("TabTip.exe")
                {
                    UseShellExecute = true
                };
                Process.Start(psi);
                success = true;
                status = "Keyboard started via TabTip.exe";
            }
            catch (Exception ex2)
            {
                status = $"TabTip launch failed: {ex2.Message}";
            }
        }
        catch (Exception ex)
        {
            status = $"Show failed: {ex.Message}";
        }
    }

    private static void ToggleKeyboard()
    {
        var uiHost = new UIHostNoLaunch();
        var tipInvocation = (ITipInvocation)uiHost;
        tipInvocation.Toggle(GetDesktopWindow());
        Marshal.ReleaseComObject(uiHost);
    }

    private static bool IsKeyboardVisible()
    {
        try
        {
            var inputPane = (IFrameworkInputPane)new FrameworkInputPane();
            inputPane.Location(out var rect);
            bool visible = rect.Bottom - rect.Top > 0 && rect.Right - rect.Left > 0;
            Marshal.ReleaseComObject(inputPane);
            return visible;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose() { }
}