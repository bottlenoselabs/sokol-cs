#include <metal_stdlib>
using namespace metal;
struct fs_in {
  float2 uv0;
  float2 uv1;
  float2 uv2;
};
fragment float4 _main(fs_in in [[stage_in]],
  texture2d<float> tex0 [[texture(0)]], sampler smp0 [[sampler(0)]],
  texture2d<float> tex1 [[texture(1)]], sampler smp1 [[sampler(1)]],
  texture2d<float> tex2 [[texture(2)]], sampler smp2 [[sampler(2)]])
{
  float3 c0 = tex0.sample(smp0, in.uv0).xyz;
  float3 c1 = tex1.sample(smp1, in.uv1).xyz;
  float3 c2 = tex2.sample(smp2, in.uv2).xyz;
  return float4(c0 + c1 + c2, 1.0);
}