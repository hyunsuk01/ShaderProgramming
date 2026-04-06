#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_TPos;

uniform float u_Time;

const float c_PI = 3.141592;

uniform vec4 u_DropInfo[1000]; //vec4(x, y, sT, lT)

void Simple()
{
	if(v_TPos.x  + v_TPos.y > 0.5)
	{
		FragColor = vec4(0);
	}
	else
	{
		FragColor = vec4(v_TPos, 0, 1);
	}
}

void LinePattern()
{
	float lineCountH = 10;
	float lineCountV = 2;
	float lineWidth = 1;
	lineCountH = lineCountH / 2;
	lineCountV = lineCountV / 2;
	lineWidth = 50 / lineWidth;
	float per = - 0.5*c_PI;
	float grey = pow(abs(sin((v_TPos.y*2*c_PI+per)*lineCountH)), lineWidth);
	float grey1 = pow(abs(sin((v_TPos.x*2*c_PI+per)*lineCountV)), lineWidth);

	FragColor = vec4(grey + grey1);
}

void Circle()
{
	vec2 center = vec2(0.5, 0.5);
	vec2 currPos = v_TPos.xy;
	float d = distance(center, currPos);
	float lineWidth = 0.01;
	float radius = 0.1;

	if(d > radius - lineWidth && d < radius)
	{
		FragColor = vec4(1);
	}
	else
	{
		FragColor = vec4(0);
	}
}

void CircleSin()
{
	vec2 center = vec2(0.5, 0.5);
	vec2 currPos = v_TPos.xy;
	float d = distance(center, currPos);
	float value = abs(sin(d * c_PI * 16 - u_Time * 20));
	FragColor = vec4(pow(value, 16));
}

//-----------------------------------------------------------------------
vec2 repeat(vec2 p, float scale)
{
    p *= scale;
    return fract(p) - 0.5;
}

void CircleSin1()
{
    vec2 center = vec2(0.5, 0.5);
    vec2 currPos = v_TPos.xy;

    currPos = repeat(currPos - center, 3.0 + sin(u_Time) * 2.0) + center;

    float d = distance(center, currPos);

    float value = abs(sin(d * c_PI * 16.0 - u_Time * 20.0));

    FragColor = vec4(pow(value, 16.0));
}
//-----------------------------------------------------------------------

void RainDrop()
{
	float accum = 0;
	//RainDrop
	for(int i=0;i<1000;i++)
	{
		float lTime = u_DropInfo[i].w;
		float sTime = u_DropInfo[i].z;
		float newTime = u_Time - sTime;

		if(newTime > 0)
		{
			newTime = fract(newTime/lTime); // 0~1
			float oneMinus = 1 - newTime; // 1~0
			float t = newTime * lTime;

			vec2 center = u_DropInfo[i].xy;
			vec2 currPos = v_TPos.xy;
	
			float range = t/10;
			float d = distance(center, currPos);
	
			float fade = 30 * clamp(range - d, 0, 1);

			float value = pow(abs(sin(d * 4 * c_PI * 10 - t * 15)), 16);
	
			accum += value * fade * oneMinus;
		}
		else
		{
		}
	}
	FragColor = vec4(accum);
}

void RainDrop1()
{
    float accum = 0.0;

    for(int i = 0; i < 300; i++)
    {
        float lTime = u_DropInfo[i].w;
        float sTime = u_DropInfo[i].z;
        float newTime = u_Time - sTime;

        if(newTime > 0.0)
        {
            float nt = fract(newTime / lTime); // 0~1
            float oneMinus = 1.0 - nt;
            float t = nt * lTime;

            vec2 center = u_DropInfo[i].xy;
            vec2 currPos = v_TPos.xy;

            float d = distance(center, currPos);

            float radius = t * 0.3;

            float ring = smoothstep(radius + 0.02, radius, d);

            float ripple = sin(d * 80.0 - t * 20.0) * 0.5 + 0.5;

            float drop = smoothstep(0.05, 0.0, d) * (1.0 - nt);

            float fade = exp(-d * 10.0) * oneMinus;

            float value = (ring * ripple + drop) * fade;

            accum += value;
        }
    }

    accum = pow(accum, 0.8);

    FragColor = vec4(vec3(accum), 1.0);
}

void main()
{
	RainDrop1();
}
