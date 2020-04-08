// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     <para>
    ///         A GPU resource that specifies the steps that a GPU performs to convert 2D or 3D geometry into pixels
    ///         (called rasterization) that can be displayed on screen (to a framebuffer) or saved off screen
    ///         (to a render target <see cref="Image" />).
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A render <see cref="Pipeline" /> processes drawing commands and writes data into a render
    ///         <see cref="Pass"/> resource's targets. A render <see cref="Pipeline" /> has many stages. Some stages can
    ///         be programmed using a <see cref="Shader" />. Some other stages have configurable behavior such as
    ///         <see cref="PipelineDepthStencilState" />, <see cref="PipelineRasterizerState" />, and
    ///         <see cref="PipelineBlendState" />. Remaining stages are hidden from the perspective of the developer as
    ///         they can't be programmed or configured.
    ///     </para>
    ///     <para>
    ///         With rasterization, lines and triangles are the building blocks for modeling any 2D and 3D geometry
    ///         (called 3D models). These lines and triangles are made up of points (called vertices) where each
    ///         point is traditionally given attributes such as a position, a color, a normal vector, or
    ///         texture coordinates. This vertex data is the input to a <see cref="Shader" /> where two programmable
    ///         stages can customize rasterization. The output a <see cref="Shader" /> is the pixels on screen
    ///         (framebuffer) or texels in a render target <see cref="Image" />.
    ///     </para>
    ///     <para>
    ///         To create a <see cref="Pipeline" />, call <see cref="Pipeline.Create" /> or <see cref="Pipeline.Init" />
    ///         with a specified <see cref="PipelineDescription" />.
    ///     </para>
    ///     <para>
    ///         To activate a <see cref="Pipeline" /> with all it's state and by consequence deactivate any other
    ///         active <see cref="Pipeline"/>, call <see cref="Apply" />.
    ///     </para>
    ///     <para>
    ///         A <see cref="Pipeline" /> must only be used or destroyed with the same active <see cref="Context" />
    ///         that was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Pipeline" /> is blittable to the C `sg_pipeline` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Pipeline
    {
        /// <summary>
        ///     Fill any zero-initialized members of an <see cref="ImageDescription" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="description">The parameters for creating an image.</param>
        /// <returns>An <see cref="ImageDescription" /> with any zero-initialized members set to default values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PipelineDescription QueryDefaults([In] ref PipelineDescription description)
        {
            return PipelinePInvoke.QueryDefaults(ref description);
        }

        // TODO: Document allocating a pipeline
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pipeline Alloc()
        {
            return PipelinePInvoke.Alloc();
        }

        /// <summary>
        ///     Creates a <see cref="Pipeline" /> from the specified <see cref="PipelineDescription" />.
        /// </summary>
        /// <param name="description">The parameters for creating a pipeline.</param>
        /// <returns>An <see cref="Image" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pipeline Create([In] ref PipelineDescription description)
        {
            return PipelinePInvoke.Create(ref description);
        }

        /// <summary>
        ///     A number which uniquely identifies the <see cref="Pipeline" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        // TODO: Document manual initialization of a pipeline
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init([In] ref PipelineDescription description)
        {
            PipelinePInvoke.Init(this, ref description);
        }

        /// <summary>
        ///     Destroys the <see cref="Image" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            PipelinePInvoke.Destroy(this);
        }

        // TODO: Document failing a pipeline.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fail()
        {
            PipelinePInvoke.Fail(this);
        }

        /// <summary>
        ///     Set the <see cref="Pipeline" /> as the active state for the next draw calls.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Apply()
        {
            PipelinePInvoke.Apply(this);
        }

        /// <summary>
        ///     Updates the data for a <see cref="Shader" /> uniform given a a specified <see cref="ShaderStageType" />,
        ///     the block the uniform belongs to, and the data itself.
        /// </summary>
        /// <param name="stage">The shader stage.</param>
        /// <param name="uniformBlockIndex">The uniform block zero-based index which the uniform belongs to.</param>
        /// <param name="value">The data.</param>
        /// <typeparam name="T">The type of <paramref name="value" />.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ApplyShaderUniform<T>(ShaderStageType stage, int uniformBlockIndex, ref T value)
            where T : unmanaged
        {
            var dataPointer = (IntPtr)Unsafe.AsPointer(ref value);
            var dataSize = Marshal.SizeOf<T>();
            PipelinePInvoke.ApplyUniforms(stage, uniformBlockIndex, dataPointer, dataSize);
        }

        /// <summary>
        ///     Updates the active <see cref="PipelineResourceBindings" />.
        /// </summary>
        /// <param name="bindings">The resource bindings.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyBindings([In] ref PipelineResourceBindings bindings)
        {
            PipelinePInvoke.ApplyBindings(ref bindings);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
