using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cameras
{
    public partial class FirstPersonCamera
    {
       

        public Vector3 Eye
        {
            get { return eye; }
            set { eye = value; }
        }

        private void KeyPressed()
        {
            Vector3 direction = new Vector3();

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.A))
            {
                direction += new Vector3(10, 0, 0);
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                direction += new Vector3(-10, 0, 0);
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                direction += new Vector3(0, 0, -10);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                direction += new Vector3(0, 0, 10);
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
                MouseFollow(mouseState.X, mouseState.Y);
            }
        }
    }
}