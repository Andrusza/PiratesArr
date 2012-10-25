using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.Surface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        public override void LoadContent()
        {
            tera = new Terrain("wire");
            SetShader("basic", "Textured");
            tera.AddTexture("water", "diffuseMap0");
            tera.BindTextures(effect);

          

            //tera.Effect.Parameters["ambientColor"].SetValue(Color.White.ToVector4());
            //tera.Effect.Parameters["ambientIntensity"].SetValue(0.4f);

            //tera.Effect.Parameters["diffuseColor"].SetValue(Color.White.ToVector4());
            //tera.Effect.Parameters["diffuseIntensity"].SetValue(0.2f);
            //tera.Effect.Parameters["lightDirection"].SetValue(new Vector3(0, 1, 0));

            //tera.Effect.Parameters["specularColor"].SetValue(Color.White.ToVector4());

        }


        public void SetShader(string shadername, string shaderTechniqueName)
        {
            effect = mainInstance.Content.Load<Effect>("Shaders//" + shadername);
            effect.CurrentTechnique = effect.Techniques[shaderTechniqueName];
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}