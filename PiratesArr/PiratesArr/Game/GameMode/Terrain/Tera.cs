using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.Camera.FirstPersonCamera;
using PiratesArr.Game.GameMode.BaseMode;


namespace PiratesArr.Game.GameMode.Terrain
{
    public partial class Tera : Mode
    {
        private Effect basic;

        private Matrix projectionMatrix;
        private Matrix viewMatrix;
        private Matrix modelMatrix = Matrix.Identity;
        private Matrix VP;
        private RasterizerState rs;
        private Texture2D sand;


        PiratesArr.Game.Terrain.Terrain tera;

        private FirstPersonCamera camera = new FirstPersonCamera();

        public Tera(): base()
        {
            SetUpCamera();

            rs = new RasterizerState();
            tera = new PiratesArr.Game.Terrain.Terrain("map2");
            
            rs.CullMode = CullMode.None;
        }

        private void SetUpCamera()
        {
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, mainInstance.GraphicsDevice.Viewport.AspectRatio, 1.0f, 10000.0f);
        }
    }
}