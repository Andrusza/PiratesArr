using Microsoft.Xna.Framework.Input;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Scene
{
    public partial class Scene : Mode
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
        }
    }
}