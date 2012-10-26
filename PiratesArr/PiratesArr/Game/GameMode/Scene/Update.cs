using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Scene : Mode
    {
        public override void Update(GameTime gameTime)
        {
            Input();

            // effect.Parameters["mat_View"].SetValue(camera.Update());
            // effect.Parameters["mat_Projection"].SetValue(camera.ProjectionMatrix);
        }
    }
}