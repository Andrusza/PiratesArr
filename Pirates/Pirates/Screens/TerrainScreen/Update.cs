using Microsoft.Xna.Framework;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        private Matrix view;
        private Matrix reflectionMatrix;
        private Vector4 lightPosition;

        public override void Update(GameTime gameTime)
        {
            Input();
            view = camera.Update();

            mvpshader.ViewMatrix = view;
            mvpshader.WorldMatrix = ship.ModelMatrix;
            mvpshader.Update(0);

            scattering.ViewMatrix = view;
            scattering.WorldMatrix = skydome.ModelMatrix;
            scattering.Update(0);
            lightPosition = scattering.LightDirection;

            effect.ViewMatrix = view;
            effect.WorldMatrix = island.WorldMatrix;
            effect.LightPosition = lightPosition;
            effect.Update(0);

            waterShader.ViewMatrix = view;
            waterShader.WorldMatrix = water.WorldMatrix;
            waterShader.LightPosition = lightPosition;
            waterShader.Update((float)gameTime.TotalGameTime.Milliseconds);
        }
    }
}