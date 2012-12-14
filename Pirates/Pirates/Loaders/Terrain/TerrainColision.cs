using System;
using Microsoft.Xna.Framework;

namespace Pirates.Loaders
{
    public partial class Terrain : ObjectGeometry
    {
        private Vector3[] verticesArrray;
        private Vector3[] normalArray;

        public bool IsOnHeightmap(Vector3 position)
        {
            Vector3 positionOnHeightmap = position - ModelMatrix.Translation;
            return (positionOnHeightmap.X > -this.halfTerrainWidth &&
                positionOnHeightmap.X < this.halfTerrainDepth &&
                positionOnHeightmap.Z > -this.halfTerrainDepth &&
                positionOnHeightmap.Z < this.halfTerrainDepth);
        }

        public int PlaceInArray(int top, int left)
        {
            int place = (top) * ((int)vertexCountZ) + left;
            //Vertices[place].Position.Y = 100;
            return place;
        }

        public void GetHeightAndNormal(Vector3 position, out float height, out Vector3 normal)
        {
            Vector3 positionOnHeightmap = position - ModelMatrix.Translation;

            int left, bottom;

            left = ((int)positionOnHeightmap.X + (int)halfTerrainWidth) / (int)blockScale;
            bottom = ((int)positionOnHeightmap.Z + (int)halfTerrainDepth) / (int)blockScale;

            float xNormalized = ((positionOnHeightmap.X + halfTerrainWidth) % blockScale) / blockScale;
            float zNormalized = ((positionOnHeightmap.Z + halfTerrainDepth) % blockScale) / blockScale;
           

            float bottomHeight = MathHelper.Lerp(
                verticesArrray[PlaceInArray(bottom, left)].Y,
                verticesArrray[PlaceInArray(bottom, left + 1)].Y,
                xNormalized);

            float topHeight = MathHelper.Lerp(
                verticesArrray[PlaceInArray(bottom + 1, left)].Y,
                verticesArrray[PlaceInArray(bottom + 1, left + 1)].Y,
                xNormalized);

            height = MathHelper.Lerp(bottomHeight, topHeight, zNormalized);

            Vector3 bottomNormal = Vector3.Lerp(
               normalArray[PlaceInArray(bottom, left)],
               normalArray[PlaceInArray(bottom, left + 1)],
                xNormalized);

            Vector3 topNormal = Vector3.Lerp(
               normalArray[PlaceInArray(bottom + 1, left)],
               normalArray[PlaceInArray(bottom + 1, left + 1)],
               xNormalized);

            normal = Vector3.Lerp(bottomNormal, topNormal, zNormalized);
            normal.Normalize();
        }
    }
}