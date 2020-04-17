#include <metal_stdlib>
using namespace metal;
struct vs_in {
  float4 position [[attribute(0)]];
  float4 color [[attribute(1)]];
};
struct vs_out {
  float4 position [[position]];
  float4 color [[user(usr0)]];
};
vertex vs_out _main(vs_in in [[stage_in]]) {
  vs_out out;
  out.position = in.position;
  out.color = in.color;
  return out;
}