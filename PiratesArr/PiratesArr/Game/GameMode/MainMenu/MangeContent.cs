using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.GUI.Objects;

namespace PiratesArr.Game.GameMode
{
    public partial class MainMenu : Mode
    {
        public override void LoadContent()
        {
            //background//
            spriteBatch = new SpriteBatch(mainInstance.GraphicsDevice);
            backgroundTexture = mainInstance.Content.Load<Texture2D>("GUI\\MainMenu\\Mainmenu");

            screenWidth = mainInstance.GraphicsDevice.PresentationParameters.BackBufferWidth;
            screenHeight = mainInstance.GraphicsDevice.PresentationParameters.BackBufferHeight;

            ///GUI///
            Button b_newGame = new Button();
            b_newGame.TextureName = "BNewGame";
            b_newGame.Pos = new Microsoft.Xna.Framework.Vector2(370, 304);
            b_newGame.Width = 100;
            b_newGame.Height = 50;

            //GUI//
            Button b_Load = new Button();
            b_Load.TextureName = "BLoad";
            b_Load.Pos = new Microsoft.Xna.Framework.Vector2(370, 364);
            b_Load.Width = 100;
            b_Load.Height = 50;

            manager.Add(b_newGame);
            manager.Add(b_Load);
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}