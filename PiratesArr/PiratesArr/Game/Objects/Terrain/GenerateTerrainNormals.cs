using Microsoft.Xna.Framework;

namespace PiratesArr.Game.Surface
{
    public partial class Terrain
    {
        private void GenerateTerrainNormals(VertexPositionNormalTangentBinormalTexture[] vertices, uint[] indices)
        {
            for (uint i = 0; i < indices.Length; i += 3)
            {
                // Get the vertex position (v1, v2, and v3)
                Vector3 v1 = vertices[indices[i]].Position;
                Vector3 v2 = vertices[indices[i + 1]].Position;
                Vector3 v3 = vertices[indices[i + 2]].Position;

                // Calculate vectors v1->v3 and v1->v2 and the normal as a cross product
                Vector3 vu = v3 - v1;
                Vector3 vt = v2 - v1;
                Vector3 normal = Vector3.Cross(vu, vt);
                normal.Normalize();

                vertices[indices[i]].Normal += normal;
                vertices[indices[i + 1]].Normal += normal;
                vertices[indices[i + 2]].Normal += normal;
            }

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal.Normalize();
        }
    }
}