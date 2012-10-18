using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.GameMode.Scene
{
    public partial class Scene : Mode
    {
        public override void Draw(GameTime gameTime)
        {
            basic.Parameters["xView"].SetValue(viewMatrix);
            basic.Parameters["xProjection"].SetValue(projectionMatrix);
            


            foreach (EffectPass pass in basic.CurrentTechnique.Passes)
            {
                pass.Apply();
                mainInstance.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1, VertexPositionColor.VertexDeclaration);
            }

        }
    }
}