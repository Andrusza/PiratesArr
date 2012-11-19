using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        public override void Draw(GameTime gameTime)
        {
            BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;

            skydome.Draw(scattering);
            cloudManager.Draw(cloudShader);
           

            BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;


            island.Draw(islandShader);
            water.Draw(waterShader);
            ship.Draw(mvpshader);

            //fogManager.Draw(cloudShader);
            BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;

           

            //SpriteBatch spriteBatch = new SpriteBatch(BaseClass.GetInstance().GraphicsDevice);
            //spriteBatch.Begin();
            //Vector2 pos = new Vector2(400, 0);
            //spriteBatch.Draw(refractionMap, pos, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            //spriteBatch.End();
        }
    }
}