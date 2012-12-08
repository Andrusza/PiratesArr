using Microsoft.Xna.Framework.Input;

namespace Pirates.KeyboardEvents
{
    public static class SpecialKeys
    {
        public static void KeyPressed()
        {
            KeyboardState keyState = Keyboard.GetState();
        }
    }
}