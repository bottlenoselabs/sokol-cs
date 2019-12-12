using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

// ReSharper disable StringLiteralTypo

namespace Sokol.Graphics.Tests
{
    public partial class PInvokeTests
    {
        static PInvokeTests()
        {
            NativeLibrary.SetDllImportResolver(typeof(sokol_gfx).Assembly, Resolver);
        }
        
        private static IntPtr Resolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            var libsPath = Path.Combine(AppContext.BaseDirectory, "runtimes");
            var filePath = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                filePath = Path.Combine(libsPath, "win-x64", "native", $"{libraryName}.dll");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                filePath = Path.Combine(libsPath, "osx-x64", "native", $"lib{libraryName}.dylib");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                filePath = Path.Combine(libsPath, "linux-x64", "native", $"lib{libraryName}.so");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                throw new NotImplementedException();
            }
            
            var handle = NativeLibrary.Load(filePath, assembly, null);
            return handle;
        }
    }
}