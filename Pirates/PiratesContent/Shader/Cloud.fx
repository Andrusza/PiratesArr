float4x4 World ;
float4x4 vp ;
float3 EyePosition;

float3 worldUp = float3(0,-1,0);

float4 lightColor = float4(1,0,1,1);

float MistingDistance = 50;

const float2 imgageUV[16] = {
		float2(0,0),float2(.25,0),float2(.50,0),float2(.75,0),
		float2(0,.25),float2(.25,.25),float2(.50,.25),float2(.75,.25),
		float2(0,.50),float2(.25,.50),float2(.50,.50),float2(.75,.50),
		float2(0,.75),float2(.25,.75),float2(.50,.75),float2(.75,.75)};

texture partTexture;
sampler partTextureSampler = sampler_state 
{ 
	Texture = <partTexture>; 
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

float4 GetTexture(float2 texCoord,float img)
{
	texCoord = (texCoord * .25) +  imgageUV[round(img * 100.0f)];
	return tex2D(partTextureSampler, texCoord);
}
float2 GetTexCoords(float2 texCoord,float img)
{
	return (texCoord * .25) +  imgageUV[round(img * 100.0f)];
}
VertexOut VS(VertexIn input, float4x4 instanceTransform : BLENDWEIGHT)
{
	VertexOut Out = (VertexOut)0;

	float4x4 world = transpose(instanceTransform);
	input.Position.xyz = float3(world._41,world._42,world._43);
	
	float3 center = mul(input.Position,World);	
	float3 eyeVector =center - EyePosition;
	
	float3 finalPos = center;
	float3 sideVector;
	float3 upVector;	
	
	sideVector = normalize(cross(eyeVector,worldUp));			
	upVector = normalize(cross(sideVector,eyeVector));	
	
	finalPos += (input.TextureCoords.x - 0.5) * sideVector * 100;
	finalPos += (0.5 - input.TextureCoords.y) * upVector * 100;

	float4 finalPos4 = float4(finalPos,1);	
	
	Out.Position = mul(finalPos4,vp);

	Out.sPos = Out.Position;
	
	Out.TextureCoords = input.TextureCoords;
	
	Out.Color = float4(.9,.9,.9,1) * world._34;	
	
	// Which sprite to draw...
	Out.image = world._12;
	
	// Alpha
	Out.Color.a = world._23;
	
	// Misting	
	float distVal =  length(abs(finalPos4 - EyePosition));
	float distVal2 = length(abs(center - EyePosition));
	
	
	if(distVal <= MistingDistance)
		Out.Color.a *= distVal / MistingDistance;		
		
	if(distVal2 <= MistingDistance)
		Out.Color.a *= distVal2 / MistingDistance;	
	

	return Out;
}

PixelToFrame PS(VertexOut input)
{
	PixelToFrame Out = (PixelToFrame)0;
	
	float color = GetTexture(input.TextureCoords,input.image).rgb;	
	
	
	
	Out.Color = input.Color;
	
	Out.Color.a *= color;
	
	// Draw lighter as we go down the texture.
	Out.Color.a *= 1-input.TextureCoords.y;

	
	// Misting	
	float distVal =  input.sPos.z * .0025f;
	
	Out.Color.a *= saturate(distVal);
	

	return Out;
}

technique Clouds
{
	pass P0 
	{
		VertexShader = compile vs_3_0 VS();
		PixelShader  = compile ps_3_0 PS();
	}
}