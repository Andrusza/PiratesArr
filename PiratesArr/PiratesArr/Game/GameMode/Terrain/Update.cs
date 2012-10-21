using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Tera
{
    public partial class Tera : Mode
    {
        public override void Update(GameTime gameTime)
        {
            Input();

            camera.Update();
            viewMatrix = camera.View;

            Matrix worldMatrix = Matrix.CreateRotationZ((float)gameTime.TotalGameTime.TotalSeconds * 0);
            basic.Parameters["xWorld"].SetValue(worldMatrix);
        }
    }
}