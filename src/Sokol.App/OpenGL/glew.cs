// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable once InconsistentNaming
namespace OpenGL
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "PInvoke.")]
    [SuppressMessage("ReSharper", "SA1600", Justification = "PInvoke.")]
    public static class glew
    {
        private const string LIBRARY_NAME = "sokol_gfx";

        [DllImport(LIBRARY_NAME)]
        public static extern int glewInit();

        [DllImport(LIBRARY_NAME)]
        public static extern IntPtr glewGetErrorString(int error);
    }
}
