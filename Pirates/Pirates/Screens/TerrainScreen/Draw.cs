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
            //island.Draw(waterShader);
            water.Draw(waterShader);

            //ship.Update(mvpshader.Technique);
            ship.Draw(mvpshader);

            skydome.Draw(scattering);

            SpriteBatch spriteBatch = new SpriteBatch(BaseClass.GetInstance().GraphicsDevice);
            spriteBatch.Begin();
            Vector2 pos = new Vector2(400, 0);
            //spriteBatch.Draw(reflectionMap, pos, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);

            spriteBatch.End();
        }

        private void DrawRefractionMap()
        {
            Plane refractionPlane = CreatePlane(waterHeight + 1.5f, new Vector3(0, -1, 0), false);

            waterShader.Technique.Parameters["ClipPlane0"].SetValue(new Vector4(refractionPlane.Normal, refractionPlane.D));
            waterShader.Technique.Parameters["Clipping"].SetValue(true);

            BaseClass.GetInstance().GraphicsDevice.SetRenderTarget(refractionRenderTarget);
            BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;

            island.Draw(waterShader);

            BaseClass.GetInstance().GraphicsDevice.SetRenderTarget(refractionRenderTarget);
            waterShader.Technique.Parameters["Clipping"].SetValue(false);
            refractionMap = refractionRenderTarget;

            //System.IO.Stream ss = System.IO.File.OpenWrite("C:\Test\Refraction.jpg");
            //refractionRenderTarget.SaveAsJpeg(ss, 500, 500);
            //ss.Close();
        }

       
    }
}