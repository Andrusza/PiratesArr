using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Intro
{
    public partial class Intro : Mode
    {
        private SpriteBatch spriteBatch;

        private Texture2D backgroundTexture;
        private GraphicsDevice device;

        private int screenWidth;
        private int screenHeight;

        public Intro() : base() { }
    }
}