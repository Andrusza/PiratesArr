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

struct VertexShaderInput
{
    float4 Position      : POSITION0;
    float3 Normal        : NORMAL0;
    float2 TextureCoord  : TEXCOORD0;
    float3 Tangent       : TANGENT0;
    float3 Binormal      : BINORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TextureCoord:TEXCOORD0;
	float3x3 WorldToTangentSpace:TEXCOORD1;
	float3  ToLight :TEXCOORD4;
	float3  ToView  :TEXCOORD5;
};

float4x4 World;
float4x4 WorldInverseTranspose;
float4x4 ViewInverseTranspose;
float4x4 ReflectionView;

float4x4 MVP;

float3 LightPosition;

float Shininess;

float3 AmbientLightColor=float3(1,1,1);
float3 DiffuseLightColor=float3(1,1,1);
float3 SpecularLightColor=float3(0.98,0.97,0.7);


float AmbientIntensity;
float DiffuseIntensity;
float SpecularIntensity;

bool Clipping;
float4 ClipPlane0;

Texture xReflectionMap;

sampler ReflectionSampler = sampler_state 
{ 
	texture = <xReflectionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter=LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};

Texture xRefractionMap;
sampler RefractionSampler = sampler_state 
{ 	
	texture = <xRefractionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter=LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};


float3 eyeFromViewInverseTranspose()
{
	return float3(ViewInverseTranspose[0].w,ViewInverseTranspose[1].w,ViewInverseTranspose[2].w);
}

float3 shade(float3 Ln, float3 Nn, float3 Vn, float2 UV){
	// simple lambert diffuse
	float3 color=tex2D(d0_Sampler,UV);

	float diffContrib = saturate(dot(Nn, Ln));
	float3 diffuse = color *diffContrib;
	
	// phong specular
	float3 Rn = reflect(-Ln, Nn);
	float  RV = saturate(dot(Rn, Vn));
	float  specContrib = pow(RV, Shininess);	
	float3 spec = float3(1,1,1)*specContrib;
	float3 ambient=	color*AmbientLightColor*AmbientIntensity;
	
	return ambient+spec + diffuse ;
}

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    output.Position = mul(input.Position, MVP);
	
	// Matrix for transformation to tangent space
	output.WorldToTangentSpace[0] = mul(normalize(input.Tangent), WorldInverseTranspose);
    output.WorldToTangentSpace[1] = mul(normalize(input.Binormal), WorldInverseTranspose);
    output.WorldToTangentSpace[2] = mul(normalize(input.Normal), WorldInverseTranspose);
	
	// Get the position in eye coordinates
	float3 pos = mul(input.Position,World);
	
	// vector to light and to eye in world space
    float3 toLight  = LightPosition - pos;
	float3 toViewer = eyeFromViewInverseTranspose() - pos;
	
	// world to tangent space
	output.ToLight = mul(output.WorldToTangentSpace, toLight);
	output.ToView  = mul(output.WorldToTangentSpace, toViewer);
	
	output.TextureCoord=input.TextureCoord;
	
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 Nn = 2*tex2D(n0_Sampler, input.TextureCoord)-1;
	float3 Ln = normalize(input.ToLight);
	float3 Vn = normalize(input.ToView);
	
	float3 pixel = shade(Ln, Nn, Vn, input.TextureCoord);
	return float4(pixel,1);
}

technique Basic
{
    pass Pass0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
