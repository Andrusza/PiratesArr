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

const float LOG2 = 1.442695;

float4 ps_main(float2 TextureCoordinate : TEXCOORD0) : COLOR
{
    float4 currentFrameColor=tex2D(currentFrameSampler, TextureCoordinate);
	return currentFrameColor;
}

technique Fog
{
	pass
	{
		pixelshader = compile ps_2_0 ps_main();
	}
}