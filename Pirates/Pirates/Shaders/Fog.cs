using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Shaders
{
    internal class Fog : BaseShader
    {
        public Texture2D currentFrame;

        public Fog()
            : base("Fog")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Fog"];
        }

        public override void InitParameters()
        {
        }

        public override void Update(float time)
        {
            Technique.Parameters["currentFrame"].SetValue(currentFrame);
        }
    }
}