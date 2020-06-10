using System.Diagnostics.CodeAnalysis;
using System.IO;
using ShaderToy.App;

[SuppressMessage("ReSharper", "SA1600", Justification = "ShaderToy")]
internal static class Program
{
    private static void Main()
    {
        var sourceCode = File.ReadAllText("assets/Heart3D.frag");
        var app = new ShaderToyApp("https://www.shadertoy.com/view/4lK3Rc", sourceCode);
        app.Run();
    }
}