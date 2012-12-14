using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;

namespace Pirates.Loaders
{
    public partial class Terrain
    {
        public void Draw(BaseShader effect)
        {
          
            foreach (EffectPass pass in effect.Technique.CurrentTechnique.Passes)
            {
                pass.Apply(); 
                ContentLoader.SetBuffers(ibo, vbo);
            }
        }
    }
}