using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    abstract public class BaseShader
    {
        protected Matrix MVP;
        protected Matrix projectionMatrix;
        protected Matrix viewMatrix;

        private Effect fx;
        protected Matrix worldMatrix = Matrix.Identity;

        public BaseShader(string path)
        {
            fx = ContentLoader.Load<Effect>(ContentType.SHADER, path);
        }

        public Matrix ProjectionMatrix
        {
            set { projectionMatrix = value; }
        }

        public Matrix ViewMatrix
        {
            set { viewMatrix = value; }
        }

        public Matrix WorldMatrix
        {
            set { worldMatrix = value; }
        }

        public void InverseTransposeWorld()
        {
            Matrix temp = Matrix.Invert(worldMatrix);
            temp = Matrix.Transpose(temp);
            Technique.Parameters["WorldInverseTranspose"].SetValue(temp);
        }

        public void InverseTransposeView()
        {
            Matrix temp = Matrix.Invert(viewMatrix);
            temp = Matrix.Transpose(temp);
            Technique.Parameters["ViewInverseTranspose"].SetValue(temp);
        }

        public void ModelViewProj()
        {
            MVP = worldMatrix * viewMatrix * projectionMatrix;
            Technique.Parameters["MVP"].SetValue(MVP);
        }

        public abstract void Update(float time);

        public Effect Technique
        {
            get { return fx; }
            set { fx = value; }
        }
    }
}