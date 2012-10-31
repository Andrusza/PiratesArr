using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class Basic : BaseShader
    {
        private EffectParameter fx_world;

        private EffectParameter fx_d0;
        private EffectParameter fx_n0;

        private EffectParameter fx_LightPosition;

        private EffectParameter fx_Shininess;
        private EffectParameter fx_AmbientIntensity;
        private EffectParameter fx_DiffuseIntensity;
        private EffectParameter fx_SpecularIntensity;

        private Texture2D color = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "d0");
        private Texture2D normal = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "n0");

        public Basic()
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
            fx_LightPosition.SetValue(new Vector3(0, 100, 0));

            fx_d0.SetValue(color);
            fx_n0.SetValue(normal);

            fx_AmbientIntensity.SetValue(0.2f);
            fx_DiffuseIntensity.SetValue(0.7f);
            fx_SpecularIntensity.SetValue(0.2f);
            fx_Shininess.SetValue(100.0f);

            InverseTransposeWorld();
            InverseTransposeView();
        }

        public override void Update(float time)
        {
            InverseTransposeWorld();
            InverseTransposeView();

            ModelViewProj();
        }
    }
}