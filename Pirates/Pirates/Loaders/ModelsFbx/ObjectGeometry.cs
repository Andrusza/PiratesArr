using Microsoft.Xna.Framework;

namespace Pirates.Loaders
{
    abstract public class ObjectGeometry
    {
        protected Matrix modelMatrix = Matrix.Identity;

        public Matrix ModelMatrix
        {
            get { return modelMatrix; }
            set { modelMatrix = value; }
        }

        public Vector2 Position2D()
        {
            return new Vector2(modelMatrix.Translation.X, modelMatrix.Translation.Z);
        }

        public Vector3 UpVector
        {
            get { return orientationMatrix.Up; }
            set { orientationMatrix.Up = value; }
        }

        public Vector3 RightVector
        {
            get { return orientationMatrix.Right; }
            set { orientationMatrix.Right = value; }
        }

        public Vector3 ForwardVector
        {
            get { return orientationMatrix.Forward; }
            set { orientationMatrix.Forward = value; }
        }

        public Matrix RotationMatrix
        {
            get { return rotateMatrix; }
        }

        private Matrix scaleMatrix = Matrix.Identity;
        private Matrix rotateMatrix = Matrix.Identity;
        private Matrix translateMatrix = Matrix.Identity;
        private Matrix orientationMatrix = Matrix.Identity;

        public void Scale(float scale)
        {
            Matrix.CreateScale(scale, out scaleMatrix);
        }

        public void Rotate(float angle, Vector3 axis)
        {
            Matrix.CreateFromAxisAngle(ref axis, MathHelper.ToRadians(angle), out rotateMatrix);
        }

        public void Rotate(float yaw, float pitch, float roll)
        {
            Matrix.CreateFromYawPitchRoll(yaw, pitch, roll, out rotateMatrix);
        }

        public void Rotate(Quaternion q)
        {
            Matrix.CreateFromQuaternion(ref q, out rotateMatrix);
        }

        public void Translate(Vector3 translation)
        {
            Matrix.CreateTranslation(ref translation, out translateMatrix);
        }

        public void Translate(float x, float y, float z)
        {
            Matrix.CreateTranslation(x, y, z, out translateMatrix);
        }

        public void Translate(Vector2 transaltion)
        {
            Matrix.CreateTranslation(transaltion.X, 0, transaltion.Y, out translateMatrix);
        }

        public void Update()
        {
            ModelMatrix = scaleMatrix * (orientationMatrix * rotateMatrix) * translateMatrix;
        }

        public void UpdateCenterOfMass()
        {
            //ModelMatrix = scaleMatrix * translateMatrix * rotateMatrix  ;
        }
    }
}