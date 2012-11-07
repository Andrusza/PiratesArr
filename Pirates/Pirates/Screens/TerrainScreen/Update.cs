using Microsoft.Xna.Framework;
using System;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        private Matrix view;
        private Matrix reflectionMatrix;
        private Vector4 lightPosition;

        float time = 0;

        public override void Update(GameTime gameTime)
        {
            Input();
            time += gameTime.TotalGameTime.Seconds;
            Console.WriteLine(time);
            view = camera.Update();

            mvpshader.ViewMatrix = view;
            //mvpshader.WorldMatrix = ship.ModelMatrix;
            mvpshader.Update(0);

            scattering.ViewMatrix = view;
            scattering.WorldMatrix = skydome.ModelMatrix;
            scattering.Update(time);
            lightPosition = scattering.lightPosition;
           

            effect.ViewMatrix = view;
            effect.WorldMatrix = island.WorldMatrix;
            effect.LightPosition = lightPosition;
            effect.Update(0);

            waterShader.ViewMatrix = view;
            waterShader.WorldMatrix = water.WorldMatrix;
            waterShader.LightPosition = lightPosition;
            waterShader.Update((float)gameTime.TotalGameTime.Milliseconds);

            DrawReflectionMap();
        }
    }
}