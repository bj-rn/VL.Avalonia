using System;
using Microsoft.CodeAnalysis;
using VL.Avalonia.CodeGen.Forwards;

namespace VL.Avalonia.CodeGen
{
    [Generator]
    public class AvaloniaReflectionGenerator : IIncrementalGenerator
    {
        const string RESTRICTED_ASSEMBLY_NAME = "VL.Avalonia";

        // A failure means VL.Avalonia would ship without its routed event nodes, so fail the build.
        // Only raised while building VL.Avalonia itself, so no analyzer release tracking.
#pragma warning disable RS2008
        internal static readonly DiagnosticDescriptor GenerationFailed = new(
            id: "VLAGEN001",
            title: "Forward generation failed",
            messageFormat: "{0} failed, its forwards are missing: {1}",
            category: "VL.Avalonia.CodeGen",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
#pragma warning restore RS2008

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

                    Run(spc, nameof(RoutedEventArgsGenerator), () => RoutedEventArgsGenerator.Generate(spc, compilation));
                    Run(spc, nameof(RoutedEventGenerator), () => RoutedEventGenerator.Generate(spc, compilation));
                }
            );
        }

        private static void Run(SourceProductionContext spc, string name, Action generate)
        {
            try
            {
                generate();
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                spc.ReportDiagnostic(
                    Diagnostic.Create(GenerationFailed, Location.None, name, e.ToString())
                );
            }
        }
    }
}
