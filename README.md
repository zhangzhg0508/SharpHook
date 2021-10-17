# SharpHook

SharpHook is a library which provides a cross-platform global keyboard and mouse hook for .NET. It is a thin wrapper of
[libuiohook](https://github.com/kwhat/libuiohook) and provides direct access to its features as well as a higher-level
interface and classes to work with it.

## Installation

The library will be available on NuGet soon. In the meantime you'll have to build and pack it yourself.

## Supported Platforms

<table>
  <tr>
    <th></th>
    <th>Windows</th>
    <th>macOS</th>
    <th>Linux</th>
  </tr>
  <tr>
    <th>x86</th>
    <td>Yes</td>
    <td>N/A</td>
    <td>No<sup>1</sup></td>
  </tr>
  <tr>
    <th>x64</th>
    <td>Yes</td>
    <td>Yes</td>
    <td>Yes</td>
  </tr>
  <tr>
    <th>Arm32</th>
    <td>Yes<sup>2</sup></td>
    <td>N/A</td>
    <td>Yes</td>
  </tr>
  <tr>
    <th>Arm64</th>
    <td>No<sup>3</sup></td>
    <td>Yes</td>
    <td>Yes</td>
  </tr>
</table>

[1] - Linux on x86 is [not supported](https://github.com/dotnet/runtime/issues/7335) by .NET itself.

[2] - Windows Arm32 support was
[dropped](https://github.com/dotnet/core/blob/main/release-notes/5.0/5.0-supported-os.md) in .NET 5 so it will most
probably be dropped by this library in a future version as well.

[3] - Windows on Arm64 is not yet supported by libuiohook.

libuiohook only supports X11 on Linux. Wayland support [may be coming](https://github.com/kwhat/libuiohook/issues/100),
but it's not yet here.

## Usage

### Native Methods of libuiohook

SharpHook exposes the methods of libuiohook in the `SharpHook.Native.UioHook` class. The `SharpHook.Native`
namespace also contains structs and enums which represent the data returned by libuiohook.

**Note**: In general, you shouldn't use native methods directly. Instead, use the higher-level interface and classes
provided by SharpHook.

`UioHook` contains the following methods for working with the global hook:

- `SetDispatchProc` - sets the function which will be called when an event is raised by libuiohook.

- `Run` - creates a global hook and runs it on the current thread, blocking it until `Stop` is called.

- `Stop` - destroys the global hook.

libuiohook also provides functions to get various system properties. The corresponding methods in `UioHook` are listed
below:

- `CreateScreenInfo` - gets the information about the screens and returns it as an array. There's also a version of
this function which returns an unmanaged array and that version shouldn't be used directly.

- `GetAutoRepeatRate`

- `GetAutoRepeatDelay`

- `GetPointerAccelerationMultiplier`

- `GetPointerAccelerationThreshold`

- `GetPointerSensitivity`

- `GetMultiClickTime`

### The Global Hook

SharpHook provides the `IGlobalHook` interface along with two default implementations which you can use to control the
hook and subscribe to its events. Here's a basic usage example:

```C#
using SharpHook;

// ...

var hook = new TaskPoolGlobalHook();

hook.HookEnabled += OnHookEnabled;
hook.HookDisabled += OnHookDisabled;

hook.KeyTyped += OnKeyTyped;
hook.KeyPressed += OnKeyPressed;
hook.KeyReleased += OnKeyReleased;

hook.MouseClicked += OnMouseClicked;
hook.MousePressed += OnMousePressed;
hook.MouseReleased += OnMouseReleased;
hook.MouseMoved += OnMouseMoved;
hook.MouseDragged += OnMouseDragged;

hook.MouseWheel += OnMouseWheel;

await hook.Start();
```

First, you create the hook, then subscribe to its events, and then start it. The `Start()` method returns a `Task`
which is finished when the hook is destroyed, so if you `await` it, you block the current async context until it stops.
You can subscribe to events after the hook is started.

`IGlobalHook` extends `IDisposable`. When you call the `Dispose` method on a hook, it's destroyed. The contract of
the interface is that once a hook has been destroyed, it cannot be started again - you'll have to create a new instance.

**Important**: Always use one instance of `IGlobalHook` at a time in the entire application since they all must use
the same static method to set the hook callback for libuiohook, and there may only be one callback at a time.

SharpHook provides two implementations of `IGlobalHook`:

- `SimpleGlobalHook` runs the hook on a separate thread, and runs all of its event handlers on that same thread. This
means that the handlers should generally be fast since they will block the hook from handling the events that follow if
they run for too long.

- `TaskPoolGlobalHook` runs the hook on a separate thread, and runs all of its event handlers on other threads inside
the default thread pool for tasks. The parallelism level of the handlers can be configured. On backpressure it will
queue the remaining handlers. This means that the hook will be able to process all events. This implementation should be
preferred to `SimpleGlobalHook` except for very simple use-cases.

The library also provides the `GlobalHookBase` class which you can extend to create your own implementation of the
global hook. It runs the hook on a separate thread and calls appropriate event handlers. You only need to implement a
strategy for dispatching the events.

## Building from Source

In order to build this library, you'll first need to get libuiohook binaries. You can build them yourself as instructued
in the [libuiohook](https://github.com/kwhat/libuiohook) repository, or you can get a
[nightly build](https://github.com/kwhat/libuiohook/actions/workflows/package.yml). Place the binaries into the
appropriate directories under the `lib` directory in the `SharpHook` project.

With libuiohook in place you can build SharpHook using your usual methods, e.g. with Visual Studio or the `dotnet` CLI.

The `SharpHook` project defines multiple platforms. If you want to run `SharpHook.Sample`, make sure you don't use
`AnyCPU` since the libuiohook version for it is not defined.

## Icon

Icon made by [Freepik](https://www.freepik.com) from [www.flaticon.com](https://www.flaticon.com).
