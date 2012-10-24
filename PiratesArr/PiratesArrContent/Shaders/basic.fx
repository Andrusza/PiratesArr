texture2D diffuseMap_water;
sampler2D sand_Sampler = sampler_state
{
	Texture   = <diffuseMap_water>;
	MinFilter = linear;
	MagFilter = linear;
	MipFilter = linear;
};


struct VertexIn
{
	float4 vec4_position      : POSITION0;
	float3 vec3_normal		  : NORMAL0;
	float2 vec2_textureCoords : TEXCOORD0;
	float3 vec3_tangent       : TANGENT0;
	float3 vec3_binormal      : BINORMAL0;
};

struct PixelIn
{
	float4 vec4_position      : POSITION0;
	float2 vec2_textureCoords : TEXCOORD0;
};

struct PixelOut
{
    float4 color : COLOR0;
};

// Light related
float4 AmbientColor=float4(1,1,1,1);
float AmbientIntensity=1.0f;

float4 DiffuseColor=float4(1,1,0,1);
float DiffuseIntensity=1.0f;

float4 SpecularColor;


float4x4 mat_MVP;


PixelIn TexturedVS(VertexIn input)
{	
	PixelIn output = (PixelIn)0;

	output.vec4_position = mul(input.vec4_position, mat_MVP);	
	output.vec2_textureCoords=input.vec2_textureCoords;
	
	return output;    
}

PixelOut TexturedPS(PixelIn input) 
{
	PixelOut output = (PixelOut)0;		
	float4 color=tex2D(sand_Sampler,input.vec2_textureCoords);
	color=color * AmbientColor * AmbientIntensity; //+ color * DiffuseIntensity * DiffuseColor;
	output.color=color;

	return output;
}

technique Textured
{
	pass Pass0
	{   
		VertexShader = compile vs_2_0 TexturedVS();
		PixelShader  = compile ps_2_0 TexturedPS();
	}
}



