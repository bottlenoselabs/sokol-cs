// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     Parameters for initializing `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsDescriptor" /> is blittable to the C `sg_desc` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 152, Pack = 8)]
    public struct GraphicsDescriptor
    {
        /// <summary>
        ///     The maximum number of available <see cref="Buffer" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 128.
        /// </summary>
        [FieldOffset(4)]
        public int BufferPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Image" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 128.
        /// </summary>
        [FieldOffset(8)]
        public int ImagePoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Shader" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 32.
        /// </summary>
        [FieldOffset(12)]
        public int ShaderPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Pipeline" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 64.
        /// </summary>
        [FieldOffset(16)]
        public int PipelinePoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Pass" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 16.
        /// </summary>
        [FieldOffset(20)]
        public int PassPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="Graphics.Context" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 16.
        /// </summary>
        [FieldOffset(24)]
        public int ContextPoolSize;

        /// <summary>
        ///     The size of a "per frame uniform buffer" which holds all the uniform data updated in a single frame via
        ///     calls to <see cref="Pass.ApplyShaderUniforms{T}" />. Only used by the <see cref="GraphicsBackend.Metal" />
        ///     and the <see cref="GraphicsBackend.WebGPU" /> back-end. Default is 4 MB (4 * 1024 * 1024).
        /// </summary>
        [FieldOffset(28)]
        public int UniformBufferSize;

        /// <summary>
        ///     The size of a "per frame staging buffer" for dynamic data uploads from CPU memory to GPU memory. The
        ///     staging buffer size must be big enough to hold all the dynamically updated data via updates or appends
        ///     in a single frame. Only used by the <see cref="GraphicsBackend.WebGPU" /> back-ends. Default is 8 MB
        ///     (8 * 1024 * 1024).
        /// </summary>
        [FieldOffset(32)]
        public int StagingBufferSize;

        /// <summary>
        ///     The number of unique entries in the internal cache for texture sampler objects. Only used by the
        ///     <see cref="GraphicsBackend.Metal" /> and the <see cref="GraphicsBackend.WebGPU" /> back-ends. Default is
        ///     64.
        /// </summary>
        [FieldOffset(36)]
        public int SamplerCacheSize;

        /// <summary>
        ///     The parameters for initializing a 3D graphics API back-end.
        /// </summary>
        [FieldOffset(40)]
        public GraphicsContextDescriptor Context;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        internal uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(144)]
        internal uint _endCanary;
    }
}
