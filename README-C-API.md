# C APIs

## Background

The C APIs [P/Invoke](https://docs.microsoft.com/en-us/dotnet/framework/interop/consuming-unmanaged-dll-functions) bindings are a pure port of the C headers; they exactly match what is in C, and the naming conventions used in C are maintained.

The structs in C# are blittable, meaning they have the [same memory layout as the C structs](https://docs.microsoft.com/en-us/dotnet/framework/interop/blittable-and-non-blittable-types). This allows the structs to be passed by value (copy of data) or reference (akin to copy of pointer) from the managed world of .NET to the unmanaged world of C [as is](https://docs.microsoft.com/en-us/dotnet/framework/interop/copying-and-pinning#formatted-blittable-classes).

In .NET, the `unsafe` keyword will most often be necessary for using the C structs and calling the C functions.

## Getting Started

### Load the API

To call the C functions, the functions need to be loaded at runtime. This is done by calling one of the `LoadApi(...)` overloads.

e.g.
```cs
var pathToLibrary = ... // path to sokol_gfx.dll, libsokol_gfx.dylib, or libsokol_gfx.so
sokol_gfx.LoadApi(pathToLibrary);
```

Because each back-end for `sokol_gfx` is a seperate native library, there can be multiple native libraries with the same name. For example for Windows, there could be a `sokol_gfx.dll` for both Direct3D11 and OpenGL. For this reason it's recommended add the back-end to be apart of the file name. For example, `sokol_gfx-opengl.dll` or `sokol_gfx-d3d11.dll`.

The `LoadApi(GraphicsBackend backend)` exists as a convience for loading the native library given the back-end and the current platform.

e.g.
```cs
sokol_gfx.LoadApi(GraphicsBackend.OpenGL);
```

This will look for `sokol_gfx-d3d11.dll` for Windows Direct3D 11, `libsokol_gfx-metal.dylib` for macOS Metal, `libsokol_gfx-opengl.so` for Linux OpenGL, etc. Where it looks for these native libraries will be in one the following paths until it finds one.

- [`Environment.CurrentDirectory`](https://docs.microsoft.com/en-us/dotnet/api/system.environment.currentdirectory): The current working directory of the application.
- [`AppDomain.CurrentDomain.BaseDirectory`](https://docs.microsoft.com/en-us/dotnet/api/system.appdomain.basedirectory): The base directory of the application.
- `runtimes/{rid}/native`: A special relative folder where the `rid` is the [runtime identifier of a specific platform target](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog). For example, `runtimes/win-x64/native` for Windows.

If you use the [`libsokol_gfx`](https://www.myget.org/feed/lithiumtoast/package/nuget/libsokol_gfx) NuGet package, the native libraries will be automatically be added to your project, and calling `LoadApi(GraphicsBackend backend)` will "just work".

### Initialize `sokol_gfx`

Once the API is loaded, the state of `sokol_gfx` needs to be initialized.

```cs
var descriptor = default(sokol_gfx.sg_desc);
// Initialize the descriptor...
// See the comments in sokol_gfx for setting up GL, Metal, D3D11, WebGPU, etc
sokol_gfx.sg_setup(&descriptor);
```

### Initializing `sokol_app`

If you are using `sokol_app`, you don't need to load the API for `sokol_gfx` directly. This is because `sokol_app` will load the API for `sokol_gfx`.

```cs
sokol_app.LoadApi(GraphicsBackend.OpenGL);
```

Next you need to fill the descriptor for initializing the application and run the application.
```cs
var desc = default(AppDescriptor);

// The call-back handles need to be stored somewhere...
// This technique is to ensure that the delegate call-backs that are called from C code are not garbage collected
// If they were garbage collected, the C code would crash

var initializeCallbackDelegate = new sokol_app.NativeCallbackDelegate(InitializeCallback);
_initializeCallbackHandle = GCHandle.Alloc(initializeCallbackDelegate);
desc.InitializeCallback = Marshal.GetFunctionPointerForDelegate(initializeCallbackDelegate);

var frameCallbackDelegate = new sokol_app.NativeCallbackDelegate(FrameCallback);
_frameCallbackHandle = GCHandle.Alloc(frameCallbackDelegate);
desc.FrameCallback = Marshal.GetFunctionPointerForDelegate(frameCallbackDelegate);

var cleanUpCallbackDelegate = new sokol_app.NativeCallbackDelegate(CleanUpCallback);
_cleanUpCallbackHandle = GCHandle.Alloc(cleanUpCallbackDelegate);
desc.CleanUpCallback = Marshal.GetFunctionPointerForDelegate(cleanUpCallbackDelegate);

var eventCallbackDelegate = new sokol_app.NativeCallbackDelegateEvent(EventCallback);
_eventCallbackHandle = GCHandle.Alloc(eventCallbackDelegate);
desc.EventCallback = Marshal.GetFunctionPointerForDelegate(eventCallbackDelegate);

sokol_app.sapp_run(&desc);

...

private static void InitializeCallback()
{
    // your code here
}

private static void FrameCallback()
{
    // your code here
}

private static void CleanUpCallback()
{
    // your code here
}

private static void EventCallback(Event @event)
{
    // your code here
}

```

Then, in the call-back for initialize you need to initialize `sokol_gfx`.

```cs
var desc = default(sokol_gfx.sg_desc);
ref var context = sokol_app.sapp_sgcontext();
sokol_gfx.sg_setup(&descriptor);
```

### Import the Namespace

While not necessary, it's recommended to import the module with all the bindings, structs, and enums like so:

```cs
using static sokol_gfx;
```

```cs
using static sokol_app;
```

This makes it possible to not have to type `sokol_gfx` or `sokol_app` for every function call.

## Samples

To learn how to use the C API, check out the [official C samples](https://github.com/floooh/sokol-samples). You can also find the same examples that run in [your browser](https://floooh.github.io/sokol-html5/index.html).

## Documentation

The comments in the [`sokol_gfx.h`](https://github.com/floooh/sokol/blob/master/sokol_gfx.h), [`sokol_app.h`](https://github.com/floooh/sokol/blob/master/sokol_app.h), etc,  are also a good reference for documentation.
