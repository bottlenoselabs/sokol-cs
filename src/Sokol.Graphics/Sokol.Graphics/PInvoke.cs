// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBeInternal
#nullable disable

namespace Sokol.Graphics
{
    [SuppressMessage("ReSharper", "SA1300", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1310", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1600", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1307", Justification = "C style.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "C style.")]
    [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "C style.")]
    [SuppressMessage("ReSharper", "SA1401", Justification = "API.")]
    internal static class PInvoke
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_activate_context(Context ctx_id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Buffer d_sg_alloc_buffer();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Image d_sg_alloc_image();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Pass d_sg_alloc_pass();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Pipeline d_sg_alloc_pipeline();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Shader d_sg_alloc_shader();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate int d_sg_append_buffer(Buffer buf, [In] IntPtr data_ptr, int data_size);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_apply_bindings([In] ref ResourceBindings bindings);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_apply_pipeline(Pipeline pip);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_apply_scissor_rect(int x, int y, int width, int height, BlittableBoolean origin_top_left);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_apply_uniforms(ShaderStageType stage, int ub_index, [In] IntPtr data, int num_bytes);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_apply_viewport(int x, int y, int width, int height, BlittableBoolean origin_top_left);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_begin_default_pass([In] ref PassAction pass_action, int width, int height);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_begin_pass(Pass pass, [In] ref PassAction pass_action);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_commit();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_destroy_buffer(Buffer buf);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_destroy_image(Image img);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_destroy_pass(Pass pass);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_destroy_pipeline(Pipeline pip);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_destroy_shader(Shader shd);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_discard_context(Context ctx_id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_draw(int base_element, int num_elements, int num_instances);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_end_pass();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_fail_buffer(Buffer buf_id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_fail_image(Image img_id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_fail_pass(Pass pass_id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_fail_pipeline(Pipeline pip_id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_fail_shader(Shader shd_id);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_init_buffer(Buffer buf_id, [In] ref BufferDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_init_image(Image img_id, [In] ref ImageDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_init_pass(Pass pass_id, [In] ref PassDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_init_pipeline(Pipeline pip_id, [In] ref PipelineDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_init_shader(Shader shd_id, [In] ref ShaderDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate TraceHooks d_sg_install_trace_hooks([In] ref TraceHooks trace_hooks);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BlittableBoolean d_sg_isvalid();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Buffer d_sg_make_buffer([In] ref BufferDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Image d_sg_make_image([In] ref ImageDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Pass d_sg_make_pass([In] ref PassDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Pipeline d_sg_make_pipeline([In] ref PipelineDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Shader d_sg_make_shader([In] ref ShaderDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_pop_debug_group([In] IntPtr name);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_push_debug_group([In] IntPtr name);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate sg_backend d_sg_query_backend();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BufferDescriptor d_sg_query_buffer_defaults([In] ref BufferDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BufferInfo d_sg_query_buffer_info(Buffer buf);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate BlittableBoolean d_sg_query_buffer_overflow(Buffer buf);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ResourceState d_sg_query_buffer_state(Buffer buf);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate GraphicsDescriptor d_sg_query_desc();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate GraphicsFeatures d_sg_query_features();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ImageDescriptor d_sg_query_image_defaults([In] ref ImageDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ImageInfo d_sg_query_image_info(Image img);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ResourceState d_sg_query_image_state(Image img);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate GraphicsLimits d_sg_query_limits();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate PassDescriptor d_sg_query_pass_defaults([In] ref PassDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate PassInfo d_sg_query_pass_info(Pass pass);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ResourceState d_sg_query_pass_state(Pass pass);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate PipelineDescriptor d_sg_query_pipeline_defaults([In] ref PipelineDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate PipelineInfo d_sg_query_pipeline_info(Pipeline pip);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ResourceState d_sg_query_pipeline_state(Pipeline pip);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate PixelFormatInfo d_sg_query_pixelformat(PixelFormat fmt);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ShaderDescriptor d_sg_query_shader_defaults([In] ref ShaderDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ShaderInfo d_sg_query_shader_info(Shader shd);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate ResourceState d_sg_query_shader_state(Shader shd);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_reset_state_cache();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_setup([In] ref GraphicsDescriptor desc);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate Context d_sg_setup_context();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_shutdown();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_update_buffer(Buffer buf, [In] IntPtr data_ptr, int data_size);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void d_sg_update_image(Image img, [In] ref ImageContent data);

        public static d_sg_setup sg_setup;

        public static d_sg_shutdown sg_shutdown;

        public static d_sg_isvalid sg_isvalid;

        public static d_sg_reset_state_cache sg_reset_state_cache;

        public static d_sg_install_trace_hooks sg_install_trace_hooks;

        public static d_sg_push_debug_group sg_push_debug_group;

        public static d_sg_pop_debug_group sg_pop_debug_group;

        public static d_sg_make_buffer sg_make_buffer;

        public static d_sg_make_image sg_make_image;

        public static d_sg_make_shader sg_make_shader;

        public static d_sg_make_pipeline sg_make_pipeline;

        public static d_sg_make_pass sg_make_pass;

        public static d_sg_destroy_buffer sg_destroy_buffer;

        public static d_sg_destroy_image sg_destroy_image;

        public static d_sg_destroy_shader sg_destroy_shader;

        public static d_sg_destroy_pipeline sg_destroy_pipeline;

        public static d_sg_destroy_pass sg_destroy_pass;

        public static d_sg_update_buffer sg_update_buffer;

        public static d_sg_update_image sg_update_image;

        public static d_sg_append_buffer sg_append_buffer;

        public static d_sg_query_buffer_overflow sg_query_buffer_overflow;

        public static d_sg_begin_default_pass sg_begin_default_pass;

        public static d_sg_begin_pass sg_begin_pass;

        public static d_sg_apply_viewport sg_apply_viewport;

        public static d_sg_apply_scissor_rect sg_apply_scissor_rect;

        public static d_sg_apply_pipeline sg_apply_pipeline;

        public static d_sg_apply_bindings sg_apply_bindings;

        public static d_sg_apply_uniforms sg_apply_uniforms;

        public static d_sg_draw sg_draw;

        public static d_sg_end_pass sg_end_pass;

        public static d_sg_commit sg_commit;

        public static d_sg_query_desc sg_query_desc;

        public static d_sg_query_backend sg_query_backend;

        public static d_sg_query_features sg_query_features;

        public static d_sg_query_limits sg_query_limits;

        public static d_sg_query_pixelformat sg_query_pixelformat;

        public static d_sg_query_buffer_state sg_query_buffer_state;

        public static d_sg_query_image_state sg_query_image_state;

        public static d_sg_query_shader_state sg_query_shader_state;

        public static d_sg_query_pipeline_state sg_query_pipeline_state;

        public static d_sg_query_pass_state sg_query_pass_state;

        public static d_sg_query_buffer_info sg_query_buffer_info;

        public static d_sg_query_image_info sg_query_image_info;

        public static d_sg_query_shader_info sg_query_shader_info;

        public static d_sg_query_pipeline_info sg_query_pipeline_info;

        public static d_sg_query_pass_info sg_query_pass_info;

        public static d_sg_query_buffer_defaults sg_query_buffer_defaults;

        public static d_sg_query_image_defaults sg_query_image_defaults;

        public static d_sg_query_shader_defaults sg_query_shader_defaults;

        public static d_sg_query_pipeline_defaults sg_query_pipeline_defaults;

        public static d_sg_query_pass_defaults sg_query_pass_defaults;

        public static d_sg_alloc_buffer sg_alloc_buffer;

        public static d_sg_alloc_image sg_alloc_image;

        public static d_sg_alloc_shader sg_alloc_shader;

        public static d_sg_alloc_pipeline sg_alloc_pipeline;

        public static d_sg_alloc_pass sg_alloc_pass;

        public static d_sg_init_buffer sg_init_buffer;

        public static d_sg_init_image sg_init_image;

        public static d_sg_init_shader sg_init_shader;

        public static d_sg_init_pipeline sg_init_pipeline;

        public static d_sg_init_pass sg_init_pass;

        public static d_sg_fail_buffer sg_fail_buffer;

        public static d_sg_fail_image sg_fail_image;

        public static d_sg_fail_shader sg_fail_shader;

        public static d_sg_fail_pipeline sg_fail_pipeline;

        public static d_sg_fail_pass sg_fail_pass;

        public static d_sg_setup_context sg_setup_context;

        public static d_sg_activate_context sg_activate_context;

        public static d_sg_discard_context sg_discard_context;
    }
}
