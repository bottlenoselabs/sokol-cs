#version 330
uniform sampler2D tex0;
uniform sampler2D tex1;
uniform sampler2D tex2;
in vec2 uv0;
in vec2 uv1;
in vec2 uv2;
out vec4 frag_color;
void main() {
  vec3 c0 = texture(tex0, uv0).xyz;
  vec3 c1 = texture(tex1, uv1).xyz;
  vec3 c2 = texture(tex2, uv2).xyz;
  frag_color = vec4(c0 + c1 + c2, 1.0);
}