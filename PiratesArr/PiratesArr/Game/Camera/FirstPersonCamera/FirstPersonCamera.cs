using Microsoft.Xna.Framework;

namespace PiratesArr.Game.Camera.FirstPersonCamera
{
    public partial class FirstPersonCamera
    {
        private static readonly Vector3 worldY = new Vector3(0, 1, 0);
        private Vector3 dir = new Vector3(0, 0, 1);

        private Matrix view;
        private Matrix projectionMatrix;

        public Matrix ProjectionMatrix
        {
            get { return projectionMatrix; }
            set { projectionMatrix = value; }
        }

        private Quaternion orientation = new Quaternion(0, 0, 0, 1);

        public FirstPersonCamera(Vector3 position, float aspectRatio, float min, float max)
        {
            view = Matrix.Identity;
            this.CameraTranslate(position);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, min, max);
        }
    }
}