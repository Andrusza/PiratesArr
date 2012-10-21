struct VertexToPixel
{
    float4 Position   	: POSITION;    
    float4 Color		: COLOR0;
    float LightingFactor: TEXCOORD0;
    float2 TextureCoords: TEXCOORD1;
};

struct PixelToFrame
{
    float4 Color : COLOR0;
};

//------- Constants --------
float4x4 mat_View;
float4x4 mat_Projection;
float4x4 mat_Model;
float4x4 mat_MVP;


VertexToPixel TexturedVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float2 inTexCoords: TEXCOORD0)
{	
	VertexToPixel Output = (VertexToPixel)0;

	float4x4 preViewProjection = mat_MVP;
	float4x4 preWorldViewProjection = mat_MVP;
    
	Output.Position = mul(inPos, preWorldViewProjection);	
	Output.Color=float4(inNormal,1);
	
	
	return Output;    
}

PixelToFrame TexturedPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
	Output.Color=PSIn.Color;

	return Output;
}

////------- Technique: Textured --------
//
//VertexToPixel TexturedVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float2 inTexCoords: TEXCOORD0)
//{	
	//VertexToPixel Output = (VertexToPixel)0;
	//float4x4 preViewProjection = mul (xView, xProjection);
	//float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    //
	//Output.Position = mul(inPos, preWorldViewProjection);	
	//Output.TextureCoords = inTexCoords;
	//
	//float3 Normal = normalize(mul(normalize(inNormal), xWorld));	
	//Output.LightingFactor = 1;
	//if (xEnableLighting)
		//Output.LightingFactor = dot(Normal, -xLightDirection);
    //
	//return Output;    
//}
//
//PixelToFrame TexturedPS(VertexToPixel PSIn) 
//{
	//PixelToFrame Output = (PixelToFrame)0;		
	//
	//Output.Color = tex2D(TextureSampler, PSIn.TextureCoords);
	//Output.Color.rgb *= saturate(PSIn.LightingFactor) + xAmbient;
//
	//return Output;
//}
//
technique Textured
{
	pass Pass0
	{   
		VertexShader = compile vs_2_0 TexturedVS();
		PixelShader  = compile ps_2_0 TexturedPS();
	}
}



