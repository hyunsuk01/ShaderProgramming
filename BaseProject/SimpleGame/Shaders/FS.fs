#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_TPos;

void main()
{
	if(v_TPos.x  + v_TPos.y < 0.5)
	{
		FragColor = vec4(1, 0, 0, 1);
	}
	else
	{
		FragColor = vec4(0, 0, 1, 1);
	}
}
