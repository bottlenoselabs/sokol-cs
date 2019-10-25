using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Xunit;
using static Sokol.sokol_gfx;

// Tests taken from https://github.com/floooh/sokol-samples/blob/master/tests/sokol-gfx-test.c

namespace Sokol.Graphics.Tests
{
    public unsafe partial class PInvokeTests
    {
        [Fact]
        public void InitShutdown()
        {
            var setupDesc = new sg_desc();
            sg_setup(ref setupDesc);

            var isValidAfterSetup = sg_isvalid();
            Assert.True(isValidAfterSetup);

            sg_shutdown();
            var isValidAfterShutdown = sg_isvalid();
            Assert.False(isValidAfterShutdown);
        }

        [Fact]
        public void QueryDesc()
        {
            var setupDesc = new sg_desc
            {
                buffer_pool_size = 1024,
                shader_pool_size = 128,
                pass_pool_size = 64
            };
            sg_setup(ref setupDesc);

            var desc = sg_query_desc();

            Assert.True(desc.buffer_pool_size == 1024);
            Assert.True(desc.image_pool_size == _SG_DEFAULT_IMAGE_POOL_SIZE);
            Assert.True(desc.shader_pool_size == 128);
            Assert.True(desc.pipeline_pool_size == _SG_DEFAULT_PIPELINE_POOL_SIZE);
            Assert.True(desc.pass_pool_size == 64);
            Assert.True(desc.context_pool_size == _SG_DEFAULT_CONTEXT_POOL_SIZE);
            Assert.True(desc.mtl_global_uniform_buffer_size == _SG_MTL_DEFAULT_UB_SIZE);
            Assert.True(desc.mtl_sampler_cache_size == _SG_MTL_DEFAULT_SAMPLER_CACHE_CAPACITY);

            sg_shutdown();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        [Fact]
        public void QueryBackend()
        {
            var setupDesc = new sg_desc();
            sg_setup(ref setupDesc);

            var backend = sg_query_backend();
            Assert.True(backend == sg_backend.SG_BACKEND_DUMMY);

            sg_shutdown();
        }

        [Fact]
        public void AllocFailDestroyBuffers()
        {
            var setupDesc = new sg_desc()
            {
                buffer_pool_size = 3
            };
            sg_setup(ref setupDesc);

            var isValidAfterSetup = sg_isvalid();
            Assert.True(isValidAfterSetup);

            var buffers = stackalloc sg_buffer[3];
            sg_resource_state bufferState;
            for (var i = 0; i < 3; i++)
            {
                buffers[i] = sg_alloc_buffer();
                Assert.True(buffers[i].id != SG_INVALID_ID);

                bufferState = sg_query_buffer_state(buffers[i]);
                Assert.True(bufferState == sg_resource_state.SG_RESOURCESTATE_ALLOC);
            }

            var invalidBuffer = sg_alloc_buffer();
            Assert.True(invalidBuffer.id == SG_INVALID_ID);
            bufferState = sg_query_buffer_state(invalidBuffer);
            Assert.True(bufferState == sg_resource_state.SG_RESOURCESTATE_INVALID);

            for (var i = 0; i < 3; i++)
            {
                sg_fail_buffer(buffers[i]);
                bufferState = sg_query_buffer_state(buffers[i]);
                Assert.True(bufferState == sg_resource_state.SG_RESOURCESTATE_FAILED);
            }

            for (var i = 0; i < 3; i++)
            {
                sg_destroy_buffer(buffers[i]);
                bufferState = sg_query_buffer_state(buffers[i]);
                Assert.True(bufferState == sg_resource_state.SG_RESOURCESTATE_INVALID);
            }

            sg_shutdown();
        }

        [Fact]
        public void AllocFailDestroyImages()
        {
            var setupDesc = new sg_desc()
            {
                image_pool_size = 3
            };
            sg_setup(ref setupDesc);

            var isValidAfterSetup = sg_isvalid();
            Assert.True(isValidAfterSetup);

            var images = stackalloc sg_image[3];
            sg_resource_state imageState;
            for (var i = 0; i < 3; i++)
            {
                images[i] = sg_alloc_image();
                Assert.True(images[i].id != SG_INVALID_ID);
                imageState = sg_query_image_state(images[i]);
                Assert.True(imageState == sg_resource_state.SG_RESOURCESTATE_ALLOC);
            }

            var invalidImage = sg_alloc_image();
            Assert.True(invalidImage.id == SG_INVALID_ID);
            imageState = sg_query_image_state(invalidImage);
            Assert.True(imageState == sg_resource_state.SG_RESOURCESTATE_INVALID);

            for (var i = 0; i < 3; i++)
            {
                sg_fail_image(images[i]);
                imageState = sg_query_image_state(images[i]);
                Assert.True(imageState == sg_resource_state.SG_RESOURCESTATE_FAILED);
            }

            for (var i = 0; i < 3; i++)
            {
                sg_destroy_image(images[i]);
                imageState = sg_query_image_state(invalidImage);
                Assert.True(imageState == sg_resource_state.SG_RESOURCESTATE_INVALID);
            }

            sg_shutdown();
        }

        [Fact]
        public void AllocFailDestroyShaders()
        {
            var setupDesc = new sg_desc()
            {
                shader_pool_size = 3
            };
            sg_setup(ref setupDesc);

            var isValidAfterSetup = sg_isvalid();
            Assert.True(isValidAfterSetup);

            var shaders = stackalloc sg_shader[3];
            sg_resource_state shaderState;
            for (var i = 0; i < 3; i++)
            {
                shaders[i] = sg_alloc_shader();
                Assert.True(shaders[i].id != SG_INVALID_ID);
                shaderState = sg_query_shader_state(shaders[i]);
                Assert.True(shaderState == sg_resource_state.SG_RESOURCESTATE_ALLOC);
            }

            var invalidShader = sg_alloc_shader();
            Assert.True(invalidShader.id == SG_INVALID_ID);
            shaderState = sg_query_shader_state(invalidShader);
            Assert.True(shaderState == sg_resource_state.SG_RESOURCESTATE_INVALID);

            for (var i = 0; i < 3; i++)
            {
                sg_fail_shader(shaders[i]);
                shaderState = sg_query_shader_state(shaders[i]);
                Assert.True(shaderState == sg_resource_state.SG_RESOURCESTATE_FAILED);
            }

            for (var i = 0; i < 3; i++)
            {
                sg_destroy_shader(shaders[i]);
                shaderState = sg_query_shader_state(invalidShader);
                Assert.True(shaderState == sg_resource_state.SG_RESOURCESTATE_INVALID);
            }

            sg_shutdown();
        }

        [Fact]
        public void AllocFailDestroyPipelines()
        {
            var setupDesc = new sg_desc()
            {
                pipeline_pool_size = 3
            };
            sg_setup(ref setupDesc);

            var isValidAfterSetup = sg_isvalid();
            Assert.True(isValidAfterSetup);

            var pipelines = stackalloc sg_pipeline[3];
            sg_resource_state pipelineState;
            for (var i = 0; i < 3; i++)
            {
                pipelines[i] = sg_alloc_pipeline();
                Assert.True(pipelines[i].id != SG_INVALID_ID);
                pipelineState = sg_query_pipeline_state(pipelines[i]);
                Assert.True(pipelineState == sg_resource_state.SG_RESOURCESTATE_ALLOC);
            }

            var invalidPipeline = sg_alloc_pipeline();
            Assert.True(invalidPipeline.id == SG_INVALID_ID);
            pipelineState = sg_query_pipeline_state(invalidPipeline);
            Assert.True(pipelineState == sg_resource_state.SG_RESOURCESTATE_INVALID);

            for (var i = 0; i < 3; i++)
            {
                sg_fail_pipeline(pipelines[i]);
                pipelineState = sg_query_pipeline_state(pipelines[i]);
                Assert.True(pipelineState == sg_resource_state.SG_RESOURCESTATE_FAILED);
            }

            for (var i = 0; i < 3; i++)
            {
                sg_destroy_pipeline(pipelines[i]);
                pipelineState = sg_query_pipeline_state(invalidPipeline);
                Assert.True(pipelineState == sg_resource_state.SG_RESOURCESTATE_INVALID);
            }

            sg_shutdown();
        }

        [Fact]
        public void AllocFailDestroyPasses()
        {
            var setupDesc = new sg_desc()
            {
                pass_pool_size = 3
            };
            sg_setup(ref setupDesc);

            var isValidAfterSetup = sg_isvalid();
            Assert.True(isValidAfterSetup);

            var passes = stackalloc sg_pass[3];
            sg_resource_state passState;
            for (var i = 0; i < 3; i++)
            {
                passes[i] = sg_alloc_pass();
                Assert.True(passes[i].id != SG_INVALID_ID);
                passState = sg_query_pass_state(passes[i]);
                Assert.True(passState == sg_resource_state.SG_RESOURCESTATE_ALLOC);
            }

            var invalidPass = sg_alloc_pass();
            Assert.True(invalidPass.id == SG_INVALID_ID);
            passState = sg_query_pass_state(invalidPass);
            Assert.True(passState == sg_resource_state.SG_RESOURCESTATE_INVALID);

            for (var i = 0; i < 3; i++)
            {
                sg_fail_pass(passes[i]);
                passState = sg_query_pass_state(passes[i]);
                Assert.True(passState == sg_resource_state.SG_RESOURCESTATE_FAILED);
            }

            for (var i = 0; i < 3; i++)
            {
                sg_destroy_pass(passes[i]);
                passState = sg_query_pass_state(invalidPass);
                Assert.True(passState == sg_resource_state.SG_RESOURCESTATE_INVALID);
            }

            sg_shutdown();
        }

        [Fact]
        public void GenerationCounter()
        {
            var setupDesc = new sg_desc()
            {
                pass_pool_size = 1
            };
            sg_setup(ref setupDesc);

            var data = stackalloc float[4];
            data[0] = 1.0f;
            data[1] = 2.0f;
            data[2] = 3.0f;
            data[4] = 4.0f;

            for (var i = 0; i < 64; i++)
            {
                var desc = new sg_buffer_desc
                {
                    size = sizeof(float) * 4,
                    content = data
                };

                var buffer = sg_make_buffer(&desc);
                Assert.True(buffer.id != SG_INVALID_ID);

                var bufferState = sg_query_buffer_state(buffer);
                Assert.True(bufferState == sg_resource_state.SG_RESOURCESTATE_VALID);
                Assert.True(buffer.id >> 16 == (uint) (i + 1));
                sg_destroy_buffer(buffer);
                bufferState = sg_query_buffer_state(buffer);
                Assert.True(sg_query_buffer_state(buffer) == sg_resource_state.SG_RESOURCESTATE_INVALID);
            }

            sg_shutdown();
        }

        [Fact]
        public void QueryBufferDefaults()
        {
            var setupDesc = new sg_desc()
            {
                pass_pool_size = 1
            };
            sg_setup(ref setupDesc);

            var bufferDesc = new sg_buffer_desc();
            var desc = sg_query_buffer_defaults(&bufferDesc);

            Assert.True(desc.type == sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER);
            Assert.True(desc.usage == sg_usage.SG_USAGE_IMMUTABLE);

            bufferDesc = new sg_buffer_desc()
            {
                type = sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER
            };
            desc = sg_query_buffer_defaults(&bufferDesc);

            Assert.True(desc.type == sg_buffer_type.SG_BUFFERTYPE_INDEXBUFFER);
            Assert.True(desc.usage == sg_usage.SG_USAGE_IMMUTABLE);

            bufferDesc = new sg_buffer_desc()
            {
                usage = sg_usage.SG_USAGE_DYNAMIC
            };
            desc = sg_query_buffer_defaults(&bufferDesc);

            Assert.True(desc.type == sg_buffer_type.SG_BUFFERTYPE_VERTEXBUFFER);
            Assert.True(desc.usage == sg_usage.SG_USAGE_DYNAMIC);

            sg_shutdown();
        }

        [Fact]
        public void QueryImageDefaults()
        {
            var setupDesc = new sg_desc();
            sg_setup(ref setupDesc);

            var desc = new sg_image_desc();
            desc = sg_query_image_defaults(&desc);

            Assert.True(desc.type == sg_image_type.SG_IMAGETYPE_2D);
            Assert.True(!desc.render_target);
            Assert.True(desc.num_mipmaps == 1);
            Assert.True(desc.usage == sg_usage.SG_USAGE_IMMUTABLE);
            Assert.True(desc.pixel_format == sg_pixel_format.SG_PIXELFORMAT_RGBA8);
            Assert.True(desc.sample_count == 1);
            Assert.True(desc.min_filter == sg_filter.SG_FILTER_NEAREST);
            Assert.True(desc.mag_filter == sg_filter.SG_FILTER_NEAREST);
            Assert.True(desc.wrap_u == sg_wrap.SG_WRAP_REPEAT);
            Assert.True(desc.wrap_v == sg_wrap.SG_WRAP_REPEAT);
            Assert.True(desc.wrap_w == sg_wrap.SG_WRAP_REPEAT);
            Assert.True(desc.max_anisotropy == 1);
            Assert.True(desc.max_lod >= float.MaxValue);

            sg_shutdown();
        }

        [Fact]
        public void QueryShaderDefaults()
        {
            var setupDesc = new sg_desc();
            sg_setup(ref setupDesc);

            var desc = new sg_shader_desc();
            desc = sg_query_shader_defaults(&desc);

            var vertexShaderEntry = Marshal.PtrToStringAnsi((IntPtr) desc.vs.entry);
            Assert.True(vertexShaderEntry == "main");

            var fragmentShaderEntry = Marshal.PtrToStringAnsi((IntPtr) desc.fs.entry);
            Assert.True(fragmentShaderEntry == "main");

            sg_shutdown();
        }

        [Fact]
        public void QueryPipelineDefaults()
        {
            var setupDesc = new sg_desc();
            sg_setup(ref setupDesc);

            var desc = new sg_pipeline_desc();
            var attrs = desc.layout.GetAttrs();
            attrs[0].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT3;
            attrs[1].format = sg_vertex_format.SG_VERTEXFORMAT_FLOAT4;
            desc = sg_query_pipeline_defaults(&desc);

            var buffers = desc.layout.GetBuffers();
            attrs = desc.layout.GetAttrs();
            Assert.True(buffers[0].stride == 28);
            Assert.True(buffers[0].step_rate == 1);
            Assert.True(buffers[0].step_func == sg_vertex_step.SG_VERTEXSTEP_PER_VERTEX);
            Assert.True(attrs[0].offset == 0);
            Assert.True(attrs[0].buffer_index == 0);
            Assert.True(attrs[0].format == sg_vertex_format.SG_VERTEXFORMAT_FLOAT3);
            Assert.True(attrs[1].offset == 12);
            Assert.True(attrs[1].buffer_index == 0);
            Assert.True(attrs[1].format == sg_vertex_format.SG_VERTEXFORMAT_FLOAT4);
            Assert.True(desc.primitive_type == sg_primitive_type.SG_PRIMITIVETYPE_TRIANGLES);
            Assert.True(desc.index_type == sg_index_type.SG_INDEXTYPE_NONE);
            Assert.True(desc.depth_stencil.stencil_front.fail_op == sg_stencil_op.SG_STENCILOP_KEEP);
            Assert.True(desc.depth_stencil.stencil_front.depth_fail_op == sg_stencil_op.SG_STENCILOP_KEEP);
            Assert.True(desc.depth_stencil.stencil_front.pass_op == sg_stencil_op.SG_STENCILOP_KEEP);
            Assert.True(desc.depth_stencil.stencil_front.compare_func == sg_compare_func.SG_COMPAREFUNC_ALWAYS);
            Assert.True(desc.depth_stencil.stencil_back.fail_op == sg_stencil_op.SG_STENCILOP_KEEP);
            Assert.True(desc.depth_stencil.stencil_back.depth_fail_op == sg_stencil_op.SG_STENCILOP_KEEP);
            Assert.True(desc.depth_stencil.stencil_back.pass_op == sg_stencil_op.SG_STENCILOP_KEEP);
            Assert.True(desc.depth_stencil.stencil_back.compare_func == sg_compare_func.SG_COMPAREFUNC_ALWAYS);
            Assert.True(desc.depth_stencil.depth_compare_func == sg_compare_func.SG_COMPAREFUNC_ALWAYS);
            Assert.True(desc.depth_stencil.depth_write_enabled == false);
            Assert.True(desc.depth_stencil.stencil_enabled == false);
            Assert.True(desc.depth_stencil.stencil_read_mask == 0);
            Assert.True(desc.depth_stencil.stencil_write_mask == 0);
            Assert.True(desc.depth_stencil.stencil_ref == 0);
            Assert.True(desc.blend.enabled == false);
            Assert.True(desc.blend.src_factor_rgb == sg_blend_factor.SG_BLENDFACTOR_ONE);
            Assert.True(desc.blend.dst_factor_rgb == sg_blend_factor.SG_BLENDFACTOR_ZERO);
            Assert.True(desc.blend.op_rgb == sg_blend_op.SG_BLENDOP_ADD);
            Assert.True(desc.blend.src_factor_alpha == sg_blend_factor.SG_BLENDFACTOR_ONE);
            Assert.True(desc.blend.dst_factor_alpha == sg_blend_factor.SG_BLENDFACTOR_ZERO);
            Assert.True(desc.blend.op_alpha == sg_blend_op.SG_BLENDOP_ADD);
            Assert.True(desc.blend.color_write_mask == 0xF);
            Assert.True(desc.blend.color_attachment_count == 1);
            Assert.True(desc.blend.color_format == sg_pixel_format.SG_PIXELFORMAT_RGBA8);
            Assert.True(desc.blend.depth_format == sg_pixel_format.SG_PIXELFORMAT_DEPTH_STENCIL);
            Assert.True(desc.rasterizer.alpha_to_coverage_enabled == false);
            Assert.True(desc.rasterizer.cull_mode == sg_cull_mode.SG_CULLMODE_NONE);
            Assert.True(desc.rasterizer.face_winding == sg_face_winding.SG_FACEWINDING_CW);
            Assert.True(desc.rasterizer.sample_count == 1);
            // ReSharper disable CompareOfFloatsByEqualityOperator
            Assert.True(desc.rasterizer.depth_bias == 0);
            Assert.True(desc.rasterizer.depth_bias_slope_scale == 0);
            Assert.True(desc.rasterizer.depth_bias_clamp == 0);
            // ReSharper restore CompareOfFloatsByEqualityOperator
            
            sg_shutdown();
        }

        [Fact]
        public void QueryPassDefaults()
        {
            var setupDesc = new sg_desc();
            sg_setup(ref setupDesc);
            
            var desc = new sg_pass_desc();
            desc = sg_query_pass_defaults(&desc);

            var colorAttachments = desc.GetColorAttachments();
            Assert.True(colorAttachments[0].image.id == SG_INVALID_ID);
            Assert.True(colorAttachments[0].mip_level == 0);
            
            sg_shutdown();
        }
    }
}