#include <metal_stdlib>
using namespace metal;
struct fs_in {
  float4 color;
};
fragment float4 _main(fs_in in [[stage_in]]) {
  return in.color;
};