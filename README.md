# sokol-cs

**Discontinued. If you wish to create your own bindings, message me.**

Automatically updated C# bindings for sokol https://github.com/floooh/sokol with native dynamic link libraries.

To learn more about `sokol` and it's philosophy, see the [*A Tour of `sokol_gfx.h`*](https://floooh.github.io/2017/07/29/sokol-gfx-tour.html) blog post, written Andre Weissflog, the owner of `sokol`. 

## How to use

1. Download and install [.NET 7](https://dotnet.microsoft.com/download).
2. Fork the repository using GitHub or clone the repository manually with submodules: `git clone --recurse-submodules https://github.com/bottlenoselabs/sokol-cs`.
3. Build the native library by running `library.sh`. To execute `.sh` scripts on Windows, use Git Bash which can be installed with Git itself: https://git-scm.com/download/win. The `library.sh` script requires that CMake is installed and in your path.
4. To setup everything you need: Either (1), add the `src/cs/production/Sokol/Sokol.csproj` C# project to your solution as an existing project and reference it within your own solution, or (2) import the MSBuild `sokol.props` file which is located in the root of this directory to your `.csproj` file. See the [Sokol.csproj](src/cs/production/Sokol/Sokol.csproj) file for how to import the `sokol.props` directly.
```xml
<!-- sokol: bindings + native library -->
<Import Project="$([System.IO.Path]::GetFullPath('path/to/sokol.props'))" />
```

## Developers: Documentation

### Run: "Hello, world!" of computer graphics

The most basic example of rendering a triangle in clip space using your GPU.

![Triangle](docs/images/1-triangle.png)

Build + run: `dotnet run --project ./src/cs/samples/Triangle/Triangle.csproj`

## Developers: Documentation

For more information on how C# bindings work, see [`C2CS`](https://github.com/lithiumtoast/c2cs), the tool that generates the bindings for `sokol` and other C libraries.

To learn how to use `sokol`, check out the [official C samples](https://github.com/floooh/sokol-samples). You can also find the same examples that run in [your browser](https://floooh.github.io/sokol-html5/index.html). The comments in the [`sokol_gfx.h`](https://github.com/floooh/sokol/blob/master/sokol_gfx.h), [`sokol_app.h`](https://github.com/floooh/sokol/blob/master/sokol_app.h), etc, are also a good reference for documentation.

## 3D Graphics APIs

[`sokol_gfx`](https://github.com/floooh/sokol#sokol_gfxh) converges old and modern graphics APIs to one simple and easy to use API. To learn more about the convergence of modern 3D graphics APIs (such as Metal, DirectX11/12, and WebGPU) and how they compare to legacy APIs (such as OpenGL), see *[A Comparison of Modern Graphics APIs](https://alain.xyz/blog/comparison-of-modern-graphics-apis)* blog written by Alain Galvan, a graphics software engineer.

## License

`sokol-cs` is licensed under the MIT license (`MIT`) - see the [LICENSE file](LICENSE) for details.

`sokol` is licensed under the ZLib license (`zlib`) - see https://github.com/floooh/sokol/blob/master/LICENSE for more details.
