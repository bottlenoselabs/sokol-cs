> :wrench: 2020/12/22: Changed repository name to `sokol-cs`; updating to use libclang tool to generate C# bindings automatically; more news soon.

<p align="center">
  <b>Sokol.NET</b> - .NET wrapper for <a href="https://github.com/floooh/sokol">https://github.com/floooh/sokol</a>
</p>

<p align="center">
    <img src="https://github.com/lithiumtoast/Sokol.NET/workflows/CI/CD/badge.svg"/>
    <a href="https://app.fossa.com/projects/git%2Bgithub.com%2Flithiumtoast%2FSokol.NET?ref=badge_shield" alt="FOSSA Status"><img src="https://app.fossa.com/api/projects/git%2Bgithub.com%2Flithiumtoast%2FSokol.NET.svg?type=shield"/></a>
</p>

## Background

A .NET wrapper for https://github.com/floooh/sokol. Includes the C style API precisely as it is and a .NET style API for convenience.

Name|Description
:---:|:---:
[`sokol_gfx`](https://github.com/floooh/sokol#sokol_gfxh)|A simple and modern wrapper around GLES2/WebGL, GLES3/WebGL2, GL3.3, D3D11 and Metal.
[`sokol_app`](https://github.com/floooh/sokol#sokol_apph)|A minimal cross-platform application-wrapper library.

To learn more about `sokol` and it's philosophy, see the [*A Tour of `sokol_gfx.h`*](https://floooh.github.io/2017/07/29/sokol-gfx-tour.html) blog post, written Andre Weissflog, the owner of `sokol`. 

## C APIs

The C APIs [P/Invoke](https://docs.microsoft.com/en-us/dotnet/framework/interop/consuming-unmanaged-dll-functions) bindings are a pure port of the C headers; they exactly match what is in C, and the naming conventions used in C are maintained.

To learn more about the C APIs, including how to get started, examples, and documentation, see the [README-C-CPI.md](README-C-API.md).

## .NET APIs

The .NET style API is a modification of the C bindings (from the side of .NET) to be more idiomatic and overall easier to use. The `unsafe` keyword is not required.

To learn more about the .NET APIs, including how to get started, examples, and documentation, see the [README-DOTNET-CPI.md](README-DOTNET-API.md).

## Supported Platforms & 3D APIs

Since `sokol_gfx` is a C library technically any platform is possible. However, currently only desktop platforms (Windows, macOS, and Linux) are supported with `Sokol.NET` by using .NET Core 3.1. In November 2020, `Sokol.NET` will move to .NET 5 and support mobile (iOS, Android), browser (WebAssembly), consoles (Nintendo Switch, Xbox One, PlayStation 4), and micro-consoles (tvOS). See [.NET 5 annoucement as the next .NET Core that will unify desktop, mobile, browser, consoles, and other platforms](https://devblogs.microsoft.com/dotnet/introducing-net-5/).

[`sokol_gfx`](https://github.com/floooh/sokol#sokol_gfxh) converges old and modern graphics APIs to one simple and easy to use API. To learn more about the convergence of modern 3D graphics APIs (such as Metal, DirectX11/12, and WebGPU) and how they compare to legacy APIs (such as OpenGL), see *[A Comparison of Modern Graphics APIs](https://alain.xyz/blog/comparison-of-modern-graphics-apis)* blog written by Alain Galvan, a graphics software engineer.

The following is a table of platforms that are known to work and their supported graphics API backends with `sokol_gfx` in C.

Platform vs 3D API|OpenGL|OpenGLES/WebGL|Direct3D11|Direct3D12|Metal|Vulkan|WebGPU
:---|:---:|:---:|:---:|:---:|:---:|:---:|:---:
Desktop Windows|:white_check_mark:|:x:|:white_check_mark:|:o:|:x:|:o:|:x:
Desktop macOS|:exclamation:|:x:|:x:|:x:|:white_check_mark:|:question:|:x:
Desktop Linux|:white_check_mark:|:x:|:x:|:x:|:x:|:o:|:x:
Mobile iOS|:x:|:x:|:x:|:x:|:white_check_mark:|:question:|:x:
Mobile Android|:x:|:white_check_mark:|:x:|:x:|:x:|:o:|:x:
Browser WebAssembly|:x:|:white_check_mark:|:x:|:x:|:x:|:x:|:construction:
Micro-console tvOS|:x:|:x:|:x:|:x:|:white_check_mark:|:question:|:x:
Console Nintendo Switch|:white_check_mark:|:x:|:x:|:x:|:x:|:o:|:x:
Console Xbox One|:x:|:x:|:white_check_mark:|:o:|:x:|:x:|:x:
Console PlayStation 4|:white_check_mark:|:x:|:x:|:x:|:x:|:o:|:x:

- :o: means the graphics API is supported on the platform but not by `sokol_gfx`.
- :construction: means the graphics API will be supported by `sokol_gfx` but is currently under construction (from `sokol` side).
- :exclamation: means the graphics API is deprecated on that platform but can still work with `sokol_gfx`. OpenGL is deprecated for macOS. It is recommended to only use Metal for macOS if hardware supports it. All Apple platforms support Metal.
- :question: means the graphics API is unofficially supported. [Vulkan has limited support on macOS and iOS.](https://github.com/KhronosGroup/MoltenVK) [Vulkan is not yet supported on tvOS](https://github.com/KhronosGroup/MoltenVK/issues/541).

## GitHub Projects & NuGet Packages

The following is the list of NuGet packages available by the [GitHub projects](https://github.com/lithiumtoast/Sokol.NET/projects) for `Sokol.NET`. Each NuGet package is an individual product. Each GitHub project is a complete suite of products that are commonly used together or otherwise are related in some fashion. NuGet packages which have the name "lib*" are only used to package native libraries and don't have any code.

To get the NuGet packages, add the following feed: `https://www.myget.org/F/lithiumtoast/api/v3/index.json`

### [Sokol.Graphics](https://github.com/lithiumtoast/Sokol.NET/projects/2)

- [`sokol_gfx`](https://www.myget.org/feed/lithiumtoast/package/nuget/sokol_gfx)
- [`Sokol.Graphics`](https://www.myget.org/feed/lithiumtoast/package/nuget/Sokol.Graphics)
- [`libsokol_gfx`](https://www.myget.org/feed/lithiumtoast/package/nuget/libsokol_gfx)

### [Sokol.App](https://github.com/lithiumtoast/Sokol.NET/projects/1)

- [`sokol_app`](https://www.myget.org/feed/lithiumtoast/package/nuget/sokol_app)
- [`Sokol.App`](https://www.myget.org/feed/lithiumtoast/package/nuget/Sokol.App)
- [`libsokol_app`](https://www.myget.org/feed/lithiumtoast/package/nuget/libsokol_app)

## Versioning

`Sokol.NET` uses [calendar versioning](https://calver.org) and [semantic versioning](https://semver.org) where appropriate. For example, the version scheme used for native shared libraries such as `sokol_gfx` is `YYYY.MM.DD` and the version scheme for `Sokol.NET` is `MAJOR.MINOR.PATCH-TAG`.

### Semantic Versioning

`Sokol.NET` uses [`GitVersion`](https://github.com/GitTools/GitVersion) to determine the exact semantic version for each build in Continuous Integration (CI) and Continuous Deployment (CD). 

How `GitVersion` is configured for `Sokol.NET`, the version is automatically bumped by `+0.0.1` after each pull-request. Also, tags are considered releases; when a new tag is created, the version is automatically bumped automatically to the specified tag version.

For a complete list of the release versions, see the [tags on this repository](https://github.com/lithiumtoast/Sokol.NET/tags).

## Support

For details about how to get support for `Sokol.NET`, see the [SUPPORT](.github/SUPPORT.md) file.

At least one maintainer will respond to an issue or comment in under 48 hours under normal circumstances. See the [list of MAINTAINERS](https://github.com/lithiumtoast/Sokol.NET/blob/develop/MAINTAINERS.md) for details about time-zones, countries of living, and circumstances of maintainers.

## Contributing

Do you want to contribute? Awesome! To get started please read the [CONTRIBUTING](.github/CONTRIBUTING.md) file for details on our code of conduct, the process for creating issues, and submitting pull requests.

For a list of contributors, see the [CONTRIBUTORS](CONTRIBUTORS.md) file.

## License

`Sokol.NET` is licensed under the MIT License - see the [LICENSE file](LICENSE) for details.

For information about 3rd party licenses, see [the latest complete open source license report](https://app.fossa.com/reports/f01b41f3-1a78-4d1d-89b3-9a54b12733e2) provided by [FOSSA](https://fossa.com).

[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Flithiumtoast%2FSokol.NET.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2Flithiumtoast%2FSokol.NET)
