using System.Collections.Immutable;
using C2CS;
using C2CS.Options;
using JetBrains.Annotations;

namespace Sokol.Bindgen;

[UsedImplicitly]
public class ReaderCCode : IReaderCCode
{
    public ReaderCCodeOptions Options { get; } = new();

    public ReaderCCode()
    {
        Configure(Options);
    }

    private static void Configure(ReaderCCodeOptions options)
    {
        options.InputHeaderFilePath =
            "../src/c/production/sokol/sokol.h";
        options.UserIncludeDirectories = new[] { "../ext/sokol" }.ToImmutableArray();
        options.OutputAbstractSyntaxTreesFileDirectory =
            "./ast";

        ConfigurePlatforms(options);
    }

    private static void ConfigurePlatforms(ReaderCCodeOptions options)
    {
        var platforms = new Dictionary<TargetPlatform, ReaderCCodeOptionsPlatform>();

        var hostOperatingSystem = Native.OperatingSystem;
        switch (hostOperatingSystem)
        {
            case NativeOperatingSystem.Windows:
                ConfigureHostOsWindows(options, platforms);
                break;
            case NativeOperatingSystem.macOS:
                ConfigureHostOsMac(options, platforms);
                break;
            case NativeOperatingSystem.Linux:
                ConfigureHostOsLinux(options, platforms);
                break;
            default:
                throw new NotImplementedException();
        }

        options.Platforms = platforms.ToImmutableDictionary();
    }

    private static void ConfigureHostOsWindows(ReaderCCodeOptions options,
        Dictionary<TargetPlatform, ReaderCCodeOptionsPlatform> platforms)
    {
        platforms.Add(TargetPlatform.aarch64_pc_windows_msvc, new ReaderCCodeOptionsPlatform());
        platforms.Add(TargetPlatform.x86_64_pc_windows_msvc, new ReaderCCodeOptionsPlatform());
    }

    private static void ConfigureHostOsMac(ReaderCCodeOptions options,
        Dictionary<TargetPlatform, ReaderCCodeOptionsPlatform> platforms)
    {
        platforms.Add(TargetPlatform.aarch64_apple_darwin, new ReaderCCodeOptionsPlatform());
        platforms.Add(TargetPlatform.x86_64_apple_darwin, new ReaderCCodeOptionsPlatform());
    }
    
    private static void ConfigureHostOsLinux(ReaderCCodeOptions options,
        Dictionary<TargetPlatform, ReaderCCodeOptionsPlatform> platforms)
    {
        platforms.Add(TargetPlatform.aarch64_unknown_linux_gnu, new ReaderCCodeOptionsPlatform());
        platforms.Add(TargetPlatform.x86_64_unknown_linux_gnu, new ReaderCCodeOptionsPlatform());
    }
}