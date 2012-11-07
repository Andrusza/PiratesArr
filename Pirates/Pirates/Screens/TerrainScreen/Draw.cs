using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        public override void Draw(GameTime gameTime)
        {
            BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;
            //island.Draw(effect);
            //water.Draw(waterShader);

            //ship.Update(mvpshader.Technique);
            //ship.DrawModel(view, projectionMatrix);

            skydome.Draw(scattering);
        }

        private void DrawRefractionMap()
        {
            Plane refractionPlane = CreatePlane(waterHeight + 1.5f, new Vector3(0, -1, 0), false);

            waterShader.Technique.Parameters["ClipPlane0"].SetValue(new Vector4(refractionPlane.Normal, refractionPlane.D));
            waterShader.Technique.Parameters["Clipping"].SetValue(true);

            BaseClass.GetInstance().GraphicsDevice.SetRenderTarget(refractionRenderTarget);
            BaseClass.GetInstance().GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            island.Draw(waterShader);

            BaseClass.GetInstance().GraphicsDevice.SetRenderTarget(refractionRenderTarget);
            waterShader.Technique.Parameters["Clipping"].SetValue(false);
            refractionMap = refractionRenderTarget;

            //System.IO.Stream ss = System.IO.File.OpenWrite("C:\Test\Refraction.jpg");
            //refractionRenderTarget.SaveAsJpeg(ss, 500, 500);
            //ss.Close();
        }

        private void DrawReflectionMap()
        {
            Plane reflectionPlane = CreatePlane(100 - 0.5f, new Vector3(0, -1, 0), true);

            Matrix temp = Matrix.CreateReflection(reflectionPlane);
            Matrix reflectionViewMatrix = temp * camera.View * temp;

            scattering.plane = new Vector4(reflectionPlane.Normal, reflectionPlane.D);
            scattering.Clipping = true;

            scattering.ViewMatrix = reflectionViewMatrix;
            scattering.Update(0);

            BaseClass.Device.SetRenderTarget(reflectionRenderTarget);
            BaseClass.GetInstance().GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Aqua, 1.0f, 0);

            skydome.Draw(scattering);

            BaseClass.Device.SetRenderTarget(null);

            scattering.Clipping = false;
            scattering.ViewMatrix = camera.View;
            scattering.Update(0);

            reflectionMap = reflectionRenderTarget;
            //System.IO.Stream ss = System.IO.File.Create("D:\\Reflection.jpg");
            //reflectionRenderTarget.SaveAsJpeg(ss, 500, 500);
            //ss.Close();
        }
    }
}