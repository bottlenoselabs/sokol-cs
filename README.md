# sokol-cs

Automatically updated C# bindings for sokol https://github.com/floooh/sokol with native dynamic link libraries.

To learn more about `sokol` and it's philosophy, see the [*A Tour of `sokol_gfx.h`*](https://floooh.github.io/2017/07/29/sokol-gfx-tour.html) blog post, written Andre Weissflog, the owner of `sokol`. 

## How to use

### NuGet package

1. Create a new `nuget.config` file if you don't have one already. Put it beside your `.sln`. If you don't have a `.sln` put it beside your `.csproj`.

```bash
dotnet new nuget
```

2. Add the following NuGet package source to the file.

```xml
<add key="lithiumtoast" value="https://www.myget.org/F/lithiumtoast/api/v3/index.json" />
```

3. Install the package named `sokol-cs`. The version is a date and then the commit number on the repository, e.g. (YYYY.MM.DD.alpha0123). If you want to always use the latest version change your `.csproj` to use "*-*" as the version. E.g.:

```xml
<ItemGroup>
    <PackageReference Include="sokol-cs" Version="*-*" />
</ItemGroup>
```

Runtime identifiers currently made available for NuGet packages for this project include: `win-x64`, `osx-x64`, `linux-x64`. If you need an other runtime identifier create an issue and I'll set it up. For more information on runtime identifiers see the [RID catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#using-rids). 

## Building from Source

1. Download and install [.NET 5](https://dotnet.microsoft.com/download).
2. Clone the repository with submodules: `git clone --recurse-submodules git@github.com:lithiumtoast/sokol-cs.git`.
3. Build the native library by running `./library.sh` on macOS or Linux and `.\library.sh` on Windows.
3. If using IDE (Visual Studio / Rider): Open `Sokol.sln` and build solution.
4. If using CLI: `dotnet build`

Additionally, if at any time you wish to re-generate the bindings, simple run `bash ./bindgen.sh`. Though if you are on Windows, you will need `bash` which can be obtained by installing Windows Subsystem for Linux with Ubuntu.

## Run: "Hello, world!" of computer graphics

The most basic example of rendering a triangle in clip space using your GPU.

![Triangle](docs/images/1-triangle.png)

### IDE (Visual Studio / Rider)

Run `Sokol.Samples.Triangle`.

### CLI

#### Windows

Build + Run: `dotnet run --project .\src\cs\samples\Sokol.Samples.Triangle\Sokol.Samples.Triangle.csproj`

#### macOS/Linux

Build + Run: `dotnet run --project ./src/cs/samples/Sokol.Samples.Triangle/Sokol.Samples/Triangle.csproj`

## Run: Native Ahead of Time Compilation with C#

Example of the previous triangle in clip space with native ahead of time compilation (nAOT). Produces a single file executable that is ~1 megabyte. For more information on nAOT see: https://github.com/dotnet/runtimelab/tree/feature/NativeAOT. The property `AheadOfTimeCompilation` is a custom property from my custom MSBuild properties/targets project called [`my-msbuild`](https://github.com/lithiumtoast/my-msbuild) to make native ahead of time compilation just work with minimal effort.

### Windows

1. Build: `dotnet publish .\src\cs\samples\Sokol.Samples.Triangle\Sokol.Samples.Triangle.csproj -r win-x64 -c Release -o .\publish\triangle /p:AheadOfTimeCompilation=true`
2. Run: `.\publish\triangle\Sokol.Samples.Triangle.exe`

### macOS

//TODO

### Ubuntu

//TODO

## Developers: Documentation

For more information on how C# bindings work, see [`C2CS`](https://github.com/lithiumtoast/c2cs), the tool that generates the bindings for `sokol` and other C libraries.

To learn how to use `sokol`, check out the [official C samples](https://github.com/floooh/sokol-samples). You can also find the same examples that run in [your browser](https://floooh.github.io/sokol-html5/index.html). The comments in the [`sokol_gfx.h`](https://github.com/floooh/sokol/blob/master/sokol_gfx.h), [`sokol_app.h`](https://github.com/floooh/sokol/blob/master/sokol_app.h), etc, are also a good reference for documentation.

## Supported Platforms & 3D Graphics APIs

Since `sokol_gfx`, `sokol_app`, etc, are C libraries technically any platform is possible. For a list of supported platforms for .NET 5 see the [RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog).

[`sokol_gfx`](https://github.com/floooh/sokol#sokol_gfxh) converges old and modern graphics APIs to one simple and easy to use API. To learn more about the convergence of modern 3D graphics APIs (such as Metal, DirectX11/12, and WebGPU) and how they compare to legacy APIs (such as OpenGL), see *[A Comparison of Modern Graphics APIs](https://alain.xyz/blog/comparison-of-modern-graphics-apis)* blog written by Alain Galvan, a graphics software engineer.

## NuGet Packages

NuGet packages are not supported and will not be supported. Recommended to fork or use Git submodules instead.

## License

`sokol-cs` is licensed under the MIT license (`MIT`) - see the [LICENSE file](LICENSE) for details.

`sokol` is licensed under the ZLib license (`zlib`) - see https://github.com/floooh/sokol/blob/master/LICENSE for more details.
