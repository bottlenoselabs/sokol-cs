// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Sokol.App;

namespace Samples.Cube
{
    internal static class Program
    {
        private static void Main()
        {
            var descriptor = default(AppDescriptor);
            descriptor.WindowTitle = "Cube";
            var app = new CubeApplication(descriptor);
            app.Run();
        }
    }
}
