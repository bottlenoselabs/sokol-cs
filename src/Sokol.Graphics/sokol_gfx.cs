/* The MIT License
 *
 * Copyright (c) 2019 Lucas Girouard-Stranks
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ShiftExpressionRealShiftCountIsZero

namespace Sokol
{
    public static unsafe class sokol_gfx
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_buffer
        {
            public uint id;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_image
        {
            public uint id;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader
        {
            public uint id;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pipeline
        {
            public uint id;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pass
        {
            public uint id;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_context
        {
            public uint id;
        }

        public const int SG_INVALID_ID = 0;
        public const int SG_NUM_SHADER_STAGES = 2;
        public const int SG_NUM_INFLIGHT_FRAMES = 2;
        public const int SG_MAX_COLOR_ATTACHMENTS = 4;
        public const int SG_MAX_SHADERSTAGE_BUFFERS = 8;
        public const int SG_MAX_SHADERSTAGE_IMAGES = 12;
        public const int SG_MAX_SHADERSTAGE_UBS = 4;
        public const int SG_MAX_UB_MEMBERS = 16;
        public const int SG_MAX_VERTEX_ATTRIBUTES = 16;
        public const int SG_MAX_MIPMAPS = 16;
        public const int SG_MAX_TEXTUREARRAY_LAYERS = 128;

        public enum sg_backend
        {
            SG_BACKEND_GLCORE33,
            SG_BACKEND_GLES2,
            SG_BACKEND_GLES3,
            SG_BACKEND_D3D11,
            SG_BACKEND_METAL_IOS,
            SG_BACKEND_METAL_MACOS,
            SG_BACKEND_METAL_SIMULATOR,
            SG_BACKEND_DUMMY,
        }

        public enum sg_pixel_format
        {
            _SG_PIXELFORMAT_DEFAULT, /* value 0 reserved for default-init */
            SG_PIXELFORMAT_NONE,

            SG_PIXELFORMAT_R8,
            SG_PIXELFORMAT_R8SN,
            SG_PIXELFORMAT_R8UI,
            SG_PIXELFORMAT_R8SI,

            SG_PIXELFORMAT_R16,
            SG_PIXELFORMAT_R16SN,
            SG_PIXELFORMAT_R16UI,
            SG_PIXELFORMAT_R16SI,
            SG_PIXELFORMAT_R16F,
            SG_PIXELFORMAT_RG8,
            SG_PIXELFORMAT_RG8SN,
            SG_PIXELFORMAT_RG8UI,
            SG_PIXELFORMAT_RG8SI,

            SG_PIXELFORMAT_R32UI,
            SG_PIXELFORMAT_R32SI,
            SG_PIXELFORMAT_R32F,
            SG_PIXELFORMAT_RG16,
            SG_PIXELFORMAT_RG16SN,
            SG_PIXELFORMAT_RG16UI,
            SG_PIXELFORMAT_RG16SI,
            SG_PIXELFORMAT_RG16F,
            SG_PIXELFORMAT_RGBA8,
            SG_PIXELFORMAT_RGBA8SN,
            SG_PIXELFORMAT_RGBA8UI,
            SG_PIXELFORMAT_RGBA8SI,
            SG_PIXELFORMAT_BGRA8,
            SG_PIXELFORMAT_RGB10A2,
            SG_PIXELFORMAT_RG11B10F,

            SG_PIXELFORMAT_RG32UI,
            SG_PIXELFORMAT_RG32SI,
            SG_PIXELFORMAT_RG32F,
            SG_PIXELFORMAT_RGBA16,
            SG_PIXELFORMAT_RGBA16SN,
            SG_PIXELFORMAT_RGBA16UI,
            SG_PIXELFORMAT_RGBA16SI,
            SG_PIXELFORMAT_RGBA16F,

            SG_PIXELFORMAT_RGBA32UI,
            SG_PIXELFORMAT_RGBA32SI,
            SG_PIXELFORMAT_RGBA32F,

            SG_PIXELFORMAT_DEPTH,
            SG_PIXELFORMAT_DEPTH_STENCIL,

            SG_PIXELFORMAT_BC1_RGBA,
            SG_PIXELFORMAT_BC2_RGBA,
            SG_PIXELFORMAT_BC3_RGBA,
            SG_PIXELFORMAT_BC4_R,
            SG_PIXELFORMAT_BC4_RSN,
            SG_PIXELFORMAT_BC5_RG,
            SG_PIXELFORMAT_BC5_RGSN,
            SG_PIXELFORMAT_BC6H_RGBF,
            SG_PIXELFORMAT_BC6H_RGBUF,
            SG_PIXELFORMAT_BC7_RGBA,
            SG_PIXELFORMAT_PVRTC_RGB_2BPP,
            SG_PIXELFORMAT_PVRTC_RGB_4BPP,
            SG_PIXELFORMAT_PVRTC_RGBA_2BPP,
            SG_PIXELFORMAT_PVRTC_RGBA_4BPP,
            SG_PIXELFORMAT_ETC2_RGB8,
            SG_PIXELFORMAT_ETC2_RGB8A1,

            _SG_PIXELFORMAT_NUM,
            _SG_PIXELFORMAT_FORCE_U32 = 0x7FFFFFFF
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pixelformat_info
        {
            public BlittableBoolean sample;
            public BlittableBoolean filter;
            public BlittableBoolean render;
            public BlittableBoolean blend;
            public BlittableBoolean msaa;
            public BlittableBoolean depth;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_features
        {
            public BlittableBoolean instancing;
            public BlittableBoolean origin_top_left;
            public BlittableBoolean multiple_render_targets;
            public BlittableBoolean msaa_render_targets;
            public BlittableBoolean imagetype_3d;
            public BlittableBoolean imagetype_array;
            public BlittableBoolean image_clamp_to_border;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_limits
        {
            public uint max_image_size_2d;
            public uint max_image_size_cube;
            public uint max_image_size_3d;
            public uint max_image_size_array;
            public uint max_image_array_layers;
            public uint max_vertex_attrs;
        }

        public enum sg_resource_state
        {
            SG_RESOURCESTATE_INITIAL,
            SG_RESOURCESTATE_ALLOC,
            SG_RESOURCESTATE_VALID,
            SG_RESOURCESTATE_FAILED,
            SG_RESOURCESTATE_INVALID,
            _SG_RESOURCESTATE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_usage
        {
            _SG_USAGE_DEFAULT,
            SG_USAGE_IMMUTABLE,
            SG_USAGE_DYNAMIC,
            SG_USAGE_STREAM,
            _SG_USAGE_NUM,
            _SG_USAGE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_buffer_type
        {
            _SG_BUFFERTYPE_DEFAULT,
            SG_BUFFERTYPE_VERTEXBUFFER,
            SG_BUFFERTYPE_INDEXBUFFER,
            _SG_BUFFERTYPE_NUM,
            _SG_BUFFERTYPE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_index_type
        {
            _SG_INDEXTYPE_DEFAULT,
            SG_INDEXTYPE_NONE,
            SG_INDEXTYPE_UINT16,
            SG_INDEXTYPE_UINT32,
            _SG_INDEXTYPE_NUM,
            _SG_INDEXTYPE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_image_type
        {
            _SG_IMAGETYPE_DEFAULT,
            SG_IMAGETYPE_2D,
            SG_IMAGETYPE_CUBE,
            SG_IMAGETYPE_3D,
            SG_IMAGETYPE_ARRAY,
            _SG_IMAGETYPE_NUM,
            _SG_IMAGETYPE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_cube_face
        {
            SG_CUBEFACE_POS_X,
            SG_CUBEFACE_NEG_X,
            SG_CUBEFACE_POS_Y,
            SG_CUBEFACE_NEG_Y,
            SG_CUBEFACE_POS_Z,
            SG_CUBEFACE_NEG_Z,
            SG_CUBEFACE_NUM,
            _SG_CUBEFACE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_shader_stage
        {
            SG_SHADERSTAGE_VS,
            SG_SHADERSTAGE_FS,
            _SG_SHADERSTAGE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_primitive_type
        {
            _SG_PRIMITIVETYPE_DEFAULT,
            SG_PRIMITIVETYPE_POINTS,
            SG_PRIMITIVETYPE_LINES,
            SG_PRIMITIVETYPE_LINE_STRIP,
            SG_PRIMITIVETYPE_TRIANGLES,
            SG_PRIMITIVETYPE_TRIANGLE_STRIP,
            _SG_PRIMITIVETYPE_NUM,
            _SG_PRIMITIVETYPE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_filter
        {
            _SG_FILTER_DEFAULT, /* value 0 reserved for default-init */
            SG_FILTER_NEAREST,
            SG_FILTER_LINEAR,
            SG_FILTER_NEAREST_MIPMAP_NEAREST,
            SG_FILTER_NEAREST_MIPMAP_LINEAR,
            SG_FILTER_LINEAR_MIPMAP_NEAREST,
            SG_FILTER_LINEAR_MIPMAP_LINEAR,
            _SG_FILTER_NUM,
            _SG_FILTER_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_wrap
        {
            _SG_WRAP_DEFAULT,
            SG_WRAP_REPEAT,
            SG_WRAP_CLAMP_TO_EDGE,
            SG_WRAP_CLAMP_TO_BORDER,
            SG_WRAP_MIRRORED_REPEAT,
            _SG_WRAP_NUM,
            _SG_WRAP_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_border_color
        {
            _SG_BORDERCOLOR_DEFAULT, /* value 0 reserved for default-init */
            SG_BORDERCOLOR_TRANSPARENT_BLACK,
            SG_BORDERCOLOR_OPAQUE_BLACK,
            SG_BORDERCOLOR_OPAQUE_WHITE,
            _SG_BORDERCOLOR_NUM,
            _SG_BORDERCOLOR_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_vertex_format
        {
            SG_VERTEXFORMAT_INVALID,
            SG_VERTEXFORMAT_FLOAT,
            SG_VERTEXFORMAT_FLOAT2,
            SG_VERTEXFORMAT_FLOAT3,
            SG_VERTEXFORMAT_FLOAT4,
            SG_VERTEXFORMAT_BYTE4,
            SG_VERTEXFORMAT_BYTE4N,
            SG_VERTEXFORMAT_UBYTE4,
            SG_VERTEXFORMAT_UBYTE4N,
            SG_VERTEXFORMAT_SHORT2,
            SG_VERTEXFORMAT_SHORT2N,
            SG_VERTEXFORMAT_USHORT2N,
            SG_VERTEXFORMAT_SHORT4,
            SG_VERTEXFORMAT_SHORT4N,
            SG_VERTEXFORMAT_USHORT4N,
            SG_VERTEXFORMAT_UINT10_N2,
            _SG_VERTEXFORMAT_NUM,
            _SG_VERTEXFORMAT_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_vertex_step : uint
        {
            _SG_VERTEXSTEP_DEFAULT,
            SG_VERTEXSTEP_PER_VERTEX,
            SG_VERTEXSTEP_PER_INSTANCE,
            _SG_VERTEXSTEP_NUM,
            _SG_VERTEXSTEP_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_uniform_type
        {
            SG_UNIFORMTYPE_INVALID,
            SG_UNIFORMTYPE_FLOAT,
            SG_UNIFORMTYPE_FLOAT2,
            SG_UNIFORMTYPE_FLOAT3,
            SG_UNIFORMTYPE_FLOAT4,
            SG_UNIFORMTYPE_MAT4,
            _SG_UNIFORMTYPE_NUM,
            _SG_UNIFORMTYPE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_cull_mode
        {
            _SG_CULLMODE_DEFAULT,
            SG_CULLMODE_NONE,
            SG_CULLMODE_FRONT,
            SG_CULLMODE_BACK,
            _SG_CULLMODE_NUM,
            _SG_CULLMODE_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_face_winding : uint
        {
            _SG_FACEWINDING_DEFAULT,
            SG_FACEWINDING_CCW,
            SG_FACEWINDING_CW,
            _SG_FACEWINDING_NUM,
            _SG_FACEWINDING_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_compare_func
        {
            _SG_COMPAREFUNC_DEFAULT,
            SG_COMPAREFUNC_NEVER,
            SG_COMPAREFUNC_LESS,
            SG_COMPAREFUNC_EQUAL,
            SG_COMPAREFUNC_LESS_EQUAL,
            SG_COMPAREFUNC_GREATER,
            SG_COMPAREFUNC_NOT_EQUAL,
            SG_COMPAREFUNC_GREATER_EQUAL,
            SG_COMPAREFUNC_ALWAYS,
            _SG_COMPAREFUNC_NUM,
            _SG_COMPAREFUNC_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_stencil_op
        {
            _SG_STENCILOP_DEFAULT,
            SG_STENCILOP_KEEP,
            SG_STENCILOP_ZERO,
            SG_STENCILOP_REPLACE,
            SG_STENCILOP_INCR_CLAMP,
            SG_STENCILOP_DECR_CLAMP,
            SG_STENCILOP_INVERT,
            SG_STENCILOP_INCR_WRAP,
            SG_STENCILOP_DECR_WRAP,
            _SG_STENCILOP_NUM,
            _SG_STENCILOP_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_blend_factor
        {
            _SG_BLENDFACTOR_DEFAULT,
            SG_BLENDFACTOR_ZERO,
            SG_BLENDFACTOR_ONE,
            SG_BLENDFACTOR_SRC_COLOR,
            SG_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
            SG_BLENDFACTOR_SRC_ALPHA,
            SG_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
            SG_BLENDFACTOR_DST_COLOR,
            SG_BLENDFACTOR_ONE_MINUS_DST_COLOR,
            SG_BLENDFACTOR_DST_ALPHA,
            SG_BLENDFACTOR_ONE_MINUS_DST_ALPHA,
            SG_BLENDFACTOR_SRC_ALPHA_SATURATED,
            SG_BLENDFACTOR_BLEND_COLOR,
            SG_BLENDFACTOR_ONE_MINUS_BLEND_COLOR,
            SG_BLENDFACTOR_BLEND_ALPHA,
            SG_BLENDFACTOR_ONE_MINUS_BLEND_ALPHA,
            _SG_BLENDFACTOR_NUM,
            _SG_BLENDFACTOR_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_blend_op
        {
            _SG_BLENDOP_DEFAULT,
            SG_BLENDOP_ADD,
            SG_BLENDOP_SUBTRACT,
            SG_BLENDOP_REVERSE_SUBTRACT,
            _SG_BLENDOP_NUM,
            _SG_BLENDOP_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_color_mask
        {
            _SG_COLORMASK_DEFAULT = 0,
            SG_COLORMASK_NONE = 0x10,
            SG_COLORMASK_R = 1 << 0,
            SG_COLORMASK_G = 1 << 1,
            SG_COLORMASK_B = 1 << 2,
            SG_COLORMASK_A = 1 << 3,
            SG_COLORMASK_RGB = 0x7,
            SG_COLORMASK_RGBA = 0xF,
            _SG_COLORMASK_FORCE_U32 = 0x7FFFFFFF
        }

        public enum sg_action
        {
            _SG_ACTION_DEFAULT,
            SG_ACTION_CLEAR,
            SG_ACTION_LOAD,
            SG_ACTION_DONTCARE,
            _SG_ACTION_NUM,
            _SG_ACTION_FORCE_U32 = 0x7FFFFFFF
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_color_attachment_action
        {
            public sg_action action;
            public fixed float val[4];
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_depth_attachment_action
        {
            public sg_action action;
            public float val;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_stencil_attachment_action
        {
            public sg_action action;
            public byte val;
        }

        //TODO: Make sg_pass_action blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pass_action
        {
            public uint _start_canary;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_COLOR_ATTACHMENTS)]
            public sg_color_attachment_action[] colors;

            public sg_depth_attachment_action depth;
            public sg_stencil_attachment_action stencil;
            public uint _end_canary;
        }

        // TODO: Make sg_bindings blittable with type proper type safety
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_bindings
        {
            public uint _start_canary;
            public fixed uint vertex_buffers[SG_MAX_SHADERSTAGE_BUFFERS];
            public fixed int vertex_buffer_offsets[SG_MAX_SHADERSTAGE_BUFFERS];
            public sg_buffer index_buffer;
            public int index_buffer_offset;
            public fixed uint vs_images[SG_MAX_SHADERSTAGE_IMAGES];
            public fixed uint fs_images[SG_MAX_SHADERSTAGE_IMAGES];
            public uint _end_canary;
        }

        //TODO: Make sg_buffer_desc blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_buffer_desc
        {
            public uint _start_canary;
            public int size;
            public sg_buffer_type type;
            public sg_usage usage;
            public static void* content;
            public static char* label;
            public fixed uint gl_buffers[SG_NUM_INFLIGHT_FRAMES];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_NUM_INFLIGHT_FRAMES)]
            public IntPtr[] mtl_buffers;

            public static void* d3d11_buffer;
            public uint _end_canary;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_subimage_content
        {
            public static void* ptr;
            public int size;
        }

        public struct sg_image_content
        {
            //TODO: Have to use fixed here I guess
            //sg_subimage_content subimage[SG_CUBEFACE_NUM][SG_MAX_MIPMAPS];
        }

        //TODO: Make sg_image_desc blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_image_desc
        {
            public uint _start_canary;
            public sg_image_type type;
            public BlittableBoolean render_target;
            public int width;
            public int height;
            public int depthOrLayers;
            public int num_mipmaps;
            public sg_usage usage;
            public sg_pixel_format pixel_format;
            public int sample_count;
            public sg_filter min_filter;
            public sg_filter mag_filter;
            public sg_wrap wrap_u;
            public sg_wrap wrap_v;
            public sg_wrap wrap_w;
            public sg_border_color border_color;
            public uint max_anisotropy;
            public float min_lod;
            public float max_lod;
            public sg_image_content content;
            public static char* label;
            public fixed uint gl_textures[SG_NUM_INFLIGHT_FRAMES];

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_NUM_INFLIGHT_FRAMES)]
            public IntPtr[] mtl_textures;

            public static void* d3d11_texture;
            public uint _end_canary;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader_attr_desc
        {
            public static char* name;
            public static char* sem_name;
            public int sem_index;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader_uniform_desc
        {
            public static char* name;
            public sg_uniform_type type;
            public int array_count;
        }

        //TODO: Make sg_shader_uniform_block_desc blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader_uniform_block_desc
        {
            public int size;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_UB_MEMBERS)]
            public sg_shader_uniform_desc[] uniforms;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader_image_desc
        {
            public static char* name;
            public sg_image_type type;
        }

        //TODO: Make sg_shader_stage_desc blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader_stage_desc
        {
            public static char* source;
            public static byte* byte_code;
            public int byte_code_size;
            public static char* entry;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_SHADERSTAGE_UBS)]
            public sg_shader_uniform_block_desc[] uniform_blocks;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_SHADERSTAGE_IMAGES)]
            public sg_shader_image_desc[] images;
        }

        //TODO: Make sg_shader_desc blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader_desc
        {
            public uint _start_canary;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_VERTEX_ATTRIBUTES)]
            public sg_shader_attr_desc[] attrs;

            public sg_shader_stage_desc vs;
            public sg_shader_stage_desc fs;
            public static char* label;
            public uint _end_canary;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_buffer_layout_desc
        {
            public int stride;
            public sg_vertex_step step_func;
            public int step_rate;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_vertex_attr_desc
        {
            public int buffer_index;
            public int offset;
            public sg_vertex_format format;
        }

        //TODO: Make sg_layout_desc blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_layout_desc
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_SHADERSTAGE_BUFFERS)]
            public sg_buffer_layout_desc[] buffers;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_VERTEX_ATTRIBUTES)]
            public sg_vertex_attr_desc[] attrs;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_stencil_state
        {
            public sg_stencil_op fail_op;
            public sg_stencil_op depth_fail_op;
            public sg_stencil_op pass_op;
            public sg_compare_func compare_func;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_depth_stencil_state
        {
            public sg_stencil_state stencil_front;
            public sg_stencil_state stencil_back;
            public sg_compare_func depth_compare_func;
            public BlittableBoolean depth_write_enabled;
            public BlittableBoolean stencil_enabled;
            public byte stencil_read_mask;
            public byte stencil_write_mask;
            public byte stencil_ref;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_blend_state
        {
            public BlittableBoolean enabled;
            public sg_blend_factor src_factor_rgb;
            public sg_blend_factor dst_factor_rgb;
            public sg_blend_op op_rgb;
            public sg_blend_factor src_factor_alpha;
            public sg_blend_factor dst_factor_alpha;
            public sg_blend_op op_alpha;
            public byte color_write_mask;
            public int color_attachment_count;
            public sg_pixel_format color_format;
            public sg_pixel_format depth_format;
            public fixed float blend_color[4];
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_rasterizer_state
        {
            public BlittableBoolean alpha_to_coverage_enabled;
            public sg_cull_mode cull_mode;
            public sg_face_winding face_winding;
            public int sample_count;
            public float depth_bias;
            public float depth_bias_slope_scale;
            public float depth_bias_clamp;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pipeline_desc
        {
            public uint _start_canary;
            public sg_layout_desc layout;
            public sg_shader shader;
            public sg_primitive_type primitive_type;
            public sg_index_type index_type;
            public sg_depth_stencil_state depth_stencil;
            public sg_blend_state blend;
            public sg_rasterizer_state rasterizer;
            public static char* label;
            public uint _end_canary;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_attachment_desc
        {
            public sg_image image;
            public int mip_level;
            public int faceOrLayerOrSlice;
        }

        // TODO: Make sg_pass_desc blittable
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pass_desc
        {
            public uint _start_canary;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SG_MAX_COLOR_ATTACHMENTS)]
            public sg_attachment_desc[] color_attachments;

            public sg_attachment_desc depth_stencil_attachment;
            public static char* label;
            public uint _end_canary;
        }

        // TODO: sg_trace_hooks
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_trace_hooks
        {
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_slot_info
        {
            public sg_resource_state state;
            public uint res_id;
            public uint ctx_id;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_buffer_info
        {
            public sg_slot_info slot;
            public uint update_frame_index;
            public uint append_frame_index;
            public int append_pos;
            public BlittableBoolean append_overflow;
            public int num_slots;
            public int active_slot;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_image_info
        {
            public sg_slot_info slot;
            public uint upd_frame_index;
            public int num_slots;
            public int active_slot;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_shader_info
        {
            public sg_slot_info slot;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pipeline_info
        {
            public sg_slot_info slot;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_pass_info
        {
            public sg_slot_info slot;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct sg_desc
        {
            public uint _start_canary;
            public int buffer_pool_size;
            public int image_pool_size;
            public int shader_pool_size;
            public int pipeline_pool_size;
            public int pass_pool_size;
            public int context_pool_size;
            public BlittableBoolean gl_force_gles2;
            public static void* mtl_device;
            public static void* mtl_renderpass_descriptor_cb;
            public static void* mtl_drawable_cb;
            public int mtl_global_uniform_buffer_size;
            public int mtl_sampler_cache_size;
            public static void* d3d11_device;
            public static void* d3d11_device_context;
            public static void* d3d11_render_target_view_cb;
            public static void* d3d11_depth_stencil_view_cb;
            public uint _end_canary;
        }

        public const float SG_DEFAULT_CLEAR_RED = 0.5f;
        public const float SG_DEFAULT_CLEAR_GREEN = 0.5f;
        public const float SG_DEFAULT_CLEAR_BLUE = 0.5f;
        public const float SG_DEFAULT_CLEAR_ALPHA = 1.0f;
        public const float SG_DEFAULT_CLEAR_STENCIL = 0;

        public const uint GL_UNSIGNED_INT_2_10_10_10_REV = 0x8368;
        public const uint GL_UNSIGNED_INT_24_8 = 0x84FA;
        public const uint GL_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FF;
        public const uint GL_COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1;
        public const uint GL_COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2;
        public const uint GL_COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3;
        public const uint GL_COMPRESSED_RED_RGTC1 = 0x8DBB;
        public const uint GL_COMPRESSED_SIGNED_RED_RGTC1 = 0x8DBC;
        public const uint GL_COMPRESSED_RED_GREEN_RGTC2 = 0x8DBD;
        public const uint GL_COMPRESSED_SIGNED_RED_GREEN_RGTC2 = 0x8DBE;
        public const uint GL_COMPRESSED_RGBA_BPTC_UNORM_ARB = 0x8E8C;
        public const uint GL_COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB = 0x8E8D;
        public const uint GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB = 0x8E8F;
        public const uint GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB = 0x83F3;
        public const uint GL_COMPRESSED_RGB_PVRTC_2BPPV1_IMG = 0x8C01;
        public const uint GL_COMPRESSED_RGB_PVRTC_4BPPV1_IMG = 0x8C00;
        public const uint GL_COMPRESSED_RGBA_PVRTC_2BPPV1_IMG = 0x8C03;
        public const uint GL_COMPRESSED_RGBA_PVRTC_4BPPV1_IMG = 0x8C02;
        public const uint GL_COMPRESSED_RGB8_ETC2 = 0x9274;
        public const uint GL_COMPRESSED_RGBA8_ETC2_EAC = 0x9278;
        public const uint GL_DEPTH24_STENCIL8 = 0x88F0;
        public const uint GL_HALF_FLOAT = 0x140B;
        public const uint GL_DEPTH_STENCIL = 0x84F9;
        public const uint GL_LUMINANCE = 0x1909;
    }
}