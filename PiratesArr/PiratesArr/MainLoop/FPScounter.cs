using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.MainLoop
{
    public class FPScounter
    {
        private SpriteFont sprFont;
        private int totalFrames;
        private float elapsedTime;
        private int fps;

        private SpriteBatch spriteBatch;

        private static Main mainInstance;

        public FPScounter()
        {
            mainInstance = Main.GetInstance();
            sprFont = mainInstance.Content.Load<SpriteFont>("GUI\\Fonts\\mono");
            spriteBatch = new SpriteBatch(mainInstance.GraphicsDevice);
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