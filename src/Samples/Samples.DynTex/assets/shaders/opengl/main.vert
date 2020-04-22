#version 330
uniform mat4 mvp;
layout(location=0) in vec4 position;
layout(location=1) in vec4 color0;
layout(location=2) in vec2 texcoord0;
out vec2 uv;
out vec4 color;
void main()
{
    gl_Position = mvp * position;
    uv = texcoord0;
    color = color0;
}