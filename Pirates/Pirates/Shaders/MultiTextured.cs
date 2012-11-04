using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class MultiTextured : BaseShader
    {
        private EffectParameter fx_LightPosition;

        public EffectParameter Fx_LightPosition
        {
            get { return fx_LightPosition; }
            set { fx_LightPosition = value; }
        }

        private Vector4 lightPosition;

        public Vector4 LightPosition
        {
            get { return lightPosition; }
            set { lightPosition = value; }
        }

        public MultiTextured()
            : base("MultiTexturing")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["MultiTexturing"];
            fx_LightPosition = Technique.Parameters["LightPosition"];
        }

        public void InitParameters()
        {
            Texture2D snow = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "snow");
            Texture2D grass = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "grass");
            Texture2D sand = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "sand");
            Texture2D weight = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "island4");

            Technique.Parameters["d0_Sand"].SetValue(sand);
            Technique.Parameters["d1_Grass"].SetValue(grass);
            Technique.Parameters["d2_Snow"].SetValue(snow);
            Technique.Parameters["WeightMap"].SetValue(weight);

            Technique.Parameters["Projection"].SetValue(projectionMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);

            //fx_LightPosition.SetValue(new Vector3(0, 0, 0));
        }

        private int x = -1000;

        public override void Update(float time)
        {
            lightPosition.Y *= -1;
            lightPosition.X *= -1;
            fx_LightPosition.SetValue(new Vector3(lightPosition.X, lightPosition.Y, lightPosition.Z));

            Technique.Parameters["World"].SetValue(worldMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
        }
    }
}