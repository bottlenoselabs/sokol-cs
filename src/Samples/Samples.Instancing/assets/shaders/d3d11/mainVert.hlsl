cbuffer params: register(b0)
{
    float4x4 mvp;
};

struct vs_in
{
    float3 pos: POSITION;
    float4 color: COLOR0;
    float3 inst_pos: INSTPOS;
};

struct vs_out
{
    float4 color: COLOR0;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = mul(mvp, float4(inp.pos + inp.inst_pos, 1.0));
    outp.color = inp.color;
    return outp;
};