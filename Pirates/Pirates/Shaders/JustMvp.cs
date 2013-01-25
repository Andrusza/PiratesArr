using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class JustMvp : BaseShader
    {
        public Matrix MVP;
        public float time = 0;

        public JustMvp()
            : base("JustMvp")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Basic"];
        }

        public override void InitParameters()
        {
            Texture2D color = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "sail2");
            Technique.Parameters["diffuseMap0"].SetValue(color);
        }

        public override void Update(float time)
        {
            this.time += time;
        }
    }
}