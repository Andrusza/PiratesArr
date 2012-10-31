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
            effect.Update(0);
        }
    }
}