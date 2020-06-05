Texture2D<float4> tex0: register(t0);
Texture2D<float4> tex1: register(t1);
Texture2D<float4> tex2: register(t2);
sampler smp0: register(s0);
sampler smp1: register(s1);
sampler smp2: register(s2);

struct fs_in
{
    float2 uv0: TEXCOORD0;
    float2 uv1: TEXCOORD1;
    float2 uv2: TEXCOORD2;
};

float4 main(fs_in inp): SV_Target0
{
    float3 c0 = tex0.Sample(smp0, inp.uv0).xyz;
    float3 c1 = tex1.Sample(smp1, inp.uv1).xyz;
    float3 c2 = tex2.Sample(smp2, inp.uv2).xyz;
    float4 c = float4(c0 + c1 + c2, 1.0);
    return c;
};