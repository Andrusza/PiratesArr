using Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;
using Pirates.Loaders;

namespace Pirates.Screens.Scene
{
    public partial class SkyDomeScreen : BaseMode
    {
        private FirstPersonCamera camera;
        private float aspectRatio = BaseClass.GetInstance().AspectRatio;

        private Matrix projectionMatrix;
        private RasterizerState rs;

        private JustMvp effect;
        private GameObject skydome;
        

        public SkyDomeScreen()
        {
            camera = new FirstPersonCamera(new Vector3(0, 10, 0));

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);

            effect = new JustMvp();
            {
                effect.ProjectionMatrix = projectionMatrix;
                effect.ViewMatrix = camera.View;
                effect.InitParameters();
            }

            skydome = new GameObject("dome", effect);
            skydome.Scale(2000);

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
        }
    }
}