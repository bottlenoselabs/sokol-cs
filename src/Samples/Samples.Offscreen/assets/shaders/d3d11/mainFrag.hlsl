Texture2D<float4> tex: register(t0);
sampler smp: register(s0);
float4 main(float4 color: COLOR0, float2 uv: TEXCOORD0): SV_Target0
{
    return tex.Sample(smp, uv) + color * 0.5;
};