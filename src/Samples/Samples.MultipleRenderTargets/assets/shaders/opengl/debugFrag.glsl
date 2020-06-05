#version 330
uniform sampler2D tex;
in vec2 uv;
out vec4 frag_color;
void main() {
  frag_color = vec4(texture(tex,uv).xyz, 1.0);
}