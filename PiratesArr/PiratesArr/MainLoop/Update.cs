using Microsoft.Xna.Framework;

namespace PiratesArr
{
    public partial class Main : Microsoft.Xna.Framework.Game
    {
        protected override void Update(GameTime gameTime)
        {
            renderMode.Update(gameTime);
            fps.Update(gameTime);

            

            base.Update(gameTime);
        }
    }
}