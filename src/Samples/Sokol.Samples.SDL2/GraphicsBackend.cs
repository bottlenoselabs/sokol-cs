namespace Sokol
{
    public enum GraphicsBackend
    {
        OpenGL, // Core 3.3
        OpenGLES2,
        OpenGLES3,
        Direct3D11,
        Metal
    }
    
    internal static class GraphicsBackendHelper
    {
        internal static GraphicsBackend GetDefaultGraphicsBackendFor(Platform platform)
        {
            return platform switch
            {
                Platform.Windows => GraphicsBackend.Direct3D11,
                Platform.macOS => GraphicsBackend.Metal,
                Platform.Linux => GraphicsBackend.OpenGL,
                Platform.iOS => GraphicsBackend.Metal,
                Platform.Android => GraphicsBackend.OpenGLES3,
                _ => GraphicsBackend.OpenGL
            };
        }
    }
}