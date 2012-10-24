using Microsoft.Xna.Framework.Input;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class MainMenu : Mode
    {
        public void Input()
        {
            KeysPressed();
            MousePressed();
        }

        static private void KeysPressed()
        {
            KeyboardState keyState = Keyboard.GetState();
        }

        private void MousePressed()
        {
            MouseState mouseState = Mouse.GetState();
            manager.MouseOver(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                manager.CheckIfClick(mouseState.X, mouseState.Y);
                if (manager.IsClicked(0))
                {
                    ButtonNewGameClicked();
                }

                if (manager.IsClicked(1))
                {
                    ButtonLoadClicked();
                }
            }
        }

        static private void ButtonNewGameClicked()
        {
            mainInstance.RenderMode = new Scene();
        }

        static private void ButtonLoadClicked()
        {
        }
    }
}