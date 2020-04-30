// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global

[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "C style")]
public static class gl
{
    private const string LIBRARY_NAME = "sokol_gfx";

    [DllImport(LIBRARY_NAME)]
    public static extern GL_ERROR glGetError();

    public enum GL_ERROR
    {
        GL_NO_ERROR = 0,
        GL_INVALID_ENUM = 1280,
        GL_INVALID_VALUE = 1281,
        GL_INVALID_OPERATION = 1282,
        GL_STACK_OVERFLOW = 1283,
        GL_STACK_UNDERFLOW = 1284,
        GL_OUT_OF_MEMORY = 1285,
        GL_INVALID_FRAMEBUFFER_OPERATION = 1286
    }
}
