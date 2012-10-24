using Microsoft.Xna.Framework.Input;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Scene : Mode
    {
        static public void Input()
        {
            KeysPressed();
            MousePressed();
        }

        static private void KeysPressed()
        {
            KeyboardState keyState = Keyboard.GetState();
        }

        static private void MousePressed()
        {
            MouseState mouseState = Mouse.GetState();
        }
    }
}