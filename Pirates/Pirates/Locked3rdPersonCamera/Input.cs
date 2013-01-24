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

        private void ShipUpdate(Vector3 position)
        {
            position.Y -= 200;
            Vector3 newPos = oldPosition - position;
            oldPosition = position;
            CameraTranslate(-newPos);
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