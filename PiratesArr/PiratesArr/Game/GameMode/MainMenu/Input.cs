using Microsoft.Xna.Framework.Input;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.MainMenu
{
    public partial class MainMenu : Mode
    {
        public void Input()
        {
            KeysPressed();
            MousePressed();
        }

        private void KeysPressed()
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
                if (manager.IsClicked(0) == true)
                {
                    ButtonNewGameClicked();
                }

                if (manager.IsClicked(1) == true)
                {
                    ButtonLoadClicked();
                }
            }
        }

        private void ButtonNewGameClicked()
        {
            mainInstance.RenderMode = new PiratesArr.Game.GameMode.Scene.Scene();
        }

        private void ButtonLoadClicked()
        {
        }
    }
}