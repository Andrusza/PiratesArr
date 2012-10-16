using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.MainMenu
{
    public partial class MainMenu : Mode
    {
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            spriteBatch.End();

            manager.Draw();
        }
    }
}