using Microsoft.Xna.Framework;

namespace Pirates.Screens.Scene
{
    public partial class TerrainScreen : BaseMode
    {
        private Matrix view;

        public override void Update(GameTime gameTime)
        {
            Input();
            view = camera.Update();

            effect.ViewMatrix = view;
            effect.WorldMatrix = island.WorldMatrix;
            effect.Update(0);

            waterShader.ViewMatrix = view;
            waterShader.WorldMatrix = water.WorldMatrix;
            waterShader.Update((float)gameTime.TotalGameTime.Milliseconds);

            scattering.ViewMatrix = view;
            scattering.WorldMatrix = skydome.ModelMatrix;
            scattering.Update(0);
          
        }
    }
}