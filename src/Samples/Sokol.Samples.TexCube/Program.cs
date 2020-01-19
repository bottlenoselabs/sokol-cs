using System;

namespace Sokol.Samples.TexCube
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var app = new TexCubeApplication();
            app.Run();
        }
    }
}