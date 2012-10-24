using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Intro : Mode
    {
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(mainInstance.GraphicsDevice);
            backgroundTexture = mainInstance.Content.Load<Texture2D>("GUI\\Intro\\Intro");

            screenWidth = mainInstance.GraphicsDevice.PresentationParameters.BackBufferWidth;
            screenHeight = mainInstance.GraphicsDevice.PresentationParameters.BackBufferHeight;
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}