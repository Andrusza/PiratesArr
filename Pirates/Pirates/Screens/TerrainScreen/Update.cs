using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        private Matrix view;
        private Vector4 lightPosition;
        private float time = 0;
        private float x = 0;

        public override void Update(GameTime gameTime)
        {
            Input();
            time += gameTime.TotalGameTime.Seconds;
            view = camera.Update();
            //Console.WriteLine(view.Translation.ToString());

            mvpshader.ViewMatrix = view;
            mvpshader.WorldMatrix = ship.ModelMatrix;
            mvpshader.Update(0);

            cloudManager.Instancer.Update(0);

            scatteringShader.ViewMatrix = view;
            scatteringShader.WorldMatrix = skydome.ModelMatrix;
            scatteringShader.Update(time);
            lightPosition = scatteringShader.lightPosition;

            cloudShader.ViewMatrix = view;
            cloudShader.WorldMatrix = cloudManager.Instancer.ModelMatrix;
            cloudShader.eyePosition = camera.Eye;
            cloudShader.lightVector = lightPosition;

            ////this formula is wrong./////////////////////
            float hour = -lightPosition.Y * 0.01f;
            if (hour < 0) hour = 24 + (hour * 2);
            cloudShader.hour = hour;
            ///////////////////////////////////////////////

            cloudShader.Update(0);

            rainShader.ViewMatrix = view;
            rainShader.WorldMatrix = rainManager.Instancer.ModelMatrix;
            rainShader.eyePosition = camera.Eye;
            rainShader.lightVector = lightPosition;
            rainShader.Update(time);

            islandShader.ViewMatrix = view;
            islandShader.WorldMatrix = island.WorldMatrix;
            islandShader.lightPosition = lightPosition;
            islandShader.Update(0);

            Matrix reflectionViewMatrix = CreateReflectionMap();

            waterShader.ViewMatrix = view;
            waterShader.WorldMatrix = water.WorldMatrix;
            waterShader.lightPosition = lightPosition;

            waterShader.reflectedViewMatrix = reflectionViewMatrix;
            waterShader.reflection = reflectionMap;
            waterShader.Update(time);

            RenderCurrentFrameToTexture();
            rainDropsShader.currentFrame = currentFrame;
            rainDropsShader.Update(time);

            fogShader.currentFrame = currentFrame;
            fogShader.Update(0);
        }

        private Plane CreatePlane(float height, Vector3 planeNormalDirection, bool clipSide)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide) planeCoeffs *= -1;
            Plane finalPlane = new Plane(planeCoeffs);
            return finalPlane;
        }

        private void RenderCurrentFrameToTexture()
        {
            BaseClass.Device.SetRenderTarget(currentFrameRenderTarget);
            {
                BaseClass.Device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

                BaseClass.Device.DepthStencilState = DepthStencilState.Default;
                BaseClass.Device.RasterizerState = rs;

                skydome.Draw(scatteringShader);
                cloudManager.Draw(cloudShader);

                BaseClass.Device.DepthStencilState = DepthStencilState.Default;
                BaseClass.Device.RasterizerState = rs;



                island.Draw(islandShader);
                water.Draw(waterShader);

                rainManager.Draw(rainShader);



                //ship.Draw(mvpshader);
            }
            BaseClass.Device.SetRenderTarget(null);
            currentFrame = currentFrameRenderTarget;

           
           
        }

        private Matrix CreateReflectionMap()
        {
            Plane reflectionPlane = CreatePlane(30, new Vector3(0, -1, 0), true);

            Matrix temp = Matrix.CreateReflection(reflectionPlane);
            Matrix reflectionViewMatrix = temp * camera.View;

            scatteringShader.ViewMatrix = reflectionViewMatrix;
            scatteringShader.Update(0);

            mvpshader.ViewMatrix = reflectionViewMatrix;
            mvpshader.Update(0);

            cloudShader.ViewMatrix = reflectionViewMatrix;
            cloudShader.Update(0);

            islandShader.ViewMatrix = reflectionViewMatrix;
            islandShader.clippingPlane = reflectionPlane;
            islandShader.clipping = true;
            islandShader.Update(0);

            BaseClass.Device.SetRenderTarget(reflectionRenderTarget);
            {
                BaseClass.Device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Aqua, 1.0f, 0);
                BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;

                skydome.Draw(scatteringShader);
                island.Draw(islandShader);
                //ship.Draw(mvpshader);
                cloudManager.Instancer.Draw(cloudShader);
            }
            BaseClass.Device.SetRenderTarget(null);

            cloudShader.ViewMatrix = view;
            cloudShader.Update(0);

            scatteringShader.ViewMatrix = view;
            scatteringShader.Update(0);

            mvpshader.ViewMatrix = view;
            mvpshader.Update(0);

            islandShader.ViewMatrix = view;
            islandShader.clipping = false;
            islandShader.Update(0);

            reflectionMap = reflectionRenderTarget;
            return reflectionViewMatrix;
        }
    }
}