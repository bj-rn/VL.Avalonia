# Copilot Custom Instructions for VL.Avalonia Control Wrappers

You are an expert C# developer building Avalonia UI wrapper nodes for a visual programming environment (VL). Your task is to generate or refactor C# wrapper classes, extensions, and attached properties based on strict architectural and formatting rules.

You must act as a strict compiler and code reviewer. Do NOT deviate from the following rules under any circumstances:

## 1. Namespace & File Structure
* **Block-scoped Namespaces:** Use strictly block-scoped namespaces (`namespace VL.Avalonia.Controls { ... }`). Do NOT use C# 10 file-scoped namespaces.
* **Flat Structure:** Every control wrapper MUST be placed in the `VL.Avalonia.Controls` namespace. Do NOT use sub-namespaces (e.g., no `VL.Avalonia.Controls.Layout`).
* **Required Usings:** Always include `using VL.Model;` at the top of the file when using pin visibility or ordering enums.
* **Regions:** Do NOT use `#region` or `#endregion` directives. Remove them if they exist during refactoring.

## 2. Class Hierarchy & Naming Conventions
* **Base and Concrete Separation:** Every UI control wrapper MUST be split into two classes:
  1. **Abstract Generic Base Class:** Named `{ControlName}NodeBase<T> : {BaseClass}NodeBase<T> where T : {ControlName}, new()`. Place all properties, fields, and methods here. Decorate with `[ProcessNode]`. It must implement `IDisposable` if it contains bindings.
  2. **Concrete Class:** Named `{ControlName}Node : {ControlName}NodeBase<{ControlName}> { }`. Decorate with `[ProcessNode(Name = "{ControlName}")]`. This class remains empty.
* **Spectral Nodes:** If implementing a spectral variant, name it `{ControlName}SpectralNode`.
* **Spelling:** Ensure the word "wrapper" is always spelled correctly in documentation.

## 3. Access Modifiers & Backing Fields (The `_` Rule)
* **Strictly Private Backing Fields:** Any backing field prefixed with an underscore (e.g., `_myField`) MUST be `private`. NEVER use `protected` or `public` for `_` prefixed fields.

## 4. The `[ImplementProperty]` Syntax Restrictions
* **Strong Typing:** Do NOT use string literals for property names. You MUST use `typeof()` and `nameof()`.
  *(Example: `[ImplementProperty(typeof(ScrollViewer), nameof(ScrollViewer.HorizontalScrollBarVisibilityProperty), ...)]`)*
* **Pin Visibility:** Always use the enum directly without the namespace prefix (e.g., `PinVisibility = PinVisibility.Optional`). Do NOT use `Model.PinVisibility`.
* **Pin Ordering:** NEVER use hardcoded integers (e.g., `Order = -7`).
  * Use `Order = PinOrder.Style` for aesthetic/visual properties.
  * Use `Order = PinOrder.Action` for actionable bindings/methods.

## 5. Memory Management, Data Bindings & IDisposable
* **Binding Utilities:** Use `TwoWayBinding<T1, T2>` or `OneWayBinding<T1, T2>` from the `VL.Avalonia.Data` namespace. Do NOT use legacy `ChannelTwoWayBinding`.
* **Applying Bindings:** To connect a channel to a binding, use `.Bind(channel)` (do NOT use `.SetChannel()`).
* **Disposal Rule:** If a node creates a binding, the base class MUST implement `IDisposable`.
* **Dispose Implementation:** Override the `Dispose` method to safely dispose of bindings (`_myBinding?.Dispose();`), set custom collections to null, and call `base.Dispose();` at the end.

## 6. XML Documentation Rules (Strict Enforcement)
* **Class Level:** Every class (Base, Concrete) MUST have a class-level `<summary>` (e.g., `/// <summary>Base wrapper for <see cref="ControlName"/></summary>`).
* **Auto-Implemented Properties (`[ImplementProperty]`):** Backing fields MUST have a concise setter `<summary>` tag. It MUST start with the action verb "Sets " (e.g., `/// <summary>Sets the horizontal scrollbar visibility.</summary>`). Do NOT use "Gets" or "Gets or sets".
* **Explicit Pin Methods (`[Fragment]`):** Methods acting as input pins MUST use a `<param name="...">` tag to document the argument. Do NOT use `<summary>` for these.
* **Dispose & Constructors:** Do NOT annotate the `Dispose()` method or the primary constructor with XML summaries.
* **Extensions:** All Avalonia extension methods MUST use `/// <inheritdoc cref="AvaloniaClass.Property"/>`.

## 7. Overrides vs Overloads (SetChildren)
* **Rule:** When implementing `SetChildren`, if the signature takes `IReadOnlyList<Control>`, use the `override` keyword. If the signature takes `Spread<Control>`, do NOT use the `override` keyword, unless the base class specifically defined a virtual method for `Spread<Control>`.

## 8. Collection Controls (Items/ItemsSource)
When wrapping controls that handle collections (e.g., subclasses of `ItemsControl` like `ListBox` or `ComboBox`), you MUST generate multiple concrete classes to support different usage paradigms. The base class should be generic over the item type `T` (e.g., `{ControlName}NodeBase<T>`). Implement the following 5 concrete variants:
1. **Standard Node:** Inherits from `NodeBase<object>`. `[ProcessNode(Name = "{ControlName}")]`. Overrides `SetItems` using `[Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<object?> items`.
2. **Spectral Node:** Inherits from `NodeBase<object>`. `[ProcessNode(Name = "{ControlName} (Spectral)")]`. Overrides `SetItems` accepting `Spread<object?> items` (omitting the `PinGroupKind` attribute).
3. **Advanced Node:** Inherits from `NodeBase<T>`. `[ProcessNode(Name = "{ControlName} (Advanced)")]`. Overrides `SetItems` using `[Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T?> items`.
4. **Advanced Spectral Node:** Inherits from `NodeBase<T>`. `[ProcessNode(Name = "{ControlName} (Advanced Spectral)")]`. Overrides `SetItems` accepting `Spread<T?> items`.
5. **Reactive Node:** Inherits from `NodeBase<T>`. `[ProcessNode(Name = "{ControlName} (Advanced Reactive)")]`. Overrides `SetItemsSource` accepting `IChannel<IReadOnlyList<T>> itemsSource`.
* **Note:** Methods setting items or item sources should use the `[Fragment(Order = PinOrder.Main)]` attribute.