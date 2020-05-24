namespace Direct3D11
{
    public static class D3d11
    {
        public uint D3D11CreateDevice(
            IDXGIAdapter *pAdapter,
            D3D_DRIVER_TYPE DriverType,
            HMODULE Software,
            uint Flags,
        const D3D_FEATURE_LEVEL *pFeatureLevels,
        uint FeatureLevels,
        uint SDKVersion,
            ID3D11Device            **ppDevice,
        D3D_FEATURE_LEVEL       *pFeatureLevel,
            ID3D11DeviceContext     **ppImmediateContext
        );
    }
}