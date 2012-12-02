using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;
using Pirates.Window;

namespace Pirates.Weather
{
    internal class Rain
    {
        private Texture2D currentFrame;
        private RainDropsShader raindrops;

        public void Draw()
        {
            SpriteBatch spriteBatch = new SpriteBatch(BaseClass.GetInstance().GraphicsDevice);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, raindrops.Technique);
            {
                spriteBatch.Draw(currentFrame, new Rectangle(0, 0, XnaWindow.Width, XnaWindow.Height), Color.White);
            }
            spriteBatch.End();
        }
    }
}