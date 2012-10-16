using Microsoft.Xna.Framework;

namespace PiratesArr.Game.GameMode.BaseMode
{
    abstract public class Mode
    {
        protected static Main mainInstance;
        protected static GraphicsDeviceManager graphicsInstance;

        public Mode()
        {
            mainInstance = Main.GetInstance();
            graphicsInstance = Main.GetInstance(mainInstance);
            LoadContent();
        }

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}