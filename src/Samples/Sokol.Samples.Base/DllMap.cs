using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Sokol.Samples
{
    public static class DllMap
    {
        private static readonly Dictionary<(OperatingSystem, string), string> FileNamesByOsAndLibraryName = 
            new Dictionary<(OperatingSystem, string), string>();

        static DllMap()
        {
        }

        public static void Register(Assembly assembly)
        {
            NativeLibrary.SetDllImportResolver(assembly, ResolveLibraryHandle);
        }

        public static void Configure(OperatingSystem os, string libraryName, string libraryFileName)
        {
            var key = (os, libraryName);
            FileNamesByOsAndLibraryName[key] = libraryFileName;
        }

        private static IntPtr ResolveLibraryHandle(string libraryName, Assembly assembly,
            DllImportSearchPath? searchPath)
        {
            var key = (RuntimeInformationHelper.OperatingSystem, libraryName);
            if (!FileNamesByOsAndLibraryName.TryGetValue(key, out var libraryFileName))
            {
                return NativeLibrary.Load(libraryName, assembly, searchPath);
            }

            var filePath = Directory.EnumerateFiles(AppContext.BaseDirectory, libraryFileName, 
                SearchOption.AllDirectories)
                .FirstOrDefault();
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (string.IsNullOrEmpty(filePath))
            {
                return NativeLibrary.Load(libraryFileName, assembly, searchPath);
            }

            return NativeLibrary.Load(filePath);
        }
    }
}