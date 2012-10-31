using Microsoft.Xna.Framework;

namespace Pirates.Screens
{
    public abstract class BaseMode
    {
        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}