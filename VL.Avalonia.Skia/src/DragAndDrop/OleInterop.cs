using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace VL.Avalonia.Skia.DragAndDrop
{
    internal static class OleInterop
    {
        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, PreserveSig = true)]
        public static extern int RegisterDragDrop(IntPtr hwnd, IDropTarget target);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, PreserveSig = true)]
        public static extern int RevokeDragDrop(IntPtr hwnd);

        [DllImport("ole32.dll")]
        public static extern int OleInitialize(IntPtr pvReserved);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int DragQueryFile(
            IntPtr hDrop,
            uint iFile,
            StringBuilder? lpszFile,
            int cch
        );

        [DllImport("ole32.dll")]
        public static extern void ReleaseStgMedium(ref STGMEDIUM medium);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClipboardFormatName(
            int format,
            StringBuilder lpszFormatName,
            int cchMaxName
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [ComImport]
        [Guid("00000122-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDropTarget
        {
            void DragEnter(
                [In, MarshalAs(UnmanagedType.Interface)]
                    System.Runtime.InteropServices.ComTypes.IDataObject pDataObj,
                int grfKeyState,
                POINT pt,
                ref int pdwEffect
            );
            void DragOver(int grfKeyState, POINT pt, ref int pdwEffect);
            void DragLeave();
            void Drop(
                [In, MarshalAs(UnmanagedType.Interface)]
                    System.Runtime.InteropServices.ComTypes.IDataObject pDataObj,
                int grfKeyState,
                POINT pt,
                ref int pdwEffect
            );
        }
    }
}
