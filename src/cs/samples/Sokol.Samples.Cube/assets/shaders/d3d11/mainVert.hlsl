cbuffer params: register(b0)
{
    float4x4 mvp;
};

struct vs_in
{
    float4 pos: POSITION;
    float4 color: COLOR1;
};

struct vs_out
{
    float4 color: COLOR0;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = mul(mvp, inp.pos);
    outp.color = inp.color;
    return outp;
};