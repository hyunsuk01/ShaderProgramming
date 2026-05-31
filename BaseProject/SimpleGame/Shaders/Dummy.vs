#version 330

uniform float u_Time;
uniform vec4 u_DropInfo[1000]; //vec4(x, y, sT, lT)

in vec3 a_Pos; // -0.5 ~ 0.5

out float v_Grey;
out vec2 v_Tex;

float c_PI = 3.141592;

void Frag()
{
	float tX, tY;
	tX = a_Pos.x + 0.5;
	tY = 1.0 - (a_Pos.y + 0.5);
	v_Tex = vec2(tX, tY);

	float value = a_Pos.x + 0.5;
	float newX = a_Pos.x;
	float newY = a_Pos.y*(1.0-(value*0.5)) + 
	value*0.25*sin((newX + 0.5)*2.0*c_PI - u_Time);

	vec4 final = vec4(newX, newY, 0.0, 1.0);

	vec4 newPosition = final;

	float grey = 
	(sin((newX + 0.5)*2.0*c_PI - u_Time)+1.0)/2.0;
	
	v_Grey = grey;

	gl_Position = newPosition;
}

void Circle()
{
	float accum = 0;

	for(int i = 0; i<100; i++)
	{
		vec2 center = u_DropInfo[i].xy - vec2(0.5, 0.5);
		vec2 pos = a_Pos.xy;
		float lTime = u_DropInfo[i].z;
		float sTime = u_DropInfo[i].w;
		float nTime = u_Time - sTime;

		if(nTime > 0)
		{
			float lVal = fract(nTime/lTime); //0 ~ 1
			float oneMinus = 1.0 - lVal;
			float t = lVal * lTime;
			float d = distance(center, pos);
			float range = t/10.0;
			float fade = 10.0*clamp(range - d, 0, 1.0);
			float sinValue = pow(abs(sin(d*4*c_PI*8 + t*2)), 3.0);

			accum += sinValue*fade*oneMinus;
		}
	}
	v_Grey = accum;
	//gl_Position = vec4(a_Pos, 1.0);
	gl_Position = vec4(a_Pos.x, a_Pos.y + 0.02*accum, a_Pos.z, 1.0);
}

void main()
{
	Circle();
}