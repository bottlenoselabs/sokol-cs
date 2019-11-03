
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using System.Runtime.InteropServices;

namespace Sokol
{
    public static class glew
    {
        private const string GlewLibraryName = "glew";
        
        [DllImport(GlewLibraryName)]
        public static extern uint glewInit();
    }
}