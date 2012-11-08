using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class JustMvp : BaseShader
    {
        public Matrix MVP;

        public JustMvp(): base("JustMvp")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Basic"];
        }

        public void InitParameters()
        {
            Texture2D color = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "Sky2");
            Technique.Parameters["diffuseMap0"].SetValue(color);
        }

        public override void Update(float time)
        {
            MVP = worldMatrix * viewMatrix * projectionMatrix;
        }
    }
}