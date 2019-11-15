using System.Runtime.InteropServices;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Sokol
{
    public static class glew
    {
        private const string GlewLibraryName = "glew";

        public const int GLEW_OK = 0;
        
        [DllImport(GlewLibraryName)]
        public static extern uint glewInit();
    }
}