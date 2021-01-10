// Copyright (c) Lucas Girouard-Stranks (https://github.com/lithiumtoast). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the Git repository root directory (https://github.com/lithiumtoast/sokol-cs) for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1300", Justification = "C style.")]
    internal static unsafe class GraphicsPInvoke
    {
        private const string LibraryName = "sokol";

        [DllImport(LibraryName)]
        public static extern void sg_setup(ref GraphicsDescriptor desc);

        [DllImport(LibraryName)]
        public static extern void sg_shutdown();

        [DllImport(LibraryName)]
        public static extern CBool sg_isvalid();

        [DllImport(LibraryName)]
        public static extern void sg_reset_state_cache();

        [DllImport(LibraryName)]
        public static extern sokol_gfx.sg_trace_hooks sg_install_trace_hooks([In] sokol_gfx.sg_trace_hooks* trace_hooks);

        [DllImport(LibraryName)]
        public static extern void sg_push_debug_group([In] byte* name);

        [DllImport(LibraryName)]
        public static extern void sg_pop_debug_group();

        [DllImport(LibraryName)]
        public static extern GraphicsBuffer sg_make_buffer([In] ref GraphicsBufferDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsImage sg_make_image([In] ref GraphicsImageDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsShader sg_make_shader([In] ref GraphicsShaderDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsPipeline sg_make_pipeline([In] ref GraphicsPipelineDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsPass sg_make_pass([In] ref GraphicsPassDescriptor desc);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_buffer(GraphicsBuffer buf);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_image(GraphicsImage img);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_shader(GraphicsShader shd);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_pipeline(GraphicsPipeline pip);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_pass(GraphicsPass pass);

        [DllImport(LibraryName)]
        public static extern void sg_update_buffer(GraphicsBuffer buf, IntPtr data_ptr, int data_size);

        [DllImport(LibraryName)]
        public static extern void sg_update_image(GraphicsImage img, [In] ref GraphicsImageContent data);

        [DllImport(LibraryName)]
        public static extern int sg_append_buffer(GraphicsBuffer buf, IntPtr data_ptr, int data_size);

        [DllImport(LibraryName)]
        public static extern CBool sg_query_buffer_overflow(GraphicsBuffer buf);

        [DllImport(LibraryName)]
        public static extern void sg_begin_default_pass(ref GraphicsPassAction pass_action, int width, int height);

        [DllImport(LibraryName)]
        public static extern void sg_begin_pass(GraphicsPass pass, [In] ref GraphicsPassAction pass_action);

        [DllImport(LibraryName)]
        public static extern void sg_apply_viewport(int x, int y, int width, int height, CBool origin_top_left);

        [DllImport(LibraryName)]
        public static extern void sg_apply_scissor_rect(int x, int y, int width, int height, CBool origin_top_left);

        [DllImport(LibraryName)]
        public static extern void sg_apply_pipeline(GraphicsPipeline pip);

        [DllImport(LibraryName)]
        public static extern void sg_apply_bindings([In] ref GraphicsResourceBindings bindings);

        [DllImport(LibraryName)]
        public static extern void sg_apply_uniforms(GraphicsShaderStageType stage, int ub_index, [In] void* data, int num_bytes);

        [DllImport(LibraryName)]
        public static extern void sg_draw(int base_element, int num_elements, int num_instances);

        [DllImport(LibraryName)]
        public static extern void sg_end_pass();

        [DllImport(LibraryName)]
        public static extern void sg_commit();

        [DllImport(LibraryName)]
        public static extern GraphicsDescriptor sg_query_desc();

        [DllImport(LibraryName)]
        public static extern sokol_gfx.sg_backend sg_query_backend();

        [DllImport(LibraryName)]
        public static extern GraphicsFeatures sg_query_features();

        [DllImport(LibraryName)]
        public static extern GraphicsLimits sg_query_limits();

        [DllImport(LibraryName)]
        public static extern GraphicsPixelFormatInfo sg_query_pixelformat(GraphicsPixelFormat fmt);

        [DllImport(LibraryName)]
        public static extern GraphicsResourceState sg_query_buffer_state(GraphicsBuffer buf);

        [DllImport(LibraryName)]
        public static extern GraphicsResourceState sg_query_image_state(GraphicsImage img);

        [DllImport(LibraryName)]
        public static extern GraphicsResourceState sg_query_shader_state(GraphicsShader shd);

        [DllImport(LibraryName)]
        public static extern GraphicsResourceState sg_query_pipeline_state(GraphicsPipeline pip);

        [DllImport(LibraryName)]
        public static extern GraphicsResourceState sg_query_pass_state(GraphicsPass pass);

        [DllImport(LibraryName)]
        public static extern GraphicsBufferInfo sg_query_buffer_info(GraphicsBuffer buf);

        [DllImport(LibraryName)]
        public static extern GraphicsImageInfo sg_query_image_info(GraphicsImage img);

        [DllImport(LibraryName)]
        public static extern GraphicsShaderInfo sg_query_shader_info(GraphicsShader shd);

        [DllImport(LibraryName)]
        public static extern GraphicsPipelineInfo sg_query_pipeline_info(GraphicsPipeline pip);

        [DllImport(LibraryName)]
        public static extern GraphicsPassInfo sg_query_pass_info(GraphicsPass pass);

        [DllImport(LibraryName)]
        public static extern GraphicsBufferDescriptor sg_query_buffer_defaults([In] ref GraphicsBufferDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsImageDescriptor sg_query_image_defaults([In] ref GraphicsImageDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsShaderDescriptor sg_query_shader_defaults([In] ref GraphicsShaderDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsPipelineDescriptor sg_query_pipeline_defaults([In] ref GraphicsPipelineDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsPassDescriptor sg_query_pass_defaults(ref GraphicsPassDescriptor desc);

        [DllImport(LibraryName)]
        public static extern GraphicsBuffer sg_alloc_buffer();

        [DllImport(LibraryName)]
        public static extern GraphicsImage sg_alloc_image();

        [DllImport(LibraryName)]
        public static extern GraphicsShader sg_alloc_shader();

        [DllImport(LibraryName)]
        public static extern GraphicsPipeline sg_alloc_pipeline();

        [DllImport(LibraryName)]
        public static extern GraphicsPass sg_alloc_pass();

        [DllImport(LibraryName)]
        public static extern void sg_dealloc_buffer(sokol_gfx.sg_buffer buf_id);

        [DllImport(LibraryName)]
        public static extern void sg_dealloc_image(sokol_gfx.sg_image img_id);

        [DllImport(LibraryName)]
        public static extern void sg_dealloc_shader(sokol_gfx.sg_shader shd_id);

        [DllImport(LibraryName)]
        public static extern void sg_dealloc_pipeline(sokol_gfx.sg_pipeline pip_id);

        [DllImport(LibraryName)]
        public static extern void sg_dealloc_pass(sokol_gfx.sg_pass pass_id);

        [DllImport(LibraryName)]
        public static extern void sg_init_buffer(GraphicsBuffer buf_id, ref GraphicsBufferDescriptor desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_image(GraphicsImage img_id, ref GraphicsImageDescriptor desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_shader(GraphicsShader shd_id, [In] ref GraphicsShaderDescriptor desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_pipeline(GraphicsPipeline pip_id, [In] ref GraphicsPipelineDescriptor desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_pass(GraphicsPass pass_id, ref GraphicsPassDescriptor desc);

        [DllImport(LibraryName)]
        public static extern CBool sg_uninit_buffer(sokol_gfx.sg_buffer buf_id);

        [DllImport(LibraryName)]
        public static extern CBool sg_uninit_image(sokol_gfx.sg_image img_id);

        [DllImport(LibraryName)]
        public static extern CBool sg_uninit_shader(sokol_gfx.sg_shader shd_id);

        [DllImport(LibraryName)]
        public static extern CBool sg_uninit_pipeline(sokol_gfx.sg_pipeline pip_id);

        [DllImport(LibraryName)]
        public static extern CBool sg_uninit_pass(sokol_gfx.sg_pass pass_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_buffer(GraphicsBuffer buf_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_image(GraphicsImage img_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_shader(GraphicsShader shd_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_pipeline(GraphicsPipeline pip_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_pass(GraphicsPass pass_id);

        [DllImport(LibraryName)]
        public static extern GraphicsContext sg_setup_context();

        [DllImport(LibraryName)]
        public static extern void sg_activate_context(GraphicsContext ctx_id);

        [DllImport(LibraryName)]
        public static extern void sg_discard_context(GraphicsContext ctx_id);

        [DllImport(LibraryName)]
        public static extern void* sg_d3d11_device();

        [DllImport(LibraryName)]
        public static extern void* sg_mtl_device();

        [DllImport(LibraryName)]
        public static extern void* sg_mtl_render_command_encoder();
    }
}
