texture2D diffuseMap0;
sampler2D d0_Sampler = sampler_state
{
    Texture   = <diffuseMap0>;
    magfilter = linear; 
	minfilter = linear; 
	mipfilter = linear; 	
};

texture2D normalMap0;
sampler2D n0_Sampler = sampler_state
{
    Texture = <normalMap0>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};

struct VertexShaderInput
{
    float4 Position      : POSITION0;
    float3 Normal        : NORMAL0;
    float2 TextureCoord  : TEXCOORD0;
    float3 Tangent       : TANGENT0;
    float3 Binormal      : BINORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TextureCoord:TEXCOORD0;
	float3x3 WorldToTangentSpace:TEXCOORD1;
	float3  ToLight :TEXCOORD4;
	float3  ToView  :TEXCOORD5;
	float4  reflectionPosition    : TEXCOORD6;
	float2 BumpMapSamplingPos        : TEXCOORD7;
};

float4x4 World;
float4x4 WorldInverseTranspose;
float4x4 ViewInverseTranspose;
float4x4 ReflectionView;

float4x4 MVP;
float4x4 ReflectedMVP;

float3 LightPosition;

float Shininess;

float3 AmbientLightColor;
float3 DiffuseLightColor;
float3 SpecularLightColor;


float AmbientIntensity;
float DiffuseIntensity;
float SpecularIntensity;

float waves[24];
float time;

float3 eyeFromViewInverseTranspose()
{
	return float3(ViewInverseTranspose[0].w,ViewInverseTranspose[1].w,ViewInverseTranspose[2].w);
}

float3 shade(float3 Ln, float3 Nn, float3 Vn, float2 UV){
	// simple lambert diffuse
	float3 color=tex2D(d0_Sampler,UV);
	
	float diffContrib = saturate(dot(Nn, Ln));
	float3 diffuse = color *diffContrib;
	
	// phong specular
	float3 Rn = reflect(-Ln, Nn);
	float  RV = saturate(dot(Rn, Vn));
	float  specContrib = pow(RV, 60);	
	float3 spec = SpecularLightColor*specContrib;
	float3 ambient=	color*AmbientLightColor*AmbientIntensity;
	
	return ambient + spec + diffuse ;
}

float PI = 3.14159265358979323846264;

float4 Wave(float4 pos)
{
				//waves[i] = p.wavelength;
                //waves[i + 1] = p.steepness;
                //waves[i + 2] = p.speed;
                //waves[i + 3] = p.kAmpOverLen;
                //waves[i + 4] = p.wave_dir.X;
                //waves[i + 5] = p.wave_dir.Y;
	
	float4 P = pos;
    for(int i = 0; i < 24; i += 6) 
	{
        float A = waves[i] * waves[i+3];         // Amplitude
        float omega = 2.0 * 3.14 / waves[i];      // Frequency
        float phi = waves[i+2] * omega;          // Phase
        float Qi = waves[i+1]/(omega * A * 4.0); // Steepness

        float term = omega * dot(float2(waves[i+4], waves[i+5]), float2(pos.x, pos.z)) + phi * time;
        float C = cos(term);
        float S = sin(term);
        P += float4(Qi * A * waves[i+4] * C,A * S * 10,i * A * waves[i+5] * C,0.0f);
	}
	P.w=1.0f;
	return P;
}

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	
	float4 a=Wave(input.Position);
	
    output.Position = mul(a, MVP);
	output.reflectionPosition = mul(a, ReflectedMVP);

	// Matrix for transformation to tangent space
	output.WorldToTangentSpace[0] = mul(normalize(input.Tangent), WorldInverseTranspose);
    output.WorldToTangentSpace[1] = mul(normalize(input.Binormal), WorldInverseTranspose);
    output.WorldToTangentSpace[2] = mul(normalize(input.Normal), WorldInverseTranspose);
	
	// Get the position in eye coordinates
	float3 pos = mul(input.Position,World);
	
	// vector to light and to eye in world space
    float3 toLight  = LightPosition - pos;
	float3 toViewer = eyeFromViewInverseTranspose() - pos;
	
	// world to tangent space
	output.ToLight = mul(output.WorldToTangentSpace, toLight);
	output.ToView  = mul(output.WorldToTangentSpace, toViewer);
	output.TextureCoord=input.TextureCoord;
	output.BumpMapSamplingPos = input.TextureCoord/0.5f; ///sadas
	
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 Nn = 2*tex2D(n0_Sampler, input.TextureCoord)-1;
	float3 Ln = normalize(input.ToLight);
	float3 Vn = normalize(input.ToView);
	
	input.TextureCoord.x =  input.reflectionPosition.x / input.reflectionPosition.w / 2.0f + 0.5f;
    input.TextureCoord.y = -input.reflectionPosition.y / input.reflectionPosition.w / 2.0f + 0.5f;
	
	 float4 bumpColor = tex2D(n0_Sampler, input.BumpMapSamplingPos);
     float2 perturbation = 0.3f*(bumpColor.rg - 0.5f)*2.0f;
     float2 perturbatedTexCoords = input.TextureCoord + perturbation;
	 
	 
	
	
	float3 pixel = shade(Ln, Nn, Vn, perturbatedTexCoords);
	return float4(pixel,1);
}

technique Water
{
    pass Pass0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
