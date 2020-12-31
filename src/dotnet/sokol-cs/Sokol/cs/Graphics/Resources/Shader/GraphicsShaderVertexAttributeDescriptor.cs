// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using lithiumtoast.NativeTools;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Information about an input to <see cref="GraphicsShaderStageType.Vertex" /> stage.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsShaderVertexAttributeDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderVertexAttributeDescriptor" /> is blittable to the C `sg_shader_attr_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsShaderVertexAttributeDescriptor
    {
        [FieldOffset(0)]
        private byte* _name;

        [FieldOffset(8)]
        private byte* _semanticName;

        /// <summary>
        ///     The index of the vertex attribute. Required for <see cref="GraphicsBackend.Direct3D11" />. Not used for
        ///     any other <see cref="GraphicsBackend" /> implementation. Must be greater than or equal to <c>0</c>.
        /// </summary>
        [FieldOffset(16)]
        public int SemanticIndex;

        /// <summary>
        ///     Sets the name of the GLSL or Metal vertex attribute. Required for
        ///     <see cref="GraphicsBackend.OpenGLES2" /> but optional for <see cref="GraphicsBackend.OpenGL" />,
        ///     <see cref="GraphicsBackend.OpenGLES3" />, and <see cref="GraphicsBackend.Metal" />. Not used for
        ///     <see cref="GraphicsBackend.Direct3D11" />, instead use <see cref="SemanticName" />.
        /// </summary>
        /// <value>
        ///     The string with the name of the GLSL or Metal vertex attribute.
        /// </value>
        public string Name
        {
            set => _name = Native.GetCStringFrom(value);
        }

        /// <summary>
        ///     Sets the name of the HLSL vertex attribute. Required for <see cref="GraphicsBackend.Direct3D11" />. Not used for
        ///     any other <see cref="GraphicsBackend" /> implementation.
        /// </summary>
        /// <value>The string with the name of the HLSL vertex attribute.</value>
        public string SemanticName
        {
            set => _semanticName = Native.GetCStringFrom(value);
        }
    }
}
