using Avalonia.Media;
using Avalonia.Media.Transformation;
using Stride.Core.Mathematics;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media
{
    [ProcessNode]
    public abstract class TransformWrapperBase<T>
        where T : ITransform
    {
        protected T _output;
        public T Output => _output;
    }

    [ProcessNode(Name = "TransformSRT")]
    public partial class TransformWrapper : TransformWrapperBase<ITransform>
    {
        protected Optional<Vector2> _scale;
        protected Optional<float> _rotation;
        protected Optional<Vector2> _translation;

        public TransformWrapper()
        {
            var builder = TransformOperations.CreateBuilder(1);
            builder.AppendIdentity();
            _output = builder.Build();
        }

        public void SetValue(
            Optional<Vector2> scale,
            Optional<float> rotation,
            Optional<Vector2> translation
        )
        {
            if (_scale != scale || _rotation != rotation || _translation != translation)
            {
                _output = TransformOperationsHelper.TransformSRT(scale, rotation, translation);

                _scale = scale;
                _rotation = rotation;
                _translation = translation;
            }
        }
    }
}
