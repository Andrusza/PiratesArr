using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Loaders
{
    public class BoundingBoxRenderer
    {
        private VertexPositionColor[] verts = new VertexPositionColor[8];
        private VertexBuffer vbo;
        private IndexBuffer ibo;
        private BasicEffect effect;

        private static int[] indices = new int[]
        {
            0,1,2,
            0,3,2,
            0,4,5,
            0,5,1,
            1,2,5,
            2,5,6,
            0,4,7,
            0,3,7,
            4,6,5,
            4,7,6,

        };

        public BoundingBoxRenderer()
        {
            vbo = new VertexBuffer(BaseClass.Device, VertexPositionColor.VertexDeclaration, 8, BufferUsage.WriteOnly);
            ibo = new IndexBuffer(BaseClass.Device, typeof(uint), indices.Length, BufferUsage.WriteOnly);
            ibo.SetData(indices);

            effect = new BasicEffect(BaseClass.Device);
            effect.VertexColorEnabled = true;
            effect.LightingEnabled = false;
        }

        public void Render(BoundingBoxOOB box,Matrix model,Matrix view,Matrix projection)
        {

            Vector3[] corners = box.Corners;
            for (int i = 0; i < 8; i++)
            {
                verts[i].Position = corners[i];
                verts[i].Color = Color.White;
            }

            vbo.SetData(verts);
            ibo.SetData(indices);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                effect.World = Matrix.Identity;
                effect.View = view;
                effect.Projection = projection;

                pass.Apply();
                ContentLoader.SetBuffers(ibo, vbo);
            }
        }
    }
}