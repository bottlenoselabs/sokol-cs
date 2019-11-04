namespace Sokol.Samples
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
                Platform.Windows => GraphicsBackend.OpenGL, //TODO: Use Direct3D11 instead
                Platform.macOS => GraphicsBackend.OpenGL, //TODO: Use Metal instead
                Platform.Linux => GraphicsBackend.OpenGL,
                Platform.iOS => GraphicsBackend.Metal,
                Platform.Android => GraphicsBackend.OpenGLES3,
                _ => GraphicsBackend.OpenGL
            };
        }
    }
}