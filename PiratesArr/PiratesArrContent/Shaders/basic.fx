texture2D diffuseMap_sand;
sampler2D sand_Sampler = sampler_state
{
	Texture   = <diffuseMap_sand>;
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
	output.color=tex2D(sand_Sampler,input.vec2_textureCoords);

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



