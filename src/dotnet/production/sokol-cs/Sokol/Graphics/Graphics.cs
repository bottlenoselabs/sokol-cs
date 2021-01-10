// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using lithiumtoast.NativeTools;

namespace Sokol
{
    /// <summary>
    ///     The functions of `sokol_gfx`.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         For practicality, it's recommended to import the module with all the bindings like so:
    ///         <c>using static Sokol.Graphics.Graphics;</c>.
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "MemberCanBeInternal", Justification = "Public API.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public API.")]
    public static class Graphics
    {
        /// <summary>
        ///     Gets the <see cref="GraphicsDescriptor" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsDescriptor" /> of the `sokol_gfx` application.</value>
        public static GraphicsDescriptor Descriptor => GraphicsPInvoke.sg_query_desc();

        /// <summary>
        ///     Gets the <see cref="GraphicsBackend" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsBackend" /> of the `sokol_gfx` application.</value>
        public static GraphicsBackend Backend
        {
            get
            {
                var value = GraphicsPInvoke.sg_query_backend();
                return value switch
                {
                    sokol_gfx.sg_backend.SG_BACKEND_GLCORE33 => GraphicsBackend.OpenGL,
                    sokol_gfx.sg_backend.SG_BACKEND_GLES2 => GraphicsBackend.OpenGLES2,
                    sokol_gfx.sg_backend.SG_BACKEND_GLES3 => GraphicsBackend.OpenGLES3,
                    sokol_gfx.sg_backend.SG_BACKEND_D3D11 => GraphicsBackend.Direct3D11,
                    sokol_gfx.sg_backend.SG_BACKEND_METAL_IOS => GraphicsBackend.Metal,
                    sokol_gfx.sg_backend.SG_BACKEND_METAL_MACOS => GraphicsBackend.Metal,
                    sokol_gfx.sg_backend.SG_BACKEND_METAL_SIMULATOR => GraphicsBackend.Metal,
                    sokol_gfx.sg_backend.SG_BACKEND_WGPU => GraphicsBackend.WebGPU,
                    sokol_gfx.sg_backend.SG_BACKEND_DUMMY => GraphicsBackend.Dummy,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        /// <summary>
        ///     Gets the <see cref="GraphicsFeatures" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsFeatures" /> of the `sokol_gfx` application.</value>
        public static GraphicsFeatures Features => GraphicsPInvoke.sg_query_features();

        /// <summary>
        ///     Gets the <see cref="GraphicsLimits" /> of the `sokol_gfx` application.
        /// </summary>
        /// <value>The <see cref="GraphicsLimits" /> of the `sokol_gfx` application.</value>
        public static GraphicsLimits Limits => GraphicsPInvoke.sg_query_limits();

        /// <summary>
        ///     Frees any memory allocated for strings used in descriptors. Only call this method
        ///     after resources are created.
        /// </summary>
        public static void FreeStrings()
        {
            Native.ClearStrings();
            GC.Collect();
        }

        /// <summary>
        ///     Initializes `sokol_gfx` for the life-time of an application.
        /// </summary>
        /// <param name="desc">The configuration to use for initialize.</param>
        public static void Setup(ref GraphicsDescriptor desc) => GraphicsPInvoke.sg_setup(ref desc);

        /// <summary>
        ///     Shutdown `sokol_gfx`. Should be called before an application exits where <see cref="Setup" /> was previously
        ///     called.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         All resources are automatically destroyed upon calling <see cref="Shutdown" />. If you need to destroy a
        ///         resource before calling <see cref="Shutdown" />, call one the following:
        ///         <list type="bullet">
        ///             <item>
        ///                 <description>
        ///                     <see cref="GraphicsBuffer.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="GraphicsImage.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="GraphicsShader.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="GraphicsPipeline.Destroy" />
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     <see cref="GraphicsPass.Destroy" />
        ///                 </description>
        ///             </item>
        ///         </list>
        ///     </para>
        /// </remarks>
        public static void Shutdown() => GraphicsPInvoke.sg_shutdown();

        /// <summary>
        ///     Creates a <see cref="GraphicsContext" />.
        /// </summary>
        /// <returns>A <see cref="GraphicsContext" />.</returns>
        public static GraphicsContext SetupContext() => GraphicsPInvoke.sg_setup_context();

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <returns>.</returns>
        public static GraphicsBuffer AllocBuffer() => GraphicsPInvoke.sg_alloc_buffer();

        /// <summary>
        ///     Creates a <see cref="GraphicsBuffer" /> from the specified <see cref="GraphicsBufferDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating the buffer.</param>
        /// <returns>A <see cref="GraphicsBuffer" />.</returns>
        public static GraphicsBuffer MakeBuffer(ref GraphicsBufferDescriptor descriptor) => GraphicsPInvoke.sg_make_buffer(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <returns>.</returns>
        public static GraphicsImage AllocImage() => GraphicsPInvoke.sg_alloc_image();

        /// <summary>
        ///     Creates a <see cref="GraphicsImage" /> from the specified <see cref="GraphicsImageDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating an image.</param>
        /// <returns>A <see cref="GraphicsImage" />.</returns>
        public static GraphicsImage MakeImage(ref GraphicsImageDescriptor descriptor) => GraphicsPInvoke.sg_make_image(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <returns>.</returns>
        public static GraphicsPass AllocPass() => GraphicsPInvoke.sg_alloc_pass();

        /// <summary>
        ///     Creates a <see cref="GraphicsPass" /> from the specified <see cref="GraphicsPassDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a pass.</param>
        /// <returns>A <see cref="GraphicsPass" />.</returns>
        public static GraphicsPass MakePass(ref GraphicsPassDescriptor descriptor) => GraphicsPInvoke.sg_make_pass(ref descriptor);

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns>.</returns>
        public static GraphicsPipeline AllocPipeline() => GraphicsPInvoke.sg_alloc_pipeline();

        /// <summary>
        ///     Creates a <see cref="GraphicsPipeline" /> from the specified <see cref="GraphicsPipelineDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a pipeline.</param>
        /// <returns>A <see cref="GraphicsPipeline" />.</returns>
        public static GraphicsPipeline MakePipeline(ref GraphicsPipelineDescriptor descriptor) => GraphicsPInvoke.sg_make_pipeline(ref descriptor);

        /// <summary>
        ///     TODO.
        /// </summary>
        /// <returns>.</returns>
        public static GraphicsShader AllocShader() => GraphicsPInvoke.sg_alloc_shader();

        /// <summary>
        ///     Creates a <see cref="GraphicsShader" /> from the specified <see cref="GraphicsShaderDescriptor" />.
        /// </summary>
        /// <param name="descriptor">The parameters for creating a shader.</param>
        /// <returns>A <see cref="GraphicsShader" />.</returns>
        public static GraphicsShader MakeShader(ref GraphicsShaderDescriptor descriptor) => GraphicsPInvoke.sg_make_shader(ref descriptor);

        /// <summary>
        ///     Gets a <see cref="bool" /> indicating whether the current state of `sokol_gfx` is initialized or not.
        /// </summary>
        /// <returns>
        ///     <c>true</c> when <see cref="Setup" /> was called successfully and <see cref="Shutdown" /> has not yet been
        ///     called; otherwise <c>false</c>.
        /// </returns>
        public static bool IsValid() => GraphicsPInvoke.sg_isvalid();

        /// <summary>
        ///     TODO.
        /// </summary>
        public static void ResetStateCache() => GraphicsPInvoke.sg_reset_state_cache();

        /// <summary>
        ///     Begins and returns the default <see cref="GraphicsPass" /> with the specified width, height, optional clear color,
        ///     optional clear depth, and optional clear stencil.
        /// </summary>
        /// <param name="width">The width of the texture attachments.</param>
        /// <param name="height">The height of the texture attachments.</param>
        /// <param name="clearColor">The color to clear the color texture attachments.</param>
        /// <param name="clearDepth">The depth to clear the depth texture attachment.</param>
        /// <param name="clearStencil">The depth to clear the stencil texture attachment.</param>
        /// <returns>The default <see cref="GraphicsPass" />.</returns>
        public static GraphicsPass BeginDefaultPass(
            int width,
            int height,
            Rgba32F? clearColor = null,
            float? clearDepth = null,
            byte? clearStencil = null)
        {
            var action = default(GraphicsPassAction);

            ref var color = ref action.Color(0);
            if (clearColor == null)
            {
                color.Action = GraphicsPassAttachmentAction.DontCare;
            }
            else
            {
                color.Action = GraphicsPassAttachmentAction.Clear;
                color.Value = clearColor.Value;
            }

            ref var depth = ref action.Depth;
            if (clearDepth == null)
            {
                depth.Action = GraphicsPassAttachmentAction.DontCare;
            }
            else
            {
                depth.Action = GraphicsPassAttachmentAction.Clear;
                depth.Value = clearDepth.Value;
            }

            ref var stencil = ref action.Stencil;
            if (clearStencil == null)
            {
                stencil.Action = GraphicsPassAttachmentAction.DontCare;
            }
            else
            {
                stencil.Action = GraphicsPassAttachmentAction.Clear;
                stencil.Value = clearStencil.Value;
            }

            return BeginDefaultPass(width, height, ref action);
        }

        /// <summary>
        ///     Begins and returns the default <see cref="GraphicsPass" /> with the specified width, height, and
        ///     <see cref="GraphicsPassAction" />.
        /// </summary>
        /// <param name="width">The width of frame buffer.</param>
        /// <param name="height">The height of the frame buffer.</param>
        /// <param name="passAction">The frame buffer pass action.</param>
        /// <returns>The frame buffer <see cref="GraphicsPass" />.</returns>
        public static GraphicsPass BeginDefaultPass(int width, int height, ref GraphicsPassAction passAction)
        {
            GraphicsPInvoke.sg_begin_default_pass(ref passAction, width, height);
            return default;
        }

        /// <summary>
        ///     Execute all scheduled rendering operations for the current frame and marks the end of the current frame.
        /// </summary>
        public static void Commit() => GraphicsPInvoke.sg_commit();

        /// <summary>
        ///     Gets the <see cref="GraphicsPixelFormatInfo" /> of a specified <see cref="GraphicsPixelFormat" />.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>A <see cref="GraphicsPixelFormatInfo" />.</returns>
        public static GraphicsPixelFormatInfo QueryPixelFormat(GraphicsPixelFormat format) => GraphicsPInvoke.sg_query_pixelformat(format);
    }
}
