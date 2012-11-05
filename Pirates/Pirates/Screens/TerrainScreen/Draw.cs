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
            island.Draw(effect);
            water.Draw(waterShader);

            //ship.Update(mvpshader.Technique);
            ship.DrawModel(view, projectionMatrix);

            skydome.Update(scattering.Technique);
            skydome.DrawModel();
        }

        private void DrawRefractionMap()
        {
            Plane refractionPlane = CreatePlane(waterHeight + 1.5f, new Vector3(0, -1, 0), view, false);

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
            Plane reflectionPlane = CreatePlane(waterHeight - 0.5f, new Vector3(0, -1, 0), reflectionViewMatrix, true);

            effect.Technique.Parameters["ClipPlane0"].SetValue(new Vector4(reflectionPlane.Normal, reflectionPlane.D));
            scattering.Technique.Parameters["ClipPlane0"].SetValue(new Vector4(reflectionPlane.Normal, reflectionPlane.D));

            effect.Technique.Parameters["ClipPlane0"].SetValue(true);
            scattering.Technique.Parameters["ClipPlane0"].SetValue(true);
            BaseClass.GetInstance().GraphicsDevice.SetRenderTarget(reflectionRenderTarget);

            BaseClass.GetInstance().GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            skydome.Update(scattering.Technique);
            skydome.DrawModel(); ///////////////////WHERE IS REFLECTION MATRIX?????////////////////
            island.Draw(effect);

            effect.Technique.Parameters["ClipPlane0"].SetValue(false);
            scattering.Technique.Parameters["ClipPlane0"].SetValue(false);

            BaseClass.GetInstance().GraphicsDevice.SetRenderTarget(null);

            reflectionMap = reflectionRenderTarget;
            //System.IO.Stream ss = System.IO.File.OpenWrite("C:\Test\Reflection.jpg");
            //reflectionRenderTarget.SaveAsJpeg(ss, 500, 500);
            //ss.Close();
        }
    }
}