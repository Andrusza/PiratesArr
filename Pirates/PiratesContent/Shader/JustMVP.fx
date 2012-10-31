float4x4 World;
float4x4 View;
float4x4 Projection;

texture2D diffuseMap0;
sampler2D d0_Sampler = sampler_state
{
    Texture   = <diffuseMap0>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2  TextureCoord:TEXCOORD0;

  
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2  TextureCoord:TEXCOORD0;
  
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	
	output.TextureCoord=input.TextureCoord;
    

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float3 color=tex2D(d0_Sampler,input.TextureCoord);
    return float4(color, 1);
}

technique Basic
{
    pass Pass0
    {
     
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
