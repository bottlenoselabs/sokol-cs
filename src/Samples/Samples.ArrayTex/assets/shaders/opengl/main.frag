#version 330
uniform sampler2DArray tex;
in vec3 uv0;
in vec3 uv1;
in vec3 uv2;
in vec3 cweights;
out vec4 frag_color;
void main()
{
    vec4 c0 = texture(tex, uv0);
    vec4 c1 = texture(tex, uv1);
    vec4 c2 = texture(tex, uv2);
    frag_color = vec4(c0.xyz * cweights.x + c1.xyz * cweights.y + c2.xyz * cweights.z, 1.0);
}