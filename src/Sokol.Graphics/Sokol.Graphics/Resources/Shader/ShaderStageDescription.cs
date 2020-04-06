// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for constructing either a "per-vertex processing" stage or a "per-fragment processing" stage.
    ///     Apart of <see cref="ShaderDescription" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ShaderStageDescription" /> is blittable to the C `sg_shader_stage_desc` struct found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 1280, Pack = 8, CharSet = CharSet.Ansi)]
    public unsafe struct ShaderStageDescription
    {
        /// <summary>
        ///     The pointer to the C style string containing the source code of the stage. Must be set for
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
        [FieldOffset(0)]
        public IntPtr SourceCode;

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
        ///     <see cref="ByteCode" /> is set. Can't be negative.
        /// </summary>
        [FieldOffset(16)]
        public int ByteCodeSize;

        /// <summary>
        ///     The pointer to a C style string containing the name of entry point function of the stage. Optional.
        /// </summary>
        [FieldOffset(24)]
        public IntPtr EntryFunctionName;

        [FieldOffset(32)]
        internal fixed ulong _uniformBlocks[264 * sokol_gfx.SG_MAX_SHADERSTAGE_UBS / 8];

        [FieldOffset(1088)]
        internal fixed ulong _images[16 * sokol_gfx.SG_MAX_SHADERSTAGE_IMAGES / 8];

        /// <summary>
        ///     Gets the <see cref="ShaderUniformBlockDescription" /> of the stage by reference given the specified
        ///     index. All uniform blocks must be configured for <see cref="GraphicsBackend.OpenGL" />,
        ///     <see cref="GraphicsBackend.OpenGLES2" />, and <see cref="GraphicsBackend.OpenGLES3" />. Optional for
        ///     every other <see cref="GraphicsBackend" /> implementation.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="ShaderUniformBlockDescription" /> by reference.</returns>
        public ref ShaderUniformBlockDescription UniformBlock(int index)
        {
            fixed (ShaderStageDescription* shaderStageDescription = &this)
            {
                var ptr = (ShaderUniformBlockDescription*)&shaderStageDescription->_uniformBlocks[0];
                return ref *(ptr + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="ShaderImageDescription" /> of the stage by reference given the specified index. All
        ///     shader images must be configured for <see cref="GraphicsBackend.OpenGLES2" />.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>A <see cref="ShaderImageDescription" /> by reference.</returns>
        public ref ShaderImageDescription Image(int index)
        {
            fixed (ShaderStageDescription* shaderStageDescription = &this)
            {
                var ptr = (ShaderImageDescription*)&shaderStageDescription->_images[0];
                return ref *(ptr + index);
            }
        }
    }
}
