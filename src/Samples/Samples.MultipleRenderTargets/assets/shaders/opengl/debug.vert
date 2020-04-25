#version 330
layout(location=0) in vec2 pos;
out vec2 uv;
void main() {
  gl_Position = vec4(pos*2.0-1.0, 0.5, 1.0);
  uv = pos;
}  