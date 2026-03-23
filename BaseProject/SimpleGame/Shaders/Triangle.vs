#version 330

uniform float u_Time;

in vec3 a_Position;
in float a_Mass;
in vec2 a_Vel;
in float a_RV;
in float a_RV1;

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

float random(float x)
{
	return fract(sin(x) * 43758.5453123);
}

void Falling()
{
	float startTime = a_RV1;
	float newTime = u_Time - startTime;

	if(newTime > 0)
	{
		float t = mod(newTime, 1.0); // 0 ~ 1 구간 반복
		float tt = t*t;
		float vx, vy;
		float sx, sy;
		vx = a_Vel.x/30;
		vy = a_Vel.y/30;
	
		sx = a_Position.x * random(a_RV1) + sin(a_RV*2*c_PI);
		sy = a_Position.y * random(a_RV1) + cos(a_RV*2*c_PI);

		vec4 newPos;
		newPos.x = sx + vx*t;
		newPos.y = sy + vy*t + 0.5*c_G*tt;
		newPos.z = 0;
		newPos.w = 1;

		gl_Position = newPos;
	}
	else
	{
		gl_Position = vec4(-1000, 0, 0, 1);
	}
}

float rand(float x)
{
    return fract(sin(x * 12.9898) * 43758.5453);
}

void Falling1()
{
    float startTime = a_RV1 * 2.0; // 시간 분산
    float newTime = u_Time - startTime;

    if(newTime > 0.0)
    {
        float t = mod(newTime, 1.0);
        float tt = t * t;

        float vx = a_Vel.x / 20.0;
        float vy = a_Vel.y / 20.0;

        float baseX = sin(a_RV * 2.0 * c_PI);
        float baseY = cos(a_RV * 2.0 * c_PI);

        float spiral = t * 0.5;
        baseX += spiral * cos(t * 10.0 + a_RV * 6.28);
        baseY += spiral * sin(t * 10.0 + a_RV * 6.28);

        float cx = baseX + vx * t;
        float cy = baseY + vy * t + 0.5 * c_G * tt;

        float angle = a_RV * 6.28 + t * 8.0;
        float cosA = cos(angle);
        float sinA = sin(angle);

        float scale = (1.0 - t) * (0.5 + random(a_RV1));

        vec2 local;
        local.x = a_Position.x * scale;
        local.y = a_Position.y * scale;

        vec2 rotated;
        rotated.x = local.x * cosA - local.y * sinA;
        rotated.y = local.x * sinA + local.y * cosA;

        float jitterX = sin(t * 20.0 + a_RV1 * 15.0) * 0.02;
        float jitterY = cos(t * 20.0 + a_RV1 * 15.0) * 0.02;

        vec4 newPos;
        newPos.x = cx + rotated.x + jitterX;
        newPos.y = cy + rotated.y + jitterY;
        newPos.z = 0;
        newPos.w = 1;

        gl_Position = newPos;
    }
    else
    {
        gl_Position = vec4(0,0,0,0);
    }
}

void main()
{
	Falling1();
}
