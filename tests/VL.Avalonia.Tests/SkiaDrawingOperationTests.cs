using System.Reflection;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
using VL.Avalonia.Skia;
using VL.Lib.IO.Notifications;
using VL.Lib.Mathematics;
using VL.Skia;
using RectangleF = Stride.Core.Mathematics.RectangleF;

namespace VL.Avalonia.Tests;

public class SkiaDrawingOperationTests : IDisposable
{
    private static readonly Rect Bounds = new(0, 0, 32, 32);

    private readonly SKBitmap _target = new(64, 64);
    private readonly SKCanvas _canvas;
    private readonly int _initialSaveCount;

    public SkiaDrawingOperationTests()
    {
        _canvas = new SKCanvas(_target);
        _initialSaveCount = _canvas.SaveCount;
    }

    public void Dispose()
    {
        _canvas.Dispose();
        _target.Dispose();
    }

    [Fact]
    public void Disposed_image_leaves_canvas_balanced()
    {
        using var source = new SKBitmap(4, 4);
        var image = SKImage.FromBitmap(source);
        image.Dispose();
        var op = new SkiaImageDrawingOperation(image, Bounds, SizeMode.FitIn, RectangleAnchor.Center);

        op.Render(CreateContext(_canvas));

        Assert.Equal(_initialSaveCount, _canvas.SaveCount);
    }

    [Fact]
    public void Image_leaves_canvas_balanced()
    {
        using var source = new SKBitmap(4, 4);
        using var image = SKImage.FromBitmap(source);
        var op = new SkiaImageDrawingOperation(image, Bounds, SizeMode.FitIn, RectangleAnchor.Center);

        op.Render(CreateContext(_canvas));

        Assert.Equal(_initialSaveCount, _canvas.SaveCount);
    }

    [Fact]
    public void Picture_leaves_canvas_balanced()
    {
        using var recorder = new SKPictureRecorder();
        recorder.BeginRecording(new SKRect(0, 0, 4, 4)).DrawColor(SKColors.Red);
        using var picture = recorder.EndRecording();
        var op = new SkiaPictureDrawingOperation(picture, Bounds, SizeMode.FitIn, RectangleAnchor.Center);

        op.Render(CreateContext(_canvas));

        Assert.Equal(_initialSaveCount, _canvas.SaveCount);
    }

    [Fact]
    public void Layer_leaves_canvas_balanced()
    {
        var op = new SkiaLayerDrawingOperation(
            new FakeLayer(), null, Bounds, SizeMode.FitIn, RectangleAnchor.Center, 1f);

        op.Render(CreateContext(_canvas));

        Assert.Equal(_initialSaveCount, _canvas.SaveCount);
        Assert.True(_canvas.TotalMatrix.IsIdentity);
    }

    [Fact]
    public void Throwing_layer_still_restores_canvas()
    {
        var op = new SkiaLayerDrawingOperation(
            new FakeLayer(throws: true), null, new Rect(5, 5, 20, 20), SizeMode.FitIn, RectangleAnchor.Center, 1f);

        Assert.Throws<LayerFailure>(() => op.Render(CreateContext(_canvas)));

        Assert.Equal(_initialSaveCount, _canvas.SaveCount);
        Assert.True(_canvas.TotalMatrix.IsIdentity);
    }

    private static ImmediateDrawingContext CreateContext(SKCanvas canvas)
    {
        var impl = DispatchProxy.Create<IDrawingContextImpl, DrawingContextProxy>();
        ((DrawingContextProxy)(object)impl).Feature = new FakeLease(canvas);
        return (ImmediateDrawingContext)Activator.CreateInstance(
            typeof(ImmediateDrawingContext),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new object[] { impl, false },
            null
        )!;
    }

    // Only GetFeature matters to the drawing operations; every other member returns a default.
    public class DrawingContextProxy : DispatchProxy
    {
        public object? Feature;

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod!.Name == nameof(IDrawingContextImpl.GetFeature)
                && args![0] is Type type
                && type.IsInstanceOfType(Feature))
                return Feature;

            var returnType = targetMethod.ReturnType;
            return returnType.IsValueType && returnType != typeof(void)
                ? Activator.CreateInstance(returnType)
                : null;
        }
    }

    private sealed class FakeLease(SKCanvas canvas) : ISkiaSharpApiLeaseFeature, ISkiaSharpApiLease
    {
        public ISkiaSharpApiLease Lease() => this;
        public SKCanvas SkCanvas => canvas;
        public GRContext GrContext => null!;
        public SKSurface SkSurface => null!;
        public double CurrentOpacity => 1;
        public ISkiaSharpPlatformGraphicsApiLease? TryLeasePlatformGraphicsApi() => null;
        public void Dispose() { }
    }

    private sealed class LayerFailure : Exception;

    private sealed class FakeLayer(bool throws = false) : ILayer
    {
        public RectangleF? Bounds => null;

        public void Render(CallerInfo caller)
        {
            if (throws)
                throw new LayerFailure();
        }

        public bool Notify(INotification notification, CallerInfo caller) => false;
    }
}
