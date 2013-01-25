using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pirates.Cameras
{
    public partial class ArcBallCamera
    {
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
        }

        private void MouseEvents()
        {
            MouseState currentState = Mouse.GetState();

            this.Zoom = currentState.ScrollWheelValue * 0.5f;

            // if the mouse has been held down
            if (currentState.LeftButton == ButtonState.Pressed &&
                _previousLeftButton == ButtonState.Pressed)
            {
                Vector2 curMouse = new Vector2(currentState.X, currentState.Y);
                Vector2 deltaMouse = _previousMousePosition - curMouse;

                this.Theta += deltaMouse.X * 0.01f;
                this.Phi -= deltaMouse.Y * 0.005f;
                _previousMousePosition = curMouse;
            }
            // It's implied that the leftPreviousState is unpressed in this situation.
            else if (currentState.LeftButton == ButtonState.Pressed)
            {
                _previousMousePosition = new Vector2(currentState.X, currentState.Y);
            }

            _previousLeftButton = currentState.LeftButton;
        }
    }
}