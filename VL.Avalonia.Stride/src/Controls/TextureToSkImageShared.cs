using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using SkiaSharp;
using Stride.Core;
using Stride.Engine;
using Stride.Graphics;
using VL.Core;
using StrideRenderContext = Stride.Rendering.RenderContext;

namespace VL.Avalonia.Stride.Controls
{
    public class TextureToSkImageShared : IDisposable
    {
        private const int EGL_D3D11_TEXTURE_ANGLE = 0x3484;
        private const int EGL_D3D11_DEVICE_ANGLE = 0x33A1;
        private const int EGL_DEVICE_EXT = 0x322C;

        private const int GL_TEXTURE_2D = 0x0DE1;
        private const int GL_RGBA8 = 0x8058;
        private const int GL_RGBA16F = 0x881A;
        private const int GL_TEXTURE_BINDING_2D = 0x8069;
        private const int GL_FRAMEBUFFER = 0x8D40;
        private const int GL_COLOR_ATTACHMENT0 = 0x8CE0;
        private const int GL_FRAMEBUFFER_BINDING = 0x8CA6;

        private static readonly Guid IID_ID3D11Texture2D = new(
            "6f15aaf2-d208-4e89-9ab4-489535d34f9c"
        );

        // Cached setup resources
        private nint _cachedSharedHandle;
        private int _cachedWidth;
        private int _cachedHeight;
        private PixelFormat _cachedFormat;

        private Texture? _sharedCopy;
        private nint _angleD3D11Texture;
        private GRContext? _ownGrContext;
        private nint _boundEglContext;
        private nint _angleDevice;
        private nint _eglDisplay;

        // EGL image (recreated each frame)
        private nint _eglImage;

        // Source GL texture (from EGL image)
        private int _srcGlTexture;

        // Destination: Skia-managed render target we blit into
        private int _fbo;
        private SKSurface? _skSurface;

        private SKColorType _colorType;
        private int _glFormat;

        private static readonly PropertyKey<Texture> NonSrgbTextureKey = new(
            nameof(NonSrgbTextureKey),
            typeof(TextureToSkImageShared)
        );
        private CommandList? _cachedCommandList;

        private bool _disposed;

        private readonly List<string> _debugLog = new();
        public IReadOnlyList<string> DebugLog => _debugLog;
        public string? LastError { get; private set; }

        public TextureToSkImageShared() { }

        public SKImage? Update(Texture? texture, GRContext? avaloniaGrContext)
        {
            _debugLog.Clear();
            LastError = null;

            if (texture == null)
            {
                Log("Texture is null");
                FullCleanup();
                return null;
            }

            Log(
                $"Input: {texture.Width}x{texture.Height} Fmt={texture.Format} Opts={texture.Options}"
            );

            var device = texture.GraphicsDevice;
            if (device.ColorSpace == ColorSpace.Linear && texture.Format.IsSRgb())
            {
                texture = GetOrCreateNonSrgbCopy(texture);
                if (texture == null)
                {
                    LogError("sRGB copy failed");
                    return null;
                }
            }

            nint sharedHandle = texture.SharedHandle;
            if (sharedHandle == IntPtr.Zero)
            {
                texture = GetOrCreateSharedCopy(texture);
                if (texture == null)
                {
                    LogError("Shared copy failed");
                    return null;
                }
                sharedHandle = texture.SharedHandle;
                if (sharedHandle == IntPtr.Zero)
                {
                    LogError("No handle");
                    return null;
                }
            }
            Log($"SharedHandle=0x{sharedHandle:X}");

            var width = texture.Width;
            var height = texture.Height;
            var format = texture.Format;
            var currentEglCtx = Egl.GetCurrentContext();

            bool needSetup =
                sharedHandle != _cachedSharedHandle
                || width != _cachedWidth
                || height != _cachedHeight
                || format != _cachedFormat
                || currentEglCtx != _boundEglContext;

            if (needSetup)
            {
                Log("Setup needed");
                CleanupGpuResources();
                _cachedSharedHandle = sharedHandle;
                _cachedWidth = width;
                _cachedHeight = height;
                _cachedFormat = format;
                _boundEglContext = currentEglCtx;

                var grContext = avaloniaGrContext ?? GetOrCreateOwnGrContext();
                if (grContext == null)
                {
                    LogError("No GRContext");
                    return null;
                }

                if (!Setup(grContext, sharedHandle, width, height, format))
                    return null;
            }
            else
            {
                Log("Cache hit");
            }

            var activeGrContext = avaloniaGrContext ?? _ownGrContext;
            if (activeGrContext == null)
            {
                LogError("No GRContext");
                return null;
            }

            var image = RefreshFrame(activeGrContext, width, height);

            if (image != null)
                Log($"Frame: {image.Width}x{image.Height}");
            else
                LogError("Frame null");

            return image;
        }

        public SKImage? Update(Texture? texture) => null;

        private bool Setup(
            GRContext grContext,
            nint sharedHandle,
            int width,
            int height,
            PixelFormat format
        )
        {
            _eglDisplay = Egl.GetCurrentDisplay();
            if (_eglDisplay == IntPtr.Zero)
            {
                LogError("No EGL display");
                return false;
            }

            var angleDevice = GetOrCacheAngleD3D11Device(_eglDisplay);
            if (angleDevice == IntPtr.Zero)
            {
                LogError("No ANGLE device");
                return false;
            }

            _angleD3D11Texture = D3D11.OpenSharedResource(
                angleDevice,
                sharedHandle,
                IID_ID3D11Texture2D
            );
            Log($"ANGLE texture=0x{_angleD3D11Texture:X}");
            if (_angleD3D11Texture == IntPtr.Zero)
            {
                LogError("OpenSharedResource failed");
                return false;
            }

            (_colorType, _glFormat) = MapFormat(format);
            Log($"Format: {format} → {_colorType} gl=0x{_glFormat:X}");

            // Create source GL texture
            Gl.GenTextures(1, out _srcGlTexture);
            Log($"Source GL texture={_srcGlTexture}");

            // Create FBO for blitting
            Gl.GenFramebuffers(1, out _fbo);
            Log($"FBO={_fbo}");

            // Create Skia surface as render target
            grContext.ResetContext();
            _skSurface = SKSurface.Create(
                grContext,
                budgeted: true,
                new SKImageInfo(
                    width,
                    height,
                    _colorType,
                    SKAlphaType.Premul,
                    SKColorSpace.CreateSrgb()
                )
            );

            if (_skSurface == null)
            {
                Log("Primary SKSurface failed, trying RGBA8888...");
                _skSurface = SKSurface.Create(
                    grContext,
                    budgeted: true,
                    new SKImageInfo(
                        width,
                        height,
                        SKColorType.Rgba8888,
                        SKAlphaType.Premul,
                        SKColorSpace.CreateSrgb()
                    )
                );
            }
            if (_skSurface == null)
            {
                Log("Trying Bgra8888...");
                _skSurface = SKSurface.Create(
                    grContext,
                    budgeted: true,
                    new SKImageInfo(
                        width,
                        height,
                        SKColorType.Bgra8888,
                        SKAlphaType.Premul,
                        SKColorSpace.CreateSrgb()
                    )
                );
            }

            if (_skSurface == null)
            {
                LogError("SKSurface creation failed");
                return false;
            }
            Log($"SKSurface created: {width}x{height}");

            return true;
        }

        /// <summary>
        /// Each frame:
        /// 1. Recreate EGL image (force ANGLE re-read)
        /// 2. Bind to source GL texture
        /// 3. Blit from source GL texture to Skia's surface via FBO + glBlitFramebuffer
        /// 4. Snapshot the Skia surface
        /// </summary>
        private SKImage? RefreshFrame(GRContext grContext, int width, int height)
        {
            if (_angleD3D11Texture == IntPtr.Zero || _skSurface == null)
                return null;

            // Destroy old EGL image
            if (_eglImage != IntPtr.Zero)
            {
                Egl.DestroyImageKHR(_eglDisplay, _eglImage);
                _eglImage = IntPtr.Zero;
            }

            // Create fresh EGL image
            _eglImage = Egl.CreateImageKHR(
                _eglDisplay,
                IntPtr.Zero,
                EGL_D3D11_TEXTURE_ANGLE,
                _angleD3D11Texture,
                null
            );
            if (_eglImage == IntPtr.Zero)
            {
                LogError($"eglCreateImageKHR err=0x{Egl.GetError():X}");
                return null;
            }

            // Bind EGL image to source texture
            Gl.GetIntegerv(GL_TEXTURE_BINDING_2D, out int prevTex);
            Gl.BindTexture(GL_TEXTURE_2D, _srcGlTexture);
            Gl.EGLImageTargetTexture2DOES(GL_TEXTURE_2D, _eglImage);
            Gl.BindTexture(GL_TEXTURE_2D, prevTex);

            // Now draw from the source texture onto the Skia surface using Skia's own canvas
            grContext.ResetContext();

            var canvas = _skSurface.Canvas;

            // Create a temporary SKImage from the source texture for this draw call
            var glInfo = new GRGlTextureInfo(
                target: (uint)GL_TEXTURE_2D,
                id: (uint)_srcGlTexture,
                format: (uint)_glFormat
            );

            using var backendTex = new GRBackendTexture(width, height, mipmapped: false, glInfo);
            if (!backendTex.IsValid)
            {
                LogError("Backend texture invalid");
                return null;
            }

            // Use FromTexture to wrap, draw onto surface, then snapshot
            // The key: drawing onto the SKSurface copies the pixels through Skia's pipeline,
            // which reads the current GL texture content (not cached by SKImage since we draw immediately)
            using var srcImage = SKImage.FromTexture(
                grContext,
                backendTex,
                GRSurfaceOrigin.TopLeft,
                _colorType,
                SKAlphaType.Premul,
                SKColorSpace.CreateSrgb()
            );

            if (srcImage == null)
            {
                // Try fallback color types
                using var fallback =
                    SKImage.FromTexture(
                        grContext,
                        backendTex,
                        GRSurfaceOrigin.TopLeft,
                        SKColorType.Rgba8888,
                        SKAlphaType.Premul,
                        SKColorSpace.CreateSrgb()
                    )
                    ?? SKImage.FromTexture(
                        grContext,
                        backendTex,
                        GRSurfaceOrigin.TopLeft,
                        SKColorType.Bgra8888,
                        SKAlphaType.Premul,
                        SKColorSpace.CreateSrgb()
                    );

                if (fallback == null)
                {
                    LogError("srcImage null");
                    return null;
                }

                canvas.Clear(SKColors.Transparent);
                canvas.DrawImage(fallback, 0, 0);
            }
            else
            {
                canvas.Clear(SKColors.Transparent);
                canvas.DrawImage(srcImage, 0, 0);
            }

            canvas.Flush();
            _skSurface.Flush();
            grContext.Flush();

            // Snapshot gives us a new SKImage with the current content
            return _skSurface.Snapshot();
        }

        private GRContext? GetOrCreateOwnGrContext()
        {
            if (_ownGrContext != null)
                return _ownGrContext;
            try
            {
                var gli = GRGlInterface.Create() ?? GRGlInterface.CreateGles(Egl.GetProcAddress);
                if (gli == null)
                {
                    LogError("GRGlInterface failed");
                    return null;
                }
                _ownGrContext = GRContext.CreateGl(gli);
                if (_ownGrContext == null)
                {
                    gli.Dispose();
                    LogError("GRContext null");
                    return null;
                }
                return _ownGrContext;
            }
            catch (Exception ex)
            {
                LogError($"GRContext: {ex.Message}");
                return null;
            }
        }

        private nint GetOrCacheAngleD3D11Device(nint eglDisplay)
        {
            if (_angleDevice != IntPtr.Zero)
                return _angleDevice;
            var eglDev = Egl.QueryDisplayAttribEXT(eglDisplay, EGL_DEVICE_EXT);
            if (eglDev == IntPtr.Zero)
                return IntPtr.Zero;
            _angleDevice = Egl.QueryDeviceAttribEXT(eglDev, EGL_D3D11_DEVICE_ANGLE);
            return _angleDevice;
        }

        private Texture? GetOrCreateSharedCopy(Texture source)
        {
            if (
                _sharedCopy == null
                || _sharedCopy.Width != source.Width
                || _sharedCopy.Height != source.Height
                || _sharedCopy.Format != source.Format
            )
            {
                _sharedCopy?.Dispose();
                var d = source.Description;
                d.Options = TextureOptions.Shared;
                d.Usage = GraphicsResourceUsage.Default;
                d.Flags = TextureFlags.ShaderResource | TextureFlags.RenderTarget;
                _sharedCopy = Texture.New(source.GraphicsDevice, d);
            }
            GetOrCacheCommandList()?.Copy(source, _sharedCopy);
            return _sharedCopy;
        }

        private static (SKColorType, int) MapFormat(PixelFormat f) =>
            f switch
            {
                PixelFormat.R8G8B8A8_UNorm or PixelFormat.R8G8B8A8_UNorm_SRgb => (
                    SKColorType.Rgba8888,
                    GL_RGBA8
                ),
                PixelFormat.B8G8R8A8_UNorm or PixelFormat.B8G8R8A8_UNorm_SRgb => (
                    SKColorType.Bgra8888,
                    GL_RGBA8
                ),
                PixelFormat.R16G16B16A16_Float => (SKColorType.RgbaF16, GL_RGBA16F),
                _ => (SKColorType.Rgba8888, GL_RGBA8),
            };

        private Texture? GetOrCreateNonSrgbCopy(Texture texture)
        {
            var n = texture.Tags.Get(NonSrgbTextureKey);
            if (n == null)
            {
                var d = texture.Description;
                d.Format = d.Format.ToNonSRgb();
                d.Options = TextureOptions.None;
                n = Texture.New(texture.GraphicsDevice, d).DisposeBy(texture);
                texture.Tags.Set(NonSrgbTextureKey, n);
            }
            GetOrCacheCommandList()?.Copy(texture, n);
            return n;
        }

        private CommandList? GetOrCacheCommandList()
        {
            if (_cachedCommandList == null)
            {
                var game = AppHost.Current.Services.GetService<Game>();
                if (game != null)
                    _cachedCommandList = StrideRenderContext
                        .GetShared(game.Services)
                        .GetThreadContext()
                        .CommandList;
            }
            return _cachedCommandList;
        }

        private void CleanupGpuResources()
        {
            _skSurface?.Dispose();
            _skSurface = null;

            if (_eglImage != IntPtr.Zero && _eglDisplay != IntPtr.Zero)
            {
                try
                {
                    Egl.DestroyImageKHR(_eglDisplay, _eglImage);
                }
                catch { }
                _eglImage = IntPtr.Zero;
            }

            if (_srcGlTexture != 0)
            {
                try
                {
                    Gl.DeleteTextures(1, ref _srcGlTexture);
                }
                catch { }
                _srcGlTexture = 0;
            }

            if (_fbo != 0)
            {
                try
                {
                    Gl.DeleteFramebuffers(1, ref _fbo);
                }
                catch { }
                _fbo = 0;
            }

            if (_angleD3D11Texture != IntPtr.Zero)
            {
                try
                {
                    Marshal.Release(_angleD3D11Texture);
                }
                catch { }
                _angleD3D11Texture = IntPtr.Zero;
            }

            _cachedSharedHandle = IntPtr.Zero;
        }

        private void FullCleanup()
        {
            CleanupGpuResources();
            _sharedCopy?.Dispose();
            _sharedCopy = null;
        }

        private void Log(string m)
        {
            _debugLog.Add(m);
            Debug.WriteLine($"[TexToSKI] {m}");
        }

        private void LogError(string m)
        {
            LastError = m;
            _debugLog.Add($"ERROR: {m}");
            Debug.WriteLine($"[TexToSKI] ERROR: {m}");
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            FullCleanup();
            _ownGrContext?.Dispose();
            _ownGrContext = null;
            _cachedCommandList = null;
            _angleDevice = IntPtr.Zero;
        }
    }

    internal static class D3D11
    {
        private const int VT_OpenSharedResource = 28;

        public static nint OpenSharedResource(nint dev, nint handle, Guid iid)
        {
            var vt = Marshal.ReadIntPtr(dev);
            var fn = Marshal.GetDelegateForFunctionPointer<Del>(
                Marshal.ReadIntPtr(vt, VT_OpenSharedResource * IntPtr.Size)
            );
            return fn(dev, handle, ref iid, out var res) < 0 ? IntPtr.Zero : res;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate int Del(nint t, nint h, ref Guid r, out nint p);
    }

    internal static class Egl
    {
        const string L = "libEGL";

        [DllImport(L)]
        public static extern IntPtr eglGetCurrentDisplay();

        [DllImport(L)]
        public static extern IntPtr eglGetCurrentContext();

        [DllImport(L)]
        public static extern int eglGetError();

        [DllImport(L)]
        public static extern IntPtr eglGetProcAddress([MarshalAs(UnmanagedType.LPStr)] string n);

        [DllImport(L)]
        public static extern IntPtr eglCreateImageKHR(
            IntPtr d,
            IntPtr c,
            int t,
            IntPtr b,
            [MarshalAs(UnmanagedType.LPArray)] int[]? a
        );

        [DllImport(L)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool eglDestroyImageKHR(IntPtr d, IntPtr i);

        [DllImport(L)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool eglQueryDisplayAttribEXT(IntPtr d, int a, out IntPtr v);

        [DllImport(L)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool eglQueryDeviceAttribEXT(IntPtr d, int a, out IntPtr v);

        [DllImport(L)]
        static extern IntPtr eglQueryString(IntPtr d, int n);

        public static IntPtr GetCurrentDisplay() => eglGetCurrentDisplay();

        public static IntPtr GetCurrentContext() => eglGetCurrentContext();

        public static int GetError() => eglGetError();

        public static IntPtr GetProcAddress(string n) => eglGetProcAddress(n);

        public static IntPtr CreateImageKHR(IntPtr d, IntPtr c, int t, IntPtr b, int[]? a) =>
            eglCreateImageKHR(d, c, t, b, a);

        public static bool DestroyImageKHR(IntPtr d, IntPtr i) => eglDestroyImageKHR(d, i);

        public static IntPtr QueryDisplayAttribEXT(IntPtr d, int a) =>
            eglQueryDisplayAttribEXT(d, a, out var v) ? v : IntPtr.Zero;

        public static IntPtr QueryDeviceAttribEXT(IntPtr d, int a) =>
            eglQueryDeviceAttribEXT(d, a, out var v) ? v : IntPtr.Zero;

        public static string? QueryString(IntPtr d, int n)
        {
            var p = eglQueryString(d, n);
            return p != IntPtr.Zero ? Marshal.PtrToStringAnsi(p) : null;
        }

        public const int EGL_EXTENSIONS = 0x3055;
        public const int EGL_VENDOR = 0x3053;
        public const int EGL_VERSION = 0x3054;
    }

    internal static class Gl
    {
        const string L = "libGLESv2";

        [DllImport(L, EntryPoint = "glGenTextures")]
        public static extern void GenTextures(int n, out int t);

        [DllImport(L, EntryPoint = "glDeleteTextures")]
        public static extern void DeleteTextures(int n, ref int t);

        [DllImport(L, EntryPoint = "glBindTexture")]
        public static extern void BindTexture(int tgt, int t);

        [DllImport(L, EntryPoint = "glGetError")]
        public static extern int GetError();

        [DllImport(L, EntryPoint = "glGetIntegerv")]
        public static extern void GetIntegerv(int p, out int v);

        [DllImport(L, EntryPoint = "glEGLImageTargetTexture2DOES")]
        public static extern void EGLImageTargetTexture2DOES(int t, IntPtr i);

        [DllImport(L, EntryPoint = "glFlush")]
        public static extern void Flush();

        [DllImport(L, EntryPoint = "glFinish")]
        public static extern void Finish();

        [DllImport(L, EntryPoint = "glGenFramebuffers")]
        public static extern void GenFramebuffers(int n, out int fb);

        [DllImport(L, EntryPoint = "glDeleteFramebuffers")]
        public static extern void DeleteFramebuffers(int n, ref int fb);

        [DllImport(L, EntryPoint = "glBindFramebuffer")]
        public static extern void BindFramebuffer(int target, int fb);

        [DllImport(L, EntryPoint = "glFramebufferTexture2D")]
        public static extern void FramebufferTexture2D(
            int target,
            int attachment,
            int texTarget,
            int texture,
            int level
        );

        [DllImport(L, EntryPoint = "glGetString")]
        static extern IntPtr _gs(int n);

        public static string? GetString(int n)
        {
            var p = _gs(n);
            return p != IntPtr.Zero ? Marshal.PtrToStringAnsi(p) : null;
        }

        public const int GL_VENDOR = 0x1F00;
        public const int GL_RENDERER = 0x1F01;
        public const int GL_VERSION = 0x1F02;
    }
}
