// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Sokol.App;

namespace Sokol.Graphics
{
    public partial class BackendRenderer
    {
        public static BackendRenderer Create(
            GraphicsBackend backend,
            ref GraphicsBackendDescriptor descriptor,
            IntPtr windowHandle)
        {
            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (backend)
            {
                case GraphicsBackend.OpenGL:
                    return new RendererOpenGL(windowHandle, ref descriptor);
                case GraphicsBackend.Metal:
                    return new RendererMetal(windowHandle, ref descriptor);
                case GraphicsBackend.Direct3D11:
                    return new RendererDirect3D11(windowHandle, ref descriptor);
                case GraphicsBackend.OpenGLES2:
                case GraphicsBackend.OpenGLES3:
                case GraphicsBackend.WebGPU:
                case GraphicsBackend.Dummy:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(backend), backend, null);
            }
        }
    }
}
