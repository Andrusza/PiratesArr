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
            islandShader.LightPosition = lightPosition;
            islandShader.Update(0);

            Matrix reflectionViewMatrix=CreateReflectionMap();

            waterShader.ViewMatrix = view;
            waterShader.WorldMatrix = water.WorldMatrix;
            waterShader.lightPosition = lightPosition;

            waterShader.reflectedViewMatrix = reflectionViewMatrix;
            waterShader.reflection = reflectionMap;
            waterShader.Update((float)gameTime.TotalGameTime.Milliseconds);

            
        }

        private Matrix CreateReflectionMap()
        {
            Plane reflectionPlane = CreatePlane(0, new Vector3(0, -1, 0), true);

            Matrix temp = Matrix.CreateReflection(reflectionPlane);
            Matrix reflectionViewMatrix = temp * camera.View;

            scattering.ViewMatrix = reflectionViewMatrix;
            scattering.Update(0);

            mvpshader.ViewMatrix = reflectionViewMatrix;
            mvpshader.Update(0);

            BaseClass.Device.SetRenderTarget(reflectionRenderTarget);

            BaseClass.GetInstance().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            BaseClass.GetInstance().GraphicsDevice.RasterizerState = rs;

            skydome.Draw(scattering);
            ship.Draw(mvpshader);

            BaseClass.Device.SetRenderTarget(null);

            scattering.ViewMatrix = camera.View;
            scattering.Update(0);

            mvpshader.ViewMatrix = camera.View;
            mvpshader.Update(0);

            reflectionMap = reflectionRenderTarget;
            return reflectionViewMatrix;
        }
    }
}