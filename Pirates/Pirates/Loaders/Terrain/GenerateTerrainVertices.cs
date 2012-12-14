using Microsoft.Xna.Framework;

namespace Pirates.Loaders
{
    public partial class Terrain
    {
       
        private float halfTerrainWidth;
        private float halfTerrainDepth;

        private VertexPositionNormalTangentBinormalTexture[] GenerateTerrainVertices()
        {
            float terrainWidth = (vertexCountX - 1) * blockScale;
            float terrainDepth = (vertexCountZ - 1) * blockScale;
            halfTerrainWidth = terrainWidth * 0.5f;
            halfTerrainDepth = terrainDepth * 0.5f;

            float tu = 0;
            float tv = 0;
            float tuDerivative = 1.0f / (vertexCountX - 1);
            float tvDerivative = 1.0f / (vertexCountZ - 1);

            VertexPositionNormalTangentBinormalTexture[] vertices = new VertexPositionNormalTangentBinormalTexture[numVertices];

            int count = 0;
            for (float i = -halfTerrainDepth; i <= halfTerrainDepth; i += blockScale)
            {
                tu = 0.0f;
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
    }
}