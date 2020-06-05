Texture2DArray<float4> tex: register(t0);
sampler smp: register(s0);

struct fs_in
{
    float3 uv0: TEXCOORD0;
    float3 uv1: TEXCOORD1;
    float3 uv2: TEXCOORD2;
};

float4 main(fs_in inp): SV_Target0
{
    float3 c0 = tex.Sample(smp, inp.uv0).xyz;
    float3 c1 = tex.Sample(smp, inp.uv1).xyz;
    float3 c2 = tex.Sample(smp, inp.uv2).xyz;
    return float4(c0 + c1 + c2, 1.0);
};