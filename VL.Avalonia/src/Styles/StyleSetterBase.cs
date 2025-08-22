using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Stride.Core.Extensions;
using Stride.Core.Mathematics;
using VL.Avalonia.Animation.Transitions;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

using AvaloniaPoint = Avalonia.Point;

namespace VL.Avalonia.Styles
{
    /// <summary>
    /// Base class for chaining styles via ImmutableStyle abstraction
    /// Does not implements styling logic, if you need this class use one of
    /// the subclasses
    /// </summary>
    /// <typeparam name="T">TProperty</typeparam>
    [ProcessNode]
    public abstract class StyleSetterBase<T>
    {
        protected string _styleName;

        public StyleSetterBase(string styleName)
        {
            _styleName = styleName;

            _output = new ImmutableStyle<T>() with { StyleName = styleName };
        }


        protected ImmutableStyle<T> _output;

        private Optional<IAvaloniaStyle> _input;
        [Fragment(Order = PinOrder.Main)]
        public void SetInput(Optional<IAvaloniaStyle> input)
        {
            if (_input != input)
            {
                if (input.HasValue)
                {
                    _output = _output with { Input = input.Value };
                }
                else
                {
                    _output = _output with { Input = default };
                }

                _input = input;
            }
        }

        public IAvaloniaStyle Output => _output;
    }

    [ProcessNode]
    public abstract class StyleSetter<TProperty> : StyleSetterBase<TProperty>
    {
        public StyleSetter(string styleName) : base(styleName) { }

        protected Optional<TProperty> _value;
        public void SetValue(Optional<TProperty> value)
        {
            if (_value != value)
            {
                if (value.HasValue)
                {
                    _output = _output with { StyleValue = value.Value };
                }
                else
                {
                    _output = _output with { StyleValue = default };
                }
                _value = value;
            }
        }
    }

    [ProcessNode]
    public abstract class StyleSetter<TValue, TProperty> : StyleSetterBase<TProperty>
    {
        protected Func<TValue, TProperty> _converter;
        public StyleSetter(string styleName, Func<TValue, TProperty> converter) : base(styleName)
        {
            _converter = converter;
        }

        protected Optional<TValue> _value;
        public void SetValue(Optional<TValue> value)
        {
            if (_value != value)
            {
                if (value.HasValue)
                {
                    _output = _output with { StyleValue = _converter.Invoke(value.Value) };
                }
                else
                {
                    _output = _output with { StyleValue = default };
                }
                _value = value;
            }
        }
    }

    [ProcessNode]
    public abstract class StyleSetterSRT : StyleSetterBase<TransformOperations>
    {
        public StyleSetterSRT(string styleName) : base(styleName) { }

        protected Optional<Vector2> _scale;
        protected Optional<float> _rotation;
        protected Optional<Vector2> _translation;
        public void SetValue(Optional<Vector2> scale, Optional<float> rotation, Optional<Vector2> translation)
        {
            if (_scale != scale || _rotation != rotation || _translation != translation)
            {
                var builder = TransformOperations.CreateBuilder(3);

                if (scale.HasValue)
                    builder.AppendScale(scale.Value.X, scale.Value.Y);
                if (rotation.HasValue)
                    builder.AppendRotate(rotation.Value);
                if (translation.HasValue)
                    builder.AppendTranslate(translation.Value.X, translation.Value.Y);

                var value = builder.Build();

                _output = _output with { StyleValue = value };

                _scale = scale;
                _rotation = rotation;
                _translation = translation;
            }
        }
    }

    [ProcessNode]
    public abstract class StyleSetterBrushColor : StyleSetter<Color4, IBrush>
    {
        public StyleSetterBrushColor(string styleName) : base(styleName, (x) => x.ToSolidColorBrush()) { }
    }

    [ProcessNode]
    public abstract class StyleSetterThicknessAll : StyleSetter<float, Thickness>
    {
        public StyleSetterThicknessAll(string styleName) : base(styleName, (x) => new Thickness(x)) { }
    }

    [ProcessNode]
    public abstract class StyleSetterThicknessHV : StyleSetter<Vector2, Thickness>
    {
        public StyleSetterThicknessHV(string styleName) : base(styleName, (x) => new Thickness(x.X, x.Y)) { }
    }

    [ProcessNode]
    public abstract class StyleSetterThicknessLTRB : StyleSetter<Vector4, Thickness>
    {
        public StyleSetterThicknessLTRB(string styleName) : base(styleName, (x) => new Thickness(x.X, x.Y, x.Z, x.W)) { }
    }

    [ProcessNode]
    public abstract class StyleSetterFontFeatureCollection : StyleSetterBase<FontFeatureCollection>
    {
        public StyleSetterFontFeatureCollection(string styleName) : base(styleName) { }

        protected Spread<string> _features;

        /// <param name="features">
        /// Parses a string to return a <see cref="FontFeature"/>.
        /// Syntax is the following:
        ///  
        ///     Syntax 	        Value 	Start 	End 	 
        ///     Setting value: 	  	  	  	 
        ///     kern 	        1 	    0 	    ∞ 	    Turn feature on
        ///     +kern 	        1 	    0 	    ∞ 	    Turn feature on
        ///     -kern 	        0 	    0 	    ∞ 	    Turn feature off
        ///     kern=0 	        0 	    0 	    ∞ 	    Turn feature off
        ///     kern=1 	        1 	    0 	    ∞ 	    Turn feature on
        ///     aalt=2 	        2 	    0 	    ∞ 	    Choose 2nd alternate
        ///     Setting index: 	  	  	  	 
        ///     kern[] 	        1 	    0 	    ∞ 	    Turn feature on
        ///     kern[:] 	    1 	    0 	    ∞ 	    Turn feature on
        ///     kern[5:] 	    1 	    5 	    ∞ 	    Turn feature on, partial
        ///     kern[:5] 	    1 	    0 	    5 	    Turn feature on, partial
        ///     kern[3:5] 	    1 	    3 	    5 	    Turn feature on, range
        ///     kern[3] 	    1 	    3 	    3+1 	Turn feature on, single char
        ///     Mixing it all: 	  	  	  	 
        ///     aalt[3:5]=2 	2 	    3 	    5 	    Turn 2nd alternate on for range
        /// 
        /// </param>
        public void SetValue(Spread<string> features)
        {
            if (_features != features)
            {
                var collection = new FontFeatureCollection();

                if (features != null)
                {
                    foreach (var feature in features)
                    {
                        if (!feature.IsNullOrEmpty())
                        {
                            collection.Add(FontFeature.Parse(feature));
                        }
                    }
                }

                _output = _output with { StyleValue = collection };

                _features = features;
            }
        }
    }

    [ProcessNode]
    public abstract class StyleSetterTextDecorationCollection : StyleSetterBase<TextDecorationCollection>
    {
        public StyleSetterTextDecorationCollection() : base("TextDecorations") { }

        protected Spread<TextDecoration> _decorations;

        public void SetValue(Spread<TextDecoration> decorations)
        {
            if (_decorations != decorations)
            {
                var collection = new TextDecorationCollection();

                if (decorations != null)
                {
                    foreach (var decoration in decorations)
                    {
                        if (decoration != null)
                            collection.Add(decoration);
                    }
                }

                _output = _output with { StyleValue = collection };

                _decorations = decorations;
            }
        }
    }

    [ProcessNode]
    public abstract class StyleSetterTransitions : StyleSetterBase<Spread<IAvaloniaTransition>>
    {
        public StyleSetterTransitions(string styleName) : base(styleName) { }

        protected Spread<IAvaloniaTransition> _transitions;
        public void SetValue(Spread<IAvaloniaTransition> transitions)
        {
            if (_transitions != transitions)
            {
                _output = _output with { StyleValue = transitions };

                _transitions = transitions;
            }
        }
    }

    [ProcessNode]
    public abstract class StyleSetterPoints : StyleSetterBase<IList<AvaloniaPoint>>
    {
        public StyleSetterPoints(string styleName) : base(styleName) { }

        protected Spread<Vector2> _points;
        public void SetValue(Spread<Vector2> points)
        {
            if (_points != points)
            {
                var pts = new AvaloniaList<AvaloniaPoint>();

                if (points != null)
                {
                    foreach (var point in points)
                    {
                        pts.Add(point.ToPoint());
                    }
                }

                _output = _output with { StyleValue = pts };

                _points = points;
            }
        }
    }

    [ProcessNode]
    public abstract class StlyeSetterTicks : StyleSetterBase<AvaloniaList<double>>
    {
        public StlyeSetterTicks(string styleName) : base(styleName) { }

        protected Spread<float> _ticks;
        public void SetValue(Spread<float> ticks)
        {
            if (_ticks != ticks)
            {
                var list = new AvaloniaList<double>();

                if (ticks != null)
                {
                    foreach (var tick in ticks)
                    {
                        list.Add((double)tick);
                    }
                }

                _output = _output with { StyleValue = list };

                _ticks = ticks;
            }

        }
    }
}
