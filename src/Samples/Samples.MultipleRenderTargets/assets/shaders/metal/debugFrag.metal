#include <metal_stdlib>
using namespace metal;
fragment float4 _main(float2 uv [[stage_in]], texture2d<float> tex [[texture(0)]], sampler smp [[sampler(0)]]) {
  return float4(tex.sample(smp, uv).xyz, 1.0);
};