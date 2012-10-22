using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Terrain
{
    public partial class Tera : Mode
    {
        public override void Draw(GameTime gameTime)
        {
            mainInstance.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            mainInstance.GraphicsDevice.RasterizerState = rs;
            basic.CurrentTechnique = basic.Techniques["Textured"];
            basic.Parameters["mat_MVP"].SetValue(VP*modelMatrix);


            foreach (EffectPass pass in basic.CurrentTechnique.Passes)
            {
                pass.Apply();

                mainInstance.GraphicsDevice.Indices = tera.Ibo;
                mainInstance.GraphicsDevice.SetVertexBuffer(tera.Vbo);
                mainInstance.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, tera.Vbo.VertexCount, 0, tera.Ibo.IndexCount / 3);
            }
        }
    }
}