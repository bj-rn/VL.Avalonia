using System.Collections.Immutable;
using System.Reflection;
using Avalonia.Interactivity;
using VL.Core;
using VL.Core.CompilerServices;

[assembly: AssemblyInitializer(typeof(VL.Avalonia.Initialization))]

namespace VL.Avalonia
{
    public sealed class Initialization : AssemblyInitializer<Initialization>
    {
        public override void Configure(AppHost appHost)
        {
            var factory = appHost.Factory;

            appHost.RegisterNodeFactory(
                "VL.Avalonia.NodeFactory",
                nodeFactory =>
                {
                    var nodes = new List<IVLNodeDescription>();

                    var assemblies = AppDomain
                        .CurrentDomain.GetAssemblies()
                        .Where(a => a.GetName().Name!.StartsWith("Avalonia"));

                    foreach (var assembly in assemblies)
                    {
                        foreach (var type in GetTypesSafe(assembly))
                        {
                            // 1. Scan for public static readonly RoutedEvent fields
                            //    These nodes will output the actual RoutedEvent object (e.g. Button.ClickEvent)
                            var routedEventFields = type.GetFields(
                                    BindingFlags.Public | BindingFlags.Static
                                )
                                .Where(f => f.FieldType == typeof(RoutedEvent));

                            foreach (var field in routedEventFields)
                            {
                                var routedEventValue = (RoutedEvent)field.GetValue(null)!;

                                // Naming convention: Remove "Event" suffix for cleaner node names
                                var name = field.Name.EndsWith("Event")
                                    ? field.Name.Substring(0, field.Name.Length - 5)
                                    : field.Name;

                                nodes.Add(
                                    CreateNodeDescription(nodeFactory, type, name, routedEventValue)
                                );
                            }

                            // 2. Scan for RoutedEventArgs types
                            //    These nodes will output the System.Type object (e.g. typeof(RoutedEventArgs))
                            //    Useful if you need to pass the type itself to other generic nodes
                            if (
                                typeof(RoutedEventArgs).IsAssignableFrom(type)
                                && !type.IsAbstract
                                && type != typeof(RoutedEventArgs)
                            )
                            {
                                nodes.Add(
                                    CreateNodeDescription(nodeFactory, type, type.Name, type)
                                );
                            }
                        }
                    }

                    Console.WriteLine(nodes);

                    return NodeBuilding.NewFactoryImpl(nodes.ToImmutableArray());
                }
            );
        }

        /// <summary>
        /// Generic helper to create a node that outputs a constant static value.
        /// </summary>
        private static IVLNodeDescription CreateNodeDescription<T>(
            IVLNodeDescriptionFactory factory,
            Type ownerType,
            string nodeName,
            T value
        )
        {
            var category = ownerType.Namespace ?? "Avalonia";

            return factory.NewNodeDescription(
                name: nodeName,
                category: category,
                fragmented: false,
                init: descriptionCtx =>
                {
                    return descriptionCtx.Node(
                        inputs: Enumerable.Empty<IVLPinDescription>(),
                        outputs: new[] { descriptionCtx.Pin("Output", typeof(T), value) },
                        newNode: instanceCtx =>
                        {
                            return instanceCtx.Node(
                                inputs: Enumerable.Empty<IVLPin>(),
                                outputs: new[] { instanceCtx.Output(() => value) }
                            );
                        }
                    );
                }
            );
        }

        /// <summary>
        /// Safely retrieves types from an assembly, handling loader exceptions for missing dependencies.
        /// </summary>
        private static IEnumerable<Type> GetTypesSafe(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null)!;
            }
            catch
            {
                return Enumerable.Empty<Type>();
            }
        }
    }
}
