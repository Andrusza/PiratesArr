using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PiratesArr.Game.Camera.FirstPersonCamera
{
    public partial class FirstPersonCamera
    {
        private Vector3 eye = new Vector3();

        private Vector3 xAxis = new Vector3();
        private Vector3 yAxis = new Vector3();
        private Vector3 zAxis = new Vector3();

        private void KeyPressed()
        {
            Vector3 direction = new Vector3();

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.A))
            {
                direction += new Vector3(1, 0, 0);
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                direction += new Vector3(-1, 0, 0);
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                direction += new Vector3(0, 0, -1);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                direction += new Vector3(0, 0, 1);
            }
            CameraTranslate(direction);
        }

     
        private void MouseEvents()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Motion(mouseState.X, mouseState.Y);
            }
            else
            {
                mouseFollow(mouseState.X, mouseState.Y);
            }
        }
    }
}