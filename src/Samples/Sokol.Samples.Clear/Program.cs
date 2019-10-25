using System;

namespace Sokol.Samples.Clear
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var app = new ClearApplication();
            app.Run();
        }
    }
}