using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.Camera.FirstPersonCamera;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.Objects.Namespace_Ship;

namespace PiratesArr.Game.GameMode.Scene
{
    public partial class Scene : Mode
    {
        private Effect basic;
      

        private Matrix projectionMatrix;
        private Matrix viewMatrix;
        private Matrix VP;
        private RasterizerState rs;

        private FirstPersonCamera camera = new FirstPersonCamera();

        private Ship playerShip;

        public Scene(): base()
        {
            SetUpCamera();
            playerShip = new Ship("ship");
            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
        }

        private void SetUpCamera()
        {
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, mainInstance.GraphicsDevice.Viewport.AspectRatio, 1.0f, 5300.0f);
        }
    }
}