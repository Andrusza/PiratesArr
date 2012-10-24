using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        public override void LoadContent()
        {
            tera = new PiratesArr.Game.Terrain.Terrain("wire");
            tera.SetShader("basic", "Textured");
            tera.AddTexture("water", "diffuseMap_water");
            tera.BindTextures();
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}