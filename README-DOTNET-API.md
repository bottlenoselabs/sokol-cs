# .NET APIs

## Background

The .NET style API is a modification of the C bindings (from the side of .NET) to be more idiomatic and overall easier to use. The `unsafe` keyword is not required.

The .NET API currently targets [.NET Standard 2.1](https://docs.microsoft.com/en-us/dotnet/standard/net-standard). This is to make use of [Single Instruction Multiple Data (SIMD)](https://devblogs.microsoft.com/dotnet/the-jit-finally-proposed-jit-and-simd-are-getting-married/) of `System.Numerics` namespace which `Sokol.NET` makes use of `Vector2`, `Vector3` and `Matrix4x4`. `Span<T>`, `Memory<T>`, and friends are also used which live in the `System.Memory` namespace which is also part of .NET Standard 2.1 These and other .NET Standard 2.1 libraries results in the `Sokol.NET` code remaining small, highly performant, and easy to use without re-inventing the wheel.

All the types are [.NET value types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types). This is to get as close as possible to zero allocations on the managed heap during the long running state of the application's loop. [This is often desirable in games](https://www.shawnhargreaves.com/blog/twin-paths-to-garbage-collector-nirvana.html) and [other demanding, high performance, applications](https://docs.microsoft.com/en-us/dotnet/csharp/write-safe-efficient-code).

## Getting Started

The easiest and fastest way to get started is to create a class that inherits the `Application` class in the `Sokol.App` namespace.

```cs
using Sokol.App;
using Sokol.Graphics;

...

public sealed class MyApplication : Application
{
    protected override void Frame()
    {
        var pass = BeginDefaultPass(Rgba32.Red);
        pass.End();

        GraphicsDevice.Commit();
    }
}
```

Then in your entry-point run the application.

```cs
var app = new MyApplication();
app.Run();
```

## Samples

To learn how to use the .NET API check out the following samples which expand on the getting started code mentioned earlier are in sync with the official [C samples](https://github.com/floooh/sokol-samples).

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