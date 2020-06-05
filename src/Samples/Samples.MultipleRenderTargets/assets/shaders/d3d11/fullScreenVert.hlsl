cbuffer params
{
    float2 offset;
};

struct vs_in
{
    float2 pos: POSITION;
};

struct vs_out
{
    float2 uv0: TEXCOORD0;
    float2 uv1: TEXCOORD1;
    float2 uv2: TEXCOORD2;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = float4(inp.pos*2.0-1.0, 0.5, 1.0);
    outp.uv0 = inp.pos + float2(offset.x, 0.0);
    outp.uv1 = inp.pos + float2(0.0, offset.y);
    outp.uv2 = inp.pos;
    return outp;
};