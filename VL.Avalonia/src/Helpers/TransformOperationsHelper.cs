using Avalonia.Media.Transformation;
using Stride.Core.Mathematics;
using VL.Core;

namespace VL.Avalonia.Helpers
{
    public static class TransformOperationsHelper
    {
        public static TransformOperations TransformSRT(Optional<Vector2> scale, Optional<float> rotation, Optional<Vector2> translation)
        {
            var builder = TransformOperations.CreateBuilder(3);

            if (scale.HasValue)
                builder.AppendScale(scale.Value.X, scale.Value.Y);
            if (rotation.HasValue)
                builder.AppendRotate((double)(rotation.Value * Math.PI * 2));
            if (translation.HasValue)
                builder.AppendTranslate(translation.Value.X, translation.Value.Y);

            return builder.Build();
        }
    }
}
