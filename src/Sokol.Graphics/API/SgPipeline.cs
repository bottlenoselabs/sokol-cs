using System;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgPipeline : SgResource
    {
        public sg_pipeline Handle { get;  }
        public SgShader Shader { get; }

        public unsafe SgPipeline(SgShader shader, sg_vertex_format[] vertexFormats, sg_index_type indexType = sg_index_type._SG_INDEXTYPE_DEFAULT, string name = null) 
            : base(name)
        {
            Shader = shader ?? throw new ArgumentNullException(nameof(shader));

            var description = new sg_pipeline_desc
            {
                shader = Shader.Handle,
                index_type = indexType
            };
            
            var attributes = description.layout.GetAttrs();
            for (var i = 0; i < vertexFormats.Length; i++)
            {
                var vertexFormat = vertexFormats[i];
                attributes[i].format = vertexFormat;
            }

            description.label = (char*) CNamePointer;

            Handle = sg_make_pipeline(ref description);
        }

        public void Apply()
        {
            sg_apply_pipeline(Handle);
        }

        ~SgPipeline()
        {
            ReleaseUnmanagedResources();
        }
    }
}