#version 330
uniform mat4 mvp;
uniform vec3 offset0;
uniform vec3 offset1;
uniform vec3 offset2;
layout(location=0) in vec4 position;
layout(location=1) in vec2 texcoord0;
out vec3 uv0;
out vec3 uv1;
out vec3 uv2;
void main()
{
    gl_Position = mvp * position;
    uv0 = vec3(texcoord0 + offset0.xy, offset0.z);
    uv1 = vec3(texcoord0 + offset1.xy, offset1.z);
    uv2 = vec3(texcoord0 + offset2.xy, offset2.z);
}