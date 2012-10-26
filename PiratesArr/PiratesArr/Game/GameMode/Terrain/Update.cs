using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        float time = 0;

        public override void Update(GameTime gameTime)
        {
            Input();

            effect.Parameters["mat_View"].SetValue(camera.Update());
            effect.Parameters["mat_Projection"].SetValue(camera.ProjectionMatrix);
            effect.Parameters["vec4_Eye"].SetValue(new Vector4(camera.Eye, 1));

            time += (float)gameTime.ElapsedGameTime.TotalSeconds / 50f;
            //effect.Parameters["time"].SetValue(time);
        }
    }
}