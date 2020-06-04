// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[SuppressMessage("ReSharper", "SA1300", Justification = "Original names.")]
[SuppressMessage("ReSharper", "IdentifierTypo", Justification = "Orignal names.")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "API.")]
[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "API.")]
internal static class NativeLibrary
{
    private static bool IsWindows
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    private static bool IsMacOS
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    private static bool IsLinux
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }

    [SuppressMessage("ReSharper", "CommentTypo", Justification = "Flags.")]
    public static IntPtr LoadLibrary(string libraryPath)
    {
        if (IsLinux)
        {
            return libdl.dlopen(libraryPath, 0x101); // RTLD_GLOBAL | RTLD_LAZY
        }

        if (IsWindows)
        {
            return Kernel32.LoadLibrary(libraryPath);
        }

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (IsMacOS)
        {
            return libSystem.dlopen(libraryPath, 0x101); // RTLD_GLOBAL | RTLD_LAZY
        }

        return IntPtr.Zero;
    }

    public static void FreeLibrary(IntPtr handle)
    {
        if (IsLinux)
        {
            libdl.dlclose(handle);
        }

        if (IsWindows)
        {
            Kernel32.FreeLibrary(handle);
        }

        if (IsMacOS)
        {
            libSystem.dlclose(handle);
        }
    }

    public static IntPtr GetLibraryFunctionPointer(IntPtr handle, string functionName)
    {
        if (IsLinux)
        {
            return libdl.dlsym(handle, functionName);
        }

        if (IsWindows)
        {
            return Kernel32.GetProcAddress(handle, functionName);
        }

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (IsMacOS)
        {
            return libSystem.dlsym(handle, functionName);
        }

        return IntPtr.Zero;
    }

    public static T GetLibraryFunction<T>(IntPtr handle)
    {
        return GetLibraryFunction<T>(handle, string.Empty);
    }

    public static T GetLibraryFunction<T>(IntPtr handle, string functionName)
    {
        if (string.IsNullOrEmpty(functionName))
        {
            functionName = typeof(T).Name;
            if (functionName.StartsWith("d_", StringComparison.Ordinal))
            {
                functionName = functionName.Substring(2);
            }
        }

        var functionHandle = GetLibraryFunctionPointer(handle, functionName);
        if (functionHandle == IntPtr.Zero)
        {
            throw new Exception($"Could not find a function with the given name '{functionName}' in the library.");
        }

        return Marshal.GetDelegateForFunctionPointer<T>(functionHandle);
    }

    private static class libdl
    {
        private const string LibraryName = "libdl";

        [DllImport(LibraryName, CharSet = CharSet.Ansi)]
        public static extern IntPtr dlopen(string fileName, int flags);

        [DllImport(LibraryName, CharSet = CharSet.Ansi)]
        public static extern IntPtr dlsym(IntPtr handle, string name);

        [DllImport(LibraryName)]
        public static extern int dlclose(IntPtr handle);
    }

    [SuppressUnmanagedCodeSecurity]
    private static class Kernel32
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr module, string procName);

        // ReSharper disable once MemberHidesStaticFromOuterClass
        [DllImport("kernel32")]
        public static extern int FreeLibrary(IntPtr module);
    }

    [SuppressUnmanagedCodeSecurity]
    private static class libSystem
    {
        private const string LibraryName = "libSystem";

        [DllImport(LibraryName, CharSet = CharSet.Ansi)]
        public static extern IntPtr dlopen(string fileName, int flags);

        [DllImport(LibraryName, CharSet = CharSet.Ansi)]
        public static extern IntPtr dlsym(IntPtr handle, string name);

        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int dlclose(IntPtr handle);
    }
}
