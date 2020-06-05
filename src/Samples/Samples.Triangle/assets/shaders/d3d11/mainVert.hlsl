struct vs_in
{
    float4 pos: POS;
    float4 color: COLOR;
};

struct vs_out
{
    float4 color: COLOR0;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = inp.pos;
    outp.color = inp.color;
    return outp;
}