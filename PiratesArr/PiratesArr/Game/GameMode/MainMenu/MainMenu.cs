using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.GUI.Objects;

namespace PiratesArr.Game.GameMode
{
    public partial class MainMenu : Mode
    {
        private SpriteBatch spriteBatch;

        private GUIManager manager = new GUIManager();

        private Texture2D backgroundTexture;

        private int screenWidth;
        private int screenHeight;

        public MainMenu() : base() { }
    }
}