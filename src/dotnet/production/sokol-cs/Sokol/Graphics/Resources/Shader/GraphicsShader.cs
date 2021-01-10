// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    /// <summary>
    ///     A GPU resource that holds the programmable <see cref="GraphicsShaderStageType.Vertex" /> and
    ///     <see cref="GraphicsShaderStageType.Fragment" /> stages of a <see cref="GraphicsPipeline" /> and the global
    ///     variables used in those stages known as uniforms.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To allocate and initialize a <see cref="GraphicsShader" />, call <see cref="Graphics.MakeShader" />. To
    ///         allocate a <see cref="GraphicsShader" /> and initialize it later, call <see cref="Graphics.AllocShader" /> to
    ///         get an un-initialized <see cref="GraphicsShader" /> and then call <see cref="Initialize" />.
    ///     </para>
    ///     <para>
    ///         The vertex stage, also called a vertex shader, executes developer programmable instructions early in the
    ///         rendering <see cref="GraphicsPipeline" /> for each vertex of input data. A vertex shader can not create nor
    ///         destroy vertices. Vertex shaders are commonly used to translate 3D mathematical models from model-space to
    ///         world-space to view-space to clip-space.
    ///     </para>
    ///     <para>
    ///         The fragment stage, also called a fragment shader or sometimes a pixel shader, executes developer instructions
    ///         later in the rendering <see cref="GraphicsPipeline" /> for each "fragment" of input data. A fragment is the
    ///         data covering some vertex primitive which includes a color, a depth and a stencil value. A fragment shader can
    ///         not access other vertices or fragments. Fragment shaders are commonly used to control the opacity, color,
    ///         z-value (depth), texture or other "material" attributes of geometric primitives. For more information see,
    ///         <a href="https://thebookofshaders.com">https://thebookofshaders.com</a>.
    ///     </para>
    ///     <para>
    ///         Each global shader variable is traditionally called a "uniform" because they don't change for all GPU
    ///         "threads" that process either vertices or fragments between drawing commands.
    ///     </para>
    ///     <para>
    ///         A <see cref="GraphicsShader" /> must only be used or destroyed with the same active
    ///         <see cref="GraphicsContext" /> that was also active when the resource was created.
    ///     </para>
    ///     <para>
    ///         <see cref="GraphicsShader" /> is blittable to the C `sg_shader` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    [SuppressMessage("Microsoft.Design", "CA1051", Justification = "Blittable struct.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public readonly struct GraphicsShader
    {
        /// <summary>
        ///     A number which uniquely identifies the <see cref="GraphicsShader" />.
        /// </summary>
        [FieldOffset(0)]
        public readonly uint Identifier;

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsShaderInfo Info => GraphicsPInvoke.sg_query_shader_info(this);

        /// <summary>
        ///     Gets TODO.
        /// </summary>
        public GraphicsResourceState State => GraphicsPInvoke.sg_query_shader_state(this);

        /// <summary>
        ///     Fill any zero-initialized members of a <see cref="GraphicsShaderDescriptor" /> with their explicit default
        ///     values.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a <see cref="GraphicsShader" />.</param>
        /// <returns>A <see cref="GraphicsShaderDescriptor" /> with any zero-initialized members set to default values.</returns>
        public static GraphicsShaderDescriptor QueryDefaults([In] ref GraphicsShaderDescriptor descriptor) =>
            GraphicsPInvoke.sg_query_shader_defaults(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <param name="descriptor">.</param>
        public void Initialize([In] ref GraphicsShaderDescriptor descriptor) => GraphicsPInvoke.sg_init_shader(this, ref descriptor);

        /// <summary>
        ///     Destroys the <see cref="GraphicsShader" />.
        /// </summary>
        public void Destroy() => GraphicsPInvoke.sg_destroy_shader(this);

        /// <summary>
        ///     TODO.
        /// </summary>
        public void Fail() => GraphicsPInvoke.sg_fail_shader(this);

        /// <inheritdoc />
        public override string ToString() => $"{Identifier}";
    }
}
