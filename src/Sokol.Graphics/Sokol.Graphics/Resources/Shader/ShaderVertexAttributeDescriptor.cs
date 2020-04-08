// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Reflection information about an input to a "per-vertex processing" stage. Apart of
    ///     <see cref="ShaderDescriptor" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ShaderVertexAttributeDescriptor" /> is blittable to the C `sg_shader_attr_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8, CharSet = CharSet.Ansi)]
    public struct ShaderVertexAttributeDescriptor
    {
        /// <summary>
        ///     The pointer to a C style string with the name of the GLSL or Metal vertex attribute. Required for
        ///     <see cref="GraphicsBackend.OpenGLES2" /> but optional for <see cref="GraphicsBackend.OpenGL" />,
        ///     <see cref="GraphicsBackend.OpenGLES3" />, and <see cref="GraphicsBackend.Metal" />. Not used for
        ///     <see cref="GraphicsBackend.Direct3D11" />, instead use <see cref="SemanticName" />.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr Name;

        /// <summary>
        ///     The pointer to a C style string with the name of the HLSL vertex attribute. Required for
        ///     <see cref="GraphicsBackend.Direct3D11" />. Not used for any other <see cref="GraphicsBackend" />
        ///     implementation.
        /// </summary>
        [FieldOffset(8)]
        public IntPtr SemanticName;

        /// <summary>
        ///     The index of the vertex attribute. Required for <see cref="GraphicsBackend.Direct3D11" />. Not used for
        ///     any other <see cref="GraphicsBackend" /> implementation. Must be greater than or equal to <c>0</c>.
        /// </summary>
        [FieldOffset(16)]
        public int SemanticIndex;
    }
}
