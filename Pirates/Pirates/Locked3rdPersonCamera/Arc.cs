using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pirates.Locked3rdPersonCamera
{
    public class ArcBallCamera
    {
        // Rotation around the two axes
        public float RotationX { get; set; }

        public float RotationY { get; set; }

        // Y axis rotation limits (radians)
        public float MinRotationY { get; set; }

        public float MaxRotationY { get; set; }

        // Distance between the target and camera
        public float Distance { get; set; }

        // Distance limits
        public float MinDistance { get; set; }

        public float MaxDistance { get; set; }

        // Calculated position and specified target
        public Vector3 Position { get; private set; }

        public Vector3 Target { get; set; }
        private MouseState lastMouseState;

        public ArcBallCamera(Vector3 Target, float RotationX,
        float RotationY, float MinRotationY, float MaxRotationY,
        float Distance, float MinDistance, float MaxDistance)
        {
            this.Target = Target;
            this.MinRotationY = MinRotationY;
            this.MaxRotationY = MaxRotationY;
            // Lock the y axis rotation between the min and max values
            this.RotationY = MathHelper.Clamp(RotationY, MinRotationY,
            MaxRotationY);
            this.RotationX = RotationX;
            this.MinDistance = MinDistance;
            this.MaxDistance = MaxDistance;

            // Lock the distance between the min and max values

            this.Distance = MathHelper.Clamp(Distance, MinDistance,
        MaxDistance);
        }

        public void Move(float DistanceChange)
        {
            this.Distance += DistanceChange;
            this.Distance = MathHelper.Clamp(Distance, MinDistance,
            MaxDistance);
        }

        public void Rotate(float RotationXChange, float RotationYChange)
        {
            this.RotationX += RotationXChange;
            this.RotationY += -RotationYChange;
            this.RotationY = MathHelper.Clamp(RotationY, MinRotationY,
            MaxRotationY);
        }

        public void Translate(Vector3 PositionChange)
        {
            this.Position += PositionChange;
        }

        public Matrix Update()
        {
            // Calculate rotation matrix from rotation values
            Matrix rotation = Matrix.CreateFromYawPitchRoll(RotationX, -
            RotationY, 0);
            // Translate down the Z axis by the desired distance
            // between the camera and object, then rotate that
            // vector to find the camera offset from the target
            Vector3 translation = new Vector3(0, 0, Distance);
            translation = Vector3.Transform(translation, rotation);

            Position = Target + translation;
            // Calculate the up vector from the rotation matrix
            Vector3 up = Vector3.Transform(Vector3.Up, rotation);
            Matrix View = Matrix.CreateLookAt(Position, Target, up);
            return View;
        }

        private void updateCamera(GameTime gameTime)
        {
            //  if (currentState.LeftButton == ButtonState.Pressed &&
            //    _previousLeftButton == ButtonState.Pressed)
            //{
            //    Vector2 curMouse = new Vector2(currentState.X, currentState.Y);
            //    Vector2 deltaMouse = _previousMousePosition - curMouse;

            //    this.Theta += deltaMouse.X * 0.01f;
            //    this.Phi -= deltaMouse.Y * 0.005f;
            //    _previousMousePosition = curMouse;
            //}
            //// It's implied that the leftPreviousState is unpressed in this situation.
            //else if (currentState.LeftButton == ButtonState.Pressed)
            //{
            //    _previousMousePosition = new Vector2(currentState.X, currentState.Y);
            //}

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            // Determine how much the camera should turn
            float deltaX = (float)lastMouseState.X - (float)mouseState.X;
            float deltaY = (float)lastMouseState.Y - (float)mouseState.Y;
            // Rotate the camera
            this.Rotate(deltaX * .01f, deltaY * .01f);
            // Calculate scroll wheel movement
            float scrollDelta = (float)lastMouseState.ScrollWheelValue -
            (float)mouseState.ScrollWheelValue;
            // Move the camera
            this.Move(scrollDelta);
            this.Update();
            // Update the mouse state
            lastMouseState = mouseState;
        }
    }
}