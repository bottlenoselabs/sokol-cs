using System;

namespace Sokol.Samples.Cube
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var app = new CubeApplication();
            app.Run();
        }
    }
}