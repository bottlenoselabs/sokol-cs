# sokol-cs

C# bindings for https://github.com/floooh/sokol.

To learn more about `sokol` and it's philosophy, see the [*A Tour of `sokol_gfx.h`*](https://floooh.github.io/2017/07/29/sokol-gfx-tour.html) blog post, written Andre Weissflog, the owner of `sokol`. 

## Developers: Building from Source

1. Download and install [.NET 5](https://dotnet.microsoft.com/download).
2. If you are on Windows: [Install Windows Subsystem for Linux v2 (WSL2)](https://docs.microsoft.com/en-us/windows/wsl/install-win10).
3. Clone the repository with submodules: `git clone --recurse-submodules git@github.com:lithiumtoast/sokol-cs.git`.
4. Run `bash ./build-native-library.sh` from the root directory of the repository to build the native shared library of `sokol`.
5. If using IDE (Visual Studio / Rider): Open `./src/dotnet/Sokol.sln` and build solution.
6. If using CLI: `dotnet build ./src/dotnet/Sokol.sln`
7. Check if everything is working correctly by running the "Hello, world!" of computer graphics:
   - IDE: Run `Sokol.Samples.Triangle`.
   - CLI: `dotnet run --project ./src/cs/samples/Sokol.Samples.Triangle/Sokol.Samples/Triangle.csproj`

Additionally, if at any time you wish to re-generate the bindings, simple run `bash ./bindgen-csharp.sh`.

## Developers: Documentation

For more information on how C# bindings work, see [`C2CS`](https://github.com/lithiumtoast/c2cs), the tool that generates the bindings for `sokol` and other C libraries.

To learn how to use `sokol`, check out the [official C samples](https://github.com/floooh/sokol-samples). You can also find the same examples that run in [your browser](https://floooh.github.io/sokol-html5/index.html). The comments in the [`sokol_gfx.h`](https://github.com/floooh/sokol/blob/master/sokol_gfx.h), [`sokol_app.h`](https://github.com/floooh/sokol/blob/master/sokol_app.h), etc, are also a good reference for documentation.

## Supported Platforms & 3D Graphics APIs

Since `sokol_gfx`, `sokol_app`, etc, are C libraries technically any platform is possible. For a list of supported platforms for .NET 5 see the [RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog).

[`sokol_gfx`](https://github.com/floooh/sokol#sokol_gfxh) converges old and modern graphics APIs to one simple and easy to use API. To learn more about the convergence of modern 3D graphics APIs (such as Metal, DirectX11/12, and WebGPU) and how they compare to legacy APIs (such as OpenGL), see *[A Comparison of Modern Graphics APIs](https://alain.xyz/blog/comparison-of-modern-graphics-apis)* blog written by Alain Galvan, a graphics software engineer.

## NuGet Packages

NuGet packages are not supported and will not be supported. Recommended to fork or use Git submodules instead.

## License

`sokol-cs` is licensed under the MIT License - see the [LICENSE file](LICENSE) for details.
