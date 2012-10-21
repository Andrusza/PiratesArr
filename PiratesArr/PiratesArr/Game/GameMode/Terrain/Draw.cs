using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.GameMode.Tera
{
    public partial class Tera : Mode
    {
        public override void Draw(GameTime gameTime)
        {
            mainInstance.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            mainInstance.GraphicsDevice.RasterizerState = rs;
        }
    }
}