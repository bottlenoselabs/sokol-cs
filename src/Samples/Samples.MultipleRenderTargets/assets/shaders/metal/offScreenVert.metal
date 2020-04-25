#include <metal_stdlib>
using namespace metal;
struct params_t {
  float4x4 mvp;
};
struct vs_in {
  float4 pos [[attribute(0)]];
  float bright [[attribute(1)]];
};
struct vs_out {
  float4 pos [[position]];
  float bright;
};
vertex vs_out _main(vs_in in [[stage_in]], constant params_t& params [[buffer(0)]]) {
  vs_out out;
  out.pos = params.mvp * in.pos;
  out.bright = in.bright;
  return out;
};
