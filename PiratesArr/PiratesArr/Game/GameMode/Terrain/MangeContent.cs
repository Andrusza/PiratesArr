using PiratesArr.Game.GameMode.BaseMode;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PiratesArr.Game.GameMode.Tera
{
    public partial class Tera : Mode
    {
        public override void LoadContent()
        {
            basic = mainInstance.Content.Load<Effect>("Shaders//basic");
            Texture2D heightMapTexture = mainInstance.Content.Load<Texture2D>("Heightmap//map");

            int heightMapSize = heightMapTexture.Width * heightMapTexture.Height;

            Color[] heightMap = new Color[heightMapSize];
            heightMapTexture.GetData<Color>(heightMap);

            //this.vertexCountX = heightMapTexture.Width;
            //this.vertexCountZ = heightMapTexture.Height;
            //this.blockScale   =  blockScale;
            //this.heightScale  = heightScale;
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}