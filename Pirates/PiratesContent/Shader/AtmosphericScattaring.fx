float4x4 MVP;
float4x4 World;

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 UV:TEXCOORD0;	
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float3 Color:COLOR0;
	float3 SecendaryColor:COLOR1;
	float3 v3Direction:TEXCOORD0;
	float2 UV:TEXCOORD1;
	
};

float3 v3CameraPos;		// The camera's current position
float3 v3LightPos;		// The direction vector to the light source
float3 v3InvWavelength;	// 1 / pow(wavelength, 4) for the red, green, and blue channels
float fCameraHeight;	// The camera's current height
float fCameraHeight2;	// fCameraHeight^2
float fOuterRadius;		// The outer (atmosphere) radius
float fOuterRadius2;	// fOuterRadius^2
float fInnerRadius;		// The inner (planetary) radius
float fInnerRadius2;	// fInnerRadius^2
float fKrESun;			// Kr * ESun
float fKmESun;			// Km * ESun
float fKr4PI;			// Kr * 4 * PI
float fKm4PI;			// Km * 4 * PI
float fScale;			// 1 / (fOuterRadius - fInnerRadius)
float fScaleDepth;		// The scale depth (i.e. the altitude at which the atmosphere's average density is found)
float fScaleOverScaleDepth;	// fScale / fScaleDepth
float g;
float g2;

const int nSamples = 2;
const float fSamples = 2.0;

float fExposure =2;

float scale(float fCos)
{
	float x = 1.0 - fCos;
	return fScaleDepth * exp(-0.00287 + x*(0.459 + x*(3.83 + x*(-6.80 + x*5.25))));
}

texture2D diffuseMap0;
sampler2D d0_Sampler = sampler_state
{
    Texture   = <diffuseMap0>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    // Get the ray from the camera to the vertex and its length (which is the far point of the ray passing through the atmosphere)
	float3 v3Pos = mul(input.Position,World);

	float3 v3Ray = v3Pos - (v3CameraPos);
	float fFar = length(v3Ray);
	v3Ray /= fFar;
	
	// Calculate the ray's starting position, then calculate its scattering offset
	float3 v3Start = v3CameraPos;
	float fHeight = length(v3Start);
	float fDepth = exp(fScaleOverScaleDepth * (fInnerRadius - fCameraHeight));
	float fStartAngle = dot(v3Ray, v3Start) / fHeight;
	float fStartOffset = fDepth*scale(fStartAngle);
	
	// Initialize the scattering loop variables
	//gl_FrontColor = vec4(0.0, 0.0, 0.0, 0.0);
	float fSampleLength = fFar / fSamples;
	float fScaledLength = fSampleLength * fScale;
	float3 v3SampleRay = v3Ray * fSampleLength;
	float3 v3SamplePoint = v3Start + v3SampleRay * 0.5;
	

	// Now loop through the sample rays
	float3 v3FrontColor = float3(0.0, 0.0, 0.0);
	for(int i=0; i<nSamples; i++)
	{
		float fHeight = length(v3SamplePoint);
		float fDepth = exp(fScaleOverScaleDepth * (fInnerRadius - fHeight));
		float fLightAngle = dot(v3LightPos, v3SamplePoint) / fHeight;
		float fCameraAngle = dot(v3Ray, v3SamplePoint) / fHeight;
		float fScatter = (fStartOffset + fDepth*(scale(fLightAngle) - scale(fCameraAngle)));
		float3 v3Attenuate = exp(-fScatter * (v3InvWavelength * fKr4PI + fKm4PI));
		v3FrontColor += v3Attenuate * (fDepth * fScaledLength);
		v3SamplePoint += v3SampleRay;
	}
	
	output.SecendaryColor=v3FrontColor * fKmESun;
	output.Color = v3FrontColor * (v3InvWavelength * fKrESun);
	output.Position = mul(input.Position,MVP);
	output.v3Direction = v3CameraPos - v3Pos; //??
	output.UV=input.UV;
    

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 Tcolor=tex2D(d0_Sampler,input.UV);
    float fCos = dot(v3LightPos, input.v3Direction) / length(input.v3Direction);
	float fRayleighPhase = 0.75 * (1.0 + fCos*fCos);
	float fMiePhase = 1.5 * ((1.0 - g2) / (2.0 + g2)) * (1.0 + fCos*fCos) / pow(abs(1.0 + g2 - 2.0*g*fCos), 1.5);
    float3 color = (fRayleighPhase * input.Color + fMiePhase * input.SecendaryColor);
	return float4(color,1);
}

technique Scattaring
{
    pass Pass0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
