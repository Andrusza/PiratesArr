using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cameras
{
    public partial class Locked3rdPersonCamera
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
                direction += new Vector3(-4f, 0, 0);
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                direction += new Vector3(4f, 0, 0);
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                direction += new Vector3(0, 0, 4f);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                direction += new Vector3(0, 0, -4f);
            }
            if (direction != Vector3.Zero)
            {
                CameraTranslate(direction);
            }
        }

       
    }
}