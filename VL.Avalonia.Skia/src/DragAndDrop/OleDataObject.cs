using System.Runtime.InteropServices;
using System.Text;
using AvaloniaDataFormats = Avalonia.Input.DataFormats;
using IAvaloniaDataObject = Avalonia.Input.IDataObject;
using IComDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;

namespace VL.Avalonia.Skia.DragAndDrop
{
    public class OleDataObject : IAvaloniaDataObject
    {
        private readonly IComDataObject _comObject;

        public OleDataObject(IComDataObject comObject)
        {
            _comObject = comObject;
        }

        public bool Contains(string dataFormat)
        {
            return GetDataFormats().Contains(dataFormat);
        }

        public IEnumerable<string> GetDataFormats()
        {
            var enumerator = _comObject.EnumFormatEtc(
                System.Runtime.InteropServices.ComTypes.DATADIR.DATADIR_GET
            );
            if (enumerator == null)
                yield break;

            enumerator.Reset();
            var formats = new System.Runtime.InteropServices.ComTypes.FORMATETC[1];
            var fetched = new int[1];

            while (enumerator.Next(1, formats, fetched) == 0 && fetched[0] > 0)
            {
                yield return GetFormatName(formats[0].cfFormat);
            }
        }

        private string GetFormatName(int formatId)
        {
            // Standard OLE Formats
            // 1 = CF_TEXT
            // 13 = CF_UNICODETEXT
            // 15 = CF_HDROP (File Drop)

            if (formatId == 1 || formatId == 13)
                return AvaloniaDataFormats.Text;

            if (formatId == 15)
                return AvaloniaDataFormats.FileNames;

            // Retrieve name for custom registered formats
            StringBuilder sb = new StringBuilder(256);
            if (OleInterop.GetClipboardFormatName(formatId, sb, sb.Capacity) > 0)
            {
                return sb.ToString();
            }

            return formatId.ToString();
        }

        public object? Get(string dataFormat)
        {
            if (dataFormat == AvaloniaDataFormats.FileNames)
            {
                return GetFileNames();
            }
            if (dataFormat == AvaloniaDataFormats.Text)
            {
                return GetText();
            }
            return null;
        }

        private IEnumerable<string>? GetFileNames()
        {
            // CF_HDROP = 15
            if (!TryGetData(15, out var medium))
                return null;

            try
            {
                var files = new List<string>();
                int count = OleInterop.DragQueryFile(medium.unionmember, 0xFFFFFFFF, null, 0);

                var sb = new StringBuilder(1024);
                for (uint i = 0; i < count; i++)
                {
                    int len = OleInterop.DragQueryFile(medium.unionmember, i, sb, sb.Capacity);
                    if (len > 0)
                        files.Add(sb.ToString());
                    sb.Clear();
                }
                return files;
            }
            finally
            {
                OleInterop.ReleaseStgMedium(ref medium);
            }
        }

        private string? GetText()
        {
            // CF_UNICODETEXT = 13
            if (!TryGetData(13, out var medium))
                return null;

            try
            {
                return Marshal.PtrToStringUni(OleInterop.GlobalLock(medium.unionmember));
            }
            finally
            {
                OleInterop.GlobalUnlock(medium.unionmember);
                OleInterop.ReleaseStgMedium(ref medium);
            }
        }

        private bool TryGetData(
            short cfFormat,
            out System.Runtime.InteropServices.ComTypes.STGMEDIUM medium
        )
        {
            medium = new System.Runtime.InteropServices.ComTypes.STGMEDIUM();
            var format = new System.Runtime.InteropServices.ComTypes.FORMATETC
            {
                cfFormat = cfFormat,
                ptd = IntPtr.Zero,
                dwAspect = System.Runtime.InteropServices.ComTypes.DVASPECT.DVASPECT_CONTENT,
                lindex = -1,
                tymed = System.Runtime.InteropServices.ComTypes.TYMED.TYMED_HGLOBAL,
            };

            if (_comObject.QueryGetData(ref format) == 0)
            {
                try
                {
                    _comObject.GetData(ref format, out medium);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
