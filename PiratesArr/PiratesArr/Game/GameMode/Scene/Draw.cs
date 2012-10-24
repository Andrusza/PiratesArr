using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Scene : Mode
    {
        public override void Draw(GameTime gameTime)
        {
            mainInstance.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            mainInstance.GraphicsDevice.RasterizerState = rs;

            //playerShip.DrawModel(VP);
        }
    }
}