using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Loaders
{
    public partial class Terrain : BaseObject
    {
        private Texture2D heightMapTexture;

        public Matrix WorldMatrix
        {
            get { return ModelMatrix; }
            set { ModelMatrix = value; }
        }

        private uint vertexCountX;
        private uint vertexCountZ;

        private uint blockScale;
        private uint heightScale;

        private uint numTriangles;
        private uint numVertices;

        private int numIndices;

        private VertexBuffer vbo;
        private IndexBuffer ibo;

        private Color[] heightMap;

        public Terrain(string assetName, uint blockScale, uint heightScale)
        {
            heightMapTexture = ContentLoader.Load<Texture2D>(ContentType.HEIGHTMAP, assetName);

            int heightMapSize = heightMapTexture.Width * heightMapTexture.Height;

            heightMap = new Color[heightMapSize];
            heightMapTexture.GetData<Color>(heightMap);

            vertexCountX = (uint)heightMapTexture.Width;
            vertexCountZ = (uint)heightMapTexture.Height;
            this.blockScale = blockScale;
            this.heightScale = heightScale;

            GenerateTerrainMesh();
        }

        private void GenerateTerrainMesh()
        {
            numVertices = vertexCountX * vertexCountZ;
            numTriangles = (vertexCountX - 1) * (vertexCountZ - 1) * 2;

            uint[] indices = GenerateTerrainIndices();
            numIndices = indices.Length;

            VertexPositionNormalTangentBinormalTexture[] vertices = GenerateTerrainVertices();
            GenerateTerrainNormals(vertices, indices);
            GenerateTerrainTangentBinormal(vertices, indices);

            vbo = new VertexBuffer(BaseClass.GetInstance().GraphicsDevice, VertexPositionNormalTangentBinormalTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vbo.SetData(vertices);

            ibo = new IndexBuffer(BaseClass.GetInstance().GraphicsDevice, typeof(uint), indices.Length, BufferUsage.WriteOnly);
            ibo.SetData(indices);
        }
    }
}