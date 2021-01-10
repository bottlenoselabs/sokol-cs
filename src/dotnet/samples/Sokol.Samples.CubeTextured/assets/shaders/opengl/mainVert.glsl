#version 330
uniform mat4 mvp;
layout(location=0) in vec4 position;
layout(location=1) in vec4 color0;
layout(location=2) in vec2 texcoord0;
out vec4 color;
out vec2 uv;
void main() {
  gl_Position = mvp * position;
  color = color0;
  uv = texcoord0;
}