namespace PiratesArr.Game.Terrain
{
    public partial class Terrain
    {
        private uint[] GenerateTerrainIndices()
        {
            uint numIndices = numTriangles * 3;
            uint[] indices = new uint[numIndices];
            uint indicesCount = 0;
            for (uint i = 0; i < (vertexCountZ - 1); i++)
            {
                for (uint j = 0; j < (vertexCountX - 1); j++)
                {
                    uint index = j + i * vertexCountZ;
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
    }
}