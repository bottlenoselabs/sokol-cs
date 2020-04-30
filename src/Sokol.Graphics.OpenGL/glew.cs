// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable once InconsistentNaming
public static class glew
{
    private const string LIBRARY_NAME = "sokol_gfx";

    [DllImport(LIBRARY_NAME)]
    public static extern int glewInit();

    [DllImport(LIBRARY_NAME)]
    public static extern IntPtr glewGetErrorString(int error);
}
