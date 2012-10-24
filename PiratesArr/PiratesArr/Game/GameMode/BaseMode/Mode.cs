using Microsoft.Xna.Framework;

namespace PiratesArr.Game.GameMode.BaseMode
{
    abstract public class Mode
    {
        protected static Main mainInstance;

        protected Mode()
        {
            mainInstance = Main.GetInstance();
            LoadContent();
        }

        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
       
    }
}