using System;

namespace Sokol.Samples.NonInterleaved
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var app = new NonInterleavedApplication();
            app.Run();
        }
    }
}