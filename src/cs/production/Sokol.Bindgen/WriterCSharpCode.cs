using System.Collections.Immutable;
using C2CS;
using C2CS.Options;
using JetBrains.Annotations;

namespace Sokol.Bindgen;

[UsedImplicitly]
public class WriterCSharpCode : IWriterCSharpCode
{
    public WriterCSharpCodeOptions Options { get; } = new();

    public WriterCSharpCode()
    {
        Configure(Options);
    }

    private static void Configure(WriterCSharpCodeOptions options)
    {
        options.InputAbstractSyntaxTreesFileDirectory = "./ast";
        
        options.OutputCSharpCodeFilePath = "../src/cs/production/Sokol/Sokol.cs";
        options.NamespaceName = "bottlenoselabs";
        // options.IsEnabledVerifyCSharpCodeCompiles = false;
        
        options.IgnoredNames = new [] { "Rgba32F" }.ToImmutableArray()!;
        options.MappedNames = new[]
        {
            new WriterCSharpCodeOptionsMappedName
            {
                Source = "sg_color", 
                Target = "Rgba32F"
            }
        }.ToImmutableArray();
    }
}