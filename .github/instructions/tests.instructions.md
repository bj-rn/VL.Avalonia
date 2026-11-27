---
applyTo: "tests/**"
---

# Test instructions for VL.Avalonia

Tests live in `tests/VL.Avalonia.Tests` (xUnit + Avalonia.Headless, `net8.0-windows`). They cover our own code (node wrappers, bindings, helpers, generators, the Skia host), not behaviour Avalonia already tests. They are not run in CI.

## 1. Process

* **Check the claim first.** Before fixing a reported bug, read the source and confirm it. Reports can be wrong or only partly right, and the code next to a real bug often has a related one, so read the surrounding code too.
* **A regression test must fail on the old code.** Prove it: stash only the source fix (`git stash push -q -- <source files>`), run the filtered tests and watch them fail, then `git stash pop`. Say in the summary how many tests failed before the fix.
* **Keep fixes small.** Fix the bug and anything tightly coupled to it. Don't refactor unrelated code or fix unrelated issues; report them instead.
* **Add guard tests** that pass before and after the fix, so it can't over-correct. Examples: the same input sent twice keeps its value; only some pins of a multi-pin setter connected; a valid value still goes through.
* **Error paths that can't be tested permanently** (e.g. an exception inside a source generator) are checked once, with a temporary throw or probe test. Remove the temporary code afterwards and confirm the tree is clean.
* **Bugs in other repositories** (e.g. VL.StandardLibs, Avalonia) aren't worked around here. Write a short hand-off note: symptom, file/line references, a suggested fix.
* **Run the full suite after every fix**, not only the filtered tests.

## 2. What to cover (VL node semantics)

VL calls node setters every frame and pins can be connected or disconnected at any time. Test behaviour over time, not just the first state:

* **`Optional<T>`:** no value (`default`) is different from a value of `null`. In VL.Core, `new Optional<T>(null)` and the implicit conversion from `null` both give `HasValue == true` with a `null` `Value`. Test both.
* **Pin transitions:** connected → disconnected must clear or reset what was set. Don't only test "never connected".
* **Input switching:** when the input control changes to another control or to none, the old control must be cleaned up (attached values, setters, subscriptions).
* **Chained nodes:** the upstream node's `Output` can be `null` when it has no input, and the downstream node must not throw.
* **Change checks:** for code like `if (_field != value) { ... }`, test that a second, different value is applied too. This catches a missing `_field = value`.
* **Dispose:** releases every binding, subscription and event handler the node created. Channels passed in by the caller are never disposed.
* **Units and conversions:** degrees vs radians, seconds vs milliseconds, culture-invariant parsing, `Nullable<T>` targets.

### Cover every subclass with reflection

When many classes share a base or pattern (attached-property nodes, style setters, easings), write `[Theory]`/`[AvaloniaTheory]` tests whose `MemberData` finds every concrete subclass by reflection. Don't hand-pick a few. New subclasses are then covered automatically.

* Check the behaviour generically. For example, find the attached properties that are set with `AvaloniaPropertyRegistry.Instance.GetRegisteredAttached(type).Where(control.IsSet)`, so a wrong property in a pair is caught.
* Order the found types (e.g. by name) so the theory cases are stable between runs.
* Examples: [AttachedPropertyNodeTests.cs](../../tests/VL.Avalonia.Tests/AttachedPropertyNodeTests.cs), [StyleSettersTests.cs](../../tests/VL.Avalonia.Tests/StyleSettersTests.cs), [EasingsTests.cs](../../tests/VL.Avalonia.Tests/EasingsTests.cs).

## 3. Test setup

| Code under test | Use |
|---|---|
| Plain logic (math, parsing, conversion, reconciliation) | `[Fact]` / `[Theory]` |
| Anything touching `AvaloniaObject`, controls, styles or the dispatcher | `[AvaloniaFact]` / `[AvaloniaTheory]` (runs on the headless UI thread of [TestApp.cs](../../tests/VL.Avalonia.Tests/TestApp.cs)) |
| Source generators | `CSharpGeneratorDriver`, with metadata references from the loaded assemblies. See [SourceGeneratorTests.cs](../../tests/VL.Avalonia.Tests/SourceGeneratorTests.cs) |

* **Go through the node's public API** (`Set*` methods, `Output`, `Dispose`), the way VL calls it. For internals, `InternalsVisibleTo("VL.Avalonia.Tests")` is set in VL.Avalonia and VL.Avalonia.Skia. `AvaloniaAccessUnstablePrivateApis` is on, for raw input event args.
* **Prefer small hand-written fakes over a mocking library:**
  * a `DispatchProxy` for wide interfaces such as `IDrawingContextImpl` ([SkiaDrawingOperationTests.cs](../../tests/VL.Avalonia.Tests/SkiaDrawingOperationTests.cs));
  * a `FakeClock : IClock` ([GammaDispatcherTests.cs](../../tests/VL.Avalonia.Tests/GammaDispatcherTests.cs));
  * a counting `FlyoutBase` subclass ([NodeDisposalTests.cs](../../tests/VL.Avalonia.Tests/NodeDisposalTests.cs));
  * `PropertyChanged` counters.
* **`NodeContext`:** pass `null!` only where the code under test never reads it. If you need one, add a small seam instead, e.g. an `internal static` helper like `ReparentingHelper.TryDetach`.
* **Threading:** to push values from another thread, use a dedicated `Thread` plus `Join()` (see `OnOtherThread` in [ItemsSourceSyncTests.cs](../../tests/VL.Avalonia.Tests/ItemsSourceSyncTests.cs)). Don't use `Task.Run(...).Wait()`: it can be inlined on the UI thread and then the test proves nothing. Never use `Thread.Sleep` to wait for results.
* **Headless has no theme.** Controls that need a template (e.g. `ItemsControl` containers) need an explicit `ControlTemplate` with an `ItemsPresenter`, a `Window` to host them, and `UpdateLayout()` before you assert.

## 4. Conventions

* One test class per area or bug, named `{Subject}Tests.cs`. File-scoped `namespace VL.Avalonia.Tests;`.
* Test names describe the behaviour as a sentence with underscores, e.g. `Disconnecting_the_input_clears_the_attached_value`, `None_still_forwards_later_channel_changes`.
* Add `using Avalonia;` and write `global::Avalonia...` where `VL.Avalonia.*` namespaces shadow Avalonia ones.
* Add a short comment only where the reason isn't obvious (e.g. why a guard test exists or why a template is needed).
* New test files: UTF-8 without BOM, CRLF line endings.
* Assert on the observable result (property value, `IsSet`, output, call count), not on private fields. Reflection on non-public members is only for setup, e.g. creating an Avalonia type with an internal constructor.

## 5. Running

```powershell
& "C:\Program Files\dotnet\dotnet.exe" test tests\VL.Avalonia.Tests\VL.Avalonia.Tests.csproj -nologo -clp:ErrorsOnly
# Filtered:
& "C:\Program Files\dotnet\dotnet.exe" test tests\VL.Avalonia.Tests\VL.Avalonia.Tests.csproj -nologo -clp:ErrorsOnly --filter "FullyQualifiedName~AttachedPropertyNodeTests"
```

* Use the x64 `dotnet` explicitly; the x86 host aborts the test run.
* Close vvvv first if the build fails because the output DLLs are locked. If the CodeGen `obj` DLL is locked (CS2012), wait a moment and retry.
* To inspect generated sources, build VL.Avalonia with `--no-incremental -p:EmitCompilerGeneratedFiles=true`.
