using System;
using Microsoft.CodeAnalysis;
using VL.Avalonia.CodeGen.Forwards;

namespace VL.Avalonia.CodeGen
{
    [Generator]
    public class AvaloniaReflectionGenerator : IIncrementalGenerator
    {
        const string RESTRICTED_ASSEMBLY_NAME = "VL.Avalonia";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var compilationProvider = context.CompilationProvider;

            context.RegisterSourceOutput(
                compilationProvider,
                (spc, compilation) =>
                {
                    // We want to generate only for VL.Avalonia assembly
                    if (compilation.AssemblyName != RESTRICTED_ASSEMBLY_NAME)
                        return;

                    try
                    {
                        RoutedEventArgsGenerator.Generate(spc, compilation);
                        RoutedEventGenerator.Generate(spc, compilation);
                    }
                    catch (Exception) { }
                }
            );
        }
    }
}
