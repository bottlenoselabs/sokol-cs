// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using lithiumtoast.NativeTools;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing some <see cref="GraphicsShaderStageType" /> of a <see cref="GraphicsShader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsShaderStageDescriptor" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShaderStageDescriptor" /> is blittable to the C `sg_shader_stage_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 1280, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsShaderStageDescriptor
    {
        [FieldOffset(0)]
        private byte* _sourceCode;

        /// <summary>
        ///     The pointer to the starting address of the block of bytes as data containing the compiled source code
        ///     of the stage. Either <see cref="SourceCode" /> or <see cref="ByteCode" /> must be set for
        ///     <see cref="GraphicsBackend.Direct3D11" /> and <see cref="GraphicsBackend.Metal" />. Not used for
        ///     <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
        ///     <see cref="GraphicsBackend.OpenGLES3" />.
        /// </summary>
        [FieldOffset(8)]
        public IntPtr ByteCode;

        /// <summary>
        ///     The size of the block of bytes as data containing the compiled source code. Must be set if
        ///     <see cref="ByteCode" /> is set.
        /// </summary>
        [FieldOffset(16)]
        public uint ByteCodeSize;

        /// <summary>
        ///     The pointer to a C style string containing the name of entry point function of the stage. Optional.
        /// </summary>
        [FieldOffset(24)]
        public IntPtr EntryFunctionName;

        /// <summary>
        ///     TODO.
        /// </summary>
        [FieldOffset(32)]
        public IntPtr D3D11Target;

        [FieldOffset(40)]
        private fixed ulong _uniformBlocks[264 * GraphicsConstants.MaximumShaderStageUniformBlockSlots / 8];

        [FieldOffset(1096)]
        private fixed ulong _images[16 * GraphicsConstants.MaximumShaderStageImages / 8];

        /// <summary>
        ///     Sets the string containing the source code of the <see cref="GraphicsShader" /> stage. Must be set for
        ///     <see cref="GraphicsBackend.OpenGL" />, <see cref="GraphicsBackend.OpenGLES2" />, and
        ///     <see cref="GraphicsBackend.OpenGLES3" />. Either <see cref="SourceCode" /> or <see cref="ByteCode" />
        ///     must be set for <see cref="GraphicsBackend.Direct3D11" /> and <see cref="GraphicsBackend.Metal" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         For <see cref="GraphicsBackend.Direct3D11" />, setting <see cref="SourceCode" /> will load the
        ///         `d3dcompiler_47.dll` on demand. If this fails, creating the shader will fail.
        ///     </para>
        /// </remarks>
        /// <value>The string containing the source code used for the <see cref="GraphicsShader" /> stage.</value>
        public string SourceCode
        {
            set => _sourceCode = Native.GetCStringFrom(value);
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsShaderUniformBlockDescriptor" /> of the stage by reference given the specified
        ///     index. All uniform blocks must be configured for <see cref="GraphicsBackend.OpenGL" />,
        ///     <see cref="GraphicsBackend.OpenGLES2" />, and <see cref="GraphicsBackend.OpenGLES3" />. Optional for
        ///     every other <see cref="GraphicsBackend" /> implementation.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="GraphicsShaderUniformBlockDescriptor" /> by reference.</returns>
        public readonly ref GraphicsShaderUniformBlockDescriptor UniformBlock(int index = 0)
        {
            fixed (GraphicsShaderStageDescriptor* shaderStageDescription = &this)
            {
                var ptr = (GraphicsShaderUniformBlockDescriptor*)&shaderStageDescription->_uniformBlocks[0];
                return ref *(ptr + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsShaderImageDescriptor" /> of the stage by reference given the specified index. All
        ///     shader images must be configured for <see cref="GraphicsBackend.OpenGLES2" />.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="GraphicsShaderImageDescriptor" /> by reference.</returns>
        public readonly ref GraphicsShaderImageDescriptor Image(int index = 0)
        {
            fixed (GraphicsShaderStageDescriptor* shaderStageDescription = &this)
            {
                var ptr = (GraphicsShaderImageDescriptor*)&shaderStageDescription->_images[0];
                return ref *(ptr + index);
            }
        }
    }
}
