// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Sokol.App;

namespace Samples.Instancing
{
    internal static class Program
    {
        private static void Main()
        {
            var descriptor = default(AppDescriptor);
            descriptor.WindowTitle = "Instancing";
            var app = new InstancingApplication(descriptor);
            app.Run();
        }
    }
}
