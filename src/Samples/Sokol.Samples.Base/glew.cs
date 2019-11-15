
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using System.Runtime.InteropServices;

namespace Sokol.Samples
{
    public static class glew
    {
        private const string GlewLibraryName = "GLEW";
        
        [DllImport(GlewLibraryName)]
        public static extern uint glewInit();
    }
}