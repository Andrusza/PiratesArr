using Microsoft.Xna.Framework;
using System;

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
            int place = (top) * ((int)halfTerrainWidth+1) + left;
            Vertices[place].Position.Y = 1100;
            return place;
            
        }

        public void GetHeightAndNormal(Vector3 position, out float height, out Vector3 normal)
        {
            Vector3 positionOnHeightmap = position - ModelMatrix.Translation;

            int left, top;

            left = ((int)positionOnHeightmap.X + (int)halfTerrainWidth) / (int)blockScale;
            top = ((int)positionOnHeightmap.Z + (int)halfTerrainWidth) / (int)blockScale;

            float xNormalized = (positionOnHeightmap.X % blockScale) / blockScale;
            float zNormalized = (positionOnHeightmap.Z % blockScale) / blockScale;
            Console.WriteLine(PlaceInArray(top, left));

            //// Now that we've calculated the indices of the corners of our cell, and
            //// where we are in that cell, we'll use bilinear interpolation to calculuate
            //// our height. This process is best explained with a diagram, so please see
            //// the accompanying doc for more information.
            //// First, calculate the heights on the bottom and top edge of our cell by
            //// interpolating from the left and right sides.
            float topHeight = MathHelper.Lerp(
                verticesArrray[PlaceInArray(top, left)].Y,
                verticesArrray[PlaceInArray(top, left + 1)].Y,
                xNormalized);

            float bottomHeight = MathHelper.Lerp(
                verticesArrray[PlaceInArray(top + 1, left)].Y,
                verticesArrray[PlaceInArray(top + 1, left + 1)].Y,
                xNormalized);

            //// next, interpolate between those two values to calculate the height at our
            //// position.
            height = MathHelper.Lerp(topHeight, bottomHeight, zNormalized);

            //// We'll repeat the same process to calculate the normal.

            Vector3 topNormal = Vector3.Lerp(
               normalArray[PlaceInArray(top, left)],
               normalArray[PlaceInArray(top, left + 1)],
                xNormalized);

            Vector3 bottomNormal = Vector3.Lerp(
               normalArray[PlaceInArray(top + 1, left)],
               normalArray[PlaceInArray(top + 1, left + 1)],
               xNormalized);

            normal = Vector3.Lerp(topNormal, bottomNormal, zNormalized);
            normal.Normalize();
            //normal = new Vector3(0, 1, 0);
        }
    }
}