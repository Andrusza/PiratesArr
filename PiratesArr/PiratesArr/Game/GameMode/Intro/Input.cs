using Microsoft.Xna.Framework.Input;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Intro
{
    public partial class Intro : Mode
    {
        public void Input()
        {
            KeysPressed();
            MousePressed();
        }

        private void KeysPressed()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Enter))
            {
                mainInstance.Exit();
            }
        }

        private void MousePressed()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                mainInstance.RenderMode = new PiratesArr.Game.GameMode.MainMenu.MainMenu();
            }
        }
    }
}