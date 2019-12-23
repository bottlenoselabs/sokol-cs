#include <metal_stdlib>
using namespace metal;
struct vs_in {
  float2 position[[attribute(0)]];
};
struct vs_out {
  float4 pos [[position]];
};
vertex vs_out vs_main(vs_in in [[stage_in]]) {
  vs_out out;
  out.pos = float4(in.position, 0.5, 1.0);
  return out;
};