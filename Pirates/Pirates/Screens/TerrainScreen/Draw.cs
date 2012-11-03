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
            island.Draw(effect);
            water.Draw(waterShader);

            skydome.Update(scattering.Technique);
            skydome.DrawModel();
            
        }
    }
}