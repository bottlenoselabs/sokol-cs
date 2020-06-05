cbuffer params
{
    float4x4 mvp;
    float2 offset0;
    float2 offset1;
    float2 offset2;
};

struct vs_in
{
    float4 pos: POSITION;
    float2 uv: TEXCOORD0;
};

struct vs_out
{
    float3 uv0: TEXCOORD0;
    float3 uv1: TEXCOORD1;
    float3 uv2: TEXCOORD2;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = mul(mvp, inp.pos);
    outp.uv0 = float3(inp.uv + offset0, 0.0);
    outp.uv1 = float3(inp.uv + offset1, 1.0);
    outp.uv2 = float3(inp.uv + offset2, 2.0);
    return outp;
};