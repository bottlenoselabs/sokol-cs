#include <metal_stdlib>
using namespace metal;
struct params_t {
  float tick;
};
fragment float4 fs_main(float4 frag_coord [[position]], constant params_t& params [[buffer(0)]]) {
  float2 xy = fract((frag_coord.xy-float2(params.tick)) / 50.0);
  return float4(float3(xy.x*xy.y), 1.0);
};