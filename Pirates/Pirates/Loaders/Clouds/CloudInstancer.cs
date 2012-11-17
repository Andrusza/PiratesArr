using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders.Clouds;

namespace Pirates.Loaders
{
    internal class CloudInstancer
    {
        public BlendState thisBlendState = BlendState.NonPremultiplied;
        public DepthStencilState thisDepthStencilState = DepthStencilState.None;

        protected DynamicVertexBuffer instanceVertexBuffer;

        public VertexBuffer modelVertexBuffer;
        public int vertCount = 0;
        public IndexBuffer indexBuffer;

        protected static VertexDeclaration instanceVertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
            new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
            new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
            new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3)
        );

        public Dictionary<ObjectCloud, Matrix> instanceTransformMatrices = new Dictionary<ObjectCloud, Matrix>();
        public Dictionary<int, ObjectCloud> Instances = new Dictionary<int, ObjectCloud>();

        protected void LoadQuad()
        {
            List<VertexPositionTexture> verts = new List<VertexPositionTexture>();
            List<int> indx = new List<int>();
            Vector2 texCoord = Vector2.Zero;

            indx.Add(0);
            indx.Add(1);
            indx.Add(2);
            indx.Add(2);
            indx.Add(3);
            indx.Add(0);

            verts.Add(new VertexPositionTexture(Vector3.Zero, new Vector2(1, 1)));
            verts.Add(new VertexPositionTexture(Vector3.Zero, new Vector2(0, 1)));
            verts.Add(new VertexPositionTexture(Vector3.Zero, new Vector2(0, 0)));
            verts.Add(new VertexPositionTexture(Vector3.Zero, new Vector2(1, 0)));

            vertCount = verts.Count;
            modelVertexBuffer = new VertexBuffer(BaseClass.Device, typeof(VertexPositionTexture), vertCount, BufferUsage.WriteOnly);
            modelVertexBuffer.SetData(verts.ToArray());
            indexBuffer = new IndexBuffer(BaseClass.Device, IndexElementSize.ThirtyTwoBits, indx.Count, BufferUsage.WriteOnly);
            indexBuffer.SetData(indx.ToArray());
        }

        public void CalcVertexBuffer()
        {
            if (instanceVertexBuffer != null)
                instanceVertexBuffer.Dispose();

            instanceVertexBuffer = new DynamicVertexBuffer(BaseClass.Device, instanceVertexDeclaration, instanceTransformMatrices.Count, BufferUsage.WriteOnly);

            // Transfer the latest instance transform matrices into the instanceVertexBuffer.
            instanceVertexBuffer.SetData(instanceTransformMatrices.Values.ToArray(), 0, instanceTransformMatrices.Count, SetDataOptions.Discard);
        }

        public CloudInstancer()
        {
            LoadQuad();
        }
    }
}