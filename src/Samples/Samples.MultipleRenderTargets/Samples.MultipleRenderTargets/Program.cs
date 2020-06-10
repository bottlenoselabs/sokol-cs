// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Sokol.App;

namespace Samples.MultipleRenderTargets
{
    internal static class Program
    {
        private static void Main()
        {
            var descriptor = default(AppDescriptor);
            descriptor.WindowTitle = "Multiple Render Targets";
            var app = new MultipleRenderTargetsApplication(descriptor);
            app.Run();
        }
    }
}
