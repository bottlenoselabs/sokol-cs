// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using SDL2;
using Sokol.Graphics;

internal static class NativeLibraries
{
    private static readonly Dictionary<string, List<string>> LibraryPathsByLibraryName =
        new Dictionary<string, List<string>>();

    private static long _isPreLoading;

    public static (GraphicsPlatform, GraphicsBackend) Load(GraphicsBackend? requestedBackend)
    {
        var resolver = StartPreLoading();

        var platform = LoadSdl2(resolver);
        var backend = LoadSokolGfx(resolver, platform, requestedBackend);

        EndPreLoading();
        return (platform, backend);
    }

    private static GraphicsPlatform LoadSdl2(DllImportResolver resolver)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            AddLibraryPath("SDL2", "runtimes/win-x64/native/SDL2.dll");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            AddLibraryPath("SDL2", "runtimes/osx-x64/native/libSDL2.dylib");
            AddLibraryPath("SDL2", "/Library/Frameworks/SDL2.framework/SDL2");
            AddLibraryPath("SDL2", "/usr/local/lib/libSDL2.dylib");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            AddLibraryPath("SDL2", "runtimes/linux-x64/native/libSDL2.so");
        }

        var exportsToIgnore = Sdl2ExportsToIgnore();

        NativeLibrary.SetDllImportResolver(typeof(SDL).Assembly, resolver);
        PreLoadDllImports(typeof(SDL), exportsToIgnore.ToArray());

        // SDL2 platforms: https://github.com/spurious/SDL-mirror/blob/6b6170caf69b4189c9a9d14fca96e97f09bbcc41/src/SDL.c#L459
        var platformString = SDL.SDL_GetPlatform();
        var platform = platformString switch
        {
            "Windows" => GraphicsPlatform.Windows,
            "Mac OS X" => GraphicsPlatform.macOS,
            "Linux" => GraphicsPlatform.Linux,
            _ => GraphicsPlatform.Unknown
        };
        return platform;
    }

    private static List<string> Sdl2ExportsToIgnore()
    {
        var exportsToIgnore = new List<string>();
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            exportsToIgnore.Add("SDL_SetWindowsMessageHook");
        }

        // Hardware specific
        exportsToIgnore.Add("SDL_IsChromebook");
        // ReSharper disable once StringLiteralTypo
        exportsToIgnore.Add("SDL_HasARMSIMD");

        // Windows RT specific
        exportsToIgnore.Add("SDL_WinRTGetDeviceFamily");
        exportsToIgnore.Add("SDL_WinRTRunApp");

        // Metal specific
        exportsToIgnore.Add("SDL_Metal_CreateView");
        exportsToIgnore.Add("SDL_Metal_DestroyView");

        // iOS specific
        exportsToIgnore.Add("SDL_UIKitRunApp");
        exportsToIgnore.Add("SDL_iPhoneSetAnimationCallback");
        exportsToIgnore.Add("SDL_iPhoneSetEventPump");

        // Android specific
        exportsToIgnore.Add("SDL_AndroidGetJNIEnv");
        exportsToIgnore.Add("SDL_AndroidGetActivity");
        exportsToIgnore.Add("SDL_AndroidBackButton");
        exportsToIgnore.Add("SDL_AndroidGetInternalStoragePath");
        exportsToIgnore.Add("SDL_AndroidGetExternalStorageState");
        exportsToIgnore.Add("SDL_GetAndroidSDKVersion");
        exportsToIgnore.Add("SDL_IsAndroidTV");
        exportsToIgnore.Add("SDL_IsDeXMode");

        // New bindings not in SDL2 v2.0.10
        exportsToIgnore.Add("SDL_GameControllerTypeForIndex");
        exportsToIgnore.Add("SDL_GameControllerGetType");
        exportsToIgnore.Add("SDL_GameControllerFromPlayerIndex");
        exportsToIgnore.Add("SDL_GameControllerSetPlayerIndex");
        exportsToIgnore.Add("SDL_JoystickFromPlayerIndex");
        exportsToIgnore.Add("SDL_JoystickSetPlayerIndex");
        exportsToIgnore.Add("SDL_LockTextureToSurface");
        exportsToIgnore.Add("SDL_SetTextureScaleMode");
        exportsToIgnore.Add("SDL_GetTextureScaleMode");

        return exportsToIgnore;
    }

    private static GraphicsBackend LoadSokolGfx(DllImportResolver resolver, GraphicsPlatform platform, GraphicsBackend? requestedBackend)
    {
        var backend = GetUseableGraphicsBackend(platform, requestedBackend);

        var rid = GetRuntimeIdentifier(platform);
        var extension = GetDynamicLibraryExtension(platform);
        var backendString = backend.ToString().ToLowerInvariant();
        var libraryPrefix = platform == GraphicsPlatform.Windows ? string.Empty : "lib";
        var libraryPath = $"runtimes/{rid}/native/{libraryPrefix}sokol_gfx-{backendString}.{extension}";

        AddLibraryPath("sokol_gfx", libraryPath);

        NativeLibrary.SetDllImportResolver(typeof(PInvoke).Assembly, resolver); // Sokol.Graphics
        NativeLibrary.SetDllImportResolver(typeof(gl).Assembly, resolver); // Sokol.Graphics.OpenGL

        PreLoadDllImports(typeof(PInvoke));
        PreLoadDllImports(typeof(gl));

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            PreLoadDllImports(typeof(glew));
        }

        return backend;
    }

    private static DllImportResolver StartPreLoading()
    {
        EnsureArchitectureIs64Bit();
        DllImportResolver resolver = Resolve;
        Interlocked.Exchange(ref _isPreLoading, 1);
        return resolver;
    }

    private static void EndPreLoading()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        Interlocked.Exchange(ref _isPreLoading, 0);
    }

    [SuppressMessage("ReSharper", "SA1011", Justification = "C# 8")]
    private static void PreLoadDllImports(Type type, params string[]? exportsToIgnore)
    {
        var methods = type.GetRuntimeMethods();
        foreach (var method in methods)
        {
            var dllImportAttribute = method.GetCustomAttribute<DllImportAttribute>();
            if (dllImportAttribute == null)
            {
                continue;
            }

            var libraryName = dllImportAttribute.Value;

            var exportName = dllImportAttribute.EntryPoint ?? method!.Name;
            if (exportsToIgnore?.Contains(exportName) == true)
            {
                continue;
            }

            try
            {
                Marshal.Prelink(method!);
            }
            catch (EntryPointNotFoundException e)
            {
                throw new Exception($"The `{libraryName}` library is out of date.", e);
            }
        }
    }

    private static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        EnsureLibraryIsPreLoaded(libraryName);

        if (NativeLibrary.TryLoad(libraryName, out var handle))
        {
            return handle;
        }

        var libraryPaths = LibraryPathsByLibraryName[libraryName];
        foreach (var libraryPath in libraryPaths)
        {
            if (NativeLibrary.TryLoad(libraryPath, out handle))
            {
                return handle;
            }
        }

        var exceptions = new List<Exception>();
        foreach (var libraryPath in libraryPaths)
        {
            var exception = new FileNotFoundException(null, libraryPath);
            exceptions.Add(exception);
        }

        throw new AggregateException("Could not find the library in the searched paths. Check the inner exception for the searched paths.", exceptions);
    }

    private static void AddLibraryPath(string libraryName, string libraryPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            libraryPath = libraryPath.Replace("/", @"\");
        }

        if (!Path.IsPathFullyQualified(libraryPath))
        {
            libraryPath = Path.Combine(Environment.CurrentDirectory, libraryPath);
        }

        if (!LibraryPathsByLibraryName.TryGetValue(libraryName, out var list))
        {
            list = new List<string>();
            LibraryPathsByLibraryName.Add(libraryName, list);
        }

        list.Add(libraryPath);
    }

    private static void EnsureLibraryIsPreLoaded(string libraryName)
    {
        var isLoading = Interlocked.Read(ref _isPreLoading) == 1;
        if (isLoading)
        {
            return;
        }

        var stackTrace = new StackTrace();
        var exportName = string.Empty;
        for (var i = 0; i < stackTrace.FrameCount; i++)
        {
            var frame = stackTrace.GetFrame(i);
            var method = frame!.GetMethod();
            var dllImportAttribute = method?.GetCustomAttribute<DllImportAttribute>();
            if (dllImportAttribute == null)
            {
                continue;
            }

            exportName = dllImportAttribute.EntryPoint ?? method!.Name;
        }

        throw new Exception(
            $"Attempt to load `{exportName}` export for library `{libraryName}` without early initialization.");
    }

    private static void EnsureArchitectureIs64Bit()
    {
        var runtimeArchitecture = RuntimeInformation.OSArchitecture;
        if (runtimeArchitecture == Architecture.Arm || runtimeArchitecture == Architecture.X86)
        {
            throw new NotSupportedException("32-bit architecture is not supported.");
        }
    }

    private static string GetRuntimeIdentifier(GraphicsPlatform platform)
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

    private static string GetDynamicLibraryExtension(GraphicsPlatform platform)
    {
        return platform switch
        {
            GraphicsPlatform.Windows => "dll",
            GraphicsPlatform.macOS => "dylib",
            GraphicsPlatform.Linux => "so",
            GraphicsPlatform.Unknown => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, null)
        };
    }

    private static GraphicsBackend GetUseableGraphicsBackend(GraphicsPlatform platform, GraphicsBackend? requestedBackend)
    {
        GraphicsBackend backend;
        if (requestedBackend == null)
        {
            backend = platform switch
            {
                GraphicsPlatform.Windows => GraphicsBackend.OpenGL, // TODO: Use Direct3D11
                GraphicsPlatform.macOS => GraphicsBackend.Metal,
                GraphicsPlatform.Linux => GraphicsBackend.OpenGL,
                GraphicsPlatform.Unknown => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, null)
            };
        }
        else
        {
            backend = (GraphicsBackend)requestedBackend;
        }

        return backend;
    }
}
