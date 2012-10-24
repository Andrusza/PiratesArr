using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Intro : Mode
    {
        private SpriteBatch spriteBatch;

        private Texture2D backgroundTexture;

        private int screenWidth;
        private int screenHeight;

        public Intro() : base() { }
    }
}