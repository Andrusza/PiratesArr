
//float time;
//float4x4 viewInverse;
//float4x4 vp;
//float4x4 World;

//struct VertexShaderInput
//{
//    float4 Position      : POSITION0;
//    float2 TextureCoord  : TEXCOORD0;
//    float3 Tangent       : TANGENT0;
//    float3 Binormal      : BINORMAL0;
//};

//struct VertexOut
//{
//	float4 Position:POSITION0;
//};


//VertexOut VertexShaderFunction(VertexShaderInput input, float4x4 instanceTransform : BLENDWEIGHT)
//{
//	VertexOut output = (VertexOut)0;

//	float4x4 world = transpose(instanceTransform);
//	input.Position.xyz = float3(world._41,world._42,world._43);
	
//	float4 position = mul(input.Position,World);
    
//float localTime = time + 0.1f; 
//float4 base_position = position + 0.1 * localTime;


//float4 quadX = viewInverse[0] * input.Position.x;
//float4 quadZ = viewInverse[1] * input.Position.y;

//position += + quadX + quadZ;

//output.Position = mul(position,vp);

//    return output;
//}

//float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
//{
//    return float4(1,1,1,1);
//}

//technique Rain
//{
//    pass Pass0
//    {
//        VertexShader = compile vs_2_0 VertexShaderFunction();
//        PixelShader = compile ps_2_0 PixelShaderFunction();
//    }
//}
