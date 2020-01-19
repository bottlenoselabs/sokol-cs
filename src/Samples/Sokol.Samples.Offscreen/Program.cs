namespace Sokol.Samples.Offscreen
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var app = new OffscreenApplication();
            app.Run();
        }
    }
}