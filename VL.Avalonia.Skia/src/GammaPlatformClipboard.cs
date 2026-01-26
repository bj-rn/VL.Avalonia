using Avalonia.Input;
using Avalonia.Threading;
using VL.Core;
using Input = Avalonia.Input;
using Platform = Avalonia.Input.Platform;

namespace VL.Avalonia.Skia
{
    /// <summary>
    /// Implementation of Avalonia's IClipboard that bridges to VL's platform services clipboard.
    /// Bridges to the UI thread (STA) to prevent ThreadStateException.
    /// </summary>
    public sealed class GammaPlatformClipboard : Platform.IClipboard
    {
        private VL.Core.IClipboard? Clipboard => PlatformServices.Default?.Clipboard;

        public async Task ClearAsync()
        {
            // Must run on UI thread (STA) for OLE clipboard
            await Dispatcher.UIThread.InvokeAsync(() => Clipboard?.SetText(string.Empty));
        }

        public async Task<object?> GetDataAsync(string format)
        {
            return await Dispatcher.UIThread.InvokeAsync<object?>(() =>
            {
                if (format == Input.DataFormats.Text)
                {
                    return Clipboard?.GetText();
                }
                return null;
            });
        }

        public Task<string[]> GetFormatsAsync()
        {
            return Task.FromResult(new[] { Input.DataFormats.Text });
        }

        public async Task<string?> GetTextAsync()
        {
            return await Dispatcher.UIThread.InvokeAsync(() =>
            {
                return Clipboard?.GetText();
            });
        }

        public async Task SetDataObjectAsync(Input.IDataObject data)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                // Try to get text from the data object
                var text = data.GetText();
                if (text != null)
                {
                    Clipboard?.SetText(text);
                }
            });
        }

        public async Task SetTextAsync(string? text)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                Clipboard?.SetText(text ?? "");
            });
        }
    }
}
