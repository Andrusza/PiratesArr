using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class Scattaring : BaseShader
    {
        private readonly float SKYDOMESIZE = 1200;

        private Vector4 lightDirection = new Vector4(0, 0, 0, 1);

        public Vector4 LightDirection
        {
            get { return lightDirection; }
            set { lightDirection = value; }
        }

        private Vector4 lightColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 lightColorAmbient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
        private Vector4 fogColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private float fogDensity = 0.000028f;
        private float sunLightness = 0.2f;
        private float sunRadiusAttenuation = 150.0f;
        private float largeSunLightness = 0.2f;
        private float largeSunRadiusAttenuation = 1.0f;
        private float dayToSunsetSharpness = 0.8f;
        private float hazeTopAltitude = 100.0f;

        public Scattaring()
            : base("Scattaring")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Scattaring"];
        }

        public void InitParameters()
        {
            Technique.Parameters["WorldViewProj"].SetValue(worldMatrix * viewMatrix * projectionMatrix);
            InverseTransposeWorld();
            Technique.Parameters["ViewInv"].SetValue(Matrix.Invert(viewMatrix));
            Technique.Parameters["World"].SetValue(worldMatrix);

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

        private Vector4 GetDirection(double Theta, double Phi)
        {
            float y = (float)Math.Cos((double)Theta);
            float x = (float)(Math.Sin((double)Theta) * Math.Cos(Phi));
            float z = (float)(Math.Sin((double)Theta) * Math.Sin(Phi));
            float w = 1.0f;

            return new Vector4(x, y, z, w) * SKYDOMESIZE;
        }

        private double Thera = 0;
        private double Phi = 0;

        public override void Update(float time)
        {
            Thera += 0.005;

            LightDirection = GetDirection(Thera, Phi);

            //Console.WriteLine(LightDirection.ToString());
            Technique.Parameters["LightDirection"].SetValue(Vector4.Normalize(LightDirection));

            Technique.Parameters["WorldViewProj"].SetValue(worldMatrix * viewMatrix * projectionMatrix);
            InverseTransposeWorld();
            viewMatrix.Translation = new Vector3(0, 0, 0);
            Technique.Parameters["ViewInv"].SetValue(Matrix.Invert(viewMatrix));
            Technique.Parameters["World"].SetValue(worldMatrix);
        }
    }
}