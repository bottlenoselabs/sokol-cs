// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Sokol.Graphics
{
    /// <summary>
    ///     Defines the different ways vertex data is encoded. Used by a <see cref="Pipeline" /> to know how to read
    ///     vertex attributes out of a vertex <see cref="Buffer" /> and into the "per-vertex processing" stage of a
    ///     <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="GraphicsBackend.Direct3D11" /> can only convert normalized vertex formats to vertex shader
    ///         floating points during vertex fetch. The normalized vertex formats are <see cref="Byte4N" />,
    ///         <see cref="UByte4N" />, <see cref="Short2N" />, <see cref="Short2N" />, and <see cref="UShort4N" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsBackend.Direct3D11" /> will not convert non-normalized vertex formats to vertex
    ///         shader floating points during vertex fetch. Instead the non-normalized vertex formats will be unpacked
    ///         to integer vectors known as "ivecn". The non-normalized vertex formats are <see cref="Byte4" />,
    ///         <see cref="UByte4" />, <see cref="Short2" />, and <see cref="Short4" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsBackend.OpenGLES2" /> does not support integer vertex formats. Only
    ///         <see cref="Float" />, <see cref="Float2" />, <see cref="Float3" />, and <see cref="Float4" /> are
    ///         supported by <see cref="GraphicsBackend.OpenGLES2" />.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsBackend.OpenGLES3" /> does not support <see cref="UInt10N2" />.
    ///     </para>
    ///     <para>
    ///         To write a vertex shader which works on all platforms supported by at least one
    ///         <see cref="GraphicsBackend" /> implementation, use one of the following vertex formats:
    ///         <see cref="Float" />, <see cref="Float2" />, <see cref="Float3" />, <see cref="Float4" />,
    ///         <see cref="Byte4N" />, <see cref="UByte4N" />, <see cref="Short2N" />, <see cref="UShort2N" />,
    ///         <see cref="Short4N" />, or <see cref="UShort4N" />. Also, if necessary, expand the components to a
    ///         normalized float range by multiplying with <c>127.0</c>, <c>255.0</c>, <c>32767.0</c>, or <c>65535.0</c>
    ///         in the vertex shader.
    ///     </para>
    ///     <para>
    ///         <see cref="PipelineVertexAttributeFormat" /> is blittable to the C `sg_vertex_format` enum found in
    ///         `sokol_gfx`.
    ///     </para>
    /// </remarks>
    public enum PipelineVertexAttributeFormat
    {
        /// <summary>
        ///     The vertex attribute slot is undefined with a read size of <c>0</c> bytes.
        /// </summary>
        Invalid,

        /// <summary>
        ///     The vertex attribute slot is read and used as a <see cref="float" /> value.
        ///     Total read size is <c>4</c> bytes.
        /// </summary>
        Float,

        /// <summary>
        ///     The vertex attribute slot is read and used as two <see cref="float" /> values.
        ///     Total read size is <c>8</c> bytes.
        /// </summary>
        Float2,

        /// <summary>
        ///     The vertex attribute slot is read and used as three <see cref="float" /> values.
        ///     Total read size is <c>12</c> bytes.
        /// </summary>
        Float3,

        /// <summary>
        ///     The vertex attribute slot is read and used as four <see cref="float" /> values.
        ///     Total read size is <c>16</c> bytes.
        /// </summary>
        Float4,

        /// <summary>
        ///     The vertex attribute slot is read as four packed <see cref="sbyte" /> components and each component is
        ///     unpacked to a un-normalized, signed <see cref="float" /> value.
        ///     Total read size is <c>4</c> bytes.
        ///     If using <see cref="GraphicsBackend.Direct3D11" /> each packed component is not unpacked to a float, but
        ///     rather an integer component.
        /// </summary>
        Byte4,

        /// <summary>
        ///     The vertex attribute slot is read as four packed <see cref="sbyte" /> components and each component is
        ///     unpacked to a normalized, signed <see cref="float" /> value. in the range of <c>-1.0f</c> to <c>+1.0f</c>.
        ///     Total read size is <c>4</c> bytes.
        /// </summary>
        Byte4N,

        /// <summary>
        ///     The vertex attribute slot is read as four packed <see cref="byte" /> components and each component is
        ///     unpacked to a un-normalized, un-signed <see cref="float" /> value.
        ///     Total read size is <c>4</c> bytes.
        ///     If using <see cref="GraphicsBackend.Direct3D11" /> each packed component is not unpacked to a float, but
        ///     rather an integer component.
        /// </summary>
        UByte4,

        /// <summary>
        ///     The vertex attribute slot is read as four packed <see cref="byte" /> components and each component is
        ///     unpacked to a normalized, un-signed <see cref="float" /> value in the range of <c>0.0f</c> to <c>+1.0f</c>.
        ///     Total read size is <c>4</c> bytes.
        /// </summary>
        UByte4N,

        /// <summary>
        ///     The vertex attribute slot is read as two packed <see cref="short" /> components and each component is
        ///     unpacked to a un-normalized, signed <see cref="float" /> value.
        ///     Total read size is <c>4</c> bytes.
        ///     If using <see cref="GraphicsBackend.Direct3D11" /> each packed component is not unpacked to a float, but
        ///     rather an integer component.
        /// </summary>
        Short2,

        /// <summary>
        ///     The vertex attribute slot is read as two packed <see cref="short" /> components and each component is
        ///     unpacked to a normalized, signed <see cref="float" /> value in the range of <c>-1.0f</c> to <c>+1.0f</c>.
        ///     Total read size is <c>4</c> bytes.
        /// </summary>
        Short2N,

        /// <summary>
        ///     The vertex attribute slot is read as two packed <see cref="ushort" /> components and each component is
        ///     unpacked to a normalized, un-signed <see cref="float" /> value in the range of <c>0.0f</c> to <c>+1.0f</c>.
        ///     Total read size is <c>4</c> bytes.
        /// </summary>
        UShort2N,

        /// <summary>
        ///     The vertex attribute slot is read as four packed <see cref="short" /> components and each component is
        ///     unpacked to a un-normalized, signed <see cref="float" /> value.
        ///     Total read size is <c>8</c> bytes.
        ///     If using <see cref="GraphicsBackend.Direct3D11" /> each packed component is not unpacked to a float, but
        ///     rather an integer component.
        /// </summary>
        Short4,

        /// <summary>
        ///     The vertex attribute slot is read as four packed <see cref="short" /> components and each component is
        ///     unpacked to a normalized, signed <see cref="float" /> value in the range of <c>-1.0f</c> to <c>+1.0f</c>.
        ///     Total read size is <c>8</c> bytes.
        /// </summary>
        Short4N,

        /// <summary>
        ///     The vertex attribute slot is read as four packed <see cref="ushort" /> components and each component is
        ///     unpacked to a normalized, un-signed <see cref="float" /> value in the range of <c>0.0f</c> to <c>+1.0f</c>.
        ///     Total read size is <c>8</c> bytes.
        /// </summary>
        UShort4N,

        /// <summary>
        ///     The vertex attribute slot is read as four packed un-signed integer components: 10-bits, 10-bits, 10-bits,
        ///     and 2-bits.
        ///     Each component is converted to a normalized, signed, float value in the range of <c>-1.0f</c> to
        ///     <c>+1.0f</c>.
        ///     Total read size <c>4</c> bytes.
        /// </summary>
        UInt10N2
    }
}
