#include <metal_stdlib>
using namespace metal;
struct fs_out {
  float4 color0 [[color(0)]];
  float4 color1 [[color(1)]];
  float4 color2 [[color(2)]];
};
fragment fs_out _main(float bright [[stage_in]]) {
  fs_out out;
  out.color0 = float4(bright, 0.0, 0.0, 1.0);
  out.color1 = float4(0.0, bright, 0.0, 1.0);
  out.color2 = float4(0.0, 0.0, bright, 1.0);
  return out;
}