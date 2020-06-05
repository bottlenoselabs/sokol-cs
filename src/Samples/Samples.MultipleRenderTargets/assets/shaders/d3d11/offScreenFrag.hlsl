struct fs_out
{
    float4 c0: SV_Target0;
    float4 c1: SV_Target1;
    float4 c2: SV_Target2;
};

fs_out main(float b: BRIGHT)
{
    fs_out outp;
    outp.c0 = float4(b, 0.0, 0.0, 1.0);
    outp.c1 = float4(0.0, b, 0.0, 1.0);
    outp.c2 = float4(0.0, 0.0, b, 1.0);
    return outp;
};