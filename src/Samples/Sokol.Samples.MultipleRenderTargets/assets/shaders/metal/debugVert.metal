#include <metal_stdlib>
using namespace metal;
struct vs_in {
  float2 pos [[attribute(0)]];
};
struct vs_out {
  float4 pos [[position]];
  float2 uv;
};
vertex vs_out _main(vs_in in [[stage_in]]) {
  vs_out out;
  out.pos = float4(in.pos*2.0-1.0, 0.5, 1.0);
  out.uv = in.pos;
  return out;
};