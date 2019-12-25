using System;

namespace Sokol.Samples.Cube
{
    static class Program
    {
        static void Main(string[] args)
        {
            using var app = new CubeApplication();
            app.Run();
        }
    }
}