using DataFormats = Avalonia.Input.DataFormats;
using IDataObject = Avalonia.Input.IDataObject;

namespace VL.Avalonia.Skia.DragAndDrop
{
    public class WinFormsAvaloniaDataObject : IDataObject, System.IDisposable
    {
        private readonly System.Windows.Forms.IDataObject _wrapped;

        public WinFormsAvaloniaDataObject(System.Windows.Forms.IDataObject wrapped)
        {
            _wrapped = wrapped;
        }

        // Avalonia calls this to list available formats
        public IEnumerable<string> GetDataFormats()
        {
            return _wrapped.GetFormats();
        }

        // Avalonia calls this to check if a specific format exists
        public bool Contains(string dataFormat)
        {
            // 1. Direct check
            if (_wrapped.GetDataPresent(dataFormat))
                return true;

            // 2. Compatibility mapping: Avalonia "FileNames" -> WinForms "FileDrop"
            if (
                dataFormat == DataFormats.FileNames
                && _wrapped.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop)
            )
            {
                return true;
            }

            return false;
        }

        // Avalonia calls this to retrieve the actual data
        public object? Get(string dataFormat)
        {
            // 1. Direct fetch
            if (_wrapped.GetDataPresent(dataFormat))
            {
                return _wrapped.GetData(dataFormat);
            }

            // 2. Compatibility mapping
            if (
                dataFormat == DataFormats.FileNames
                && _wrapped.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop)
            )
            {
                // WinForms returns string[], which Avalonia accepts for FileNames
                return _wrapped.GetData(System.Windows.Forms.DataFormats.FileDrop);
            }

            return null;
        }

        public void Dispose()
        {
            // No explicit disposal needed for the WinForms object usually
        }
    }
}
