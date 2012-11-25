float time;
float amount=0.6;
float viscosity=0.02;

const float EtaR = 0.65;
const float EtaG = 0.67;         // Ratio of indices of refraction
const float EtaB = 0.69;
const float FresnelPower = 5.0;


texture currentFrame : POSTEFFECTINPUT;
sampler currentFrameSampler = sampler_state
{
	Texture = <currentFrame>;
	MipFilter = LINEAR;
    MinFilter = LINEAR;
    MagFilter = LINEAR;
	ADDRESSW = CLAMP;
    ADDRESSU = CLAMP;
    ADDRESSV = CLAMP;
};

texture waterNormal;
sampler WaterNormalMap = sampler_state
{
	Texture = <waterNormal>;
	AddressU = Wrap;
	AddressV = Wrap;
	AddressW = Wrap;
	MinFilter = Linear;
	MagFilter = Linear;
    MipFilter = Linear;
};

float4 ps_main(float2 TextureCoordinate : TEXCOORD0) : COLOR
{
	float4 noiseCoord;
	noiseCoord.xy = (TextureCoordinate * 0.5f) + float2(sin(0.25f * time), -0.25f * time);
	noiseCoord.zw = TextureCoordinate - float2(0.0f, time);

	float4 normalColor = (tex2D(WaterNormalMap, noiseCoord.xy) * 2.0f) - 1.0f;
	float4 animColor = (tex2D(WaterNormalMap, noiseCoord.zw) * 2.0f) - 1.0f;
	float4 currentFrameColor=tex2D(currentFrameSampler, TextureCoordinate);
	
	normalColor.z += animColor.w;
	float3 normal = normalize(normalColor.xyz);
	
	//Find the refraction from the normal.  It's essentially the normal restrained in such a way that 
	float3 refractionVec = normalize(normal * float3(viscosity, viscosity, 1.0f));
	
	//Fetch the screen by displacing the texture coordinates by the refraction
	float3 screenColor = tex2D(currentFrameSampler, TextureCoordinate - (refractionVec.xy * 0.2f * amount)).xyz;
	
	float2 refractHighlight = refractionVec.xy * amount;
	float3 refColor = (saturate(pow(refractHighlight.x, 6.0f)) + saturate(pow(refractHighlight.y, 6.0f)))
	* float3(0.85f, 0.85f, 1.0f);
	
	return float4(screenColor + refColor, 1.0f);
}

technique RainDrop
{
	pass
	{
		pixelshader = compile ps_2_0 ps_main();
	}
}