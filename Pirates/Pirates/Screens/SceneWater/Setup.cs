using Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;
using Pirates.Shaders;
using System.Runtime.Serialization;

namespace Pirates.Screens.Scene
{
    public partial class SceneScreen : BaseMode
    {
        private Terrain tera;
        private Basic water;

        private FirstPersonCamera camera;
        private float aspectRatio = BaseClass.GetInstance().AspectRatio;

        private Matrix projectionMatrix;
        private RasterizerState rs;

        public SceneScreen()
        {
            camera = new FirstPersonCamera(new Vector3(0, 10, 0));

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);

            water = new Basic();
            {
                water.ProjectionMatrix = projectionMatrix;
                water.ViewMatrix = camera.View;
                water.ModelViewProj();
                water.InitParameters();
            }

            tera = new Terrain("map2", 1, 1);

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
        }

        public SceneScreen(SerializationInfo info, StreamingContext ctxt)
        {
           
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            
        }

        public override void ToFile()
        {
           
        }

        public static SceneScreen FromFile()
        {
            return null;
        }
    }
}