using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class Scattaring : BaseShader
    {
        public float skydomeSize;
        public float dayToSunsetSharpness;
        public Vector4 fogColor;
        public float fogDensity;
        public float hazeTopAltitude;
        public float largeSunLightness;
        public float largeSunRadiusAttenuation;
        public Vector4 lightColor;
        public Vector4 lightColorAmbient;
        public Vector4 lightDirection;
        public Vector4 lightPosition;
        public double phi;
        public float sunLightness;
        public float sunRadiusAttenuation;
        public double theta;

        public Matrix viewProjection;
        public Matrix invertView;
        public Matrix invertTransposeWorld;

        public Scattaring()
            : base("Scattaring")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Scattaring"];

            dayToSunsetSharpness = 0.8f;
            fogColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            fogDensity = 0.000028f;
            hazeTopAltitude = 100.0f;
            largeSunLightness = 0.8f;
            largeSunRadiusAttenuation = 100.0f;
            lightColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            lightColorAmbient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            lightDirection = new Vector4(0, 0, 0, 1);
            phi = 0;
            sunLightness = 0.9f;
            sunRadiusAttenuation = 150.0f;
            theta = 0;
        }

        public Vector4 LightDirection
        {
            get { return lightDirection; }
            set { lightDirection = value; }
        }

        public override void InitParameters()
        {
            Texture2D night = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "night");
            Technique.Parameters["SkyTextureNight"].SetValue(night);

            Texture2D sunset = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "sunrise");
            Technique.Parameters["SkyTextureSunset"].SetValue(sunset);

            Texture2D day = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "sky2");
            Technique.Parameters["SkyTextureDay"].SetValue(day);

            Technique.Parameters["isSkydome"].SetValue(true);

            Technique.Parameters["LightDirection"].SetValue(lightDirection);
            Technique.Parameters["LightColor"].SetValue(lightColor);
            Technique.Parameters["LightColorAmbient"].SetValue(lightColorAmbient);
            Technique.Parameters["FogColor"].SetValue(fogColor);
            Technique.Parameters["fDensity"].SetValue(fogDensity);
            Technique.Parameters["SunLightness"].SetValue(sunLightness);
            Technique.Parameters["sunRadiusAttenuation"].SetValue(sunRadiusAttenuation);
            Technique.Parameters["largeSunLightness"].SetValue(largeSunLightness);
            Technique.Parameters["largeSunRadiusAttenuation"].SetValue(largeSunRadiusAttenuation);
            Technique.Parameters["dayToSunsetSharpness"].SetValue(dayToSunsetSharpness);
            Technique.Parameters["hazeTopAltitude"].SetValue(hazeTopAltitude);
        }

        private Vector4 GetLightPosition(double Theta, double Phi)
        {
            float y = (float)Math.Cos((double)Theta);
            float x = (float)(Math.Sin((double)Theta) * Math.Cos(Phi));
            float z = (float)(Math.Sin((double)Theta) * Math.Sin(Phi));
            float w = 1.0f;

            return new Vector4(x, y, z, w) * skydomeSize;
        }

        public override void Update(float time)
        {
            theta += 0.001f;
            lightPosition = GetLightPosition(theta, phi);

            lightDirection = Vector4.Normalize(lightPosition);

            viewProjection = viewMatrix * projectionMatrix;

            Matrix temp = Matrix.Invert(worldMatrix);
            invertTransposeWorld = Matrix.Transpose(temp);

            viewMatrix.Translation = new Vector3(0, 0, 0);
            invertView = Matrix.Invert(viewMatrix);
        }
    }
}