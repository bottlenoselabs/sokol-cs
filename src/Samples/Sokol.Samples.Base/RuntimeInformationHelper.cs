using System.Runtime.InteropServices;

namespace Sokol.Samples
{
    public static class RuntimeInformationHelper
    {
        public static OperatingSystem OperatingSystem { get; }

        public static bool Is32BitArchitecture
        {
            get
            {
                var runtimeArchitecture = RuntimeInformation.OSArchitecture;
                return runtimeArchitecture == Architecture.Arm || runtimeArchitecture == Architecture.X86;
            }
        }

        static RuntimeInformationHelper()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                OperatingSystem = OperatingSystem.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                OperatingSystem = OperatingSystem.Darwin;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                OperatingSystem = OperatingSystem.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                OperatingSystem = OperatingSystem.FreeBSD;
            }
            else
            {
                OperatingSystem = OperatingSystem.Unknown;
            }
        }
    }
}