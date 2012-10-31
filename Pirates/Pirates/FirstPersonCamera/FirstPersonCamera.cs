using Microsoft.Xna.Framework;

namespace Cameras
{
    public partial class FirstPersonCamera
    {
        private static readonly Vector3 worldY = new Vector3(0, 1, 0);
        private Vector3 dir = new Vector3(0, 0, 1);

        private Quaternion orientation;
        private Matrix view;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        public FirstPersonCamera(Vector3 position)
        {
            view = Matrix.Identity;
            this.orientation = new Quaternion(0, 0, 0, 1);
            this.CameraTranslate(position);
        }

        public FirstPersonCamera(Vector3 position, Quaternion orientation)
        {
            view = Matrix.Identity;
            this.orientation = orientation;
            this.CameraTranslate(position);
        }
    }
}