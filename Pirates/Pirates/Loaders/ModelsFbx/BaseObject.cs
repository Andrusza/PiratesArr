using Microsoft.Xna.Framework;

namespace Pirates.Loaders
{
    abstract public class BaseObject
    {
        protected Matrix modelMatrix = Matrix.Identity;

        public Matrix ModelMatrix
        {
            get { return modelMatrix; }
            set { modelMatrix = value; }
        }

        private Matrix scaleMatrix = Matrix.Identity;
        private Matrix rotateMatrix = Matrix.Identity;
        private Matrix translateMatrix = Matrix.Identity;

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

        public void Update()
        {
            ModelMatrix = scaleMatrix * rotateMatrix * translateMatrix;
        }
    }
}