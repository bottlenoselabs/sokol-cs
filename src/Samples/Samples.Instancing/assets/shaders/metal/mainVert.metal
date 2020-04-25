#include <metal_stdlib>
using namespace metal;
struct params_t {
  float4x4 mvp;
};
struct vs_in {
  float3 pos [[attribute(0)]];
  float4 color [[attribute(1)]];
  float3 instance_pos [[attribute(2)]];
};
struct vs_out {
  float4 pos [[position]];
  float4 color;
};
vertex vs_out _main(vs_in in [[stage_in]], constant params_t& params [[buffer(0)]]) {
  vs_out out;
  float4 pos = float4(in.pos + in.instance_pos, 1.0);
  out.pos = params.mvp * pos;
  out.color = in.color;
  return out;
}