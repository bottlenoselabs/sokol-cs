using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Sokol.Samples.Triangle
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var app = new TriangleApplication();
            app.Run();
        }
    }
}