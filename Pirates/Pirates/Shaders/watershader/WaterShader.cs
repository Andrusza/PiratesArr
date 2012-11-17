using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public partial class waterShader : BaseShader
    {
        public float shininess;
        public float ambientIntensity;
        public float diffuseIntensity;
        public float specularIntensity;

        public Vector3 ambientLightColor;
        public Vector3 diffuseLightColor;
        public Vector3 specularLightColor;

        public Vector4 lightPosition;

        public Texture2D color;
        public Texture2D normal;

        public Texture2D reflection;
        public Texture2D refraction;

        public Matrix reflectedViewMatrix;

        private WaveParameters param;
        private float[] waves;

        public waterShader()
            : base("basic")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Basic"];

            color = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "seamwater6");
            normal = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "normalwater6");

            shininess = 60;
            ambientIntensity = 0.2f;
            diffuseIntensity = 0.7f;
            specularIntensity = 0.2f;

            ambientLightColor = new Vector3(1, 1, 1);
            diffuseLightColor = new Vector3(1, 1, 1);
            specularLightColor = new Vector3(0.98f, 0.97f, 0.7f);

            param.wavelength = 85f;
            param.steepness = 0.5f;
            param.speed = 0.0085f;
            param.kAmpOverLen = 0.001f;

            waves = new float[24];
            for (int i = 0; i < 24; i += 6)
            {
                WaveParameters p = GenerateWaves(param);
                waves[i] = p.wavelength;
                waves[i + 1] = p.steepness;
                waves[i + 2] = p.speed;
                waves[i + 3] = p.kAmpOverLen;
                waves[i + 4] = p.wave_dir.X;
                waves[i + 5] = p.wave_dir.Y;
            }
        }

        public void InitParameters()
        {
            Technique.Parameters["diffuseMap0"].SetValue(color);
            Technique.Parameters["normalMap0"].SetValue(normal);

            Technique.Parameters["Shininess"].SetValue(shininess);
            Technique.Parameters["AmbientIntensity"].SetValue(ambientIntensity);
            Technique.Parameters["DiffuseIntensity"].SetValue(diffuseIntensity);
            Technique.Parameters["SpecularIntensity"].SetValue(specularIntensity);

            Technique.Parameters["AmbientLightColor"].SetValue(ambientLightColor);
            Technique.Parameters["DiffuseLightColor"].SetValue(diffuseLightColor);
            Technique.Parameters["SpecularLightColor"].SetValue(specularLightColor);

            Technique.Parameters["waves"].SetValue(waves);
            Technique.Parameters["time"].SetValue(0);
        }

        public override void Update(float time)
        {
            lightPosition.Y *= -1;
            lightPosition.X *= -1;

            Technique.Parameters["diffuseMap0"].SetValue(reflection);

            Technique.Parameters["LightPosition"].SetValue(new Vector3(lightPosition.X, lightPosition.Y, lightPosition.Z));
            Technique.Parameters["World"].SetValue(worldMatrix);

            Matrix worldInverseTranspose = Matrix.Invert(worldMatrix);
            worldInverseTranspose = Matrix.Transpose(worldInverseTranspose);
            Technique.Parameters["WorldInverseTranspose"].SetValue(worldInverseTranspose);

            Matrix viewInverseTranspose = Matrix.Invert(viewMatrix);
            viewInverseTranspose = Matrix.Transpose(viewInverseTranspose);
            Technique.Parameters["ViewInverseTranspose"].SetValue(viewInverseTranspose);

            Technique.Parameters["MVP"].SetValue(worldMatrix * viewMatrix * projectionMatrix);
            Technique.Parameters["ReflectedMVP"].SetValue(worldMatrix * reflectedViewMatrix * projectionMatrix);

            Technique.Parameters["time"].SetValue(time * 0.03f);
        }
    }
}