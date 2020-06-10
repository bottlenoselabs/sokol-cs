// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Sokol.App;

namespace Samples.TexCube
{
    internal static class Program
    {
        private static void Main()
        {
            var descriptor = default(AppDescriptor);
            descriptor.WindowTitle = "Textured Cube";
            var app = new TextureCubeApplication(descriptor);
            app.Run();
        }
    }
}
