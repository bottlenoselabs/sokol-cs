// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Sokol.Graphics;

namespace Sokol.App
{
    public struct AppDescriptor
    {
        public GraphicsBackend? RequestedBackend;
        public AppLoop? Loop;
        public GraphicsDescriptor? Graphics;
        public bool? AllowHighDpi;
    }
}
