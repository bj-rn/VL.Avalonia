// For examples, see:
// https://thegraybook.vvvv.org/reference/extending/writing-nodes.html#examples


using System;
using System.Runtime.InteropServices;


// [assembly: ImportAsIs]

/// <summary>
/// Disables Windows press-and-hold and other touch gestures on a given window handle.
/// This fixes the "need to tap twice" problem on touchscreens.
/// </summary>
namespace TouchFormAdapterUtils;
[ProcessNode]
public class DisableTouchGestures : IDisposable
{
    // --- Win32 P/Invoke declarations ---

    [DllImport("user32.dll")]
    private static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);

    [DllImport("user32.dll")]
    private static extern IntPtr RemoveProp(IntPtr hWnd, string lpString);

    [DllImport("user32.dll")]
    private static extern bool SetGestureConfig(
        IntPtr hwnd, int dwReserved, int cIDs,
        ref GESTURECONFIG pGestureConfig, int cbSize);

    [StructLayout(LayoutKind.Sequential)]
    private struct GESTURECONFIG
    {
        public int dwID;
        public int dwWant;
        public int dwBlock;
    }

    private const int GC_ALLGESTURES = 0x00000001;

    // Tablet pen service flags
    private const uint TABLET_DISABLE_PRESSANDHOLD     = 0x00000001;
    private const uint TABLET_DISABLE_PENTAPFEEDBACK   = 0x00000008;
    private const uint TABLET_DISABLE_PENBARRELFEEDBACK = 0x00000010;
    private const uint TABLET_DISABLE_TOUCHUIFORCEOFF  = 0x00000200;
    private const uint TABLET_DISABLE_FLICKS           = 0x00010000;
    private const uint TABLET_DISABLE_SMOOTHSCROLLING  = 0x00080000;
    private const uint TABLET_DISABLE_FLICKFALLBACKKEYS = 0x00100000;

    private const string PROPERTY_NAME = "MicrosoftTabletPenServiceProperty";

    private IntPtr _lastHandle;
    private bool _applied;

    public void Update(
        out bool success,
        out string status,
        IntPtr windowHandle = default,
        bool enabled = true)
    {
        // If handle changed or enable toggled, reapply
        if (windowHandle != _lastHandle || (enabled && !_applied))
        {
            _applied = false;
            _lastHandle = windowHandle;
        }

        if (windowHandle == IntPtr.Zero || !enabled)
        {
            success = false;
            status = windowHandle == IntPtr.Zero
                ? "No window handle provided"
                : "Disabled";
            return;
        }

        if (_applied)
        {
            success = true;
            status = "Touch gestures already disabled";
            return;
        }

        try
        {
            // Method 1: Set MicrosoftTabletPenServiceProperty
            // This disables press-and-hold, flicks, and pen feedback
            uint flags = TABLET_DISABLE_PRESSANDHOLD
                       | TABLET_DISABLE_PENTAPFEEDBACK
                       | TABLET_DISABLE_PENBARRELFEEDBACK
                       | TABLET_DISABLE_TOUCHUIFORCEOFF
                       | TABLET_DISABLE_FLICKS
                       | TABLET_DISABLE_SMOOTHSCROLLING
                       | TABLET_DISABLE_FLICKFALLBACKKEYS;

            SetProp(windowHandle, PROPERTY_NAME, (IntPtr)flags);

            // Method 2: SetGestureConfig — block all gestures
            // This covers Windows 7+ touch gesture recognition
            var config = new GESTURECONFIG
            {
                dwID = 0,
                dwWant = 0,
                dwBlock = GC_ALLGESTURES
            };

            SetGestureConfig(
                windowHandle, 0, 1,
                ref config, Marshal.SizeOf<GESTURECONFIG>());

            _applied = true;
            success = true;
            status = "Touch gestures disabled successfully";
        }
        catch (Exception ex)
        {
            success = false;
            status = ex.Message;
        }
    }

    public void Dispose()
    {
        if (_lastHandle != IntPtr.Zero && _applied)
        {
            RemoveProp(_lastHandle, PROPERTY_NAME);
        }
    }
}

// namespace Main;

// public static class Utils
// {
//     public static float DemoNode(float a, float b)
//     {
//         return a + b;
//     }
// }