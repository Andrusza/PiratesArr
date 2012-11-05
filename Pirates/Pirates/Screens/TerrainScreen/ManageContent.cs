using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        public override void LoadContent()
        {
            PresentationParameters pp = BaseClass.GetInstance().GraphicsDevice.PresentationParameters;
            refractionRenderTarget = new RenderTarget2D(BaseClass.GetInstance().GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, pp.BackBufferFormat, pp.DepthStencilFormat);
            reflectionRenderTarget = new RenderTarget2D(BaseClass.GetInstance().GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, pp.BackBufferFormat, pp.DepthStencilFormat);
        }

        public override void UnloadContent()
        {
        }

        private Plane CreatePlane(float height, Vector3 planeNormalDirection, Matrix currentViewMatrix, bool clipSide)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide) planeCoeffs *= -1;
            Plane finalPlane = new Plane(planeCoeffs);
            return finalPlane;
        }
    }
}