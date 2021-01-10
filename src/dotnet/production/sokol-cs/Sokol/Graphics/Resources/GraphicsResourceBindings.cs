// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     The resource bindings for a <see cref="GraphicsPass" /> to use for rendering. Can include 1 to 8 vertex
    ///     <see cref="GraphicsBuffer" /> resources, 0 to 1 vertex-index <see cref="GraphicsBuffer" /> resources, and 0 to 12
    ///     <see cref="GraphicsImage" /> resources.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To apply a <see cref="GraphicsResourceBindings" />, call <see cref="GraphicsPass.ApplyBindings" /> after a
    ///         <see cref="GraphicsPipeline" /> is initialized.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="GraphicsResourceBindings" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsResourceBindings" /> is blittable to the C `sg_bindings` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 176, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global", Justification = "Mutable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    [SuppressMessage("StyleCop.OrderingRules", "SA1202", Justification = "Blittable struct.")]
    public unsafe struct GraphicsResourceBindings
    {
        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        private readonly uint _startCanary;

        [FieldOffset(4)]
        private readonly int _vertexBuffers;

        [FieldOffset(36)]
        private readonly int _vertexBufferOffsets;

        /// <summary>
        ///     The vertex-index <see cref="GraphicsBuffer" /> to use. Default is <c>default(GraphicsBuffer)</c> which indicates to
        ///     not
        ///     use indexing.
        /// </summary>
        [FieldOffset(68)]
        public GraphicsBuffer IndexBuffer;

        /// <summary>
        ///     The zero-based offset into the <see cref="IndexBuffer" /> which to start using vertex-index data.
        ///     Default is <c>0</c>.
        /// </summary>
        [FieldOffset(72)]
        public int IndexBufferOffset;

        [FieldOffset(76)]
        private readonly int _vsImages;

        [FieldOffset(124)]
        private readonly int _fsImages;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(172)]
        private readonly uint _endCanary;

        /// <summary>
        ///     Gets the vertex <see cref="GraphicsBuffer" />, by reference, to use given a specified slot.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index indicating what vertex <see cref="GraphicsBuffer" /> to use. Can't be
        ///     negative. Must be <c>8</c> or less. Default is <c>0</c>.
        /// </param>
        /// <returns>A vertex <see cref="GraphicsBuffer" /> by reference.</returns>
        public readonly ref GraphicsBuffer VertexBuffer(int index)
        {
            fixed (GraphicsResourceBindings* bindings = &this)
            {
                var pointer = (GraphicsBuffer*)&bindings->_vertexBuffers;
                return ref *(pointer + index);
            }
        }

        /// <summary>
        ///     Gets the zero-based offset, by reference, into a <see cref="VertexBuffer" /> (specified by a
        ///     slot) which to start using vertex data.
        /// </summary>
        /// <param name="index">
        ///     The zero-based slot indicating what vertex <see cref="VertexBuffer" /> to for which the offset
        ///     applies to.
        /// </param>
        /// <returns>A zero-based offset.</returns>
        public ref int VertexBufferOffset(int index)
        {
            fixed (GraphicsResourceBindings* bindings = &this)
            {
                var pointer = &bindings->_vertexBufferOffsets;
                return ref *(pointer + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsImage" /> to use, by reference, for the <see cref="GraphicsShaderStageType.Vertex" />
        ///     stage of a <see cref="GraphicsShader" />.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index indicating what slot to use to get the <see cref="GraphicsImage" />.
        /// </param>
        /// <returns>A <see cref="GraphicsImage" />.</returns>
        public readonly ref GraphicsImage VertexStageImage(int index = 0)
        {
            fixed (GraphicsResourceBindings* bindings = &this)
            {
                var pointer = (GraphicsImage*)&bindings->_vsImages;
                return ref *(pointer + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsImage" /> to use, by reference, for the <see cref="GraphicsShaderStageType.Fragment" />
        ///     stage of a <see cref="GraphicsShader" />.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index indicating what slot to use to get the <see cref="GraphicsImage" />.
        /// </param>
        /// <returns>A <see cref="GraphicsImage" />.</returns>
        public readonly ref GraphicsImage FragmentStageImage(int index = 0)
        {
            fixed (GraphicsResourceBindings* bindings = &this)
            {
                var pointer = (GraphicsImage*)&bindings->_fsImages;
                return ref *(pointer + index);
            }
        }
    }
}
