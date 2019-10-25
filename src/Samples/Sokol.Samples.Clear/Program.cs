using System;

namespace Sokol.Samples.Clear
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("DYLD_PRINT_LIBRARIES", "1");
            using var app = new ClearApplication();
            app.Run();
        }
    }
}