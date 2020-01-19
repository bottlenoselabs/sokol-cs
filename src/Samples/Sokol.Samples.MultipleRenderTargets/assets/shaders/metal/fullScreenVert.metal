#include <metal_stdlib>
using namespace metal;
struct params_t {
  float2 offset;
};
struct vs_in {
  float2 pos [[attribute(0)]];
};
struct vs_out {
  float4 pos [[position]];
  float2 uv0;
  float2 uv1;
  float2 uv2;
};
vertex vs_out _main(vs_in in [[stage_in]], constant params_t& params [[buffer(0)]]) {
  vs_out out;
  out.pos = float4(in.pos*2.0-1.0, 0.5, 1.0);
  out.uv0 = in.pos + float2(params.offset.x, 0.0);
  out.uv1 = in.pos + float2(0.0, params.offset.y);
  out.uv2 = in.pos;
  return out;
}