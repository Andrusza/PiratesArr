using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.MainLoop
{
    public class FPScounter
    {
        private SpriteFont _spr_font;
        private int _total_frames;
        private float _elapsed_time;
        private int _fps;

        private SpriteBatch spriteBatch;

        private static Main mainInstance;

        public FPScounter()
        {
            mainInstance = Main.GetInstance();
            _spr_font = mainInstance.Content.Load<SpriteFont>("GUI\\Fonts\\mono");
            spriteBatch = new SpriteBatch(mainInstance.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsed_time >= 1000.0f)
            {
                _fps = _total_frames;
                _total_frames = 0;
                _elapsed_time = 0;
            }
        }

        public void Draw()
        {
            _total_frames++;

            spriteBatch.Begin();
            spriteBatch.DrawString(_spr_font, string.Format("FPS={0}", _fps),
                new Vector2(10.0f, 20.0f), Color.White);
            spriteBatch.End();
        }
    }
}