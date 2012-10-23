using Microsoft.Xna.Framework;

namespace PiratesArr.Game.Camera.FirstPersonCamera
{
    public partial class FirstPersonCamera
    {
        private static readonly Vector3 worldY = new Vector3(0, 1, 0);
        private Vector3 dir = new Vector3(0, 0, 1);
        private Matrix view;
        private Main mainInstance = Main.GetInstance();

        private Matrix projectionMatrix;
       
        private Quaternion orientation = new Quaternion(0, 0, 0, 1);

        public FirstPersonCamera(Vector3 position)
        {
            view = Matrix.CreateLookAt(new Vector3(0,0,50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, mainInstance.GraphicsDevice.Viewport.AspectRatio, 1.0f, 10000.0f);
        }
    }
}