# .NET APIs

## Background

The .NET style API is a modification of the C bindings (from the side of .NET) to be more idiomatic and overall easier to use. The `unsafe` keyword is not required.

The .NET API currently targets [.NET Standard 2.1](https://docs.microsoft.com/en-us/dotnet/standard/net-standard). This is to make use of [Single Instruction Multiple Data (SIMD)](https://devblogs.microsoft.com/dotnet/the-jit-finally-proposed-jit-and-simd-are-getting-married/) of `System.Numerics` namespace which `Sokol.NET` makes use of `Vector2`, `Vector3` and `Matrix4x4`. `Span<T>`, `Memory<T>`, and friends are also used which live in the `System.Memory` namespace which is also part of .NET Standard 2.1 These and other .NET Standard 2.1 libraries results in the `Sokol.NET` code remaining small, highly performant, and easy to use without re-inventing the wheel.

All the types are [.NET value types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types). This is to get as close as possible to zero allocations on the managed heap during the long running state of the application's loop. [This is often desirable in games](https://www.shawnhargreaves.com/blog/twin-paths-to-garbage-collector-nirvana.html) and [other demanding, high performance, applications](https://docs.microsoft.com/en-us/dotnet/csharp/write-safe-efficient-code).

## Getting Started

### Load the API

To call the C functions, the functions need to be loaded at runtime. This is done by calling one of the `LoadApi(...)` overloads.

e.g.
```cs
var pathToLibrary = ... // path to sokol_gfx.dll, libsokol_gfx.dylib, or libsokol_gfx.so
Sokol.Graphics.LoadApi(pathToLibrary);
```

Because each back-end for `sokol_gfx` is a seperate native library, there can be multiple native libraries with the same name. For example for Windows, there could be a `sokol_gfx.dll` for both Direct3D11 and OpenGL. For this reason it's recommended add the back-end to be apart of the file name. For example, `sokol_gfx-opengl.dll` or `sokol_gfx-d3d11.dll`.

The `LoadApi(GraphicsBackend backend)` exists as a convience for loading the native library given the back-end and the current platform.

e.g.
```cs
Sokol.Graphics.LoadApi(GraphicsBackend.OpenGL);
```

This will look for `sokol_gfx-d3d11.dll` for Windows Direct3D 11, `libsokol_gfx-metal.dylib` for macOS Metal, `libsokol_gfx-opengl.so` for Linux OpenGL, etc. Where it looks for these native libraries will be in one the following paths until it finds one.

- [`Environment.CurrentDirectory`](https://docs.microsoft.com/en-us/dotnet/api/system.environment.currentdirectory): The current working directory of the application.
- [`AppDomain.CurrentDomain.BaseDirectory`](https://docs.microsoft.com/en-us/dotnet/api/system.appdomain.basedirectory): The base directory of the application.
- `runtimes/{rid}/native`: A special folder where the `rid` is the [runtime identifier of a specific platform target](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog). For example, `runtimes/win-x64/native` for Windows.

If you use the [`libsokol_gfx`](https://www.myget.org/feed/lithiumtoast/package/nuget/libsokol_gfx) NuGet package, the native libraries will be automatically be added to your project, and calling `LoadApi(GraphicsBackend backend)` will "just work".

## Samples

To learn how to use the .NET API, check out the following samples, which are in sync with the official [C samples](https://github.com/floooh/sokol-samples).

Name|Description|GIF/Screenshot
:---:|:---:|:---:
[Clear](src/Samples/Samples.Clear/Samples.Clear/ClearApplication.cs)|[Clears the frame buffer with a specific color.](src/Samples/Samples.Clear/Samples.Clear/ClearApplication.cs)|<img src="screenshots/clear.gif" width="350">
[Triangle](src/Samples/Samples.Triangle/Samples.Triangle/TriangleApplication.cs)|[Draw a triangle in clip space using a vertex buffer and a index buffer.](src/Samples/Samples.Triangle/Samples.Triangle/TriangleApplication.cs)|<img src="screenshots/triangle.png" width="350">
[Quad](src/Samples/Samples.Quad/Samples.Quad/QuadApplication.cs)|[Draw a quad in clip space using a vertex buffer and a index buffer.](src/Samples/Samples.Quad/Samples.Quad/QuadApplication.cs)|<img src="screenshots/quad.png" width="350">
[BufferOffsets](src/Samples/Samples.Cube/Samples.BufferOffsets/BufferOffsetsApplication.cs)|[Draw a triangle and a quad in clip space using the same vertex buffer and and index buffer.](src/Samples/Samples.BufferOffsets/Samples.BufferOffsets/BufferOffsetsApplication.cs)|<img src="screenshots/buffer-offsets.png" width="350">
[Cube](src/Samples/Samples.Cube/Samples.Cube/CubeApplication.cs)|[Draw a cube using a vertex buffer, a index buffer, and a Model, View, Projection matrix (MVP).](src/Samples/Samples.Cube/Samples.Cube/CubeApplication.cs)|<img src="screenshots/cube.gif" width="350">
[NonInterleaved](src/Samples/Samples.NonInterleaved/Samples.NonInterleaved/NonInterleavedApplication.cs)|[Draw a cube using a vertex buffer with non-interleaved vertices, a index buffer, and a Model, View, Projection matrix (MVP).](src/Samples/Samples.NonInterleaved/Samples.NonInterleaved/NonInterleavedApplication.cs)|<img src="screenshots/non-interleaved.gif" width="350">
[TexCube](src/Samples/Samples.TexCube/Samples.TexCube/TextureCubeApplication.cs)|[Draw a textured cube using a vertex buffer, a index buffer, and a Model, View, Projection matrix (MVP).](src/Samples/Samples.TexCube/Samples.TexCube/TextureCubeApplication.cs)|<img src="screenshots/tex-cube.gif" width="350">
[Offscreen](src/Samples/Samples.Offscreen/Samples.Offscreen/OffscreenApplication.cs)|[Draw a non-textured cube off screen to a render target and use the result as as the texture when drawing a cube to the framebuffer.](src/Samples/Samples.Offscreen/Samples.Offscreen/OffscreenApplication.cs)|<img src="screenshots/off-screen.gif" width="350">
[Instancing](src/Samples/Samples.Instancing/Samples.Instancing/InstancingApplication.cs)|[Draw multiple particles using one immutable vertex, one immutable index buffer, and one vertex buffer with streamed instance data.](src/Samples/Samples.Instancing/Samples.Instancing/InstancingApplication.cs)|<img src="screenshots/instancing.gif" width="350">
[MultipleRenderTargets](src/Samples/Samples.MultipleRenderTargets/Samples.MultipleRenderTargets/MultipleRenderTargetsApplication.cs)|[Draw a cube to multiple render targets and then blend the results.](src/Samples/Samples.MultipleRenderTargets/Samples.MultipleRenderTargets/MultipleRenderTargetsApplication.cs)|<img src="screenshots/mrt.gif" width="350">
[ArrayTexture](src/Samples/Samples.ArrayTex/Samples.ArrayTex/ArrayTexApplication.cs)|[Draw a cube with multiple 2D textures using one continous block of texture data (texture array).](src/Samples/Samples.ArrayTex/Samples.ArrayTex/ArrayTexApplication.cs)|<img src="screenshots/array-tex.gif" width="350">
[DynamicTexture](src/Samples/Samples.DynTex/Samples.DynTex/DynTexApplication.cs)|[Draw a cube with streamed 2D texture data. The data is updated to with the rules of Conway's Game of Life.](src/Samples/Samples.DynTex/Samples.DynTex/DynTexApplication.cs)|<img src="screenshots/dyn-tex.gif" width="350">

## ShaderToy

There are also some [3rd party ShaderToy samples](src/ShaderToy/3rdParty) using `Sokol.NET`.

## Documentation

Almost all the code has XML documentation comments which are drived from the comments in [`sokol_gfx.h`](https://github.com/floooh/sokol/blob/master/sokol_gfx.h), [`sokol_app.h`](https://github.com/floooh/sokol/blob/master/sokol_app.h), [Apple developer docs on Metal](https://developer.apple.com/documentation), [Microsoft developer docs on DirectX](https://docs.microsoft.com/en-ca/), [Khronos docs on OpenGL & OpenGL ES](https://www.khronos.org/registry/OpenGL-Refpages/).