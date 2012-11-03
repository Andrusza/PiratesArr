float4x4 World;
float4x4 View;
float4x4 Projection;

texture d0_Sand;
sampler SandSampler = sampler_state
{
	texture = <d0_Sand>;
	AddressU = Wrap;
	AddressV = Wrap;
	MinFilter = Anisotropic;
	MagFilter = Anisotropic;
};

texture d1_Grass;
sampler GrassSampler = sampler_state
{
	texture = <d1_Grass>;
	AddressU = Wrap;
	AddressV = Wrap;
	MinFilter = Anisotropic;
	MagFilter = Anisotropic;
};

texture d2_Snow;
sampler SnowSampler = sampler_state
{
	texture = <d2_Snow>;
	AddressU = Wrap;
	AddressV = Wrap;
	MinFilter = Anisotropic;
	MagFilter = Anisotropic;
};

texture WeightMap;
sampler WeightMapSampler = sampler_state 
{
	texture = <WeightMap>;
	AddressU = Clamp;
	AddressV = Clamp;
	MinFilter = Linear;
	MagFilter = Linear;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float3 Normal:NORMAL0;
	float4 TexCoord:TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 TexCoord:TEXCOORD0; 
	float3 ToLight: TEXCOORD1;
	float3 Normal:TEXCOORD2;
};

float3 LightPosition;

float3 AmbientLightColor=float3(1,1,1);
float3 DiffuseLightColor=float3(1,1,1);

float AmbientIntensity=0.2;
float DiffuseIntensity=0.6;



VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
	
	output.ToLight  = LightPosition - worldPosition;
	output.Normal   = mul(input.Normal, World);
	
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.TexCoord=input.TexCoord;
	
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 Snow=tex2D( SnowSampler,input.TexCoord);
	float3 Grass=tex2D( GrassSampler,input.TexCoord);
	float3 Sand=tex2D( SandSampler,input.TexCoord);
    float3 Height=tex2D( WeightMapSampler,input.TexCoord);
	
	float3 color=0;
	if(Height.x!=1)
	{
	
	float3 ToLight = normalize(input.ToLight);
	float3 diffuse = saturate(dot(ToLight,input.Normal));
	
	
	float3 ambientColor=AmbientLightColor*AmbientIntensity;
	float3 diffuseColor=DiffuseLightColor*DiffuseIntensity*diffuse;
	
	
	color=Height.x*Sand+Height.y*Grass+Height.z*Snow;
	float3 finalColor=color*ambientColor+color*diffuseColor;
	return float4(finalColor, 1);
	}
	else
	{
	return 0;
	}
	
	
}

technique MultiTexturing
{
    pass Pass0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
