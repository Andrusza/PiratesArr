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
}
;
// TODO: add effect parameters here.

struct VertexShaderInput
{
    float4 Position : POSITION0;
	 float2 vec2_textureCoords : TEXCOORD0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	 float2 vec2_textureCoords : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	 float3 color=tex2D(d0_Sampler,input.vec2_textureCoords);

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);

	output.vec2_textureCoords=input.vec2_textureCoords;
    output.Position = mul(viewPosition, Projection);

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
	 float3 color=tex2D(d0_Sampler,input.vec2_textureCoords);

    return float4(color, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
