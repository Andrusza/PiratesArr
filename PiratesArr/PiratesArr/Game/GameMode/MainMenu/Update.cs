using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.MainMenu
{
    public partial class MainMenu : Mode
    {
        public override void Update(GameTime gameTime)
        {
            Input();
        }
    }
}