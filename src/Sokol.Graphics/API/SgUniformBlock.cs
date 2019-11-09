namespace Sokol
{
    public struct SgUniformBlock
    {
        public readonly SgUniform[] Uniforms;

        public SgUniformBlock(params SgUniform[] uniforms)
        {
            Uniforms = uniforms;
        }
    }
}