using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Scene
{
    public partial class Scene : Mode
    {
        public override void Update(GameTime gameTime)
        {
            Input();

            camera.Update();
            viewMatrix = camera.View;
            VP = viewMatrix * projectionMatrix;
        }
    }
}