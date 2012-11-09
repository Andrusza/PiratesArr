using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        public override void LoadContent()
        {
            PresentationParameters pp = BaseClass.GetInstance().GraphicsDevice.PresentationParameters;
            refractionRenderTarget = new RenderTarget2D(BaseClass.GetInstance().GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, pp.BackBufferFormat, pp.DepthStencilFormat);
            reflectionRenderTarget = new RenderTarget2D(BaseClass.GetInstance().GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, pp.BackBufferFormat, pp.DepthStencilFormat);
        }

        public override void UnloadContent()
        {
        }

      
    }
}