Texture2D<float4> tex: register(t0);
sampler smp: register(s0);

float4 main(float2 uv: TEXCOORD0): SV_Target0
{
    return float4(tex.Sample(smp, uv).xyz, 1.0);
};