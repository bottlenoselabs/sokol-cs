using System.Runtime.InteropServices;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Sokol
{
    public static class glew
    {
        private const string GlewLibraryName = "glew";

        [DllImport(GlewLibraryName)]
        public static extern int glewInit();
    }
}