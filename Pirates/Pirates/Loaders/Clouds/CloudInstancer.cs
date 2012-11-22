using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders.Cloud;
using Pirates.Shaders;

namespace Pirates.Loaders
{
    internal class CloudInstancer : ObjectGeometry
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

        private float x = 0;

        public void Update(float time)
        {
            x += 0.0001f;

            this.Rotate(x, x, 0);
            this.Update();
        }

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

        public virtual void Draw(CloudShader shader)
        {
            if ((instanceVertexBuffer == null) || (instanceTransformMatrices.Count != instanceVertexBuffer.VertexCount))
                CalcVertexBuffer();

            BaseClass.Device.BlendState = thisBlendState;
            BaseClass.Device.DepthStencilState = thisDepthStencilState;

            shader.Technique.CurrentTechnique.Passes[0].Apply();

            BaseClass.Device.SetVertexBuffers(
                       new VertexBufferBinding(modelVertexBuffer, 0, 0),
                       new VertexBufferBinding(instanceVertexBuffer, 0, 1)
                   );

            BaseClass.Device.Indices = indexBuffer;
            BaseClass.Device.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0,
                                                   modelVertexBuffer.VertexCount, 0,
                                                   2,
                                                   instanceTransformMatrices.Count);
        }

        public void CalcVertexBuffer()
        {
            if (instanceVertexBuffer != null)
                instanceVertexBuffer.Dispose();

            instanceVertexBuffer = new DynamicVertexBuffer(BaseClass.Device, instanceVertexDeclaration, instanceTransformMatrices.Count, BufferUsage.WriteOnly);
            instanceVertexBuffer.SetData(instanceTransformMatrices.Values.ToArray(), 0, instanceTransformMatrices.Count, SetDataOptions.Discard);
        }

        public CloudInstancer()
        {
            LoadQuad();
        }
    }
}