using Microsoft.Xna.Framework;

namespace Pirates.Loaders
{
    abstract public class BaseObject
    {
        protected Matrix modelMatrix = Matrix.Identity;
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
            Matrix.CreateFromQuaternion(ref q, out modelMatrix);
        }

        public void Translate(Vector3 translation)
        {
            Matrix.CreateTranslation(ref translation, out modelMatrix);
        }

        public void Translate(float x, float y, float z)
        {
            Matrix.CreateTranslation(x, y, z, out modelMatrix);
        }

        public void Update()
        {
            modelMatrix = scaleMatrix * rotateMatrix * translateMatrix;
        }
    }
}