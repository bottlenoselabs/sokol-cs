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
            var libsPath = Path.Combine(AppContext.BaseDirectory, "libs");
            var filePath = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new NotImplementedException();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                filePath = Path.Combine(libsPath, "macOS", $"lib{libraryName}.dylib");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                throw new NotImplementedException();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                throw new NotImplementedException();
            }
            
            return NativeLibrary.Load(filePath, assembly, null);
        }
    }
}