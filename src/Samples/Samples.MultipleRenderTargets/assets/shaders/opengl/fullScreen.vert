#version 330
uniform vec2 offset;
layout(location=0) in vec2 pos;
out vec2 uv0;
out vec2 uv1;
out vec2 uv2;
void main() {
  gl_Position = vec4(pos*2.0-1.0, 0.5, 1.0);
  uv0 = pos + vec2(offset.x, 0.0);
  uv1 = pos + vec2(0.0, offset.y);
  uv2 = pos;
}