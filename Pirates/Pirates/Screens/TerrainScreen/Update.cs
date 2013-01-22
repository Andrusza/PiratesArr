using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Physics;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        private Matrix view;
        private Vector4 lightPosition;
        private float time = 0;
        private float oldtime = 0;
        private float dt = 0;

        public override void Update(GameTime gameTime)
        {
            Input();
            time += gameTime.TotalGameTime.Seconds;

            view = camera.Update();
            //Console.WriteLine(camera.Eye.ToString());

            shipShader.View = view;

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
            islandShader.WorldMatrix = island.ModelMatrix;
            islandShader.lightPosition = lightPosition;
            islandShader.Update(0);

            Matrix reflectionViewMatrix = CreateReflectionMap();

            waterShader.ViewMatrix = view;
            waterShader.WorldMatrix = water.ModelMatrix;
            waterShader.lightPosition = lightPosition;

            waterShader.reflectedViewMatrix = reflectionViewMatrix;
            waterShader.reflection = reflectionMap;

            waterShader.Update(time);

            RenderCurrentFrameToTexture();
            rainDropsShader.currentFrame = currentFrame;
            rainDropsShader.Update(time);

            fogShader.currentFrame = currentFrame;
            fogShader.Update(0);

            ship.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (island.IsOnHeightmap(ship.ModelMatrix.Translation))
            {
                ship.Physics.material = MaterialType.Island;
                island.ColisionWithTerrain(ship);
            }
            else
            {
                //ship.Physics.material = MaterialType.Water;
                //water.GetObjectPositionOnWater(ship, waterShader);
            }
           

            //Console.WriteLine(camera.Eye.ToString());
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
                BaseClass.Device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.AliceBlue, 1.0f, 0);
                BaseClass.Device.DepthStencilState = DepthStencilState.Default;
                BaseClass.Device.RasterizerState = rs;

                skydome.Draw(scatteringShader);
                //cloudManager.Draw(cloudShader);

                BaseClass.Device.DepthStencilState = DepthStencilState.Default;
                BaseClass.Device.RasterizerState = rs;

                ship.Draw(shipShader);
                island.Draw(islandShader);
                water.Draw(waterShader);

                //rainManager.Draw(rainShader);
            }
            BaseClass.Device.SetRenderTarget(null);
            currentFrame = currentFrameRenderTarget;
        }

        private Matrix CreateReflectionMap()
        {
            Matrix reflectionViewMatrix = reflectionMatrix * camera.View;

            scatteringShader.ViewMatrix = reflectionViewMatrix;
            scatteringShader.Update(0);

            mvpshader.ViewMatrix = reflectionViewMatrix;
            mvpshader.Update(0);

            cloudShader.ViewMatrix = reflectionViewMatrix;
            cloudShader.Update(0);

            shipShader.View = reflectionViewMatrix;

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
                ship.Draw(shipShader);
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

            shipShader.View = view;

            reflectionMap = reflectionRenderTarget;
            return reflectionViewMatrix;
        }
    }
}