// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ShiftExpressionRealShiftCountIsZero
/// <summary>
///     The structs, enums, and static methods of `sokol_gfx`. Everything in this module exactly matches what is in
///     C, and the naming conventions used in C are maintained. For documentation see the comments in the
///     <a href="https://github.com/floooh/sokol/blob/master/sokol_gfx.h">`sokol_gfx.h` source code</a>.
/// </summary>
/// <remarks>
///     <para>
///         For practicality, it's recommended to import the module like so:
///         <c>using static sokol_gfx;</c>.
///     </para>
/// </remarks>
[SuppressMessage("ReSharper", "SA1204", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1300", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1307", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1310", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1600", Justification = "C style code.")]
[SuppressMessage("ReSharper", "SA1602", Justification = "C style code.")]
public static unsafe class sokol_gfx
{
    public enum sg_action : uint
    {
        _SG_ACTION_DEFAULT,
        SG_ACTION_CLEAR,
        SG_ACTION_LOAD,
        SG_ACTION_DONTCARE,
        _SG_ACTION_NUM,
        _SG_ACTION_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_backend : byte
    {
        SG_BACKEND_GLCORE33,
        SG_BACKEND_GLES2,
        SG_BACKEND_GLES3,
        SG_BACKEND_D3D11,
        SG_BACKEND_METAL_IOS,
        SG_BACKEND_METAL_MACOS,
        SG_BACKEND_METAL_SIMULATOR,
        SG_BACKEND_DUMMY
    }

    public enum sg_blend_factor : uint
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

    public enum sg_blend_op : uint
    {
        _SG_BLENDOP_DEFAULT,
        SG_BLENDOP_ADD,
        SG_BLENDOP_SUBTRACT,
        SG_BLENDOP_REVERSE_SUBTRACT,
        _SG_BLENDOP_NUM,
        _SG_BLENDOP_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_border_color : uint
    {
        _SG_BORDERCOLOR_DEFAULT,
        SG_BORDERCOLOR_TRANSPARENT_BLACK,
        SG_BORDERCOLOR_OPAQUE_BLACK,
        SG_BORDERCOLOR_OPAQUE_WHITE,
        _SG_BORDERCOLOR_NUM,
        _SG_BORDERCOLOR_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_buffer_type : uint
    {
        _SG_BUFFERTYPE_DEFAULT,
        SG_BUFFERTYPE_VERTEXBUFFER,
        SG_BUFFERTYPE_INDEXBUFFER,
        _SG_BUFFERTYPE_NUM,
        _SG_BUFFERTYPE_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_color_mask : uint
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

    public enum sg_compare_func : uint
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

    public enum sg_cube_face : uint
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

    public enum sg_cull_mode : uint
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

    public enum sg_filter : uint
    {
        _SG_FILTER_DEFAULT,
        SG_FILTER_NEAREST,
        SG_FILTER_LINEAR,
        SG_FILTER_NEAREST_MIPMAP_NEAREST,
        SG_FILTER_NEAREST_MIPMAP_LINEAR,
        SG_FILTER_LINEAR_MIPMAP_NEAREST,
        SG_FILTER_LINEAR_MIPMAP_LINEAR,
        _SG_FILTER_NUM,
        _SG_FILTER_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_image_type : uint
    {
        _SG_IMAGETYPE_DEFAULT,
        SG_IMAGETYPE_2D,
        SG_IMAGETYPE_CUBE,
        SG_IMAGETYPE_3D,
        SG_IMAGETYPE_ARRAY,
        _SG_IMAGETYPE_NUM,
        _SG_IMAGETYPE_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_index_type : uint
    {
        _SG_INDEXTYPE_DEFAULT,
        SG_INDEXTYPE_NONE,
        SG_INDEXTYPE_UINT16,
        SG_INDEXTYPE_UINT32,
        _SG_INDEXTYPE_NUM,
        _SG_INDEXTYPE_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_pixel_format : uint
    {
        _SG_PIXELFORMAT_DEFAULT,
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

    public enum sg_primitive_type : uint
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

    public enum sg_resource_state : uint
    {
        SG_RESOURCESTATE_INITIAL,
        SG_RESOURCESTATE_ALLOC,
        SG_RESOURCESTATE_VALID,
        SG_RESOURCESTATE_FAILED,
        SG_RESOURCESTATE_INVALID,
        _SG_RESOURCESTATE_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_shader_stage : uint
    {
        SG_SHADERSTAGE_VS,
        SG_SHADERSTAGE_FS,
        _SG_SHADERSTAGE_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_stencil_op : uint
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

    public enum sg_uniform_type : uint
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

    public enum sg_usage : uint
    {
        _SG_USAGE_DEFAULT,
        SG_USAGE_IMMUTABLE,
        SG_USAGE_DYNAMIC,
        SG_USAGE_STREAM,
        _SG_USAGE_NUM,
        _SG_USAGE_FORCE_U32 = 0x7FFFFFFF
    }

    public enum sg_vertex_format : uint
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

    public enum sg_wrap : uint
    {
        _SG_WRAP_DEFAULT,
        SG_WRAP_REPEAT,
        SG_WRAP_CLAMP_TO_EDGE,
        SG_WRAP_CLAMP_TO_BORDER,
        SG_WRAP_MIRRORED_REPEAT,
        _SG_WRAP_NUM,
        _SG_WRAP_FORCE_U32 = 0x7FFFFFFF
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const float SG_DEFAULT_CLEAR_RED = 0.5f;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const float SG_DEFAULT_CLEAR_GREEN = 0.5f;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const float SG_DEFAULT_CLEAR_BLUE = 0.5f;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const float SG_DEFAULT_CLEAR_ALPHA = 1.0f;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const float SG_DEFAULT_CLEAR_STENCIL = 0;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_UNSIGNED_INT_2_10_10_10_REV = 0x8368;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_UNSIGNED_INT_24_8 = 0x84FA;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FF;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RED_RGTC1 = 0x8DBB;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_SIGNED_RED_RGTC1 = 0x8DBC;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RED_GREEN_RGTC2 = 0x8DBD;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_SIGNED_RED_GREEN_RGTC2 = 0x8DBE;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGBA_BPTC_UNORM_ARB = 0x8E8C;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB = 0x8E8D;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB = 0x8E8F;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB = 0x83F3;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGB_PVRTC_2BPPV1_IMG = 0x8C01;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGB_PVRTC_4BPPV1_IMG = 0x8C00;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGBA_PVRTC_2BPPV1_IMG = 0x8C03;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGBA_PVRTC_4BPPV1_IMG = 0x8C02;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGB8_ETC2 = 0x9274;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_COMPRESSED_RGBA8_ETC2_EAC = 0x9278;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_DEPTH24_STENCIL8 = 0x88F0;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_HALF_FLOAT = 0x140B;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_DEPTH_STENCIL = 0x84F9;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const uint GL_LUMINANCE = 0x1909;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_STRING_SIZE = 16;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_SLOT_SHIFT = 16;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_SLOT_MASK = 65535;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_MAX_POOL_SIZE = 65536;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_DEFAULT_BUFFER_POOL_SIZE = 128;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_DEFAULT_IMAGE_POOL_SIZE = 128;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_DEFAULT_SHADER_POOL_SIZE = 32;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_DEFAULT_PIPELINE_POOL_SIZE = 64;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_DEFAULT_PASS_POOL_SIZE = 16;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_DEFAULT_CONTEXT_POOL_SIZE = 16;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_MTL_DEFAULT_UB_SIZE = 4 * 1024 * 1024;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public const int _SG_MTL_DEFAULT_SAMPLER_CACHE_CAPACITY = 64;

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct sg_buffer
    {
        [FieldOffset(0)]
        public uint id;

        public static implicit operator sg_buffer(uint value)
        {
            return new sg_buffer
            {
                id = value
            };
        }

        public static implicit operator uint(sg_buffer buffer)
        {
            return buffer.id;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct sg_image
    {
        [FieldOffset(0)]
        public uint id;

        public static implicit operator sg_image(uint value)
        {
            return new sg_image
            {
                id = value
            };
        }

        public static implicit operator uint(sg_image image)
        {
            return image.id;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct sg_shader
    {
        [FieldOffset(0)]
        public uint id;

        public static implicit operator sg_shader(uint value)
        {
            return new sg_shader
            {
                id = value
            };
        }

        public static implicit operator uint(sg_shader image)
        {
            return image.id;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct sg_pipeline
    {
        [FieldOffset(0)]
        public uint id;

        public static implicit operator sg_pipeline(uint value)
        {
            return new sg_pipeline
            {
                id = value
            };
        }

        public static implicit operator uint(sg_pipeline pipeline)
        {
            return pipeline.id;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct sg_pass
    {
        [FieldOffset(0)]
        public uint id;

        public static implicit operator sg_pass(uint value)
        {
            return new sg_pass
            {
                id = value
            };
        }

        public static implicit operator uint(sg_pass pass)
        {
            return pass.id;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct sg_context
    {
        [FieldOffset(0)]
        public uint id;
    }

    [StructLayout(LayoutKind.Explicit, Size = 6, Pack = 1)]
    public struct sg_pixelformat_info
    {
        [FieldOffset(0)]
        public BlittableBoolean sample;

        [FieldOffset(1)]
        public BlittableBoolean filter;

        [FieldOffset(2)]
        public BlittableBoolean render;

        [FieldOffset(3)]
        public BlittableBoolean blend;

        [FieldOffset(4)]
        public BlittableBoolean msaa;

        [FieldOffset(5)]
        public BlittableBoolean depth;
    }

    [StructLayout(LayoutKind.Explicit, Size = 7, Pack = 1)]
    public struct sg_features
    {
        [FieldOffset(0)]
        public BlittableBoolean instancing;

        [FieldOffset(1)]
        public BlittableBoolean origin_top_left;

        [FieldOffset(2)]
        public BlittableBoolean multiple_render_targets;

        [FieldOffset(3)]
        public BlittableBoolean msaa_render_targets;

        [FieldOffset(4)]
        public BlittableBoolean imagetype_3d;

        [FieldOffset(5)]
        public BlittableBoolean imagetype_array;

        [FieldOffset(6)]
        public BlittableBoolean image_clamp_to_border;
    }

    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public struct sg_limits
    {
        [FieldOffset(0)]
        public uint max_image_size_2d;

        [FieldOffset(4)]
        public uint max_image_size_cube;

        [FieldOffset(8)]
        public uint max_image_size_3d;

        [FieldOffset(12)]
        public uint max_image_size_array;

        [FieldOffset(16)]
        public uint max_image_array_layers;

        [FieldOffset(20)]
        public uint max_vertex_attrs;
    }

    [StructLayout(LayoutKind.Explicit, Size = 20, Pack = 4)]
    public struct sg_color_attachment_action
    {
        [FieldOffset(0)]
        public sg_action action;

        [FieldOffset(4)]
        public Vector4 val;
    }

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public struct sg_depth_attachment_action
    {
        [FieldOffset(0)]
        public sg_action action;

        [FieldOffset(4)]
        public float val;
    }

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public struct sg_stencil_attachment_action
    {
        [FieldOffset(0)]
        public sg_action action;

        [FieldOffset(4)]
        public byte val;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 4)]
    public struct sg_pass_action
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(4)]
        public fixed int _colors[20 * SG_MAX_COLOR_ATTACHMENTS / 4];

        [FieldOffset(84)]
        public sg_depth_attachment_action depth;

        [FieldOffset(92)]
        public sg_stencil_attachment_action stencil;

        [FieldOffset(100)]
        public uint _end_canary;

        public ref sg_color_attachment_action color(int index)
        {
            fixed (sg_pass_action* pass_action = &this)
            {
                var ptr = (sg_color_attachment_action*)&pass_action->_colors[0];
                return ref *(ptr + index);
            }
        }

        public static sg_pass_action clear(Vector4? color = null)
        {
            var passAction = default(sg_pass_action);
            ref var colorAttachment0 = ref passAction.color(0);
            colorAttachment0.action = sg_action.SG_ACTION_CLEAR;
            colorAttachment0.val = color ?? new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            return passAction;
        }

        public static sg_pass_action dontCare()
        {
            var passAction = default(sg_pass_action);
            for (var i = 0; i < SG_MAX_COLOR_ATTACHMENTS; i++)
            {
                passAction.color(i).action = sg_action.SG_ACTION_DONTCARE;
            }

            passAction.depth.action = sg_action.SG_ACTION_DONTCARE;
            passAction.stencil.action = sg_action.SG_ACTION_DONTCARE;
            return passAction;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 176, Pack = 4)]
    public struct sg_bindings
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(4)]
        public fixed uint _vertex_buffers[SG_MAX_SHADERSTAGE_BUFFERS];

        [FieldOffset(36)]
        public fixed int _vertex_buffer_offsets[SG_MAX_SHADERSTAGE_BUFFERS];

        [FieldOffset(68)]
        public sg_buffer index_buffer;

        [FieldOffset(72)]
        public int index_buffer_offset;

        [FieldOffset(76)]
        public fixed uint _vs_images[SG_MAX_SHADERSTAGE_IMAGES];

        [FieldOffset(124)]
        public fixed uint _fs_images[SG_MAX_SHADERSTAGE_IMAGES];

        [FieldOffset(172)]
        public uint _end_canary;

        public ref sg_buffer vertex_buffer(int index)
        {
            fixed (sg_bindings* bindings = &this)
            {
                var ptr = (sg_buffer*)&bindings->_vertex_buffers[0];
                return ref *(ptr + index);
            }
        }

        public ref int vertex_buffer_offset(int index)
        {
            return ref _vertex_buffer_offsets[index];
        }

        public ref sg_image vs_image(int index)
        {
            fixed (sg_bindings* bindings = &this)
            {
                var ptr = (sg_image*)&bindings->_vs_images[0];
                return ref *(ptr + index);
            }
        }

        public ref sg_image fs_image(int index)
        {
            fixed (sg_bindings* bindings = &this)
            {
                var ptr = (sg_image*)&bindings->_fs_images[0];
                return ref *(ptr + index);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 72, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_buffer_desc
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(4)]
        public int size;

        [FieldOffset(8)]
        public sg_buffer_type type;

        [FieldOffset(12)]
        public sg_usage usage;

        [FieldOffset(16)]
        public void* content;

        [FieldOffset(24)]
        public byte* label;

        [FieldOffset(32)]
        public fixed uint gl_buffers[SG_NUM_INFLIGHT_FRAMES];

        [FieldOffset(40)]
        public fixed ulong _mtl_buffers[SG_NUM_INFLIGHT_FRAMES];

        [FieldOffset(56)]
        public void* d3d11_buffer;

        [FieldOffset(64)]
        public uint _end_canary;

        public void* GetMTLBuffers()
        {
            fixed (sg_buffer_desc* buffer_desc = &this)
            {
                return &buffer_desc->_mtl_buffers[0];
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    public struct sg_subimage_content
    {
        [FieldOffset(0)]
        public void* ptr;

        [FieldOffset(8)]
        public int size;
    }

    [StructLayout(LayoutKind.Explicit, Size = 1536, Pack = 8)]
    public struct sg_image_content
    {
        [FieldOffset(0)]
        public fixed ulong _subimage[16 * (int)sg_cube_face.SG_CUBEFACE_NUM * SG_MAX_MIPMAPS / 8];

        public ref sg_subimage_content subimage(int cubeFaceIndex, int mipMapIndex)
        {
            fixed (sg_image_content* image_content = &this)
            {
                var ptr = (sg_subimage_content*)&image_content->_subimage[0];
                var pointerOffset = (cubeFaceIndex * (int)sg_cube_face.SG_CUBEFACE_NUM) + mipMapIndex;
                return ref *(ptr + pointerOffset);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 1664, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_image_desc
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(4)]
        public sg_image_type type;

        [FieldOffset(8)]
        public BlittableBoolean render_target;

        [FieldOffset(12)]
        public int width;

        [FieldOffset(16)]
        public int height;

        [FieldOffset(20)]
        public int depth;

        [FieldOffset(20)]
        public int layers;

        [FieldOffset(24)]
        public int num_mipmaps;

        [FieldOffset(28)]
        public sg_usage usage;

        [FieldOffset(32)]
        public sg_pixel_format pixel_format;

        [FieldOffset(36)]
        public int sample_count;

        [FieldOffset(40)]
        public sg_filter min_filter;

        [FieldOffset(44)]
        public sg_filter mag_filter;

        [FieldOffset(48)]
        public sg_wrap wrap_u;

        [FieldOffset(52)]
        public sg_wrap wrap_v;

        [FieldOffset(56)]
        public sg_wrap wrap_w;

        [FieldOffset(60)]
        public sg_border_color border_color;

        [FieldOffset(64)]
        public uint max_anisotropy;

        [FieldOffset(68)]
        public float min_lod;

        [FieldOffset(72)]
        public float max_lod;

        [FieldOffset(80)]
        public sg_image_content content;

        [FieldOffset(1616)]
        public byte* label;

        [FieldOffset(1624)]
        public fixed uint gl_textures[SG_NUM_INFLIGHT_FRAMES];

        [FieldOffset(1632)]
        public fixed ulong _mtl_textures[SG_NUM_INFLIGHT_FRAMES];

        [FieldOffset(1648)]
        public void* d3d11_texture;

        [FieldOffset(1656)]
        public uint _end_canary;

        public void* mtlTextures()
        {
            fixed (sg_image_desc* image_desc = &this)
            {
                return &image_desc->_mtl_textures[0];
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_shader_attr_desc
    {
        [FieldOffset(0)]
        public byte* name;

        [FieldOffset(8)]
        public byte* sem_name;

        [FieldOffset(16)]
        public int sem_index;
    }

    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_shader_uniform_desc
    {
        [FieldOffset(0)]
        public byte* name;

        [FieldOffset(8)]
        public sg_uniform_type type;

        [FieldOffset(12)]
        public int array_count;
    }

    [StructLayout(LayoutKind.Explicit, Size = 264, Pack = 8)]
    public struct sg_shader_uniform_block_desc
    {
        [FieldOffset(0)]
        public int size;

        [FieldOffset(8)]
        public fixed ulong _uniforms[16 * SG_MAX_UB_MEMBERS / 8];

        public ref sg_shader_uniform_desc uniform(int index)
        {
            fixed (sg_shader_uniform_block_desc* shader_uniform_block_desc = &this)
            {
                var ptr = (sg_shader_uniform_desc*)&shader_uniform_block_desc->_uniforms[0];
                return ref *(ptr + index);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_shader_image_desc
    {
        [FieldOffset(0)]
        public byte* name;

        [FieldOffset(8)]
        public sg_image_type type;
    }

    [StructLayout(LayoutKind.Explicit, Size = 1280, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_shader_stage_desc
    {
        [FieldOffset(0)]
        public byte* source;

        [FieldOffset(8)]
        public byte* byte_code;

        [FieldOffset(16)]
        public int byte_code_size;

        [FieldOffset(24)]
        public byte* entry;

        [FieldOffset(32)]
        public fixed ulong _uniform_blocks[264 * SG_MAX_SHADERSTAGE_UBS / 8];

        [FieldOffset(1088)]
        public fixed ulong _images[16 * SG_MAX_SHADERSTAGE_IMAGES / 8];

        public ref sg_shader_uniform_block_desc uniformBlock(int index)
        {
            fixed (sg_shader_stage_desc* sg_shader_stage_desc = &this)
            {
                var ptr = (sg_shader_uniform_block_desc*)&sg_shader_stage_desc->_uniform_blocks[0];
                return ref *(ptr + index);
            }
        }

        public ref sg_shader_image_desc image(int index)
        {
            fixed (sg_shader_stage_desc* sg_shader_stage_desc = &this)
            {
                var ptr = (sg_shader_image_desc*)&sg_shader_stage_desc->_images[0];
                return ref *(ptr + index);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 2968, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_shader_desc
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(8)]
        public fixed ulong _attrs[24 * SG_MAX_VERTEX_ATTRIBUTES / 8];

        [FieldOffset(392)]
        public sg_shader_stage_desc vs;

        [FieldOffset(1672)]
        public sg_shader_stage_desc fs;

        [FieldOffset(2952)]
        public byte* label;

        [FieldOffset(2960)]
        public uint _end_canary;

        public ref sg_shader_attr_desc attr(int index)
        {
            fixed (sg_shader_desc* sg_shader_stage_desc = &this)
            {
                var ptr = (sg_shader_attr_desc*)&sg_shader_stage_desc->_attrs[0];
                return ref *(ptr + index);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct sg_buffer_layout_desc
    {
        [FieldOffset(0)]
        public int stride;

        [FieldOffset(4)]
        public sg_vertex_step step_func;

        [FieldOffset(8)]
        public int step_rate;
    }

    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct sg_vertex_attr_desc
    {
        [FieldOffset(0)]
        public int buffer_index;

        [FieldOffset(4)]
        public int offset;

        [FieldOffset(8)]
        public sg_vertex_format format;
    }

    [StructLayout(LayoutKind.Explicit, Size = 288, Pack = 4)]
    public struct sg_layout_desc
    {
        [FieldOffset(0)]
        public fixed int _buffers[12 * SG_MAX_SHADERSTAGE_BUFFERS / 4];

        [FieldOffset(96)]
        public fixed int _attrs[12 * SG_MAX_VERTEX_ATTRIBUTES / 4];

        public ref sg_buffer_layout_desc buffer(int index)
        {
            fixed (sg_layout_desc* layout_desc = &this)
            {
                var ptr = (sg_buffer_layout_desc*)&layout_desc->_buffers[0];
                return ref *(ptr + index);
            }
        }

        public ref sg_vertex_attr_desc attr(int index)
        {
            fixed (sg_layout_desc* layout_desc = &this)
            {
                var ptr = (sg_vertex_attr_desc*)&layout_desc->_attrs[0];
                return ref *(ptr + index);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public struct sg_stencil_state
    {
        [FieldOffset(0)]
        public sg_stencil_op fail_op;

        [FieldOffset(4)]
        public sg_stencil_op depth_fail_op;

        [FieldOffset(8)]
        public sg_stencil_op pass_op;

        [FieldOffset(12)]
        public sg_compare_func compare_func;
    }

    [StructLayout(LayoutKind.Explicit, Size = 44, Pack = 4)]
    public struct sg_depth_stencil_state
    {
        [FieldOffset(0)]
        public sg_stencil_state stencil_front;

        [FieldOffset(16)]
        public sg_stencil_state stencil_back;

        [FieldOffset(32)]
        public sg_compare_func depth_compare_func;

        [FieldOffset(36)]
        public BlittableBoolean depth_write_enabled;

        [FieldOffset(37)]
        public BlittableBoolean stencil_enabled;

        [FieldOffset(38)]
        public byte stencil_read_mask;

        [FieldOffset(39)]
        public byte stencil_write_mask;

        [FieldOffset(40)]
        public byte stencil_ref;
    }

    [StructLayout(LayoutKind.Explicit, Size = 60, Pack = 4)]
    public struct sg_blend_state
    {
        [FieldOffset(0)]
        public BlittableBoolean enabled;

        [FieldOffset(4)]
        public sg_blend_factor src_factor_rgb;

        [FieldOffset(8)]
        public sg_blend_factor dst_factor_rgb;

        [FieldOffset(12)]
        public sg_blend_op op_rgb;

        [FieldOffset(16)]
        public sg_blend_factor src_factor_alpha;

        [FieldOffset(20)]
        public sg_blend_factor dst_factor_alpha;

        [FieldOffset(24)]
        public sg_blend_op op_alpha;

        [FieldOffset(28)]
        public byte color_write_mask;

        [FieldOffset(32)]
        public int color_attachment_count;

        [FieldOffset(36)]
        public sg_pixel_format color_format;

        [FieldOffset(40)]
        public sg_pixel_format depth_format;

        [FieldOffset(44)]
        public Vector4 blend_color;
    }

    [StructLayout(LayoutKind.Explicit, Size = 28, Pack = 4)]
    public struct sg_rasterizer_state
    {
        [FieldOffset(0)]
        public BlittableBoolean alpha_to_coverage_enabled;

        [FieldOffset(4)]
        public sg_cull_mode cull_mode;

        [FieldOffset(8)]
        public sg_face_winding face_winding;

        [FieldOffset(12)]
        public int sample_count;

        [FieldOffset(16)]
        public float depth_bias;

        [FieldOffset(20)]
        public float depth_bias_slope_scale;

        [FieldOffset(24)]
        public float depth_bias_clamp;
    }

    [StructLayout(LayoutKind.Explicit, Size = 456, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_pipeline_desc
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(4)]
        public sg_layout_desc layout;

        [FieldOffset(292)]
        public sg_shader shader;

        [FieldOffset(296)]
        public sg_primitive_type primitive_type;

        [FieldOffset(300)]
        public sg_index_type index_type;

        [FieldOffset(304)]
        public sg_depth_stencil_state depth_stencil;

        [FieldOffset(348)]
        public sg_blend_state blend;

        [FieldOffset(408)]
        public sg_rasterizer_state rasterizer;

        [FieldOffset(440)]
        public byte* label;

        [FieldOffset(448)]
        public uint _end_canary;
    }

    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct sg_attachment_desc
    {
        [FieldOffset(0)]
        public sg_image image;

        [FieldOffset(4)]
        public int mip_level;

        [FieldOffset(8)]
        public int face;

        [FieldOffset(8)]
        public int layer;

        [FieldOffset(8)]
        public int slice;
    }

    [StructLayout(LayoutKind.Explicit, Size = 80, Pack = 8, CharSet = CharSet.Ansi)]
    public struct sg_pass_desc
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(4)]
        public fixed int _color_attachments[12 * SG_MAX_COLOR_ATTACHMENTS / 4];

        [FieldOffset(52)]
        public sg_attachment_desc depth_stencil_attachment;

        [FieldOffset(64)]
        public byte* label;

        [FieldOffset(72)]
        public uint _end_canary;

        public ref sg_attachment_desc color_attachment(int index)
        {
            fixed (sg_pass_desc* pass_desc = &this)
            {
                var ptr = (sg_attachment_desc*)&pass_desc->_color_attachments[0];
                return ref *(ptr + index);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    // [StructLayout(LayoutKind.Explicit, Size = 408, Pack = 8)]
    public struct sg_trace_hooks
    {
        // TODO
    }

    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct sg_slot_info
    {
        [FieldOffset(0)]
        public sg_resource_state state;

        [FieldOffset(4)]
        public uint res_id;

        [FieldOffset(8)]
        public uint ctx_id;
    }

    [StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
    public struct sg_buffer_info
    {
        [FieldOffset(0)]
        public sg_slot_info slot;

        [FieldOffset(12)]
        public uint update_frame_index;

        [FieldOffset(16)]
        public uint append_frame_index;

        [FieldOffset(20)]
        public int append_pos;

        [FieldOffset(24)]
        public BlittableBoolean append_overflow;

        [FieldOffset(28)]
        public int num_slots;

        [FieldOffset(32)]
        public int active_slot;
    }

    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public struct sg_image_info
    {
        [FieldOffset(0)]
        public sg_slot_info slot;

        [FieldOffset(12)]
        public uint upd_frame_index;

        [FieldOffset(16)]
        public int num_slots;

        [FieldOffset(20)]
        public int active_slot;
    }

    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct sg_shader_info
    {
        [FieldOffset(0)]
        public sg_slot_info slot;
    }

    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct sg_pipeline_info
    {
        [FieldOffset(0)]
        public sg_slot_info slot;
    }

    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public struct sg_pass_info
    {
        [FieldOffset(0)]
        public sg_slot_info slot;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104, Pack = 8)]
    public struct sg_desc
    {
        [FieldOffset(0)]
        public uint _start_canary;

        [FieldOffset(4)]
        public int buffer_pool_size;

        [FieldOffset(8)]
        public int image_pool_size;

        [FieldOffset(12)]
        public int shader_pool_size;

        [FieldOffset(16)]
        public int pipeline_pool_size;

        [FieldOffset(20)]
        public int pass_pool_size;

        [FieldOffset(24)]
        public int context_pool_size;

        [FieldOffset(28)]
        public BlittableBoolean gl_force_gles2;

        [FieldOffset(32)]
        public void* mtl_device;

        [FieldOffset(40)]
        public void* mtl_renderpass_descriptor_cb;

        [FieldOffset(48)]
        public void* mtl_drawable_cb;

        [FieldOffset(56)]
        public int mtl_global_uniform_buffer_size;

        [FieldOffset(60)]
        public int mtl_sampler_cache_size;

        [FieldOffset(64)]
        public void* d3d11_device;

        [FieldOffset(72)]
        public void* d3d11_device_context;

        [FieldOffset(80)]
        public void* d3d11_render_target_view_cb;

        [FieldOffset(88)]
        public void* d3d11_depth_stencil_view_cb;

        [FieldOffset(96)]
        public uint _end_canary;
    }

    public static class opengl
    {
        private const string LibraryName = "sokol_gfx-opengl";

        [DllImport(LibraryName)]
        public static extern void sg_setup([In] ref sg_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_shutdown();

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool sg_isvalid();

        [DllImport(LibraryName)]
        public static extern void sg_reset_state_cache();

        [DllImport(LibraryName)]
        public static extern sg_trace_hooks sg_install_trace_hooks(sg_trace_hooks* trace_hooks);

        [DllImport(LibraryName)]
        public static extern void sg_push_debug_group(char* name);

        [DllImport(LibraryName)]
        public static extern void sg_pop_debug_group();

        [DllImport(LibraryName)]
        public static extern sg_buffer sg_make_buffer([In] ref sg_buffer_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_image sg_make_image([In] ref sg_image_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_shader sg_make_shader([In] ref sg_shader_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pipeline sg_make_pipeline([In] ref sg_pipeline_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pass sg_make_pass([In] ref sg_pass_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_buffer(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_image(sg_image img);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_shader(sg_shader shd);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_pipeline(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_pass(sg_pass pass);

        [DllImport(LibraryName)]
        public static extern void sg_update_buffer(sg_buffer buf, void* data_ptr, int data_size);

        [DllImport(LibraryName)]
        public static extern void sg_update_image(sg_image img, sg_image_content* data);

        [DllImport(LibraryName)]
        public static extern int sg_append_buffer(sg_buffer buf, void* data_ptr, int data_size);

        [DllImport(LibraryName)]
        public static extern bool sg_query_buffer_overflow(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern void sg_begin_default_pass([In] ref sg_pass_action pass_action, int width, int height);

        [DllImport(LibraryName)]
        public static extern void sg_begin_pass(sg_pass pass, [In] ref sg_pass_action pass_action);

        [DllImport(LibraryName)]
        public static extern void sg_apply_viewport(int x, int y, int width, int height, bool origin_top_left);

        [DllImport(LibraryName)]
        public static extern void sg_apply_scissor_rect(int x, int y, int width, int height, bool origin_top_left);

        [DllImport(LibraryName)]
        public static extern void sg_apply_pipeline(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern void sg_apply_bindings([In] ref sg_bindings bindings);

        [DllImport(LibraryName)]
        public static extern void sg_apply_uniforms(sg_shader_stage stage, int ub_index, void* data, int num_bytes);

        [DllImport(LibraryName)]
        public static extern void sg_draw(int base_element, int num_elements, int num_instances);

        [DllImport(LibraryName)]
        public static extern void sg_end_pass();

        [DllImport(LibraryName)]
        public static extern void sg_commit();

        [DllImport(LibraryName)]
        public static extern sg_desc sg_query_desc();

        [DllImport(LibraryName)]
        public static extern sg_backend sg_query_backend();

        [DllImport(LibraryName)]
        public static extern sg_features sg_query_features();

        [DllImport(LibraryName)]
        public static extern sg_limits sg_query_limits();

        [DllImport(LibraryName)]
        public static extern sg_pixelformat_info sg_query_pixelformat(sg_pixel_format fmt);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_buffer_state(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_image_state(sg_image img);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_shader_state(sg_shader shd);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_pipeline_state(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_pass_state(sg_pass pass);

        [DllImport(LibraryName)]
        public static extern sg_buffer_info sg_query_buffer_info(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern sg_image_info sg_query_image_info(sg_image img);

        [DllImport(LibraryName)]
        public static extern sg_shader_info sg_query_shader_info(sg_shader shd);

        [DllImport(LibraryName)]
        public static extern sg_pipeline_info sg_query_pipeline_info(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern sg_pass_info sg_query_pass_info(sg_pass pass);

        [DllImport(LibraryName)]
        public static extern sg_buffer_desc sg_query_buffer_defaults([In] ref sg_buffer_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_image_desc sg_query_image_defaults([In] ref sg_image_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_shader_desc sg_query_shader_defaults([In] ref sg_shader_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pipeline_desc sg_query_pipeline_defaults([In] ref sg_pipeline_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pass_desc sg_query_pass_defaults([In] ref sg_pass_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_buffer sg_alloc_buffer();

        [DllImport(LibraryName)]
        public static extern sg_image sg_alloc_image();

        [DllImport(LibraryName)]
        public static extern sg_shader sg_alloc_shader();

        [DllImport(LibraryName)]
        public static extern sg_pipeline sg_alloc_pipeline();

        [DllImport(LibraryName)]
        public static extern sg_pass sg_alloc_pass();

        [DllImport(LibraryName)]
        public static extern void sg_init_buffer(sg_buffer buf_id, [In] ref sg_buffer_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_image(sg_image img_id, [In] ref sg_image_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_shader(sg_shader shd_id, [In] ref sg_shader_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_pipeline(sg_pipeline pip_id, [In] ref sg_pipeline_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_pass(sg_pass pass_id, [In] ref sg_pass_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_fail_buffer(sg_buffer buf_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_image(sg_image img_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_shader(sg_shader shd_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_pipeline(sg_pipeline pip_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_pass(sg_pass pass_id);

        [DllImport(LibraryName)]
        public static extern sg_context sg_setup_context();

        [DllImport(LibraryName)]
        public static extern void sg_activate_context(sg_context ctx_id);

        [DllImport(LibraryName)]
        public static extern void sg_discard_context(sg_context ctx_id);
    }

    public static class metal
    {
        private const string LibraryName = "sokol_gfx-metal";

        [DllImport(LibraryName)]
        public static extern void sg_setup([In] ref sg_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_shutdown();

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool sg_isvalid();

        [DllImport(LibraryName)]
        public static extern void sg_reset_state_cache();

        [DllImport(LibraryName)]
        public static extern sg_trace_hooks sg_install_trace_hooks(sg_trace_hooks* trace_hooks);

        [DllImport(LibraryName)]
        public static extern void sg_push_debug_group(char* name);

        [DllImport(LibraryName)]
        public static extern void sg_pop_debug_group();

        [DllImport(LibraryName)]
        public static extern sg_buffer sg_make_buffer([In] ref sg_buffer_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_image sg_make_image([In] ref sg_image_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_shader sg_make_shader([In] ref sg_shader_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pipeline sg_make_pipeline([In] ref sg_pipeline_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pass sg_make_pass([In] ref sg_pass_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_buffer(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_image(sg_image img);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_shader(sg_shader shd);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_pipeline(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern void sg_destroy_pass(sg_pass pass);

        [DllImport(LibraryName)]
        public static extern void sg_update_buffer(sg_buffer buf, void* data_ptr, int data_size);

        [DllImport(LibraryName)]
        public static extern void sg_update_image(sg_image img, sg_image_content* data);

        [DllImport(LibraryName)]
        public static extern int sg_append_buffer(sg_buffer buf, void* data_ptr, int data_size);

        [DllImport(LibraryName)]
        public static extern bool sg_query_buffer_overflow(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern void sg_begin_default_pass([In] ref sg_pass_action pass_action, int width, int height);

        [DllImport(LibraryName)]
        public static extern void sg_begin_pass(sg_pass pass, [In] ref sg_pass_action pass_action);

        [DllImport(LibraryName)]
        public static extern void sg_apply_viewport(int x, int y, int width, int height, bool origin_top_left);

        [DllImport(LibraryName)]
        public static extern void sg_apply_scissor_rect(int x, int y, int width, int height, bool origin_top_left);

        [DllImport(LibraryName)]
        public static extern void sg_apply_pipeline(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern void sg_apply_bindings([In] ref sg_bindings bindings);

        [DllImport(LibraryName)]
        public static extern void sg_apply_uniforms(sg_shader_stage stage, int ub_index, void* data, int num_bytes);

        [DllImport(LibraryName)]
        public static extern void sg_draw(int base_element, int num_elements, int num_instances);

        [DllImport(LibraryName)]
        public static extern void sg_end_pass();

        [DllImport(LibraryName)]
        public static extern void sg_commit();

        [DllImport(LibraryName)]
        public static extern sg_desc sg_query_desc();

        [DllImport(LibraryName)]
        public static extern sg_backend sg_query_backend();

        [DllImport(LibraryName)]
        public static extern sg_features sg_query_features();

        [DllImport(LibraryName)]
        public static extern sg_limits sg_query_limits();

        [DllImport(LibraryName)]
        public static extern sg_pixelformat_info sg_query_pixelformat(sg_pixel_format fmt);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_buffer_state(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_image_state(sg_image img);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_shader_state(sg_shader shd);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_pipeline_state(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern sg_resource_state sg_query_pass_state(sg_pass pass);

        [DllImport(LibraryName)]
        public static extern sg_buffer_info sg_query_buffer_info(sg_buffer buf);

        [DllImport(LibraryName)]
        public static extern sg_image_info sg_query_image_info(sg_image img);

        [DllImport(LibraryName)]
        public static extern sg_shader_info sg_query_shader_info(sg_shader shd);

        [DllImport(LibraryName)]
        public static extern sg_pipeline_info sg_query_pipeline_info(sg_pipeline pip);

        [DllImport(LibraryName)]
        public static extern sg_pass_info sg_query_pass_info(sg_pass pass);

        [DllImport(LibraryName)]
        public static extern sg_buffer_desc sg_query_buffer_defaults([In] ref sg_buffer_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_image_desc sg_query_image_defaults([In] ref sg_image_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_shader_desc sg_query_shader_defaults([In] ref sg_shader_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pipeline_desc sg_query_pipeline_defaults([In] ref sg_pipeline_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_pass_desc sg_query_pass_defaults([In] ref sg_pass_desc desc);

        [DllImport(LibraryName)]
        public static extern sg_buffer sg_alloc_buffer();

        [DllImport(LibraryName)]
        public static extern sg_image sg_alloc_image();

        [DllImport(LibraryName)]
        public static extern sg_shader sg_alloc_shader();

        [DllImport(LibraryName)]
        public static extern sg_pipeline sg_alloc_pipeline();

        [DllImport(LibraryName)]
        public static extern sg_pass sg_alloc_pass();

        [DllImport(LibraryName)]
        public static extern void sg_init_buffer(sg_buffer buf_id, [In] ref sg_buffer_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_image(sg_image img_id, [In] ref sg_image_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_shader(sg_shader shd_id, [In] ref sg_shader_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_pipeline(sg_pipeline pip_id, [In] ref sg_pipeline_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_init_pass(sg_pass pass_id, [In] ref sg_pass_desc desc);

        [DllImport(LibraryName)]
        public static extern void sg_fail_buffer(sg_buffer buf_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_image(sg_image img_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_shader(sg_shader shd_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_pipeline(sg_pipeline pip_id);

        [DllImport(LibraryName)]
        public static extern void sg_fail_pass(sg_pass pass_id);

        [DllImport(LibraryName)]
        public static extern sg_context sg_setup_context();

        [DllImport(LibraryName)]
        public static extern void sg_activate_context(sg_context ctx_id);

        [DllImport(LibraryName)]
        public static extern void sg_discard_context(sg_context ctx_id);
    }
}
