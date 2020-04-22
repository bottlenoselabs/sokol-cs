#include <metal_stdlib>
using namespace metal;
struct params_t
{
    float4x4 mvp;
    float2 offset0;
    float2 offset1;
    float2 offset2;
};
struct vs_in
{
    float4 pos [[attribute(0)]];
    float2 uv [[attribute(1)]];
};
struct vs_out
{
    float4 pos [[position]];
    float3 uv0;
    float3 uv1;
    float3 uv2;
};
vertex vs_out _main(vs_in in [[stage_in]], constant params_t& params [[buffer(0)]])
{
    vs_out out;
    out.pos = params.mvp * in.pos;
    out.uv0 = float3(in.uv + params.offset0, 0.0);
    out.uv1 = float3(in.uv + params.offset1, 1.0);
    out.uv2 = float3(in.uv + params.offset2, 2.0);
    return out;
}