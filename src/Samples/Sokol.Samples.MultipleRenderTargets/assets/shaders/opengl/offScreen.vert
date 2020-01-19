#version 330
uniform mat4 mvp;
layout(location=0) in vec4 position;
layout(location=1) in float bright0;
out float bright;
void main() {
  gl_Position = mvp * position;
  bright = bright0;
}