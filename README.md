<p align="center">
  <b>sokol-cs</b> - C# bindings for <a href="https://github.com/floooh/sokol">https://github.com/floooh/sokol</a>
</p>

## Background

C# bindings for https://github.com/floooh/sokol. Includes the C style API precisely as it is and a slightly more idiomatic C# style API for convenience.

Name|Description
:---:|:---:
[`sokol_gfx`](https://github.com/floooh/sokol#sokol_gfxh)|A simple and modern wrapper around GLES2/WebGL, GLES3/WebGL2, GL3.3, D3D11 and Metal.
[`sokol_app`](https://github.com/floooh/sokol#sokol_apph)|A minimal cross-platform application-wrapper library.

To learn more about `sokol` and it's philosophy, see the [*A Tour of `sokol_gfx.h`*](https://floooh.github.io/2017/07/29/sokol-gfx-tour.html) blog post, written Andre Weissflog, the owner of `sokol`. 

## Developers: Building from Source

### Prerequisites

1. Download and install [.NET 5](https://dotnet.microsoft.com/download).
2. If you are on Windows: [Install Windows Subsystem for Linux (WSL)](https://docs.microsoft.com/en-us/windows/wsl/install-win10).
3. Optional: If you are on Windows, [install Windows terminal](https://docs.microsoft.com/en-us/windows/terminal/get-started).
4. Clone the repository with submodules: `git clone --recurse-submodules git@github.com:lithiumtoast/sokol-cs.git`.
5. Run `bash ./get-libs.sh` from the root directory of the repository.
6. If using IDE (Visual Studio / Rider): Open `./src/dotnet/Sokol.sln` and build solution.
7. If using CLI: `dotnet build ./src/dotnet/Sokol.sln`

## Developers: Documentation

[P/Invoke](https://docs.microsoft.com/en-us/dotnet/framework/interop/consuming-unmanaged-dll-functions) is used to call the C functions. All parameters of the functions, including structs, in C# are blittable which means that they have the [same memory layout in C](https://docs.microsoft.com/en-us/dotnet/framework/interop/blittable-and-non-blittable-types). This allows data to be passed between .NET (managed context) and C/C++/ObjC (unmanaged context) and vice-versa [as is for best performance](https://docs.microsoft.com/en-us/dotnet/framework/interop/copying-and-pinning#formatted-blittable-classes). Each graphics back-end for `sokol_gfx` and `sokol_app` is a seperate native library. The [advice here](https://github.com/floooh/sokol/issues/338#issuecomment-660881902) is to simply use D3D11 on Windows, Metal on macOS, and OpenGL on Linux. This advice simplifies the native library so that there is simply one for each platform: `sokol.dll` on Windows, `libsokol.dylib` on macOS, and `libsokol.so` on Linux.

### C style

The C style APIs are a pure port of the C headers; they exactly match what is in C, and the naming conventions used in C are maintained. To learn how to use the C APIs, check out the [official C samples](https://github.com/floooh/sokol-samples). You can also find the same examples that run in [your browser](https://floooh.github.io/sokol-html5/index.html). The comments in the [`sokol_gfx.h`](https://github.com/floooh/sokol/blob/master/sokol_gfx.h), [`sokol_app.h`](https://github.com/floooh/sokol/blob/master/sokol_app.h), etc, are also a good reference for documentation.

### C# style

The C# style API is a modification of the C bindings to be more idiomatic and overall easier to use. The `unsafe` keyword is not required. However, all the types are still [.NET value types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types). This is on purpose to avoid the garbage collector (GC) [in games](https://www.shawnhargreaves.com/blog/twin-paths-to-garbage-collector-nirvana.html) and [other demanding, high performance, applications](https://docs.microsoft.com/en-us/dotnet/csharp/write-safe-efficient-code). All the code has XML documentation comments which are drived from the comments in [`sokol_gfx.h`](https://github.com/floooh/sokol/blob/master/sokol_gfx.h), [`sokol_app.h`](https://github.com/floooh/sokol/blob/master/sokol_app.h), [Apple developer docs on Metal](https://developer.apple.com/documentation), [Microsoft developer docs on DirectX](https://docs.microsoft.com/en-ca/), [Khronos docs on OpenGL & OpenGL ES](https://www.khronos.org/registry/OpenGL-Refpages/).

### Samples

The samples can be found at: https://github.com/lithiumtoast/learn-graphics.

## Supported Platforms & 3D APIs

Since `sokol_gfx`, `sokol_app`, etc, are C libraries technically any platform is possible. However, currently only desktop platforms (Windows, macOS, and Linux) are currently supported by using .NET 5. [Unification of .NET and Mono for mobile platforms (iOS, Android), browser platform (WebAssembly), and consoles has been pushed back to .NET 6 (Novemeber 2021) due to COVID-19](https://visualstudiomagazine.com/articles/2020/11/10/net-5-ga.aspx).

[`sokol_gfx`](https://github.com/floooh/sokol#sokol_gfxh) converges old and modern graphics APIs to one simple and easy to use API. To learn more about the convergence of modern 3D graphics APIs (such as Metal, DirectX11/12, and WebGPU) and how they compare to legacy APIs (such as OpenGL), see *[A Comparison of Modern Graphics APIs](https://alain.xyz/blog/comparison-of-modern-graphics-apis)* blog written by Alain Galvan, a graphics software engineer.

The following is a table of platforms that are known to work and their supported graphics API backends with `sokol_gfx` in C.

Platform vs 3D API|OpenGL|GLES/WebGL|Direct3D11|Direct3D12|Metal|Vulkan|WebGPU
:---|:---:|:---:|:---:|:---:|:---:|:---:|:---:
Desktop Windows|:white_check_mark:|:x:|:white_check_mark:|:o:|:x:|:o:|:x:
Desktop macOS|:exclamation:|:x:|:x:|:x:|:white_check_mark:|:o:|:x:
Desktop Linux|:white_check_mark:|:x:|:x:|:x:|:x:|:o:|:x:
Mobile iOS|:x:|:x:|:x:|:x:|:white_check_mark:|:o:|:x:
Mobile Android|:x:|:white_check_mark:|:x:|:x:|:x:|:o:|:x:
Browser WebAssembly|:x:|:white_check_mark:|:x:|:x:|:x:|:x:|:white_check_mark:
Micro-console tvOS|:x:|:x:|:x:|:x:|:white_check_mark:|:o:|:x:
Console Nintendo Switch|:white_check_mark:|:x:|:x:|:x:|:x:|:o:|:x:
Console Xbox One|:x:|:x:|:white_check_mark:|:o:|:x:|:x:|:x:
Console Xbox Series X|:x:|:x:|:white_check_mark:|:o:|:x:|:x:|:x:
Console PlayStation 4|:white_check_mark:|:x:|:x:|:x:|:x:|:o:|:x:
Console PlayStation 5|:white_check_mark:|:x:|:x:|:x:|:x:|:o:|:x:

- :o: means the graphics API is supported on the platform but not by `sokol_gfx`.
- :exclamation: means the graphics API is deprecated on that platform but can still work with `sokol_gfx`. OpenGL is deprecated for macOS. It is recommended to only use Metal or [Vulkan](https://github.com/KhronosGroup/MoltenVK) for macOS if hardware supports it. All Apple platforms support Metal.

## NuGet Packages

NuGet packages are not supported. Recommended to fork or use Git submodules instead.

## Versioning

`sokol-cs` uses [calendar versioning](https://calver.org) and [semantic versioning](https://semver.org) where appropriate. For example, the version scheme used for native shared libraries such as `sokol_gfx` is `YYYY.MM.DD` and the version scheme for C# projects is `MAJOR.MINOR.PATCH-TAG`.

### Releases

When a version of the repository is suitable for a release a Git tag is created. For a complete list of the release versions, see the [tags on this repository](https://github.com/lithiumtoast/Sokol.NET/tags).

## License

`sokol-cs` is licensed under the MIT License - see the [LICENSE file](LICENSE) for details.
