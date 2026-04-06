#pragma once

#include <string>
#include <cstdlib>
#include <fstream>
#include <iostream>
#include <vector>

#include "Dependencies\glew.h"

class Renderer
{
public:
	Renderer(int windowSizeX, int windowSizeY);
	~Renderer();

	bool IsInitialized();
	void DrawSolidRect(float x, float y, float z, float size, float r, float g, float b, float a);
	void DrawTriangle();
	void DrawParticle();
	void DrawFS();

	void AddParticle(float x, float y, float z, float mass, float vx, float vy, float RV, float RV1, float RV2);

private:
	void Initialize(int windowSizeX, int windowSizeY);
	bool ReadFile(char* filename, std::string *target);
	void AddShader(GLuint ShaderProgram, const char* pShaderText, GLenum ShaderType);
	GLuint CompileShaders(char* filenameVS, char* filenameFS);
	void CreateVertexBufferObjects();
	void GetGLPosition(float x, float y, float *newX, float *newY);

	bool m_Initialized = false;
	
	unsigned int m_WindowSizeX = 0;
	unsigned int m_WindowSizeY = 0;

	GLuint m_VBORect = 0;
	GLuint m_SolidRectShader = 0;

	GLuint m_VBOTriangle = 0;
	GLuint m_TriangleShader = 0;

	GLuint m_VBOParticle = 0;
	GLuint m_ParticleShader = 0;
	std::vector<float> m_Particles;
	int m_ParticleCount = 0;

	//FragmentShader
	GLuint m_VBOFS = 0;
	GLuint m_FSShader = 0;

	//RainDrops
	float m_DropPoints[1000 * 4];
};

