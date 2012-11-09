using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class MultiTextured : BaseShader
    {
        public float ambientIntensity;
        public float diffuseIntensity;

        public Vector3 ambientLightColor;
        public Vector3 diffuseLightColor;

        public Vector4 lightPosition;

        private Texture2D snow;
        private Texture2D grass;
        private Texture2D sand;
        private Texture2D weight;

        public Texture2D reflection;
        public Matrix reflectedViewMatrix;
        public Plane clippingPlane;
        public bool clipping;

        public MultiTextured()
            : base("MultiTexturing")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["MultiTexturing"];

            ambientIntensity = 0.1f;
            diffuseIntensity = 0.9f;
            clipping = false;

            ambientLightColor = new Vector3(1, 1, 1);
            diffuseLightColor = new Vector3(1, 1, 1);
        }

        public void InitParameters()
        {
            snow = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "snow");
            grass = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "grass");
            sand = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "sand");
            weight = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "island4");

            Technique.Parameters["d0_Sand"].SetValue(sand);
            Technique.Parameters["d1_Grass"].SetValue(grass);
            Technique.Parameters["d2_Snow"].SetValue(snow);
            Technique.Parameters["WeightMap"].SetValue(weight);

            Technique.Parameters["Projection"].SetValue(projectionMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
            Technique.Parameters["World"].SetValue(worldMatrix);

            Technique.Parameters["Clipping"].SetValue(clipping);

            Technique.Parameters["AmbientIntensity"].SetValue(ambientIntensity);
            Technique.Parameters["DiffuseIntensity"].SetValue(diffuseIntensity);

            Technique.Parameters["AmbientLightColor"].SetValue(ambientLightColor);
            Technique.Parameters["DiffuseLightColor"].SetValue(diffuseLightColor);
        }

        public override void Update(float time)
        {
            lightPosition.Y *= -1;
            lightPosition.X *= -1;
            Technique.Parameters["LightPosition"].SetValue(new Vector3(lightPosition.X, lightPosition.Y, lightPosition.Z));

            Technique.Parameters["World"].SetValue(worldMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
            Technique.Parameters["Projection"].SetValue(projectionMatrix);

            Technique.Parameters["ClipPlane0"].SetValue(new Vector4(clippingPlane.Normal, clippingPlane.D));
            Technique.Parameters["Clipping"].SetValue(clipping);
        }
    }
}