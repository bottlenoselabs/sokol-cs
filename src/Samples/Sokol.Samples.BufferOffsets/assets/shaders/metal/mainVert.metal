#include <metal_stdlib>
using namespace metal;
struct vs_in {
  float2 pos [[attribute(0)]];
  float3 color [[attribute(1)]];
};
struct vs_out {
  float4 pos [[position]];
  float4 color;
};
vertex vs_out _main(vs_in in [[stage_in]]) {
  vs_out out;
  out.pos = float4(in.pos, 0.5f, 1.0f);
  out.color = float4(in.color, 1.0);
  return out;
}