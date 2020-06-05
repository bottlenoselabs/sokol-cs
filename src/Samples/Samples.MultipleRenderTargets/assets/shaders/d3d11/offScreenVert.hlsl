cbuffer params: register(b0)
{
    float4x4 mvp;
};

struct vs_in
{
    float4 pos: POSITION;
    float bright: BRIGHT;
};

struct vs_out
{
    float bright: BRIGHT;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = mul(mvp, inp.pos);
    outp.bright = inp.bright;
    return outp;
};