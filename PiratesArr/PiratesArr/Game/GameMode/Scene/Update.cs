using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Scene : Mode
    {
        public override void Update(GameTime gameTime)
        {
            Input();

            camera.Update();
        }
    }
}