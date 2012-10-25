using Microsoft.Xna.Framework;


namespace PiratesArr.Game.Surface
{
    public partial class Terrain
    {
        public void GenerateTerrainTangentBinormal(VertexPositionNormalTangentBinormalTexture[] vertices, uint[] indices)
        {
            for (uint i = 0; i < vertexCountZ; i++)
            {
                for (uint j = 0; j < vertexCountX; j++)
                {
                    uint vertexIndex = j + i * vertexCountX;
                    Vector3 v1 = vertices[vertexIndex].Position;
                    if (j < vertexCountX - 1)
                    {
                        Vector3 v2 = vertices[vertexIndex + 1].Position;
                        vertices[vertexIndex].Tangent = (v2 - v1);
                    }
                    else
                    {
                        Vector3 v2 = vertices[vertexIndex - 1].Position;
                        vertices[vertexIndex].Tangent = (v1 - v2);
                    }
                    vertices[vertexIndex].Tangent.Normalize();
                    vertices[vertexIndex].Binormal = Vector3.Cross(
                    vertices[vertexIndex].Tangent, vertices[vertexIndex].Normal);
                }
            }
        }
    }
}