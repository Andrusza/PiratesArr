using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.Camera.FirstPersonCamera;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Tera
{
    public partial class Tera : Mode
    {
        private Effect basic;

        private Matrix projectionMatrix;
        private Matrix worldMatrix = Matrix.Identity;
        private Matrix viewMatrix;
        private RasterizerState rs;

        private FirstPersonCamera camera = new FirstPersonCamera();

        private Texture2D heightMapTexture;

        public Tera(): base()
        {
            SetUpCamera();
            worldMatrix = Matrix.CreateRotationZ(-MathHelper.PiOver2);

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
        }

        private void SetUpCamera()
        {
            mainInstance.ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, mainInstance.GraphicsDevice.Viewport.AspectRatio, 1.0f, 5300.0f);
        }
    }
}