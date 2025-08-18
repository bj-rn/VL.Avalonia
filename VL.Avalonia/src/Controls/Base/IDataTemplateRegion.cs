using Avalonia.Controls;

namespace VL.Avalonia.Controls.Base
{
    /// <summary>
    /// Base interface for DataTemplate region in VL
    /// You can check DataTemplate in vl definitions
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TIn"></typeparam>
    public interface IDataTemplateRegion<TState, TIn>
    {
        /// <summary>
        /// Used internally to convert patch to delegate
        /// </summary>
        public TState InvokeRegion(TState state, TIn input, out Control output);
    }
}
