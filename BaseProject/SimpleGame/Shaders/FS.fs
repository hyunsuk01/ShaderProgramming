#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_TPos;

uniform float u_Time;
uniform sampler2D u_RGBTex; //0
uniform sampler2D u_CurrNumTex;
uniform sampler2D u_NumsTex;
uniform int u_InputNum;

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

void Flag()
{
	float amp = 0.5;
	float speed = 15;
	float sinInput = v_TPos.x * c_PI * 2 - u_Time * speed;
	float sinValue = v_TPos.x*amp*(((sin(sinInput) + 1) / 2)-0.5)+0.5; //0~1
	
	float fWidth = 0.0;
	float width = 0.5 * mix(1, fWidth, v_TPos.x);
	float grey = 0;

	if(v_TPos.y < sinValue + width/2 && v_TPos.y > sinValue - width/2)
	{
		grey = 1;
	}
	else
	{
		grey = 0;
		discard;
	}

	FragColor = vec4(grey);
}

void Flame()
{
	float amp = 0.5;
	float speed = 15;
	float newY = 1 - v_TPos.y;
	float sinInput = newY * c_PI * 2 - u_Time * speed;
	float sinValue = newY*amp*(((sin(sinInput) + 1) / 2)-0.5)+0.5; //0~1
	
	float fWidth = 0.0;
	float width = 0.5 * mix(fWidth, 1, newY);
	float grey = 0;

	if(v_TPos.x < sinValue + width/2 && v_TPos.x > sinValue - width/2)
	{
		grey = 1;
	}
	else
	{
		grey = 0;
		discard;
	}

	FragColor = vec4(grey);
}

void TextureSampling()
{
	vec4 c0;
	vec4 c1;
	vec4 c2;
	vec4 c3;
	vec4 c4;

	float offsetX = 0.01;
	
	c0 = texture(u_RGBTex, vec2(v_TPos.x - offsetX * 2, v_TPos.y));
	c1 = texture(u_RGBTex, vec2(v_TPos.x - offsetX * 1, v_TPos.y));
	c2 = texture(u_RGBTex, vec2(v_TPos.x - offsetX * 0, v_TPos.y));
	c3 = texture(u_RGBTex, vec2(v_TPos.x + offsetX * 1, v_TPos.y));
	c4 = texture(u_RGBTex, vec2(v_TPos.x + offsetX * 2, v_TPos.y));

	vec4 sum = c0 + c1 + c2 + c3 + c4;
	sum = sum / 5;

	FragColor = sum;
}

void TextureQ1()
{
	float tx = v_TPos.x;
	float ty = 1 - 2 * abs(v_TPos.y - 0.5);
	vec2 newTex = vec2(tx, ty);
	FragColor = texture(u_RGBTex, newTex);
}

void TextureQ2()
{
	float tx = fract(v_TPos.x * 3);
	float ty = v_TPos.y / 3;

	float offsetX = 0;
	float offsetY = (2 - floor(v_TPos.x * 3)) / 3;

	vec2 newTex = vec2(tx + offsetX, ty + offsetY);
	FragColor = texture(u_RGBTex, newTex);
}

void TextureQ3()
{
	float tx = fract(v_TPos.x * 3);
	float ty = v_TPos.y / 3;

	float offsetX = 0;
	float offsetY = floor(v_TPos.x * 3) / 3;

	vec2 newTex = vec2(tx + offsetX, ty + offsetY);
	FragColor = texture(u_RGBTex, newTex);
}

void TextureQ4()
{
	float resolX = 5;
	float resolY = 5;
	float shear = 0.5 * u_Time;

	float offsetX = fract(ceil(v_TPos.y*resolY) * shear); //offset
	float offsetY = 0;

	float tx = fract(v_TPos.x * resolX + offsetX); //range
	float ty = fract(v_TPos.y * resolY + offsetY);

	vec2 newTex = vec2(tx, ty);
	FragColor = texture(u_RGBTex, newTex);
}

void Num()
{
	float tx = v_TPos.x;
	float ty = v_TPos.y;

	float offsetX = 0;
	float offsetY = 0;

	vec2 newTex = vec2(tx + offsetX, ty + offsetY);
	FragColor = texture(u_CurrNumTex, newTex);
}

void Nums()
{
	float index = float(u_InputNum);

	float tx = v_TPos.x / 5;
	float ty = v_TPos.y / 2;

	float offsetX = fract(index / 5.0);
	float offsetY = floor(index / 5.0) / 2.0;

	vec2 newTex = vec2(tx + offsetX, ty + offsetY);
	FragColor = texture(u_NumsTex, newTex);
}

void main()
{
	//TextureSampling();
	Nums();
}
