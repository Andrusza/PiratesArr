using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        private Matrix view;
        private Vector4 lightPosition;

        private float time = 0;

        public override void Update(GameTime gameTime)
        {
            Input();
            time += gameTime.TotalGameTime.Seconds;
            Console.WriteLine(time);
            view = camera.Update();

            mvpshader.ViewMatrix = view;
            mvpshader.WorldMatrix = ship.ModelMatrix;
            mvpshader.Update(0);

            scattering.ViewMatrix = view;
            scattering.WorldMatrix = skydome.ModelMatrix;
            scattering.Update(time);
            lightPosition = scattering.lightPosition;

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
            waterShader.Update((float)gameTime.TotalGameTime.Milliseconds);
        }

        private Plane CreatePlane(float height, Vector3 planeNormalDirection, bool clipSide)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide) planeCoeffs *= -1;
            Plane finalPlane = new Plane(planeCoeffs);
            return finalPlane;
        }

        private Matrix CreateReflectionMap()
        {
            Plane reflectionPlane = CreatePlane(30, new Vector3(0, -1, 0), true);

            Matrix temp = Matrix.CreateReflection(reflectionPlane);
            Matrix reflectionViewMatrix = temp * camera.View;

            scattering.ViewMatrix = reflectionViewMatrix;
            scattering.Update(0);

            mvpshader.ViewMatrix = reflectionViewMatrix;
            mvpshader.Update(0);

            islandShader.ViewMatrix = reflectionViewMatrix;
            islandShader.clippingPlane = reflectionPlane;
            islandShader.clipping = true;
            islandShader.Update(0);

            BaseClass.Device.SetRenderTarget(reflectionRenderTarget);

            BaseClass.Device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Aqua, 1.0f, 0);
            BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;

            skydome.Draw(scattering);
            island.Draw(islandShader);
            ship.Draw(mvpshader);

            BaseClass.Device.SetRenderTarget(null);

            scattering.ViewMatrix = view;
            scattering.Update(0);

            mvpshader.ViewMatrix = view;
            mvpshader.Update(0);

            islandShader.ViewMatrix = view;
            islandShader.clipping = false;
            islandShader.Update(0);

            reflectionMap = reflectionRenderTarget;
            return reflectionViewMatrix;
        }

        //private void CreateRefractionMap()
        //{
        //    Plane refractionPlane = CreatePlane(30, new Vector3(0, -1, 0), false);

        //    Matrix temp = Matrix.CreateReflection(refractionPlane);

        //    islandShader.clippingPlane = refractionPlane;
        //    islandShader.clipping = true;
        //    islandShader.Update(0);

        //    BaseClass.Device.SetRenderTarget(refractionRenderTarget);

        //    BaseClass.Device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Aqua, 1.0f, 0);
        //    BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        //    BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;

        //    island.Draw(islandShader);

        //    BaseClass.Device.SetRenderTarget(null);

        //    islandShader.clipping = false;
        //    islandShader.Update(0);

        //    refractionMap = refractionRenderTarget;
        //}
    }
}