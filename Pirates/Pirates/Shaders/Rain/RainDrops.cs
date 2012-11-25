using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class RainDropsShader : BaseShader
    {
        public Texture2D waterNormal;
        public Texture2D currentFrame;
        

        public RainDropsShader()
            : base("RainDrop")
        {
            waterNormal = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "waterDroplets");
        }

        public override void InitParameters()
        {
            Technique.Parameters["waterNormal"].SetValue(waterNormal);
        }

        public override void Update(float time)
        {
            Technique.Parameters["waterNormal"].SetValue(waterNormal);
            Technique.Parameters["currentFrame"].SetValue(currentFrame);
            Technique.Parameters["time"].SetValue(time * 0.001f);
        }
    }
}