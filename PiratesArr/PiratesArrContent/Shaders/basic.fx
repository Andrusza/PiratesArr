texture2D diffuseMap0;
sampler2D d0_Sampler = sampler_state
{
	Texture   = <diffuseMap0>;
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
	float4 vec4_normal		  : NORMAL0;
	float2 vec2_textureCoords : TEXCOORD0;
	float3 vec3_View          : TEXCOORD1;
};

struct PixelOut
{
    float4 color : COLOR0;
};

float4 ambientColor;
float  ambientIntensity;

float3 lightDirection;
float4 diffuseColor;
float  diffuseIntensity;

float4 vec4_eye;
float4 specularColor;

float4x4 mat_World;
float4x4 mat_MVP;

float time;

PixelIn TexturedVS(VertexIn input)
{	
	PixelIn output = (PixelIn)0;
	//input.vec4_position.y += sin(input.vec4_position.z*time)/2;

	output.vec4_position = mul(input.vec4_position, mat_MVP);	
	output.vec2_textureCoords=input.vec2_textureCoords;
	
	float3 normal =normalize(mul(input.vec3_normal, mat_World));
	output.vec4_normal=float4(normal,1.0);
	
	
	float4 worldPosition = mul(input.vec4_position, mat_World);
	output.vec3_View = normalize(input.vec4_position - worldPosition);
	
	
	return output;    
}

PixelOut TexturedPS(PixelIn input) 
{
	PixelOut output = (PixelOut)0;	

	//float4 color=tex2D(d0_Sampler,input.vec2_textureCoords);
	float4 color=float4(1,1,1,1);
	
	float4 AmbientColor=ambientColor * ambientIntensity;
	
	float4  diffuse = saturate(dot(-lightDirection,input.vec4_normal));
	float4 DiffuseColor=diffuseColor * diffuseIntensity * diffuse;
	
	float4 reflect = normalize(2*diffuse*input.vec4_normal-float4(lightDirection,1.0));
	float4 specular = pow(saturate(dot(reflect,input.vec3_View)),15);
	float4 SpecularColor=diffuse+specularColor*specular;
	
	color=color * DiffuseColor + color * AmbientColor + color* SpecularColor;
	output.color=color;

	return output;
}

technique Textured
{
	pass Pass0
	{   
		VertexShader = compile vs_3_0 TexturedVS();
		PixelShader  = compile ps_3_0 TexturedPS();
	}
}



