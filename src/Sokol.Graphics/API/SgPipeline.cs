/* 
MIT License

Copyright (c) 2019 Lucas Girouard-Stranks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using static Sokol.sokol_gfx;

namespace Sokol
{
    public sealed class SgPipeline : SgResource
    {
        internal readonly sg_pipeline Handle;
        
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