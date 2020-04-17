// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "C style")]
    [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "C style")]
    [SuppressMessage("ReSharper", "SA1300", Justification = "C style")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "C style")]
    [SuppressMessage("ReSharper", "SA1600", Justification = "C style")]
    public static class PInvoke
    {
        private const string LIBRARY_NAME = "sokol_gfx";

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_setup([In] ref GraphicsDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_shutdown();

        [DllImport(LIBRARY_NAME)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool sg_isvalid();

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_reset_state_cache();

        [DllImport(LIBRARY_NAME)]
        public static extern TraceHooks sg_install_trace_hooks([In] ref TraceHooks trace_hooks);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_push_debug_group(IntPtr name);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_pop_debug_group();

        [DllImport(LIBRARY_NAME)]
        public static extern Buffer sg_make_buffer([In] ref BufferDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern Image sg_make_image([In] ref ImageDescriptor desc);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern Shader sg_make_shader([In] ref ShaderDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern Pipeline sg_make_pipeline([In] ref PipelineDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern Pass sg_make_pass([In] ref PassDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_destroy_buffer(Buffer buf);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_destroy_image(Image img);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_destroy_shader(Shader shd);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_destroy_pipeline(Pipeline pip);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_destroy_pass(Pass pass);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_update_buffer(Buffer buf, IntPtr data_ptr, int data_size);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_update_image(Image img, [In] ref ImageContent data);

        [DllImport(LIBRARY_NAME)]
        public static extern int sg_append_buffer(Buffer buf, IntPtr data_ptr, int data_size);

        [DllImport(LIBRARY_NAME)]
        public static extern bool sg_query_buffer_overflow(Buffer buf);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_begin_default_pass([In] ref PassAction pass_action, int width, int height);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_begin_pass(Pass pass, [In] ref PassAction pass_action);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_apply_viewport(int x, int y, int width, int height, bool origin_top_left);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_apply_scissor_rect(int x, int y, int width, int height, bool origin_top_left);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_apply_pipeline(Pipeline pip);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_apply_bindings([In] ref ResourceBindings bindings);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_apply_uniforms(ShaderStageType stage, int ub_index, IntPtr data, int num_bytes);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_draw(int base_element, int num_elements, int num_instances);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_end_pass();

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_commit();

        [DllImport(LIBRARY_NAME)]
        public static extern GraphicsDescriptor sg_query_desc();

        [DllImport(LIBRARY_NAME)]
        public static extern sokol_gfx.sg_backend sg_query_backend();

        [DllImport(LIBRARY_NAME)]
        public static extern GraphicsFeatures sg_query_features();

        [DllImport(LIBRARY_NAME)]
        public static extern GraphicsLimits sg_query_limits();

        [DllImport(LIBRARY_NAME)]
        public static extern PixelFormatInfo sg_query_pixelformat(PixelFormat fmt);

        [DllImport(LIBRARY_NAME)]
        public static extern ResourceState sg_query_buffer_state(Buffer buf);

        [DllImport(LIBRARY_NAME)]
        public static extern ResourceState sg_query_image_state(Image img);

        [DllImport(LIBRARY_NAME)]
        public static extern ResourceState sg_query_shader_state(Shader shd);

        [DllImport(LIBRARY_NAME)]
        public static extern ResourceState sg_query_pipeline_state(Pipeline pip);

        [DllImport(LIBRARY_NAME)]
        public static extern ResourceState sg_query_pass_state(Pass pass);

        [DllImport(LIBRARY_NAME)]
        public static extern BufferInfo sg_query_buffer_info(Buffer buf);

        [DllImport(LIBRARY_NAME)]
        public static extern ImageInfo sg_query_image_info(Image img);

        [DllImport(LIBRARY_NAME)]
        public static extern ShaderInfo sg_query_shader_info(Shader shd);

        [DllImport(LIBRARY_NAME)]
        public static extern PipelineInfo sg_query_pipeline_info(Pipeline pip);

        [DllImport(LIBRARY_NAME)]
        public static extern PassInfo sg_query_pass_info(Pass pass);

        [DllImport(LIBRARY_NAME)]
        public static extern BufferDescriptor sg_query_buffer_defaults([In] ref BufferDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern ImageDescriptor sg_query_image_defaults([In] ref ImageDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern ShaderDescriptor sg_query_shader_defaults([In] ref ShaderDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern PipelineDescriptor sg_query_pipeline_defaults([In] ref PipelineDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern PassDescriptor sg_query_pass_defaults([In] ref PassDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern Buffer sg_alloc_buffer();

        [DllImport(LIBRARY_NAME)]
        public static extern Image sg_alloc_image();

        [DllImport(LIBRARY_NAME)]
        public static extern Shader sg_alloc_shader();

        [DllImport(LIBRARY_NAME)]
        public static extern Pipeline sg_alloc_pipeline();

        [DllImport(LIBRARY_NAME)]
        public static extern Pass sg_alloc_pass();

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_init_buffer(Buffer buf_id, [In] ref BufferDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_init_image(Image img_id, [In] ref ImageDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_init_shader(Shader shd_id, [In] ref ShaderDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_init_pipeline(Pipeline pip_id, [In] ref PipelineDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_init_pass(Pass pass_id, [In] ref PassDescriptor desc);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_fail_buffer(Buffer buf_id);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_fail_image(Image img_id);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_fail_shader(Shader shd_id);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_fail_pipeline(Pipeline pip_id);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_fail_pass(Pass pass_id);

        [DllImport(LIBRARY_NAME)]
        public static extern Context sg_setup_context();

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_activate_context(Context ctx_id);

        [DllImport(LIBRARY_NAME)]
        public static extern void sg_discard_context(Context ctx_id);
    }
}
