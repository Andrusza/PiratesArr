using Microsoft.Xna.Framework;

namespace Pirates.Screens.Scene
{
    public partial class SceneScreen : BaseMode
    {
        private Matrix view;

        public override void Update(GameTime gameTime)
        {
            Input();
            view = camera.Update();

            water.ViewMatrix = view;
            water.WorldMatrix = tera.WorldMatrix;

            water.Update((float)gameTime.TotalGameTime.TotalSeconds);
        }
    }
}