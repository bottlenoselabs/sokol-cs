using Xunit;
using static Sokol.sokol_gfx;

// Tests taken from https://github.com/floooh/sokol-samples/blob/master/tests/sokol-gfx-test.c

namespace Sokol.Graphics.Tests
{
    public unsafe class PInvokeTests
    {
        [Fact]
        public void InitShutdown()
        {
            var setupDesc = new sg_desc();
            sg_setup(&setupDesc);

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
            sg_setup(&setupDesc);
            
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
    }
}