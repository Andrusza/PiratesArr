using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.GUI.Objects;

namespace PiratesArr.Game.GameMode.MainMenu
{
    public partial class MainMenu : Mode
    {
        private SpriteBatch spriteBatch;

        private GUIManager manager = new GUIManager();

        private Texture2D backgroundTexture;
        private GraphicsDevice device;

        private int screenWidth;
        private int screenHeight;

        public MainMenu() : base() { }
    }
}