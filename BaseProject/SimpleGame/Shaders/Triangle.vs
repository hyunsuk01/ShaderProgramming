#version 330

uniform float u_Time;

in vec3 a_Position;
in float a_Mass;
in vec2 a_Vel;

const float c_PI = 3.141592;
const float c_G = -9.8;

void Sin1()
{
	float t = u_Time;
	vec4 newPosition;
	newPosition.x = a_Position.x + t;
	newPosition.y = a_Position.y + sin(t * 3.141592 * 2) * 0.5;
	newPosition.z = 0;
	newPosition.w = 1.0;
	
	gl_Position = newPosition;
}

void Sin2()
{
	float t = u_Time;
	vec4 newPosition;
	newPosition.x = a_Position.x + t - 1;
	newPosition.y = a_Position.y + sin(t * 3.141592) * 0.5;
	newPosition.z = 0;
	newPosition.w = 1.0;
	
	gl_Position = newPosition;
}

void Circle()
{
	float t = u_Time;
	vec4 newPosition;
	newPosition.x = a_Position.x - cos(t * 2 * 3.141592);
	newPosition.y = a_Position.y + sin(t * 2 * 3.141592);
	newPosition.z = 0;
	newPosition.w = 1.0;
	
	gl_Position = newPosition;
}

void Lissajous()
{
	float t = u_Time;

	vec4 newPosition;

	newPosition.x = a_Position.x + sin(t * 2 * 3.141592) * 0.3;
	newPosition.y = a_Position.y + sin(t * 3 * 3.141592) * 0.3;

	newPosition.z = 0;
	newPosition.w = 1.0;

	gl_Position = newPosition;
}

void PrettyMove()
{
	float t = u_Time;

	vec4 newPosition;

	newPosition.x = a_Position.x + sin(t * 3.141592) * 0.7 + sin(t * 3) * 0.2;
	newPosition.y = a_Position.y + cos(t * 2 * 3.141592) * 0.5;

	newPosition.z = 0;
	newPosition.w = 1.0;

	gl_Position = newPosition;
}

void Spiral()
{
	float t = u_Time;
	float r = t * 0.1;

	vec4 newPosition;

	newPosition.x = a_Position.x + r * cos(t * 3.141592);
	newPosition.y = a_Position.y + r * sin(t * 3.141592);

	newPosition.z = 0;
	newPosition.w = 1.0;

	gl_Position = newPosition;
}

void Falling()
{
    float t = mod(u_Time, 1.0); // 0 ~ 1 구간 반복
	float tt = t*t;
	float vx, vy;
	vx = a_Vel.x;
	vy = a_Vel.y;
	
	vec4 newPos;
	newPos.x = a_Position.x + vx*t;
	newPos.y = a_Position.y + vy*t + 0.5*c_G*tt;
	newPos.z = 0;
	newPos.w = 1;

	gl_Position = newPos;
}

void main()
{
	Falling();
}
