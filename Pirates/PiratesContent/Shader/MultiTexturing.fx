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
	float4 TexCoord:TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 TexCoord:TEXCOORD0; 
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
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
	color=Height.x*Sand+Height.y*Grass+Height.z*Snow;
	return float4(color, 1);
	}
	else
	{
	return float4(0,0,0,0);
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
