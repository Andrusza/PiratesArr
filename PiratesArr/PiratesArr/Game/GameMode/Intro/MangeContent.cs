using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Intro
{
    public partial class Intro : Mode
    {
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(mainInstance.GraphicsDevice);
            backgroundTexture = mainInstance.Content.Load<Texture2D>("GUI\\Intro\\Intro");
            device = graphicsInstance.GraphicsDevice;

            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}