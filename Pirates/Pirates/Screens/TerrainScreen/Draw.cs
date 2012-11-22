using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Window;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(BaseClass.GetInstance().GraphicsDevice);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone,rainDropsShader.Technique);

            ///spriteBatch.Draw(currentFrameRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(currentFrame, new Rectangle(0, 0, XnaWindow.Width, XnaWindow.Height), Color.White);
            spriteBatch.End();
        }
    }
}