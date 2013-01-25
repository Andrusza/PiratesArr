using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pirates.Cameras
{
    public partial class ArcBallCamera
    {
        private static readonly float MAX_RADIANS = (float)(Math.PI * 2.0);
        private static readonly float MAX_PHI = (float)(Math.PI / 2.0);
        private static readonly float MIN_PHI = 0.3f;

        private Vector3 _cameraPosition;

        public Vector3 Eye
        {
            get { return _cameraPosition; }
            set { _cameraPosition = value; }
        }

        private Vector3 _lookatPosition;

        public Vector3 LookatPosition
        {
            get { return _lookatPosition; }
            set { _lookatPosition = value; }
        }

        private ButtonState _previousLeftButton;
        private Vector2 _previousMousePosition;

        private float _theta;
        private float _phi;
        private float _zoom;

        public ArcBallCamera(Vector3 pos, Vector3 lookAt)
        {
            _cameraPosition = pos;
            _lookatPosition = lookAt;
            _previousLeftButton = ButtonState.Released;
        }

        public Matrix View
        {
            get
            {
                Vector3 newCameraPosition = Eye;
                Matrix rotationMatrix;

                // Apply zoom
                newCameraPosition += GetZoomVector();

                // Apply Z rotation
                rotationMatrix = Matrix.CreateRotationZ(_phi);
                newCameraPosition = Vector3.Transform(newCameraPosition,
                    rotationMatrix);

                // Apply Y rotation
                rotationMatrix = Matrix.CreateRotationY(_theta);
                newCameraPosition = Vector3.Transform(newCameraPosition,
                    rotationMatrix);

                return Matrix.CreateLookAt(newCameraPosition, LookatPosition,
                    Vector3.Up);
            }
        }

        public float Theta
        {
            get { return _theta; }
            set
            {
                _theta = value % MAX_RADIANS;
            }
        }

        public float Phi
        {
            get { return _phi; }
            set
            {
                if (value > MAX_PHI)
                {
                    _phi = MAX_PHI;
                }
                else if (value < MIN_PHI)
                {
                    _phi = MIN_PHI;
                }
                else
                {
                    _phi = value;
                }
            }
        }

        public float Zoom
        {
            get { return _zoom; }

            set { _zoom = value; }
        }

        private Vector3 GetZoomVector()
        {
            Vector3 diff = Eye - LookatPosition;
            //.WriteLine(diff.ToString());

            diff.Normalize();
            diff *= _zoom;

            return diff;
        }
    }
}