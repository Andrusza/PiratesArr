using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class waterShader : BaseShader
    {
        private EffectParameter fx_world;

        private EffectParameter fx_d0;
        private EffectParameter fx_n0;

        private Vector4 lightPosition;

        public Vector4 LightPosition
        {
            get { return lightPosition; }
            set { lightPosition = value; }
        }

        private EffectParameter fx_LightPosition;

        public EffectParameter Fx_LightPosition
        {
            get { return fx_LightPosition; }
            set { fx_LightPosition = value; }
        }

        private EffectParameter fx_Shininess;
        private EffectParameter fx_AmbientIntensity;
        private EffectParameter fx_DiffuseIntensity;
        private EffectParameter fx_SpecularIntensity;

        private Texture2D color = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "seamwater6");
        private Texture2D normal = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "normalwater6");

        public waterShader()
            : base("basic")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Basic"];

            fx_world = Technique.Parameters["World"];

            fx_d0 = Technique.Parameters["diffuseMap0"];
            fx_n0 = Technique.Parameters["normalMap0"];

            fx_Shininess = Technique.Parameters["Shininess"];
            fx_AmbientIntensity = Technique.Parameters["AmbientIntensity"];
            fx_DiffuseIntensity = Technique.Parameters["DiffuseIntensity"];
            fx_SpecularIntensity = Technique.Parameters["SpecularIntensity"];

            fx_LightPosition = Technique.Parameters["LightPosition"];
        }

        public void InitParameters()
        {
            fx_world.SetValue(worldMatrix);

            fx_d0.SetValue(color);
            fx_n0.SetValue(normal);

            fx_AmbientIntensity.SetValue(0.2f);
            fx_DiffuseIntensity.SetValue(0.7f);
            fx_SpecularIntensity.SetValue(0.2f);
            fx_Shininess.SetValue(2.0f);

            InverseTransposeWorld();
            InverseTransposeView();
        }

        public override void Update(float time)
        {
            lightPosition.Y *= -1;
            lightPosition.X *= -1;
            fx_LightPosition.SetValue(new Vector3(lightPosition.X, lightPosition.Y, lightPosition.Z));

            InverseTransposeWorld();
            InverseTransposeView();

            ModelViewProj();
        }
    }
}