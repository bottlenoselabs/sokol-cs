#version 330
in float bright;
layout(location=0) out vec4 frag_color_0;
layout(location=1) out vec4 frag_color_1;
layout(location=2) out vec4 frag_color_2;
void main() {
  frag_color_0 = vec4(bright, 0.0, 0.0, 1.0);
  frag_color_1 = vec4(0.0, bright, 0.0, 1.0);
  frag_color_2 = vec4(0.0, 0.0, bright, 1.0);
}