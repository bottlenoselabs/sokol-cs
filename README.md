# Sokol\#

A C# wrapper for https://github.com/floooh/sokol.

Includes "unsafe" [Platform Invoke (P/Invoke)](https://docs.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke) API bindings exactly as they are in C and safe .NET API over the C API for convenience.

## News

- 2019/10/23: "Unsafe" API fairly well finished.
- 2019/10/15: Initial project creation.

## "Unsafe" API

The [P/Invoke](https://docs.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke) bindings are a pure port of the C headers; they exactly match what is found in C and the naming conventions used in C are maintained. To use the C API in .NET, the `unsafe` keyword will most often be required for using the C structs and calling the C functions.

For examples on how to use the C API, check out the [official C samples](https://github.com/floooh/sokol-samples). You can also find the same examples that run in your browser [here](https://floooh.github.io/sokol-html5/index.html).

## "Safe" API

The .NET API is just wrappers over the C API for convenience and ease of use. The `unsafe` keyword is not required. All the "safe" classes/structs have the prefix `Sg`. E.g. `SgBuffer`, `SgShader`, etc. The safe API uses .NET Core v3 and makes use of `System.Numerics` for `Vector2`, `Vector3`, `Matrix4x4`, etc and of `System.Memory` for `Span<T>`, `Memory<T>`, etc. By using these, the code required for the safe API remains small, highly performant, and easy to use without re-inventing the wheel.

For examples on how to use the .NET API, check out the [.NET Core v3 samples](src/Samples) which are in sync with the the official [C samples](https://github.com/floooh/sokol-samples).

## Supported Platforms

Since Sokol is a C library, technically any platform is possible. The following is a table of platforms which are known to work and their supported graphics API backends with `sokol`. If you find that anything is incorrect I would be more than happy to discuss and change the table in a existing or new [issue](https://github.com/lithiumtoast/sokol-csharp/issues).

Platform|OpenGL 3.x|OpenGLES2/WebGL|OpenGLES3/WebGL2|Direct3D11|Direct3D12|Metal|Vulkan|.NET Support
---|---|---|---|---|---|---|---|---
Desktop Windows|✅|❌|❌|✅|❓|❌|❓|✅
Desktop macOS|❗|❌|❌|❌|❌|✅|❓|✅
Desktop Linux|✅|❌|❌|❌|❌|❌|❓|✅
Mobile iOS|❌|❌|❌|❌|❌|✅|❓|✅
Mobile Android|❌|✅|✅|❌|❌|❌|❓|✅
Browser WebAssembly|❌|✅|✅|❌|❌|❌|❓|❗
Smartwatch watchOS|❌|❌|❌|❌|❌|✅|❓|✅
Microconsole tvOS|❌|❌|❌|❌|❌|✅|❓|✅
Console Nintendo Switch|✅|❌|❌|❌|❌|❌|❓|❗
Console Xbox One|❌|❌|❌|✅|❓|❌|❓|❗
Console PlayStation 4|❓|❌|❌|❌|❌|❌|❓|❗

- OpenGL is deprecated for macOS; recommended to only use Metal for macOS if hardware supports it. All Apple platforms support Metal and are supported with .NET using [Xamarin](https://dotnet.microsoft.com/apps/xamarin).
- Android is supported with .NET using [Xamarin](https://dotnet.microsoft.com/apps/xamarin).
- As of Q4 2019, WebAssembly is made possible with .NET using [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor). I have not tried to get Sokol working with Blazor yet.
- As of Q4 2019, consoles are made possible with .NET using [BRUTE](http://brute.rocks). However, the tool is not yet released to the general public. Also, SDK licenses are required for each console.

## Contributing

You want to contribute? Awesome! To get started please read the [CONTRIBUTING](CONTRIBUTING.md) file for details on our code of conduct, and the process for submitting pull requests.

## Versioning

While [semantic versioning](https://semver.org) is industry standard, especially for NuGet packages, [calendar versioning](https://calver.org) and [semantic versioning](https://semver.org) is used for `Sokol#`. Calendar versioning is used when necessary because most of the changes are driven by external forces which are time sensitive such as updates to `sokol` itself, changes in graphics API technology, etc.

The exact version scheme used for dynamic link libraries such as `sokol_gfx` is `YYYY.MM.DD` and the version scheme for `Sokol#` is `MAJOR.MINOR.PATCH`. This means that major versions are for new features which require breaking API changes; minor versions are for new features which are backwards-compatible; and patch versions are for backwards-compatible bug fixes. For a complete list of the versions available,see the [tags on this repository](https://github.com/lithiumtoast/sokol-csharp/tags).

## License

`Sokol#` is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Authors

- **Lucas Girouard-Stranks** [@lithiumtoast](https://github.com/lithiumtoast) *Owner*
