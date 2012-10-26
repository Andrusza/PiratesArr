using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.Objects.Namespace_Ship;

namespace PiratesArr.Game.GameMode
{
    public partial class Scene : Mode
    {
        public override void LoadContent()
        {
            SetShader("basic", "Textured");

            effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
            effect.Parameters["AmbientIntensity"].SetValue(1.5f);

            effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
            effect.Parameters["DiffuseIntensity"].SetValue(0.0f);
            Vector3 direction = Vector3.Normalize(new Vector3(1, 0, -1));
            effect.Parameters["LightDirection"].SetValue(direction);

            effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());

            playerShip = new Ship("ship", effect);
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