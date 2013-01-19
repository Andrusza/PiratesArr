using Microsoft.Xna.Framework;
using Pirates.Loaders.ModelsFbx;

namespace Pirates.Loaders
{
    public partial class Terrain : ObjectGeometry
    {
        private Vector3[] verticesArrray;
        private Vector3[] normalArray;

        public bool IsOnHeightmap(Vector3 position)
        {
            Vector3 positionOnHeightmap = position - ModelMatrix.Translation;

            bool onMap = (positionOnHeightmap.X > -this.halfTerrainWidth &&
                positionOnHeightmap.X < this.halfTerrainDepth &&
                positionOnHeightmap.Z > -this.halfTerrainDepth &&
                positionOnHeightmap.Z < this.halfTerrainDepth);
            if (onMap)
            {
                int left = ((int)positionOnHeightmap.X + (int)halfTerrainWidth) / (int)blockScale;
                int bottom = ((int)positionOnHeightmap.Z + (int)halfTerrainDepth) / (int)blockScale;
                if (verticesArrray[PlaceInArray(bottom, left)].Y > 0f)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        
        public int PlaceInArray(int top, int left)
        {
            int place = (top) * ((int)vertexCountZ) + left;
            //Vertices[place].Position.Y = 100;
            return place;
        }

        public bool ColisionWithTerrain(ObjectShip ship)
        {
            if (IsOnHeightmap(ship.ModelMatrix.Translation))
            {
                float height;
                Vector3 normal;
                GetHeightAndNormal(ship.ModelMatrix.Translation, out height, out normal);

                ship.Translate(new Vector3(ship.ModelMatrix.Translation.X, height, ship.ModelMatrix.Translation.Z));
                ship.Update();

                ship.UpVector = normal;
                ship.RightVector = Vector3.Cross(ship.ForwardVector, ship.UpVector);
                ship.RightVector = Vector3.Normalize(ship.RightVector);

                ship.ForwardVector = Vector3.Cross(ship.UpVector, ship.RightVector);
                ship.ForwardVector = Vector3.Normalize(ship.ForwardVector);
                return true;
            }
            return false;
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