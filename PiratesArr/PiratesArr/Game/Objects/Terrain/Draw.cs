using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.Surface
{
    public partial class Terrain
    {
        public void Draw(Effect effect)
        {
            effect.Parameters["mat_World"].SetValue(worldMatrix);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                mainInstance.GraphicsDevice.Indices = this.ibo;
                mainInstance.GraphicsDevice.SetVertexBuffer(this.vbo);
                mainInstance.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.vbo.VertexCount, 0, numIndices);
            }
        }
    }
}