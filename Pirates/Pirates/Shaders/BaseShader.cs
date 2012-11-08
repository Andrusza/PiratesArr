using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    abstract public class BaseShader
    {
        
        protected Matrix projectionMatrix;
        protected Matrix viewMatrix;

        private Effect fx;
        protected Matrix worldMatrix = Matrix.Identity;

        public Effect Technique
        {
            get { return fx; }
            set { fx = value; }
        }

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
            get { return worldMatrix; }
            set { worldMatrix = value; }
        }

        public abstract void Update(float time);
    }
}