using Microsoft.Xna.Framework.Input;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Intro : Mode
    {
        static public void Input()
        {
            KeysPressed();
            MousePressed();
        }

        static private void KeysPressed()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Enter))
            {
                mainInstance.Exit();
            }
        }

        static private void MousePressed()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                mainInstance.RenderMode = new MainMenu();
            }
        }
    }
}