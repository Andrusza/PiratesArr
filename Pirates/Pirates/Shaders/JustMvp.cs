using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class JustMvp : BaseShader
    {
        public JustMvp(): base("JustMvp")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Basic"];
        }

        public void InitParameters()
        {
            Texture2D color = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "BlueSky");
            Technique.Parameters["diffuseMap0"].SetValue(color);

            Technique.Parameters["Projection"].SetValue(projectionMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
        }

        public override void Update(float time)
        {
            Technique.Parameters["World"].SetValue(worldMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
        }
    }
}