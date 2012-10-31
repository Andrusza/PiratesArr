using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Utility
{
    public class FPScounter
    {
        private SpriteFont sprFont;
        private int totalFrames;
        private float elapsedTime;
        private int fps;

        private SpriteBatch spriteBatch;

        public FPScounter(GraphicsDevice device)
        {
            sprFont = ContentLoader.Load<SpriteFont>(ContentType.FONT, "mono");
            spriteBatch = new SpriteBatch(device);
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= 1000.0f)
            {
                fps = totalFrames;
                totalFrames = 0;
                elapsedTime = 0;
            }
        }

        public void Draw()
        {
            totalFrames++;

            spriteBatch.Begin();
            spriteBatch.DrawString(sprFont, string.Format("FPS={0}", fps),
                new Vector2(10.0f, 20.0f), Color.White);
            spriteBatch.End();
        }
    }
}