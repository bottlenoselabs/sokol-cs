using System.Diagnostics.CodeAnalysis;
using System.IO;
using ShaderToy.App;

[SuppressMessage("ReSharper", "SA1600", Justification = "ShaderToy")]
internal static class Program
{
    private static void Main()
    {
        var sourceCode = File.ReadAllText("assets/Seascape.frag");
        var app = new ShaderToyApp("https://www.shadertoy.com/view/Ms2SD1", sourceCode);
        app.Run();
    }
}