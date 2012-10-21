using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.Terrain
{
    public class Terrain
    {
        private Effect basic;

        private Texture2D heightMapTexture;
        private Texture2D sand;

        private static Main mainInstance;

        private uint vertexCountX;
        private uint vertexCountZ;

        private VertexDeclaration vertexDeclaration;

        private uint blockScale;
        private uint heightScale;

        private uint numTriangles;
        private uint numVertices;

        private Color[] heightMap;

        private void Load(string assetName)
        {
            mainInstance = Main.GetInstance();
            basic = mainInstance.Content.Load<Effect>("Shaders//basic");
            heightMapTexture = mainInstance.Content.Load<Texture2D>("Heightmap//assetName");

            int heightMapSize = heightMapTexture.Width * heightMapTexture.Height;

            heightMap = new Color[heightMapSize];
            heightMapTexture.GetData<Color>(heightMap);

            vertexCountX = (uint)heightMapTexture.Width;
            vertexCountZ = (uint)heightMapTexture.Height;
            blockScale = 1;
            heightScale = 1;

            vertexDeclaration = new VertexDeclaration(VertexPositionNormalTangentBinormalTexture.VertexElements);
        }

        private void GenerateTerrainMesh()
        {
            numVertices = vertexCountX * vertexCountZ;
            numTriangles = (vertexCountX - 1) * (vertexCountZ - 1) * 2;

            uint[] indices = GenerateTerrainIndices();

            VertexPositionNormalTangentBinormalTexture[] vertices = GenerateTerrainVertices();
            GenerateTerrainNormals(vertices);
        }

        private VertexPositionNormalTangentBinormalTexture[] GenerateTerrainVertices()
        {
            float terrainWidth = (vertexCountX - 1) * blockScale;
            float terrainDepth = (vertexCountZ - 1) * blockScale;
            float halfTerrainWidth = terrainWidth * 0.5f;
            float halfTerrainDepth = terrainDepth * 0.5f;

            float tu = 0;
            float tv = 0;
            float tuDerivative = 1.0f / (vertexCountX - 1);
            float tvDerivative = 1.0f / (vertexCountZ - 1);

            VertexPositionNormalTangentBinormalTexture[] vertices = new VertexPositionNormalTangentBinormalTexture[numVertices];

            int count = 0;
            for (float i = -halfTerrainDepth; i <= halfTerrainDepth; i += blockScale)
            {
                for (float j = -halfTerrainWidth; j <= halfTerrainWidth; j += blockScale)
                {
                    vertices[count].Position = new Vector3(j, heightMap[count].R * heightScale, i);
                    vertices[count].TextureCoordinate = new Vector2(tu, tv);

                    tu += tuDerivative;
                    count++;
                }
                tv += tvDerivative;
            }
            return vertices;
        }

        private void GenerateTerrainNormals(VertexPositionNormalTangentBinormalTexture[] vectices)
        {
           
        }

        private uint[] GenerateTerrainIndices()
        {
            uint numIndices = numTriangles * 3;
            uint[] indices = new uint[numIndices];
            int indicesCount = 0;
            for (uint i = 0; i < (vertexCountZ - 1); i++)
            {
                for (uint j = 0; j < (vertexCountX - 1); j++)
                {
                    uint index = (j + i) * vertexCountZ;
                    // First triangle
                    indices[indicesCount++] = index;
                    indices[indicesCount++] = index + 1;
                    indices[indicesCount++] = index + vertexCountX + 1;
                    // Second triangle
                    indices[indicesCount++] = index + vertexCountX + 1;
                    indices[indicesCount++] = index + vertexCountX;
                    indices[indicesCount++] = index;
                }
            }
            return indices;
        }

        public struct VertexPositionNormalTangentBinormalTexture
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 TextureCoordinate;
            public Vector3 Tangent;
            public Vector3 Binormal;

            public static readonly VertexElement[] VertexElements = new VertexElement[]
            {
                new VertexElement(0,VertexElementFormat.Vector3,VertexElementUsage.Position,0),
                new VertexElement(12,VertexElementFormat.Vector3,VertexElementUsage.Position,0),
                new VertexElement(24,VertexElementFormat.Vector2,VertexElementUsage.Position,0),
                new VertexElement(32,VertexElementFormat.Vector3,VertexElementUsage.Position,0),
                new VertexElement(44,VertexElementFormat.Vector3,VertexElementUsage.Position,0),
            };

            public static readonly int SizeInBytes = sizeof(float) * (3 + 3 + 2 + 3 + 3);
        }
    }
}