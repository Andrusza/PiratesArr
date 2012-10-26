texture2D diffuseMap0;
sampler2D d0_Sampler = sampler_state
{
	Texture   = <diffuseMap0>;
	MinFilter = linear;
	MagFilter = linear;
	MipFilter = linear;
};

texture2D normalMap0;
sampler2D n0_Sampler = sampler_state
{
	Texture = <normalMap0>;
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
	float3 vec3_View          : TEXCOORD1;
	float3x3 WorldToTangentSpace : TEXCOORD2;
};

struct PixelOut
{
    float4 color : COLOR0;
};


float3 LightDirection;  // 0.7 0 -0.7
float4 DiffuseColor;     //White
float  DiffuseIntensity; // 0.5

float4 vec4_Eye;  //last column in view matrix
float4 SpecularColor; //white

float4x4 mat_World; //identity
float4x4 mat_View;  //camera matrix
float4x4 mat_Projection; //projection

float4 AmbientColor; //white
float AmbientIntensity; // 1.0


PixelIn TexturedVS(VertexIn input)
{	
	PixelIn output = (PixelIn)0;
	
	float4 worldPosition = mul(input.vec4_position, mat_World);
    float4 viewPosition = mul(worldPosition, mat_View);
    output.vec4_position = mul(viewPosition, mat_Projection);
	
	output.vec3_View = normalize(vec4_Eye - worldPosition);
	
	output.WorldToTangentSpace[0] = mul(normalize(input.vec3_tangent), mat_World);
	output.WorldToTangentSpace[1] = mul(normalize(input.vec3_binormal), mat_World);
	output.WorldToTangentSpace[2] = mul(normalize(input.vec3_normal), mat_World);
	
	output.vec2_textureCoords=input.vec2_textureCoords;
	
	return output;    
}

PixelOut TexturedPS(PixelIn input) 
{
	PixelOut output = (PixelOut)0;	

	float3 color=tex2D(d0_Sampler,input.vec2_textureCoords);
	
	float3 bump = 2.0 *(tex2D(n0_Sampler, input.vec2_textureCoords)) - 1.0;
	bump = normalize(mul(bump, input.WorldToTangentSpace));
	
	float3 diffuse = saturate(dot(-LightDirection,bump));
	
	float3 reflect = normalize(2*diffuse*bump-LightDirection);
	float3 specular = pow(saturate(dot(reflect,input.vec3_View)),5);
	
	float3 ambientColor=color*AmbientColor*AmbientIntensity;
	float3 diffuseColor=color*DiffuseColor*DiffuseIntensity*diffuse;
	float3 specularColor=color*SpecularColor*specular;
	
	output.color=float4(ambientColor+diffuseColor+specularColor,1);

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



