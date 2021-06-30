struct vs_in
{
    float2 pos: POSITION;
    float3 color: COLOR0;
};

struct vs_out
{
    float4 color: COLOR0;
    float4 pos: SV_Position;
};

vs_out main(vs_in inp)
{
    vs_out outp;
    outp.pos = float4(inp.pos, 0.5, 1.0);
    outp.color = float4(inp.color, 1.0);
    return outp;
}