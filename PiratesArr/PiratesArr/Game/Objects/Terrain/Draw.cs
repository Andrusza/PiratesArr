using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.Terrain
{
    public partial class Terrain
    {
        public void Draw(Matrix VP)
        {
            basic.Parameters["mat_MVP"].SetValue(VP * worldMatrix);

            foreach (EffectPass pass in basic.CurrentTechnique.Passes)
            {
                pass.Apply();

                mainInstance.GraphicsDevice.Indices = this.ibo;
                mainInstance.GraphicsDevice.SetVertexBuffer(this.vbo);
                mainInstance.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.vbo.VertexCount, 0, numIndices);
            }
        }
    }
}