#include <metal_stdlib>
using namespace metal;
struct fs_in
{
    float4 color;
    float2 uv;
};
fragment float4 _main(fs_in in [[stage_in]], texture2d<float> tex [[texture(0)]], sampler smp [[sampler(0)]])
{
    return float4(tex.sample(smp, in.uv).xyz, 1.0) * in.color;
};