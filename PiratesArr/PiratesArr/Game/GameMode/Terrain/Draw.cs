using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        float time = 0;

        public override void Draw(GameTime gameTime)
        {
            mainInstance.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            mainInstance.GraphicsDevice.RasterizerState = rs;
            time+=(float)gameTime.ElapsedGameTime.TotalSeconds/50f;
            float radians = MathHelper.ToRadians(time);

            tera.Draw(effect);
        }
    }
}