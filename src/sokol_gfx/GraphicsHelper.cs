// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/// <summary>
///     Utility class for helper functions related to a `sokol_gfx` application.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
internal static class GraphicsHelper
{
    public static GraphicsPlatform Platform
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GraphicsPlatform.Windows;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return GraphicsPlatform.macOS;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return GraphicsPlatform.Linux;
            }

            return GraphicsPlatform.Unknown;
        }
    }

    public static GraphicsBackend DefaultBackend(GraphicsPlatform? platform = null)
    {
        var platform1 = platform ?? Platform;
        return platform1 switch
        {
            GraphicsPlatform.Windows => GraphicsBackend.Direct3D11,
            GraphicsPlatform.macOS => GraphicsBackend.Metal,
            GraphicsPlatform.Linux => GraphicsBackend.OpenGL,
            GraphicsPlatform.Unknown => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, null)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIs64BitArchitecture()
    {
        var runtimeArchitecture = RuntimeInformation.OSArchitecture;
        if (runtimeArchitecture == Architecture.Arm || runtimeArchitecture == Architecture.X86)
        {
            throw new NotSupportedException("32-bit architecture is not supported.");
        }
    }

    public static string GetRuntimeIdentifier(GraphicsPlatform platform)
    {
        return platform switch
        {
            GraphicsPlatform.Windows => "win-x64",
            GraphicsPlatform.macOS => "osx-x64",
            GraphicsPlatform.Linux => "linux-x64",
            GraphicsPlatform.Unknown => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, null)
        };
    }

    public static string GetDynamicLibraryExtension(GraphicsPlatform platform)
    {
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return platform switch
        {
            GraphicsPlatform.Windows => "dll",
            GraphicsPlatform.macOS => "dylib",
            GraphicsPlatform.Linux => "so",
            GraphicsPlatform.Unknown => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, null)
        };
    }

    public static string GetBackendString(GraphicsBackend backend)
    {
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return backend switch
        {
            GraphicsBackend.OpenGL => "opengl",
            GraphicsBackend.Metal => "metal",
            GraphicsBackend.Direct3D11 => "d3d11",
            GraphicsBackend.Dummy => "dummy",
            _ => throw new ArgumentOutOfRangeException(nameof(backend), backend, null)
        };
    }

    public static string GetLibraryPath(GraphicsBackend backend, string libraryName)
    {
        var platform = Platform;
        var libraryPrefix = platform == GraphicsPlatform.Windows ? string.Empty : "lib";
        var backendString = GetBackendString(backend);
        var extension = GetDynamicLibraryExtension(platform);
        var libraryFileName = $"{libraryPrefix}{libraryName}-{backendString}.{extension}";

        var workingDirectoryLibraryPath = Path.Combine(Environment.CurrentDirectory, libraryFileName);
        if (File.Exists(workingDirectoryLibraryPath))
        {
            return workingDirectoryLibraryPath;
        }

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
        var applicationDirectoryLibraryPath = Path.Combine(baseDirectory, libraryName);
        if (File.Exists(applicationDirectoryLibraryPath))
        {
            return applicationDirectoryLibraryPath;
        }

        var rid = GetRuntimeIdentifier(platform);
        var runtimeLibraryPath = Path.GetFullPath($"runtimes/{rid}/native/{libraryPrefix}{libraryName}-{backendString}.{extension}");
        if (File.Exists(runtimeLibraryPath))
        {
            return runtimeLibraryPath;
        }

        throw new Exception($"Could not find the library path for {libraryName} with the given {backend} back-end.");
    }
}
