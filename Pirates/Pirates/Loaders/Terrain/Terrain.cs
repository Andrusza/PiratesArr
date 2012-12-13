using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Colision;

namespace Pirates.Loaders
{
    public partial class Terrain : ObjectGeometry
    {
        private Texture2D heightMapTexture;
        private QuadTree quadTree;

        public QuadTree QuadTree
        {
            get { return quadTree; }
            set { quadTree = value; }
        }

        public void CreateQuadTree()
        {
            //quadTree = new QuadTree(this.GetVertices());
        }

        private float realWidth;
        private float realHeight;

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

        private VertexPositionNormalTangentBinormalTexture[] vertices;

        public VertexPositionNormalTangentBinormalTexture[] Vertices
        {
            get { return vertices; }
            set { vertices = value; }
        }

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

            realHeight = this.blockScale * vertexCountX;
            realWidth = this.blockScale * vertexCountZ;

            GenerateTerrainMesh();
            GetVerticesAndNormals(out verticesArrray, out normalArray);
        }

        public void GetVerticesAndNormals(out Vector3[] verticesArray, out Vector3[] normalsArray)
        {
            verticesArray = new Vector3[this.Vertices.Length];
            normalsArray = new Vector3[this.Vertices.Length];
            int x = 0;
            foreach (VertexPositionNormalTangentBinormalTexture v in this.Vertices)
            {
                verticesArray[x] = v.Position;
                normalsArray[x++] = v.Normal;
            }
        }

        private void GenerateTerrainMesh()
        {
            numVertices = vertexCountX * vertexCountZ;
            numTriangles = (vertexCountX - 1) * (vertexCountZ - 1) * 2;

            uint[] indices = GenerateTerrainIndices();
            numIndices = indices.Length;

            Vertices = GenerateTerrainVertices();
            GenerateTerrainNormals(Vertices, indices);
            GenerateTerrainTangentBinormal(Vertices, indices);

            vbo = new VertexBuffer(BaseClass.GetInstance().GraphicsDevice, VertexPositionNormalTangentBinormalTexture.VertexDeclaration, Vertices.Length, BufferUsage.WriteOnly);
            vbo.SetData(Vertices);

            ibo = new IndexBuffer(BaseClass.GetInstance().GraphicsDevice, typeof(uint), indices.Length, BufferUsage.WriteOnly);
            ibo.SetData(indices);
        }
    }
}