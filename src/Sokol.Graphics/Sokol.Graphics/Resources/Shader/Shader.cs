// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
namespace Sokol.Graphics
{
    /// <summary>
    ///     A GPU resource that holds the programmable "per-vertex processing" and "per-fragment processing" stages
    ///     of a <see cref="Pipeline" /> and the global variables used in those stages known as uniforms.
    /// </summary>
    /// <remarks>
    ///      <para>
    ///         To create a <see cref="Shader" /> synchronously, call
    ///         <see cref="GraphicsDevice.CreateShader(ref ShaderDescriptor)" /> with a specified
    ///         <see cref="PipelineDescriptor" />. To create a <see cref="Shader" /> asynchronously,call
    ///         <see cref="GraphicsDevice.AllocShader" /> to get an un-initialized <see cref="Pipeline" /> and then call
    ///         <see cref="Initialize" />.
    ///     </para>
    ///     <para>
    ///         The vertex stage, also called a vertex shader, executes programmable instructions early in the rendering
    ///         <see cref="Pipeline" /> for each vertex of input data. A vertex shader can not create nor destroy
    ///         vertices. It also doesn't know which <see cref="PipelineVertexPrimitiveType" /> each vertex belongs to.
    ///         Vertex shaders are commonly used to translate 3D models from "model space" to "world space", and then
    ///         from "world space" to "camera space". For more information see,
    ///         <a href="http://www.codinglabs.net/article_world_view_projection_matrix.aspx">http://www.codinglabs.net/article_world_view_projection_matrix.aspx</a>
    ///         .
    ///     </para>
    ///     <para>
    ///         The fragment stage, also called a fragment shader or sometimes a pixel shader, executes instructions
    ///         later in the rendering <see cref="Pipeline" /> for each "fragment". A fragment is the sample of pixels
    ///         covering a primitive (see <see cref="PipelineVertexPrimitiveType" />) which is generated during
    ///         rasterization of vertex data. A fragment shader can not access other vertices or fragments. Fragment
    ///         shaders are commonly used to control the opacity, color, z-value, texture or other "material" attributes
    ///         of primitives. For more information see,
    ///         <a href="https://thebookofshaders.com">https://thebookofshaders.com</a>.
    ///     </para>
    ///     <para>
    ///         Each global shader variable is traditionally called a "uniform" because they don't change for all GPU
    ///         "threads" that process either vertices or fragments between drawing commands.
    ///     </para>
    ///     <para>
    ///         A <see cref="Shader" /> must only be used or destroyed with the same active <see cref="Context" /> that
    ///         was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="Shader" /> is blittable to the C `sg_shader` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public readonly struct Shader
    {
        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="ShaderDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a shader.</param>
        /// <returns>A <see cref="ShaderDescriptor" /> with any zero-initialized members set to default values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ShaderDescriptor QueryDefaults([In] ref ShaderDescriptor descriptor)
        {
            return PInvoke.sg_query_shader_defaults(ref descriptor);
        }

        /// <summary>
        ///     A number which uniquely identifies the <see cref="Shader" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        // TODO: Document `ImageInfo`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ShaderInfo Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => PInvoke.sg_query_shader_info(this);
        }

        // TODO: Document `ResourceState`.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        public ResourceState State
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => PInvoke.sg_query_shader_state(this);
        }

        // TODO: Document manual initialization of a shader.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize([In] ref ShaderDescriptor descriptor)
        {
            PInvoke.sg_init_shader(this, ref descriptor);
        }

        /// <summary>
        ///     Destroys the <see cref="Shader" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            PInvoke.sg_destroy_shader(this);
        }

        // TODO: Document failing a shader.
        [SuppressMessage("ReSharper", "SA1600", Justification = "TODO")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fail()
        {
            PInvoke.sg_fail_shader(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}
