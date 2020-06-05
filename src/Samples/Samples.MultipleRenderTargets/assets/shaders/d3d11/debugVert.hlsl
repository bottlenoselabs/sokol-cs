struct vs_in
{
    float2 pos: POSITION;
};

struct vs_out
{
    float2 uv: TEXCOORD0;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = float4(inp.pos*2.0-1.0, 0.5, 1.0);
    outp.uv = inp.pos;
    return outp;
};