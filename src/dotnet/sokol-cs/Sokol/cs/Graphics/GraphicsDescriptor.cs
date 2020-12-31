// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

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
    [StructLayout(LayoutKind.Explicit, Size = 232, Pack = 8)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public struct GraphicsDescriptor
    {
        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private uint _startCanary;

        /// <summary>
        ///     The maximum number of available <see cref="GraphicsBuffer" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 128.
        /// </summary>
        [FieldOffset(4)]
        public int BufferPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="GraphicsImage" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 128.
        /// </summary>
        [FieldOffset(8)]
        public int ImagePoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="GraphicsShader" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 32.
        /// </summary>
        [FieldOffset(12)]
        public int ShaderPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="GraphicsPipeline" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 64.
        /// </summary>
        [FieldOffset(16)]
        public int PipelinePoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="GraphicsPass" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 16.
        /// </summary>
        [FieldOffset(20)]
        public int PassPoolSize;

        /// <summary>
        ///     The maximum number of available <see cref="GraphicsContext" /> instances for the life-time of a `sokol_gfx`
        ///     application. Default is 16.
        /// </summary>
        [FieldOffset(24)]
        public int ContextPoolSize;

        /// <summary>
        ///     The size of a "per frame uniform buffer" which holds all the uniform data updated in a single frame via
        ///     calls to <see cref="GraphicsPass.ApplyShaderUniforms{T}" />. Only used by the <see cref="GraphicsBackend.Metal" />
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
        [FieldOffset(144)]
        private uint _endCanary;
    }
}
