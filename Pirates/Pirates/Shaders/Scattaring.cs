using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;
using Microsoft.Xna.Framework;
using System;

namespace Pirates.Shaders
{
    public class AtmosphericScattaring : BaseShader
    {

        EffectParameter v3CameraPos;		// The camera's current position
        EffectParameter v3LightPos;		// The direction vector to the light source
        EffectParameter v3InvWavelength;	// 1 / pow(wavelength, 4) for the red, green, and blue channels
        EffectParameter fCameraHeight;	// The camera's current height
        EffectParameter fCameraHeight2;	// fCameraHeight^2
        EffectParameter fOuterRadius;		// The outer (atmosphere) radius
        EffectParameter fOuterRadius2;	// fOuterRadius^2
        EffectParameter fInnerRadius;		// The inner (planetary) radius
        EffectParameter fInnerRadius2;	// fInnerRadius^2
        EffectParameter fKrESun;			// Kr * ESun
        EffectParameter fKmESun;			// Km * ESun
        EffectParameter fKr4PI;			// Kr * 4 * PI
        EffectParameter fKm4PI;			// Km * 4 * PI
        EffectParameter fScale;			// 1 / (fOuterRadius - fInnerRadius)
        EffectParameter fScaleDepth;		// The scale depth (i.e. the altitude at which the atmosphere's average density is found)
        EffectParameter fScaleOverScaleDepth;	// fScale / fScaleDepth
        EffectParameter g;
        EffectParameter g2;

        public AtmosphericScattaring()
            : base("AtmosphericScattaring")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Scattaring"];
            v3CameraPos = Technique.Parameters["v3CameraPos"];
            v3LightPos = Technique.Parameters["v3LightPos"];
            v3InvWavelength = Technique.Parameters["v3InvWavelength"];
            fCameraHeight = Technique.Parameters["fCameraHeight"];
            fCameraHeight2 = Technique.Parameters["fCameraHeight2"];
            fOuterRadius = Technique.Parameters["fOuterRadius"];
            fOuterRadius2 = Technique.Parameters["fOuterRadius2"];
            fInnerRadius = Technique.Parameters["fInnerRadius"];
            fInnerRadius2 = Technique.Parameters["fInnerRadius2"];
            fKrESun = Technique.Parameters["fKrESun"];
            fKmESun = Technique.Parameters["fKmESun"];
            fKr4PI=Technique.Parameters["fKr4PI"];
            fKm4PI = Technique.Parameters["fKm4PI"];
            fScale = Technique.Parameters["fScale"];
            fScaleDepth = Technique.Parameters["fScaleDepth"];
            fScaleOverScaleDepth = Technique.Parameters["fScaleOverScaleDepth"];
            g = Technique.Parameters["g"];
            g2 = Technique.Parameters["g2"];
        }

        public float lenght(Vector3 v)
        {
            float l = 0;
            l += v.X * v.X;
            l += v.Y * v.Y;
            l += v.Z * v.Z;
            l = (float)Math.Sqrt(l);
            return l;
        }

        public void InitParameters()
        {

            Texture2D color = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "sand");
            Technique.Parameters["diffuseMap0"].SetValue(color);

            Vector3 cameraPosition = new Vector3(200, 500, 0);

            v3CameraPos.SetValue(cameraPosition);

            Vector3 lightPosition = Vector3.Normalize(new Vector3(10, 10, 0));
            v3LightPos.SetValue(lightPosition);

            Vector3 m_fWavelength=new Vector3();
            m_fWavelength.X= 0.650f;		// 650 nm for red
	        m_fWavelength.Y = 0.570f;		// 570 nm for green
	        m_fWavelength.Z = 0.475f;		// 475 nm for blue

            m_fWavelength.X = (float)Math.Pow(m_fWavelength.X, 4);
            m_fWavelength.Y = (float)Math.Pow(m_fWavelength.Y, 4);
            m_fWavelength.Z = (float)Math.Pow(m_fWavelength.Z, 4);

            v3InvWavelength.SetValue(new Vector3(1.0f/m_fWavelength.X,1.0f/m_fWavelength.Y,1.0f/m_fWavelength.Z));

            float l = lenght(cameraPosition);
            fCameraHeight.SetValue(l); 
            fCameraHeight2.SetValue(l*l);

            float planet_radius = 200f;
            float atmosphere_radius = 205f;
            float cloud_radius = 200.5f;


            fOuterRadius.SetValue(atmosphere_radius);
            fOuterRadius2.SetValue(atmosphere_radius* atmosphere_radius); 
            fInnerRadius.SetValue(planet_radius);
            fInnerRadius2.SetValue(planet_radius*planet_radius);
            fKrESun.SetValue(0.0025f);
            fKmESun.SetValue(0.0010f); 
            fKr4PI.SetValue(4.0f*MathHelper.Pi*0.0025f);
            fKm4PI.SetValue(4.0f*MathHelper.Pi*0.0010f);
            fScale.SetValue(1.0f / (0.25f));
            fScaleDepth.SetValue(0.25f);
            fScaleOverScaleDepth.SetValue(((1.0f / (atmosphere_radius - planet_radius)) / 0.5f));
            g.SetValue(-0.980f);
            g2.SetValue(-0.980f * -0.980f);

            ModelViewProj();
        }

        public override void Update(float time)
        {
            ModelViewProj();
        }
    }
}