using Microsoft.Xna.Framework;

namespace PiratesArr.Game.Camera.FirstPersonCamera
{
    public partial class FirstPersonCamera
    {
        private static readonly Vector3 worldY = new Vector3(0, 1, 0);
        private Vector3 dir = new Vector3(0, 0, 1);
        private Matrix view = Matrix.Identity;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Quaternion orientation = new Quaternion(0, 0, 0, 1);

        public FirstPersonCamera()
        {
        }
    }
}