    texture2D diffuseMap0;
sampler2D d0_Sampler = sampler_state
    {
    Texture   = <diffuseMap0>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
}
;
texture2D normalMap0;
sampler2D n0_Sampler = sampler_state
    {
    Texture = <normalMap0>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
}
;
struct VertexIn
    {
    float3 vec4_position      : POSITION0;
    float3 vec3_normal        : NORMAL0;
    float2 vec2_textureCoords : TEXCOORD0;
    float3 vec3_tangent       : TANGENT0;
    float3 vec3_binormal      : BINORMAL0;
}
;
struct PixelIn
    {
    float4 vec4_position      : POSITION0;
    float2 vec2_textureCoords : TEXCOORD0;
    float3 vec3_View          : TEXCOORD1;
    float3x3 WorldToTangentSpace : TEXCOORD2;
}
;
struct PixelOut
    {
    float4 color : COLOR0;
}
;
float3 LightDirection;
// 0.7 0 -0.7
    float4 DiffuseColor;
//White
    float  DiffuseIntensity;
// 0.5
     
    float4 vec4_Eye;
//last column in view matrix
    float4 SpecularColor;
//white
     
    float4x4 mat_World;
//identity
    float4x4 mat_View;
//camera matrix
    float4x4 mat_Projection;
//projection
    float4x4 WorldInverseTranspose;
float4 AmbientColor;
//white
    float AmbientIntensity;
// 1.0
    

const float pi = 3.14159;;
int numWaves;
float amplitude[8];
float wavelength[8];
float speed[8];
float2 direction[8];
float time;



	float wave(int i, float x, float y) 
{
    float frequency = 2*pi/wavelength[i];
    float phase = speed[i] * frequency;
    float theta = dot(direction[i], float2(x, y));
    return amplitude[i] * sin(theta * frequency + time * phase);
}

float waveHeight(float x, float y) {
    float height = 0.0;
    for (int i = 0; i < numWaves; ++i)
        height += wave(i, x, y);
    return height;
}
    PixelIn TexturedVS(VertexIn input)
    {
    PixelIn output = (PixelIn)0;




    float4 worldPosition = mul(float4(input.vec4_position,1), mat_World);
    float4 viewPosition = mul(worldPosition, mat_View);
    output.vec4_position = mul(viewPosition, mat_Projection);
    output.vec3_View = normalize(vec4_Eye - worldPosition);
    output.WorldToTangentSpace[0] = mul(normalize(input.vec3_tangent), WorldInverseTranspose);
    output.WorldToTangentSpace[1] = mul(normalize(input.vec3_binormal), WorldInverseTranspose);
    output.WorldToTangentSpace[2] = mul(normalize(input.vec3_normal), WorldInverseTranspose);
    output.vec2_textureCoords=input.vec2_textureCoords;
    return output;
}

     
    PixelOut TexturedPS(PixelIn input)
    {
    PixelOut output = (PixelOut)0;
	
    float3 color=tex2D(d0_Sampler,input.vec2_textureCoords);
	
	float h=waveHeight(input.vec4_position.x,input.vec4_position.y);
	input.vec4_position.y+=(h*5);
	
  
    float3 bump = 2.0 *(tex2D(n0_Sampler, input.vec2_textureCoords)) - 1.0;
    bump = normalize(mul(bump, input.WorldToTangentSpace));
    //float3 diffuse = saturate(dot(-LightDirection,bump));
    //float3 reflect = normalize(2*diffuse*bump-LightDirection);
    //float3 specular = pow(saturate(dot(reflect,input.vec3_View)),5);
    //float3 ambientColor=color*AmbientColor*AmbientIntensity;
    //float3 diffuseColor=color*DiffuseColor*DiffuseIntensity*diffuse;
    //float3 specularColor=specular*float4(1,1,0,1);
    //output.color=float4(ambientColor+diffuseColor+specularColor,1);
    output.color=float4(bump,1);
            return output;
}

     
    technique Textured
    {
    pass Pass0
            {
        VertexShader = compile vs_3_0 TexturedVS();
        PixelShader  = compile ps_3_0 TexturedPS();
    }

    }
