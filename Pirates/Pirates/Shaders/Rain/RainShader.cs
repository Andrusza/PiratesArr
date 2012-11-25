using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders.Rain
{
    internal class RainShader : BaseShader
    {
        public Vector4 lightColor;

        public Vector3 eyePosition;
        public Vector4 lightVector;
        private Texture2D drops;

        public RainShader()
            : base("Rain")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Rain"];
            lightColor = Color.White.ToVector4();
            drops = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "drop1");
        }

        public override void InitParameters()
        {
            Technique.Parameters["lightColor"].SetValue(lightColor);
            Technique.Parameters["dropTexture"].SetValue(drops);
        }

        public override void Update(float time)
        {
            Technique.Parameters["EyePosition"].SetValue(eyePosition);
            Technique.Parameters["vp"].SetValue(viewMatrix * projectionMatrix);
            Technique.Parameters["World"].SetValue(WorldMatrix);
            Technique.Parameters["time"].SetValue(time);
        }
    }
}