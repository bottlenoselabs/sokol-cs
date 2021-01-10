#include <metal_stdlib>
#include <simd/simd.h>
using namespace metal;
struct fs_in {
  float4 color [[user(usr0)]];
};
fragment float4 _main(fs_in in [[stage_in]]) {
  return in.color;
};