// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1600", Justification = "Internal usage")]
    public static class ShaderPInvoke
    {
        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_shader_defaults")]
        public static extern ShaderDescriptor QueryDefaults([In] ref ShaderDescriptor descriptor);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_alloc_shader")]
        public static extern Shader AllocShader();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_make_shader")]
        public static extern Shader CreateShader([In] ref ShaderDescriptor descriptor);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_init_shader")]
        public static extern void Init(Shader shader, [In] ref ShaderDescriptor desc);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_destroy_shader")]
        public static extern void Destroy(Shader shader);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_fail_shader")]
        public static extern void Fail(Shader shader);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_shader_state")]
        public static extern ResourceState QueryState(Shader shader);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_query_shader_info")]
        public static extern ShaderInfo QueryInfo(Shader shader);
    }
}
