#include <metal_stdlib>
using namespace metal;
struct fs_in
{
    float3 uv0;
    float3 uv1;
    float3 uv2;
};
fragment float4 _main(fs_in in [[stage_in]], texture2d_array<float> tex [[texture(0)]], sampler smp [[sampler(0)]])
{
    float4 c0 = tex.sample(smp, in.uv0.xy, int(in.uv0.z));
    float4 c1 = tex.sample(smp, in.uv1.xy, int(in.uv1.z));
    float4 c2 = tex.sample(smp, in.uv2.xy, int(in.uv2.z));
    return float4(c0.xyz + c1.xyz + c2.xyz, 1.0);
}