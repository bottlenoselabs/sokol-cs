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

        var platform = UseSDL2(resolver);
        var backend = UseSokolGfx(resolver, platform, requestedBackend);
        UseGLEW(platform, backend);

        EndPreLoading();

        return (platform, backend);
    }

    private static GraphicsPlatform UseSDL2(DllImportResolver resolver)
    {
        var exportsToIgnore = new List<string>();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            AddLibraryPath("SDL2", "runtimes/win-x64/native/SDL.dll");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            AddLibraryPath("SDL2", "runtimes/osx-x64/native/libSDL2.dylib");
            AddLibraryPath("SDL2", "/Library/Frameworks/SDL2.framework/SDL2");
            AddLibraryPath("SDL2", "/usr/local/lib/libSDL2.dylib");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            throw new NotImplementedException();
        }

        // exports required to ignore for macOS, needs more testing on other platforms
        exportsToIgnore.Add("SDL_WinRTGetDeviceFamily");
        exportsToIgnore.Add("SDL_SetWindowsMessageHook");
        exportsToIgnore.Add("SDL_iPhoneSetAnimationCallback");
        exportsToIgnore.Add("SDL_iPhoneSetEventPump");
        exportsToIgnore.Add("SDL_AndroidGetJNIEnv");
        exportsToIgnore.Add("SDL_AndroidGetActivity");
        exportsToIgnore.Add("SDL_IsAndroidTV");
        exportsToIgnore.Add("SDL_IsChromebook");
        exportsToIgnore.Add("SDL_IsDeXMode");
        exportsToIgnore.Add("SDL_AndroidBackButton");
        exportsToIgnore.Add("SDL_AndroidGetInternalStoragePath");
        exportsToIgnore.Add("SDL_AndroidGetExternalStorageState");
        exportsToIgnore.Add("SDL_GetAndroidSDKVersion");
        exportsToIgnore.Add("SDL_WinRTRunApp");
        exportsToIgnore.Add("SDL_UIKitRunApp");

        exportsToIgnore.Add("SDL_HasARMSIMD");
        exportsToIgnore.Add("SDL_GameControllerTypeForIndex");
        exportsToIgnore.Add("SDL_GameControllerGetType");
        exportsToIgnore.Add("SDL_GameControllerFromPlayerIndex");
        exportsToIgnore.Add("SDL_GameControllerSetPlayerIndex");
        exportsToIgnore.Add("SDL_JoystickFromPlayerIndex");
        exportsToIgnore.Add("SDL_JoystickSetPlayerIndex");
        exportsToIgnore.Add("SDL_LockTextureToSurface");
        exportsToIgnore.Add("SDL_Metal_CreateView");
        exportsToIgnore.Add("SDL_Metal_DestroyView");
        exportsToIgnore.Add("SDL_SetTextureScaleMode");
        exportsToIgnore.Add("SDL_GetTextureScaleMode");

        NativeLibrary.SetDllImportResolver(typeof(SDL).Assembly, resolver);
        PreLoadLibrary("SDL2", typeof(SDL), exportsToIgnore.ToArray());

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

    private static GraphicsBackend UseSokolGfx(DllImportResolver resolver, GraphicsPlatform platform, GraphicsBackend? requestedBackend)
    {
        GraphicsBackend backend;
        if (requestedBackend == null)
        {
            backend = platform switch
            {
                GraphicsPlatform.Windows => GraphicsBackend.Direct3D11,
                GraphicsPlatform.macOS => GraphicsBackend.Metal,
                GraphicsPlatform.Linux => GraphicsBackend.OpenGL,
                _ => throw new ArgumentOutOfRangeException(nameof(platform), platform, null)
            };
        }
        else
        {
            backend = (GraphicsBackend)requestedBackend;
        }

        if (platform == GraphicsPlatform.Windows)
        {
            switch (backend)
            {
                case GraphicsBackend.Dummy:
                    AddLibraryPath("sokol_gfx", Path.Combine(
                        Environment.CurrentDirectory,
                        "runtimes",
                        "win-x64",
                        "native",
                        "sokol_gfx-dummy.dll"));
                    break;
                case GraphicsBackend.OpenGL:
                    AddLibraryPath("sokol_gfx", Path.Combine(
                        Environment.CurrentDirectory,
                        "runtimes",
                        "win-x64",
                        "native",
                        "sokol_gfx-opengl.dll"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestedBackend), requestedBackend, null);
            }
        }

        if (platform == GraphicsPlatform.macOS)
        {
            switch (backend)
            {
                case GraphicsBackend.Dummy:
                    AddLibraryPath("sokol_gfx", Path.Combine(
                        Environment.CurrentDirectory,
                        "runtimes",
                        "osx-x64",
                        "native",
                        "libsokol_gfx-dummy.dylib"));
                    break;
                case GraphicsBackend.OpenGL:
                    AddLibraryPath("sokol_gfx", Path.Combine(
                        Environment.CurrentDirectory,
                        "runtimes",
                        "osx-x64",
                        "native",
                        "libsokol_gfx-opengl.dylib"));
                    break;
                case GraphicsBackend.Metal:
                    AddLibraryPath("sokol_gfx", Path.Combine(
                        Environment.CurrentDirectory,
                        "runtimes",
                        "osx-x64",
                        "native",
                        "libsokol_gfx-metal.dylib"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestedBackend), requestedBackend, null);
            }
        }

        if (platform == GraphicsPlatform.Linux)
        {
            switch (backend)
            {
                case GraphicsBackend.Dummy:
                    AddLibraryPath("sokol_gfx", Path.Combine(
                        Environment.CurrentDirectory,
                        "runtimes",
                        "linux-x64",
                        "native",
                        "libsokol_gfx-dummy.so"));
                    break;
                case GraphicsBackend.OpenGL:
                    AddLibraryPath("sokol_gfx", Path.Combine(
                        Environment.CurrentDirectory,
                        "runtimes",
                        "linux-x64",
                        "native",
                        "libsokol_gfx-opengl.so"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestedBackend), requestedBackend, null);
            }
        }

        NativeLibrary.SetDllImportResolver(typeof(Sokol.Graphics.PInvoke).Assembly, resolver);
        PreLoadLibrary("sokol_gfx", typeof(PInvoke));

        return backend;
    }

    private static void UseGLEW(GraphicsPlatform platform, GraphicsBackend backend)
    {
        if (backend != GraphicsBackend.OpenGL)
        {
            return;
        }

        switch (platform)
        {
            case GraphicsPlatform.Windows:
                AddLibraryPath("glew", "runtimes/win-x64/native/glew32.dll");
                break;
            case GraphicsPlatform.Linux:
                AddLibraryPath("glew", "runtimes/linux-x64/native/libGLEW.so");
                break;
            default:
                return;
        }

        PreLoadLibrary("glew", typeof(glew));
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
    private static void PreLoadLibrary(string libraryName, Type type, string[]? exportsToIgnore = null)
    {
        try
        {
            var methods = type.GetRuntimeMethods();
            foreach (var method in methods)
            {
                var dllImportAttribute = method?.GetCustomAttribute<DllImportAttribute>();
                if (dllImportAttribute == null)
                {
                    continue;
                }

                var exportName = dllImportAttribute.EntryPoint ?? method!.Name;
                if (exportsToIgnore != null && exportsToIgnore.Contains(exportName))
                {
                    continue;
                }

                Marshal.Prelink(method!);
            }
        }
        catch (EntryPointNotFoundException e)
        {
            throw new Exception($"The `{libraryName}` library is out of date.", e);
        }
    }

    private static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        EnsureLibraryIsPreLoaded(libraryName);

        if (NativeLibrary.TryLoad(libraryName, out var handle))
        {
            return handle;
        }

        var list = LibraryPathsByLibraryName[libraryName];
        foreach (var libraryPath in list)
        {
            if (NativeLibrary.TryLoad(libraryPath, out handle))
            {
                return handle;
            }
        }

        var exceptions = new List<Exception>();
        foreach (var libraryPath in list)
        {
            var exception = new FileNotFoundException(null, libraryPath);
            exceptions.Add(exception);
        }

        throw new AggregateException("Could not find the library in the searched paths. Check the inner exception for the searched paths.", exceptions);
    }

    private static void AddLibraryPath(string libraryName, string libraryPath)
    {
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
}
