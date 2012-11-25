float4x4 World : World;
float4x4 vp : ViewProjection;
float3 EyePosition : CAMERAPOSITION;
float time;

float3 worldUp = float3(0,1,0);

float4 lightColor=float4(1,1,1,1);

texture dropTexture;
sampler dropTextureSampler = sampler_state 
{ 
	Texture = <dropTexture>; 
	MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
};

struct VertexIn
{
	float4 Position   	: POSITION0;         	
    float2 TextureCoords: TEXCOORD0; 
};
struct VertexOut
{
    float4 Position   	: POSITION0;      
	float4 sPos   	: TEXCOORD1;      
    float2 TextureCoords: TEXCOORD0;
    float4  Color		: COLOR0;
    float image : COLOR1;
    float lightLerp : TEXCOORD2;
};

struct PixelToFrame
{
    float4 Color : COLOR0;
};

VertexOut VS(VertexIn input, float4x4 instanceTransform : BLENDWEIGHT)
{
	VertexOut Out = (VertexOut)0;

	float4x4 world = transpose(instanceTransform);
	input.Position.xyz = float3(world._41,world._42,world._43);
	input.Position.y =(input.Position.y- (time*world._23) % 320 );
	
	float3 center = mul(input.Position,World);	
	float3 eyeVector = center - EyePosition;
	
	float3 finalPos = center;
	float3 sideVector;
	float3 upVector;	
	
	sideVector = normalize(cross(eyeVector,worldUp));			
	upVector = normalize(cross(sideVector,eyeVector));	
	
	finalPos += (input.TextureCoords.x - 0.5) * sideVector * world._13;
	finalPos += (0.5 - input.TextureCoords.y) * upVector * (world._24);	

	float4 finalPos4 = float4(finalPos,1);	
	
	Out.Position = mul(finalPos4,vp);

	Out.sPos = Out.Position;
	
	Out.TextureCoords = input.TextureCoords;
	
	Out.Color = float4(.9,.9,.9,1);
	Out.Color.a = 1.0f;
	
	return Out;
}

PixelToFrame PS(VertexOut input)
{
	PixelToFrame Out = (PixelToFrame)0;
	
	Out.Color = tex2D(dropTextureSampler, input.TextureCoords);
	
	Out.Color.a *= Out.Color;
	
	// Draw lighter as we go down the texture.
	Out.Color.a *= 1-input.TextureCoords.y;

	return Out;
}

technique Rain
{
	pass P0 
	{
		VertexShader = compile vs_3_0 VS();
		PixelShader  = compile ps_3_0 PS();
	}
}