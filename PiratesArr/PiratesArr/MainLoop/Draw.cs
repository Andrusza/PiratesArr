using Microsoft.Xna.Framework;

namespace PiratesArr
{
    public partial class Main : Microsoft.Xna.Framework.Game
    {
        protected override void Draw(GameTime gameTime)
        {
            renderMode.Draw(gameTime);
            fps.Draw();

            base.Draw(gameTime);
        }
    }
}